using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public enum LogType { Error, Info, Debug}
    public class LogProvider
    {
        private List<String> logs;
        private static LogProvider instance;
        private string dateFormat = "MM/dd/yyyy HH:mm:ss";  

        public LogProvider()
        {
            logs = new List<string>();
        }

        public static LogProvider Instance
        {
            get
            {
                if (instance == null)
                { 
                    instance = new LogProvider();
                }
                return instance;
            }
        }

        /// <summary>
        /// Добавления лога инфо-тип
        /// </summary>
        /// <param name="message"></param>
        public void AddLog(String message)
        {
            logs.Add($"[{DateTime.Now.ToString(dateFormat)}][{LogType.Info}]{message}");
        }
        public void AddLog(String message,LogType logType)
        {
            logs.Add($"[{DateTime.Now.ToString(dateFormat)}][{logType}]{message}");
        }

        public void AddError(String message)
        {
            logs.Add($"[{DateTime.Now.ToString(dateFormat)}][{LogType.Error}]{message}");
        }
        public void AddDebug(String message)
        {
            logs.Add($"[{DateTime.Now.ToString(dateFormat)}][{LogType.Debug}]{message}");
        }

        public void SaveLogsToFile(string filename = "Logs.txt")
        { 

            AddLog("Save log's to file.");
            using (TextWriter tw = new StreamWriter(filename, true))
            {
                foreach (String s in logs)
                    tw.WriteLine(s);
            }
        }

    }
}
