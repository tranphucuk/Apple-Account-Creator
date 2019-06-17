using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apple_Accounts_Creator.Settings.Referrer
{
    public class ReferrerController
    {
        private static ReferrerController _instance;
        public static ReferrerController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ReferrerController();
                }
                return _instance;
            }
        }
        private readonly string referrerFolder = Application.StartupPath + "\\referrer";
        private readonly string referrer1 = Application.StartupPath + "\\referrer\\referrer1.txt";
        private readonly string referrer2 = Application.StartupPath + "\\referrer\\referrer2.txt";
        private readonly string referrer3 = Application.StartupPath + "\\referrer\\referrer3.txt";

        public ReferrerController()
        {
            if(!Directory.Exists(referrerFolder))
            {
                Directory.CreateDirectory(referrerFolder);
                CheckFileExist();
            }
            else
            {
                CheckFileExist();
            }
        }

        public SettingModels GetReferrer1()
        {
            var referrer = new SettingModels();
            string[] fileReferrer = File.ReadAllLines(referrer1);
            referrer.Referrer = fileReferrer.ToList();
            return referrer;
        }
        public SettingModels GetReferrer2()
        {
            var referrer = new SettingModels();
            string[] fileReferrer = File.ReadAllLines(referrer2);
            referrer.Referrer = fileReferrer.ToList();
            return referrer;
        }
        public SettingModels GetReferrer3()
        {
            var referrer = new SettingModels();
            string[] fileReferrer = File.ReadAllLines(referrer3);
            referrer.Referrer = fileReferrer.ToList();
            return referrer;
        }

        public void UpdateReferrer1(SettingModels referrer)
        {
            File.WriteAllLines(referrer1, referrer.Referrer);
        }

        public void UpdateReferrer2(SettingModels referrer)
        {
            File.WriteAllLines(referrer2, referrer.Referrer);
        }

        public void UpdateReferrer3(SettingModels referrer)
        {
            File.WriteAllLines(referrer3, referrer.Referrer);
        }

        public void CheckFileExist()
        {
            if(!File.Exists(referrer1))
            {
                var defaultReferrer1 = new SettingModels()
                {
                    Referrer = new List<string>()
                };
                File.WriteAllLines(referrer1, defaultReferrer1.Referrer.ToArray());
            }
            if(!File.Exists(referrer2))
            {
                var defaultReferrer2 = new SettingModels()
                {
                    Referrer = new List<string>()
                };
                File.WriteAllLines(referrer2, defaultReferrer2.Referrer.ToArray());
            }
            if (!File.Exists(referrer3))
            {
                var defaultReferrer3 = new SettingModels()
                {
                    Referrer = new List<string>()
                };
                File.WriteAllLines(referrer3, defaultReferrer3.Referrer.ToArray());
            }
        }
    }
}
