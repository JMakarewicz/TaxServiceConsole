using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace TaxCalculator
{
    internal static class CalculatorSettings
    {
        private static IConfiguration Configuration = null;

        static CalculatorSettings()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
        }

        public static string GetCalculatorAssembly(string clientKey)
        {
            string defaultKey = "Default";
            string calculatorAssembly = Configuration.GetSection("CalculatorSettings")[clientKey];

            if(String.IsNullOrEmpty(calculatorAssembly))
            {
                calculatorAssembly = Configuration.GetSection("CalculatorSettings")[defaultKey];
            }

            return calculatorAssembly;
        }
    }
}
