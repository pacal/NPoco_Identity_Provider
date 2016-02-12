using System.Collections.Generic;
using System.Linq;

namespace Pacal.NPoco_Idenity_Provider
{
    public class UserRolesTable
    {
        private DataProvider _database;
        
        public UserRolesTable(DataProvider database)
        {
            _database = database;
        }

        public List<string> FindByUserId(string userId)
        {
            var ret = new List<string>();
            var sql = "Select AspNetRoles.Name from AspNetUserRoles, AspNetRoles where AspNetUserRoles.UserId = @0 and AspNetUserRoles.RoleId = AspNetRoles.Id";
            var fetchResult = _database.ExecutRawFetch(sql, userId);

            ret =  fetchResult.Select(role => role.Values.First().ToString()).ToList();
            return ret;
        }

        public int Delete(string userId)
        {
            string sql = "Delete from AspNetUserRoles where UserId = @0";
            return _database.ExecuteRawScalar(sql, userId);
        }

        public int Insert(IdentityUser user, string roleId)
        {
            string sql = "Insert into AspNetUserRoles (UserId, RoleId) values (@0, @1)";
            return _database.ExecuteRawScalar(sql, user.Id, roleId);
        }

        public int RemoveUserFromRole(IdentityUser user, string roleName)
        {
            string sql = @"DELETE u FROM AspNetUserRoles u
                                                Inner join AspNetRoles r
                                                on u.RoleId = r.Id
                                                where r.Name = @0 and u.userid = @1";

            return _database.ExecuteRawScalar(sql, roleName, user.Id);
        }
    }
}
