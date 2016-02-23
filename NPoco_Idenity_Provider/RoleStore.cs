using System;
using System.Threading.Tasks;
using NPoco.Linq;

namespace Pacal.NPoco_Idenity_Provider
{
    public class RoleStore<TRole>
    {
        private RoleTable<TRole> roleTable;
        public DataProvider Database { get; set; }

        public RoleStore(DataProvider database)
        {
            roleTable = new RoleTable<TRole>(database);
            Database = database;
        }

        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Update(role);

            return Task.FromResult<Object>(null);
        }

        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Delete(role);

            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            TRole result = roleTable.GetRoleById(roleId);

            return Task.FromResult<TRole>(result);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            TRole result = roleTable.GetRoleByName(roleName);

            return Task.FromResult<TRole>(result);
        }

        public IQueryProviderWithIncludes<TRole> Roles => Database.GetNPocoIqProviderWithIncludes<TRole>();

        public void Dispose()
        {
            if (Database != null)
            {
                Database.Dispose();
                Database = null;
            }
        }
    }
}
