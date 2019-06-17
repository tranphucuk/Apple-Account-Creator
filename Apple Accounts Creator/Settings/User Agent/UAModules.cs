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
using Apple_Accounts_Creator.Settings.User_Agent;
using System.IO;
using Apple_Accounts_Creator.Settings;
using System.Threading;
using thanhps42.HttpClient;

namespace Apple_Accounts_Creator.User_Agent
{
    public partial class UAModules : DevExpress.XtraEditors.XtraUserControl
    {
        public UAModules()
        {
            InitializeComponent();
            Http http = new Http();
            var homeRes = http.Get("https://forum.bkav.com.vn/forum/may-tinh/hoi-dap");
            LoadUAToMemoEdit();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void CheckEnable(List<string> listUA)
        {
            windowsUIButtonPanel1.Buttons["Filter"].Properties.Enabled = listUA.Count > 0;
            windowsUIButtonPanel1.Buttons["Save"].Properties.Enabled = listUA.Count > 0;
            windowsUIButtonPanel1.Buttons["Clear"].Properties.Enabled = listUA.Count > 0;
        }



        private void LoadUAToMemoEdit()
        {
            var listUA = SettingController.Instance.GetUA();
            foreach (var ua in listUA.UserAgent)
            {
                if (ua == string.Empty) continue;
                mmUserAgent.Text += ua + Environment.NewLine;
                mmUserAgent.SelectionStart = mmUserAgent.Text.Length;
            }
            mmUserAgent.ScrollToCaret();
            mmUserAgent.Text.Trim();
            lblUserAgent.Text = $"Total: {(mmUserAgent.Lines.Length - 1).ToString()} UA";
            CheckEnable(listUA.UserAgent);
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            switch (e.Button.Properties.Tag.ToString())
            {
                case "load":
                    Add();
                    break;
                case "filter":
                    Filter();
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
            mmUserAgent.Text = string.Empty;
            listUAs.Clear();
        }

        private void Save()
        {
            SettingController.Instance.UpdateUA(new SettingModels()
            {
                UserAgent = mmUserAgent.Lines.ToList(),
            });
            windowsUIButtonPanel1.Buttons["Filter"].Properties.Enabled = mmUserAgent.Text != string.Empty;
            windowsUIButtonPanel1.Buttons["Save"].Properties.Enabled = mmUserAgent.Text != string.Empty;
            windowsUIButtonPanel1.Buttons["Clear"].Properties.Enabled = mmUserAgent.Text != string.Empty;
            lblUserAgent.Text = $"Total: {(mmUserAgent.Lines.Length - 1).ToString()} UA";
        }

        private bool isFilter = true;
        private void Filter()
        {
            mmUserAgent.Text = string.Empty;
            new Thread(() =>
            {
                if (isFilter) // Da co san listUA tu file
                {
                    var listUA = SettingController.Instance.GetUA();
                    listUA.UserAgent = listUA.UserAgent.Distinct().ToList();
                    for (int i = 0; i < listUA.UserAgent.Count; i++)
                    {
                        mmUserAgent.Text += listUA.UserAgent[i] + Environment.NewLine;
                        mmUserAgent.SelectionStart = mmUserAgent.Text.Length;
                    }
                    mmUserAgent.ScrollToCaret();
                    lblUserAgent.Text = $"Total: {(mmUserAgent.Lines.Length - 1).ToString()} UA";
                }
                else // list UAs vua moi add vao
                {
                    listUAs = listUAs.Distinct().ToList();
                    for (int i = 0; i < listUAs.Count; i++)
                    {
                        mmUserAgent.Text += listUAs[i] + Environment.NewLine;
                        mmUserAgent.SelectionStart = mmUserAgent.Text.Length;
                    }
                    mmUserAgent.ScrollToCaret();
                    lblUserAgent.Text = $"Total: {(mmUserAgent.Lines.Length - 1).ToString()} UA";
                }
                CheckEnable(listUAs);
            })
            { IsBackground = true }.Start();
        }
        private List<string> listUAs = new List<string>();
        private void Add()
        {
            isFilter = false;
            var ofd = new OpenFileDialog();
            ofd.Title = "Text File";
            ofd.Filter = "Text File|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                new Thread(() =>
                {
                    string[] fileUA = File.ReadAllLines(ofd.FileName);
                    mmUserAgent.Text = string.Empty;
                    for (int i = 0; i < fileUA.Length; i++)
                    {
                        if (fileUA[i] == string.Empty) continue;
                        listUAs.Add(fileUA[i]);
                        mmUserAgent.Text += fileUA[i] + Environment.NewLine;
                        mmUserAgent.SelectionStart = mmUserAgent.Text.Length;
                    }
                    mmUserAgent.ScrollToCaret();
                    lblUserAgent.Text = $"Total: {(mmUserAgent.Lines.Length - 1).ToString()} UA";
                    CheckEnable(listUAs);
                })
                { IsBackground = true }.Start();
            }
        }
    }
}
