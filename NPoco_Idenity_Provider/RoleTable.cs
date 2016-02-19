using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco.Linq;

namespace Pacal.NPoco_Idenity_Provider
{
    public class RoleTable<TRole>
    {
        private DataProvider _database;

        public RoleTable(DataProvider database)
        {
            _database = database;
        }

        public bool Delete(string roleId)
        {
            return _database.Delete<TRole>(roleId);
        }

        public bool Delete(TRole role)
        {
            return _database.Delete<TRole>(role);
        }

        public string Insert(TRole role)
        {
            return _database.Insert<string, TRole>(role);
        }

        public TRole GetRoleById(string roledId)
        {
            return _database.GetPocobyId<TRole>(roledId);
        }

        public TRole GetRoleByName(string roleName)
        {
            return _database.GetPocoWhereSingle<TRole>(new Dictionary<string, string>() {{"Name", roleName}});
        }

        public bool Update(TRole role)
        {
            return _database.Update<TRole>(role);
        }

        public IQueryProviderWithIncludes<TRole> Roles => _database.GetNPocoIqProviderWithIncludes<TRole>();
    }
}
