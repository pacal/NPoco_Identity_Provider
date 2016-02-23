using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pacal.NPoco_Idenity_Provider
{
    public class UserClaimsTable<TUser> where TUser : class, INPocoIdentity<TUser>
    {
        private DataProvider _database;

        public UserClaimsTable(DataProvider database)
        {
            _database = database;
        }

        public IList<Claim> FindByUserId(string userId)
        {
            var userClaims = _database.GetPocoWhere<UserClaim>(new Dictionary<string, string>(){{"UserId", userId}});
            return userClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        public int DeleteSingleclaim(TUser user, Claim claim)
        {
            return _database.DeletePocoWhere<UserClaim>(new Dictionary<string, string>()
                {
                    {"UserId", user.Id},
                    {"ClaimValue", claim.Value},
                    {"ClaimType", claim.Type}
                });
        }

        public int Insert(TUser user, Claim claim)
        {
            var newUserClaim = new UserClaim()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                UserId = user.Id
            };

            var ret = _database.Insert<decimal, UserClaim>(newUserClaim);
            return Convert.ToInt32(ret);
        }

    }
}
