using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apple_Accounts_Creator.Settings.User_Agent
{
    public class SettingController
    {
        private static SettingController _instance;
        public static SettingController Instance => _instance ?? (_instance = new SettingController());
        private string userAgentFileName = Application.StartupPath + "\\ua.txt";

        public SettingController()
        {
            if (!File.Exists(userAgentFileName))
            {
                var defaultSettings = new SettingModels()
                {
                    UserAgent = new List<string>(),
                };
                var fileUA = defaultSettings.UserAgent;
                File.WriteAllLines(userAgentFileName, fileUA.ToArray());
            }
        }

        public SettingModels GetUA()
        {
            var UA = new SettingModels();
            string[] fileUA = File.ReadAllLines(userAgentFileName);
            UA.UserAgent = fileUA.ToList();
            return UA;
        }

        public void UpdateUA(SettingModels fileUA)
        {
            File.WriteAllLines(userAgentFileName, fileUA.UserAgent);
        }
    }
}
