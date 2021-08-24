using System;
using System.Linq;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Models;
using MonitoringStations.Core.Interfaces;

namespace MonitoringStations.Core.Services
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly DataContext _context;

        public ApiTokenService(DataContext context)
        {
            _context = context;
        }

        public bool IsTokenExist(string token)
        {
            var result = false;

            if (string.IsNullOrEmpty(token)) return result;

            result = _context.ApiToken.Any(x => x.Token.Equals(token));

            return result;
        }

        public void SaveApiToken(ApiToken token)
        {
            try
            {
                if (token != null)
                {
                    _context.ApiToken.Add(token);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
