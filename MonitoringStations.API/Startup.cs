using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Models;
using MonitoringStations.Core.Services;
using MonitoringStations.Core.Interfaces;

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
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                //cfg.Audience = string.Empty;
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = authenticationJwtCfg.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(authenticationJwtCfg.JwtKey)),
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
                options.AddPolicy("AccessAPi",
                    policy => policy.RequireClaim("Permission")));
            #endregion

            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("LocalPG"));
            });


            services.AddAutoMapper(typeof(Core.MappingProfile));
            services.AddSingleton(authenticationJwtCfg);
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IApiTokenService, ApiTokenService>();
           

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

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