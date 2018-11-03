using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MarsRover.Logic
{
    public static class Extensions
    {
        public static bool TryParseAsDateTime(this string inputString, out DateTime result)
        {
            //02/27/17
            if (inputString.Contains("/"))
            {
                if (DateTime.TryParseExact(inputString, "MM/dd/yy", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out result))
                {
                    return true;
                }
            }

            //02/27/17
            if (inputString.Contains("-"))
            {
                if (DateTime.TryParseExact(inputString, "MMM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return true;
                }
            }


            //June 2, 2018 & April 31, 2018
            Regex reg = new Regex(@"([a-zA-Z]+) (\d+)");
            if (reg.Match(inputString).Success)
            {
                if (DateTime.TryParse(inputString, out result))
                {
                    return true;
                }
            }

            result = DateTime.MinValue;
            return false;
        }

        public static string GetHashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create(); 
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
