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
using Apple_Accounts_Creator.SSH;
using Apple_Accounts_Creator.Settings.User_Agent;
using Apple_Accounts_Creator.Register;

namespace Apple_Accounts_Creator.Home
{
    public partial class HomeModules : DevExpress.XtraEditors.XtraUserControl
    {
        public HomeModules()
        {
            InitializeComponent();
            gridControl1.DataSource = new List<TaskStartInfo>();
            InnitTaskToGridControl();
            FillBasicInfo();
            LoadSaveOptions();
        }

        public void LoadSaveOptions()
        {
            var options = Static.saveOptions ;
            leSave.DataSource = options;
            leSave.DisplayMember = "Key";
            leSave.ValueMember = "Value";
            leSave.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Key", "Text"));
            
        }

        private RegisterMulti multiRegister = new RegisterMulti();

        private void FillBasicInfo()
        {
            var sshes = SshController.Instance.getAllSsh();
            var UAs = SettingController.Instance.GetUA();
            teSsh.Text = sshes.Count.ToString();
            teUAs.Text = UAs.UserAgent.Count.ToString();
        }

        private List<RegisterInfo> listInfo = new List<RegisterInfo>();
        private List<EmailRegister> listemails = new List<EmailRegister>();
        private void bbiLoadListUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Text File";
            ofd.Filter = "Text File|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] fileInfoRegister = File.ReadAllLines(ofd.FileName);
                for (int i = 0; i < fileInfoRegister.Length; i++)
                {
                    var info = new RegisterInfo();
                    var line = fileInfoRegister[i].Split('|');
                    if (line.Length < 4) continue;
                    info.Fname = line[0];
                    info.Lname = line[1];
                    info.DOB = line[2];
                    info.Password = line[3];
                    listInfo.Add(info);
                }
                teListRegisterInfo.Text = listInfo.Count.ToString();
                XtraMessageBox.Show("Imported Info Succeed !!","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void seThreads_EditValueChanged(object sender, EventArgs e)
        {
            InnitTaskToGridControl();
        }

        private void InnitTaskToGridControl()
        {
            var tasks = gridControl1.DataSource as List<TaskStartInfo>;
            tasks.Clear();
            for (int i = 1; i <= Convert.ToInt32(seThreads.Value); i++)
            {
                tasks.Add(new TaskStartInfo()
                {
                    TaskID = i,
                    CurrentIP = "Local IP",
                    Status = "Ready",
                });
                gridControl1.RefreshDataSource();
            }
        }

        private void bbiLoadEmails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "File Emails";
            ofd.Filter = "File Email|*.txt";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    var email = new EmailRegister();
                    email.Email = lines[i];
                    listemails.Add(email);
                }
                teListEmailRegister.Text = listemails.Count.ToString();
                XtraMessageBox.Show("Imported Emails Succeed !!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void bbiStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DisableFunction();

            await multiRegister.RegisterAsync(new InfoRegister()
            {
                Emails = listemails,
                Infos = listInfo,
                Threads = Convert.ToInt32(seThreads.Value)
            });

            EnableFunctions();
        }

        private void EnableFunctions()
        {
            bbiLoadEmails.Enabled = bbiLoadListUser.Enabled = bbiStart.Enabled = groupControl1.Enabled = true;
            (this.FindForm() as Form1).bbiHome.Enabled = true;
            (this.FindForm() as Form1).bbiSsh.Enabled = true;
            (this.FindForm() as Form1).bbiUserAgent.Enabled = true;
            (this.FindForm() as Form1).bbiReferrer.Enabled = true;
            (this.FindForm() as Form1).bbiUser.Enabled = true;
            (this.FindForm() as Form1).bbiAbout.Enabled = true;
        }

        private void DisableFunction()
        {
            bbiLoadEmails.Enabled = bbiLoadListUser.Enabled = bbiStart.Enabled = groupControl1.Enabled = false;
            (this.FindForm() as Form1).bbiHome.Enabled = false;
            (this.FindForm() as Form1).bbiSsh.Enabled = false;
            (this.FindForm() as Form1).bbiUserAgent.Enabled = false;
            (this.FindForm() as Form1).bbiReferrer.Enabled = false;
            (this.FindForm() as Form1).bbiUser.Enabled = false;
            (this.FindForm() as Form1).bbiAbout.Enabled = false;
        }
    }
}
