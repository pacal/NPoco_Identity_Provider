using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Pacal.NPoco_Idenity_Provider
{
    public class UserLoginsTable<TUser> where TUser: class, INPocoIdentity<TUser>
    {
        private DataProvider _database;

        public UserLoginsTable(DataProvider database)
        {
            _database = database;
        }

        public int Delete(TUser user, UserLoginInfo login)
        {
            return _database.DeletePocoWhere<UserLogin>(new Dictionary<string, string>()
            {
                {"LoginProvider", login.LoginProvider},
                {"ProviderKey", login.ProviderKey},
                {"UserId", user.Id}
            });
        }

        public string Insert(TUser user, UserLoginInfo login)
        {
            return _database.Insert<string, UserLogin>(new UserLogin {LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, UserId = user.Id});
        }

        public string FindUserIdbyLogin(UserLoginInfo login)
        {
            string ret = null;            
            var lgi =  _database.GetPocoWhereSingle<UserLogin>(new Dictionary<string, string>()
            {
                {"LoginProvider", login.LoginProvider},
                {"ProviderKey", login.ProviderKey}
            });

            if (lgi != null)
            {
                ret = lgi.UserId;
            }

            return ret;
        }

        public List<UserLoginInfo> FindAllByUserId(string userId)
        {
            List<UserLoginInfo> ret = new List<UserLoginInfo>();            
            var lst = _database.GetPocoWhere<UserLogin>(new Dictionary<string, string>() {{"UserId", userId}});
            ret.AddRange(lst.Select(login => new UserLoginInfo(login.LoginProvider, login.ProviderKey)));

            return ret;
        }
    }
}
