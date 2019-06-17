using Apple_Accounts_Creator.Home;
using System.Collections.Generic;

namespace Apple_Accounts_Creator.Register
{
    public class InfoRegister
    {
        public List<RegisterInfo> Infos { get; set; }
        public List<EmailRegister> Emails { get; set; }
        public int Threads { get; set; }
    }
}