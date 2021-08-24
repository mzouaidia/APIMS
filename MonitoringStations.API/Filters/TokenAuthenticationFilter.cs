using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MonitoringStations.API.TokenAuthentication;
using AutoMapper;
using MonitoringStations.Core.Services;

namespace MonitoringStations.API.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        private readonly TokenManager _tokenManager;
        private readonly IMapper _mapper;
        private readonly ApiTokenService _apiTokenService;

        public TokenAuthenticationFilter(ApiTokenService apiTokenService, IMapper mapper)
        {
            _apiTokenService = apiTokenService;
            _mapper = mapper;
            _tokenManager = new TokenManager(_apiTokenService, _mapper);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            var token = string.Empty;

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            if(result)
            {
                token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.Contains("Authorization")).Value;
                if(!_tokenManager.VerifyToken(token))
                    result = false;
            }

            if (!result)
            {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized!");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
