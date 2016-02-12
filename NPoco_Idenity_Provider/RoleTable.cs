using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacal.NPoco_Idenity_Provider
{
    public class RoleTable
    {
        private DataProvider _database;

        public RoleTable(DataProvider database)
        {
            _database = database;
        }

        public bool Delete(string roleId)
        {
            return _database.Delete<IdentityRole>(roleId);
        }

        public bool Delete(IdentityRole role)
        {
            return _database.Delete<IdentityRole>(role);
        }

        public string Insert(IdentityRole role)
        {
            return _database.Insert<string, IdentityRole>(role);
        }

        public IdentityRole GetRoleById(string roledId)
        {
            return _database.GetPocobyId<IdentityRole>(roledId);
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return _database.GetPocoWhereSingle<IdentityRole>(new Dictionary<string, string>() {{"Name", roleName}});
        }

        public bool Update(IdentityRole role)
        {
            return _database.Update<IdentityRole>(role);
        }
    }
}
