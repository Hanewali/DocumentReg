using System;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace DocumentRegistry.Api.Infrastructure
{
    public class Configuration
    {
        private static IConfigurationRoot _configuration;

        public static class Database
        {
            public static string ConnectionString = _configuration.GetConnectionString("ConnectionString");
            public static IsolationLevel DefaultIsolationLevel = IsolationLevel.ReadUncommitted;
            public static TimeSpan DefaultTimeout = TransactionManager.DefaultTimeout;
        }

        public static class Api
        {
            public static string AuthorizationToken = GetConfigurationField("Api:AuthorizationToken");
        }

        public static string GetConfigurationField(string fieldName)
        {
            return _configuration[fieldName];
        }

        public static void SetConfiguration(bool IsDevelopment)
        {
            var appsettingsFile = IsDevelopment ? "appsettings.Development.json" : "appsettings.json";
            
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(appsettingsFile, optional: false, reloadOnChange: true) 
                .Build();
        }
    }
}