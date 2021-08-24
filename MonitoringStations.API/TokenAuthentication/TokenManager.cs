using System;
using System.Collections.Generic;
using AutoMapper;
using MonitoringStations.Core.Services;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.API.TokenAuthentication
{
    public class TokenManager
    {
        private ApiTokenService _apiTokenService;
        private IMapper _mapper;
        private List<ApiTokenDto> _listTokens;

        public TokenManager(ApiTokenService apiTokenService, IMapper mapper)
        {
            _apiTokenService = apiTokenService;
            _mapper = mapper;
            _listTokens = new List<ApiTokenDto>();
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            
            var result = false;

            result = _apiTokenService.IsTokenExist(token);

            return result;
        }

        public ApiTokenDto NewToken()
        {
            var token = new ApiTokenDto{
                CreateDate = DateTime.Now,
                ExpireDate = new DateTime(2100, 1, 1),
                IsExpire = false,
                Token = Guid.NewGuid().ToString()
            };

            _listTokens.Add(token);

            _apiTokenService.SaveApiToken(_mapper.Map<ApiToken>(token));

            return token;
        }
    }
}
