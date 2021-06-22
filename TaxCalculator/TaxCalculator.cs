using System;
using Common;
using System.IO;
using System.Reflection;

namespace TaxCalculator
{
    public static class TaxCalculator
    {
        public static ITaxCalculator GetCalculator(string client)
        {
            string dllPath = String.Empty;
            Assembly calculatorAssembly = null;
            Type[] types = null;
            Type calculatorType = null;
            ITaxCalculator calculator = null;

            if(String.IsNullOrWhiteSpace(client))
            {
                throw new ArgumentNullException("client");
            }

            dllPath = CalculatorSettings.GetCalculatorAssembly(client);

            if (VerifyPath(ref dllPath))
            {
                calculatorAssembly = Assembly.LoadFrom(dllPath);
                types = calculatorAssembly.GetTypes();
                
                foreach(Type type in types)
                {
                    if(type.GetInterface("ITaxCalculator") != null)
                    {
                        calculatorType = type;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Unable to find specified calculator dll.");
            }
            
            if(!Object.Equals(calculatorType, null))
            {
                calculator = Activator.CreateInstance(calculatorType) as ITaxCalculator;
            }

            return calculator;
        }

        private static bool VerifyPath(ref string dllPath)
        {
            bool result = true;
            string path = String.Concat("TaxCalculators", "\\", dllPath);

            if (File.Exists(path))
            {
                dllPath = path;
            }
            else
            {
#if DEBUG
                path = String.Concat("TaxCalculators", "\\", "netcoreapp3.1", "\\", dllPath);

                if (File.Exists(path))
                {
                    dllPath = path;
                }
                else
                {
                    result = false;
                    dllPath = String.Empty;
                }
#else
                result = false;
                dllPath = String.Empty;
#endif
            }

            return result;
        }
    }
}
