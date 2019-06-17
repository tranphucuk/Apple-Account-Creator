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
using DevExpress.XtraSplashScreen;
using System.IO;

namespace Apple_Accounts_Creator.SSH
{
    public partial class SshModules : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd;
        public SshModules()
        {
            InitializeComponent();
            FillSshToListView();
        }

        private void FillSshToListView()
        {
            var sshes = SshController.Instance.getAllSsh();
            lblTotalSsh.Text = "Total: "+ sshes.Count.ToString() + " SSH";
            gridControl1.DataSource = sshes;
            gridView1.Columns["Id"].Visible = false;
            windowsUIButtonPanel1.Buttons["Edit"].Properties.Enabled = gridView1.SelectedRowsCount > 0;
            windowsUIButtonPanel1.Buttons["Remove"].Properties.Enabled = gridView1.SelectedRowsCount > 0;
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            switch (e.Button.Properties.Tag.ToString())
            {
                case "add":
                    Add();
                    break;
                case "edit":
                    Edit();
                    break;
                case "remove":
                    Remove();
                    break;
                case "file":
                    LoadSsh();
                    break;
                default:
                    break;
            }
        }
        int ssh = 0;
        private void LoadSsh()
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "SSH File(Host|Username|Password)";
            ofd.Filter = "SSH file|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SplashScreenManager.ShowForm(this.FindForm(), typeof(WaitForm1), true, true, false);
                var lines = File.ReadAllLines(ofd.FileName);

                foreach (var line in lines)
                {
                    ssh++;
                    SplashScreenManager.Default.SetWaitFormDescription($"Processing {ssh}/{lines.Length} SSH...");
                    var sp = line.Split('|');
                    if (sp.Length < 3) continue;
                    var item = new SshItems()
                    {
                        Host = sp[0],
                        Username = sp[1],
                        Password = sp[2],
                        IsAlive = true,
                    };
                    SshController.Instance.Insert(item);
                }
                FillSshToListView();
                SplashScreenManager.CloseForm();
            }
        }

        private void Remove()
        {
            var selectedItem = gridView1.GetSelectedItems<SshItems>()?.ToList();
            if (selectedItem != null && selectedItem.Count > 0)
            {
                var removeComfirm = XtraMessageBox.Show($"Delete {selectedItem.Count} SSH ?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (removeComfirm == DialogResult.Cancel) return;

                var removeSuccess = SshController.Instance.DeleteSsh(selectedItem.Select(s => s.Id).ToList());
                if (!removeSuccess)
                {
                    XtraMessageBox.Show($"Delete Error. Try Again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            FillSshToListView();
        }

        private SshItems selectedSsh = new SshItems();
        private void Edit()
        {
            labelControl1.Text = "EDIT";
            isAdd = false;
            var selectedItem = gridView1.GetSelectedItems<SshItems>()?.ToList();
            selectedSsh = selectedItem[0];
            if (selectedItem != null && selectedItem.Count > 0)
            {
                teHost.Text = selectedItem[0].Host;
                teUsername.Text = selectedItem[0].Username;
                teUsername.Text = selectedItem[0].Password;
                flyoutPanel1.ShowBeakForm();
                teHost.Focus();
            }
        }

        private void Add()
        {
            labelControl1.Text = "ADD";
            isAdd = true;
            teHost.Text = teUsername.Text = teUsername.Text = string.Empty;
            flyoutPanel1.ShowBeakForm();
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            if (isAdd)
            {
                SshController.Instance.Insert(new SshItems()
                {
                    Host = teHost.Text,
                    Username = teUsername.Text,
                    Password = tePassword.Text,
                    IsAlive = true,
                });
                FillSshToListView();
                flyoutPanel1.HideBeakForm();
            }
            else
            {
                var updateSuccess = SshController.Instance.UpdateSsh(selectedSsh.Id, new SshItems()
                {
                    Host = teHost.Text,
                    Username = teUsername.Text,
                    Password = tePassword.Text,
                    IsAlive = selectedSsh.IsAlive.ToString() == "1",
                });
                if (!updateSuccess)
                {
                    XtraMessageBox.Show("Update Error. Try Again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flyoutPanel1.HideBeakForm();
                    return;
                }
                FillSshToListView();
                flyoutPanel1.HideBeakForm();
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            flyoutPanel1.HideBeakForm();
        }
    }
}
