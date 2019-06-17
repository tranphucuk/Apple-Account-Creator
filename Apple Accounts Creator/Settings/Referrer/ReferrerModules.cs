using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace Apple_Accounts_Creator.Settings.Referrer
{
    public partial class ReferrerModules : DevExpress.XtraEditors.XtraUserControl
    {
        public ReferrerModules()
        {
            InitializeComponent();
            GetReferrerAndLoadToMemoEdit();
            lblTotalReferrer.Text = ReferrerController.Instance.GetReferrer1().Referrer.Count().ToString();
        }
        private int n = 0;
        private SettingModels refs = new SettingModels();

        private void GetReferrerAndLoadToMemoEdit()
        {
            var listReferrer = ReferrerController.Instance.GetReferrer1();
            LoadToMemoEdit(listReferrer.Referrer);
            CheckEnable();
        }

        private void LoadToMemoEdit(List<string> referrer)
        {
            mmReferrer.Text = string.Empty;
            mmReferrer.Update();
            for (int i = 0; i < referrer.Count; i++)
            {
                if (referrer[i] == string.Empty) continue;
                mmReferrer.Text += referrer[i] + Environment.NewLine;
                mmReferrer.SelectionStart = mmReferrer.Text.Length;
            }
            mmReferrer.ScrollToCaret();
            CheckEnable();
        }

        private void sbProfile1_Click(object sender, EventArgs e)
        {
            lblCurrentProfile.Text = "Profile 1";
            lblTotalReferrer.Text = ReferrerController.Instance.GetReferrer1().Referrer.Count.ToString();
            n = 0;
            var listReferrer = ReferrerController.Instance.GetReferrer1();
            LoadToMemoEdit(listReferrer.Referrer);
        }

        private void sbProfile2_Click(object sender, EventArgs e)
        {
            lblCurrentProfile.Text = "Profile 2";
            lblTotalReferrer.Text = ReferrerController.Instance.GetReferrer2().Referrer.Count.ToString();
            n = 1;
            var listReferrer = ReferrerController.Instance.GetReferrer2();
            LoadToMemoEdit(listReferrer.Referrer);
        }

        private void sbProfile3_Click(object sender, EventArgs e)
        {
            lblCurrentProfile.Text = "Profile 3";
            lblTotalReferrer.Text = ReferrerController.Instance.GetReferrer3().Referrer.Count.ToString();
            n = 2;
            var listReferrer = ReferrerController.Instance.GetReferrer3();
            LoadToMemoEdit(listReferrer.Referrer);
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            switch (e.Button.Properties.Tag.ToString())
            {
                case "load":
                    Add();
                    break;
                case "save":
                    Save();
                    break;
                case "clear":
                    Clear();
                    break;
                default:
                    break;
            }
        }

        private void Clear()
        {
            mmReferrer.Text = string.Empty;
        }

        private void CheckEnable()
        {
            windowsUIButtonPanel1.Buttons["Save"].Properties.Enabled = mmReferrer.Text != string.Empty;
            windowsUIButtonPanel1.Buttons["Clear"].Properties.Enabled = mmReferrer.Text != string.Empty;
        }

        private void Save()
        {
            if (n == 0)
            {
                ReferrerController.Instance.UpdateReferrer1(new SettingModels()
                {
                    Referrer = mmReferrer.Lines.ToList()
                });
            }
            else if( n ==1)
            {
                ReferrerController.Instance.UpdateReferrer2(new SettingModels()
                {
                    Referrer = mmReferrer.Lines.ToList()
                });
            }
            else if(n ==2)
            {
                ReferrerController.Instance.UpdateReferrer3(new SettingModels()
                {
                    Referrer = mmReferrer.Lines.ToList()
                });
            }
            CheckEnable();
        }

        private void Add()
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Text File";
            ofd.Filter = "Text File|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] fileRef = File.ReadAllLines(ofd.FileName);
                mmReferrer.Text = string.Empty;
                for (int i = 0; i < fileRef.Length; i++)
                {
                    mmReferrer.Text += fileRef[i] + Environment.NewLine;
                    mmReferrer.SelectionStart = mmReferrer.Text.Length;
                }
                mmReferrer.ScrollToCaret();
            }
            CheckEnable();
        }
    }
}
