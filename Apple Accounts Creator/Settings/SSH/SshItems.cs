using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apple_Accounts_Creator.SSH
{
    public class SshItems
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAlive { get; set; }
    }
}
