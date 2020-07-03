using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.IO;
using Newtonsoft.Json;
using SqlTest.Data;
using WpfApp.Utils;

namespace WpfApp
{
    [Serializable]
    public class SettingsProvider
    {

        private static SettingsProvider instance;
        public static SettingsProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    LogProvider.Instance.AddLog("Init settings.");
                    instance = new SettingsProvider();
                }
                return instance;
            }
        }

        public User CurentUser { get; set; }
         

        public static void LoadSettings()
        {
            StreamReader reader = new StreamReader("settings.json");
            var jsonString = File.ReadAllText("settings.json");

            SettingsProvider.instance = JsonConvert.DeserializeObject<SettingsProvider>(jsonString);
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public  void SaveSettings()
        { 
            
            if(!File.Exists("settings.json"))
            {
                File.WriteAllText("settings.json", this.ToString());
            }
            else
            {
                File.AppendAllText("settings.json", this.ToString());
            }
        }
    }
}
