using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apple_Accounts_Creator.SSH
{
    public class SshController
    {
        private static SshController _instance;
        public static SshController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SshController();
                }
                return _instance;
            }
        }

        public List<SshItems> getAllSsh()
        {
            var table = DataController.Instance.GetDataTable("SELECT * FROM Sshes");
            List<SshItems> items = new List<SshItems>();

            foreach (DataRow row in table.Rows)
            {
                var ssh = new SshItems();
                ssh.Id = int.Parse(row["Id"].ToString());
                ssh.Host = row["Host"].ToString();
                ssh.Username = row["Username"].ToString();
                ssh.Password = row["Password"].ToString();
                ssh.IsAlive = row["IsAlive"].ToString() == "1";

                items.Add(ssh);
            }
            return items;
        }

        public void Insert(SshItems ssh)
        {
            var insertCmd = $"INSERT INTO Sshes(Host,Username,Password,IsAlive)VALUES('{ssh.Host}','{ssh.Username}','{ssh.Password}','1')";
            DataController.Instance.ExecuteNonQuerry(insertCmd);
        }

        public bool UpdateSsh(int id,SshItems newSsh)
        {
            var updateCmd = $"UPDATE Sshes SET Host='{newSsh.Host}',Username='{newSsh.Username}',Password='{newSsh.Password}' WHERE Id = {id}";
            return DataController.Instance.ExecuteNonQuerry(updateCmd) > 0;
        }

        public bool DeleteSsh(List<int> ids)
        {
            var deleteCmd = $"DELETE FROM Sshes WHERE";
            foreach (var id in ids)
            {
                deleteCmd += $" Id = {id} OR";
            }
            deleteCmd.Remove(deleteCmd.Length - 3);
            return DataController.Instance.ExecuteNonQuerry(deleteCmd) > 0;
        }
    }
}
