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
                .AddJsonFile(GetRootPath("config.json"))
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

        private static string GetRootPath(string rootFileName)
        {
            string root = String.Empty, appRoot = String.Empty;
            string rootDir = null;
            Regex match = null;

            root = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            if(!String.IsNullOrEmpty(rootDir))
            {
                match = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                appRoot = match.Match(rootDir).Value;
                root = Path.Combine(appRoot, rootFileName);
            }

            return root;
        }
    }
}
