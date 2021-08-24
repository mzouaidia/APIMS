using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MonitoringStations.API.Filters;
using MonitoringStations.API.TokenAuthentication;
using MonitoringStations.Core.Interfaces;
using MonitoringStations.Core.Services;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Models;
using System.Text;

namespace MonitoringStations.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region JwtConfiguration
            var authenticationJwtCfg = new AuthenticationJwt();

            Configuration.GetSection("Authentication").Bind(authenticationJwtCfg);

            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = "Bearer";
                op.DefaultScheme = "Bearer";
                op.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                //cfg.Audience = string.Empty;
                cfg.RequireHttpsMetadata = true;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    //RequireExpirationTime = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    //ValidIssuer = authenticationJwtCfg.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(authenticationJwtCfg.JwtKey)),
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
                options.AddPolicy("AccessAPi",
                    policy => policy.RequireClaim("Permission")));
            #endregion

            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("LocalPG"));
            });

            //services.AddSingleton<TokenManager>();

            services.AddAutoMapper(typeof(Core.MappingProfile));
            services.AddScoped<IStationService, StationService>();
            //services.AddScoped<IApiTokenService, ApiTokenService>();

            //services.AddControllers(options => { options.Filters.Add<TokenAuthenticationFilter>(); });
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}