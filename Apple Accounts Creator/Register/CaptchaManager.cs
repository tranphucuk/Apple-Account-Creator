using Aioseoservice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apple_Accounts_Creator.Register
{
    public class CaptchaManager
    {
        private static CaptchaManager _instance;
        public static CaptchaManager Instance => _instance ?? (_instance = new CaptchaManager());
        private CaptchaManager()
        {
            if (!Directory.Exists(CaptchaPath))
            {
                Directory.CreateDirectory(CaptchaPath);
            }
        }
        private static string CaptchaPath { get; } = Application.StartupPath + "\\Captcha";

        public string GetCaptchaText(string captchaContent)
        {
            //lock (MyHelper.CaptchaKeyObject)
            {
                var imgPath = ConvertFromBase64AndWriteToFile(captchaContent);

                var captcha = DetectCaptcha(imgPath);

                File.Delete(imgPath);

                return captcha;
            }
        }

        private string DetectCaptcha(string imgPath)
        {
            var client = new Aioseo();
            client.Post(imgPath, "4480b112ab65f8e3df4983de0c3fe110");
            return client.capt;
        }

        private string ConvertFromBase64AndWriteToFile(string captchaContent)
        {

            //var captchaFileName = string.Empty;
            //lock (MyHelper.CaptchaKeyObject)
            //{
            //    captchaFileName = Path.GetRandomFileName();
            //}

            //if (MyHelper.TryStartAction(3, () =>
            //{
            //    var imgBytes = Convert.FromBase64String(captchaContent);
            //    File.WriteAllBytes(Path.Combine(CaptchaPath, captchaFileName), imgBytes);
            //}))
            //{
            //    return Path.Combine(CaptchaPath, captchaFileName);
            //}

            var imgBytes = Convert.FromBase64String(captchaContent);
            File.WriteAllBytes(CaptchaPath + "\\captcha.jpg", imgBytes);

            return CaptchaPath + "\\captcha.jpg";
        }
    }
}
