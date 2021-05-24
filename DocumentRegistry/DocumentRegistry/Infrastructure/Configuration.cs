using System;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace DocumentRegistry.Infrastructure
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

        public static string GetConfigurationField(string fieldName)
        {
            return _configuration[fieldName];
        }

        public static void SetConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true) //todo:Can I get right appsettings file based on environment?
                .Build();
        }
    }
}