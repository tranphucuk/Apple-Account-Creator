using Apple_Accounts_Creator.Home;
using Apple_Accounts_Creator.Settings.Referrer;
using Apple_Accounts_Creator.SSH;
using Apple_Accounts_Creator.User_Agent;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using thanhps42.HttpClient;

namespace Apple_Accounts_Creator
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void ChangeModuleTo<T>() where T : XtraUserControl
        {
            SplashScreenManager.ShowForm(FindForm(), typeof(WaitForm1), true, true, false);
            panelControl1.Controls.Clear();
            var newModules = Activator.CreateInstance<T>();
            newModules.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(newModules);
            SplashScreenManager.CloseForm(false);
        }

        private void bbiHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangeModuleTo<HomeModules>();
        }

        private void bbiSsh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangeModuleTo<SshModules>();
        }

        private void bbiUserAgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangeModuleTo<UAModules>();
        }

        private void bbiReferrer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangeModuleTo<ReferrerModules>();
        }
    }
}
