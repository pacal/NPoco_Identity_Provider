using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace Pacal.NPoco_Idenity_Provider
{
    [TableName("AspNetUserLogins")]
    [PrimaryKey("LoginProvider, ProviderKey, UserId")]
    public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
    }
}
