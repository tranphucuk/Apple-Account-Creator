using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apple_Accounts_Creator
{
    public class Static
    {
        public static List<KeyValuePair<string, string>> saveOptions = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Text","0"),
            new KeyValuePair<string, string>("Excel","1"),
        };
    }
}
