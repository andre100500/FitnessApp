using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SqlTest
{
   public static  class Validation
    {
        private static int LoginReqLengs = 2;
        private static int PassReqLengs = 4;

        public static void Login(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                throw new Exception("Login is Empty ");
            }
            if (str.Length < LoginReqLengs)
            {
                throw new Exception($"Login length less then {LoginReqLengs} symbols.");
            }
            if (!IsValidEmail(str))
            {

            }
        }
        
        public static void Pass(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                throw new Exception("Pass is Empty");
            }
            if(str.Length<PassReqLengs)
            {
                throw new Exception($"Pass length less then {PassReqLengs} symbols.");
            }

        }

        public static bool IsValidEmail(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new Exception("GG");

            try
            {
                // Normalize the domain
                str = Regex.Replace(str, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(str,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
