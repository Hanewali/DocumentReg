using System;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace DocumentRegistry.Web.Infrastructure
{
    public class Configuration
    {
        private static IConfigurationRoot _configuration;

        public static class Api
        {
            public static string AuthorizationToken = GetConfigurationField("Api:AuthorizationToken");
            public static string Url = GetConfigurationField("Api:Url");
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