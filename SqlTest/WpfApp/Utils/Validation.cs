using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp
{/// <summary>
/// Валидация отвечает за проверку введеных даных пользователя
/// </summary>
   public static  class Validation
    {
        private static int MIN_LOGIN = 2;
        private static int MAX_LOGIN = 10;
        private static int MIN_PASS = 4;
        private static int MAX_PASS = 8;

        private static int MIN_AGE = 16;
        private static int MAX_AGE = 70;
        /// <summary>
        /// Проверка Возроста
        /// </summary>
        /// <param name="age"></param>
        public static void Age(int age)
        {
            if(age<=MIN_AGE)
            {
                throw new Exception($"Age connot be less {MIN_AGE} age!");
            }
            else if(age>=MAX_AGE)
            {
                throw new Exception($"Age connot be less {MAX_AGE} age!");
            }
        }


        public const string FILE_EXTENSION = ".json";
        public static void FilePath(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new Exception("Fille is Epmty ");
            } 
            if(Path.GetExtension(path) != FILE_EXTENSION)
            {
                throw new Exception("Fille is wrong format.");
            }
        }

        //private static int MIN_LENGTH = 4;
        //private static int MAX_LENGTH = 16;
        //public static void SaveNameFile(string name)
        //{
        //    if(string.IsNullOrWhiteSpace(name))
        //    {
        //        throw new Exception("Name file is empty");
        //    }
        //    else if (name.Length<=MIN_LENGTH)
        //    {
        //        throw new Exception($"Minimum lenght {MIN_LENGTH}");
        //    }
        //    else if (name.Length>= MAX_LENGTH)
        //    {
        //        throw new Exception($"Maximum leght {MAX_LENGTH}");
        //    }
        //}

        private const double MIN_MASS = 30;
        private const double MAX_MASS = 200;
        /// <summary>
        /// Проверка массы
        /// </summary>
        /// <param name="mass"></param>
        public static void Mass(double mass)
        {
            
            if(mass<= MIN_MASS)
            {
                throw new Exception($"Mass cannot be less {MIN_MASS} kg!");
            }
            else if(mass >= MAX_MASS)
            {
                throw new Exception($"Mass cannot to be much {MAX_MASS} kg!");
            }
        }

        private const double MIN_Height = 120;
        private const double MAX_Height = 250;
        /// <summary>
        /// Проверка высоты
        /// </summary>
        /// <param name="height">Высота</param>
        public static void Height(double height)
        {
            if(height<=MIN_Height)
            {
                   throw new Exception($"Height cannot to be less {MIN_Height} metrs! ");
            }
            else if(height >= MAX_Height)
            {
                throw new Exception($"Height cannot  to be much {MAX_Height} metrs! ");
            }
        }

        /// <summary>
        /// Проверка логина
        /// </summary>
        /// <param name="str">Строка логина</param>
        public static void Login(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                throw new Exception("Login is Empty ");
            }
            else if (str.Length <= MIN_LOGIN)
            {
                throw new Exception($"Login length less then {MIN_LOGIN} symbols.");
            }
            else if (str.Length>=MAX_LOGIN)
            {
                throw new Exception($"Login length connot to be much {MAX_LOGIN}");
            }

        }
        
        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="str">Строка пароля</param>
        public static void Pass(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                throw new Exception("Pass is Empty");
            }
            else if(str.Length<MIN_PASS)
            {
                throw new Exception($"Pass length less then {MIN_PASS} symbols.");
            }
            else if(str.Length>=MAX_PASS)
            {
                throw new Exception($"Pass length cannot to be much{MAX_PASS} symbols.");
            }

        }
        /// <summary>
        /// Проверка почты
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new Exception("Email is Empty");

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
