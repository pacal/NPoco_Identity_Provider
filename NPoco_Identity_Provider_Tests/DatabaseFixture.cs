using System;
using NPoco;
using Pacal.NPoco_Idenity_Provider;
using Xunit;

namespace Pacal.NPoco_Identity_Provider_Tests
{
    public class DatabaseFixture : IDisposable
    {

        readonly string connString = "server = localhost\\sqlexpress;initial catalog = aspnet_NPoco_Identiy_Provider; persist security info=True;Integrated Security = SSPI;";        
        public UserStore<IdentityUser, IdentityRole> UserStore { get; private set; }
        public RoleStore<IdentityRole> RoleStore { get; private set; }
        public UserStore<MyCustomUser, MyCustomRole> UserStoreCustom { get; private set; }
        public RoleStore<MyCustomRole> RoleStoreCustom { get; private set; }

        // used for setup
        public Database RawDB { get; private set; }

        public DatabaseFixture()
        {
            UserStore = new UserStore<IdentityUser, IdentityRole>(new DataProvider(connString, DatabaseType.SqlServer2012));
            RoleStore = new RoleStore<IdentityRole>(new DataProvider(connString, DatabaseType.SqlServer2012));

            UserStoreCustom = new UserStore<MyCustomUser, MyCustomRole>(new DataProvider(connString, DatabaseType.SqlServer2012));
            RoleStoreCustom = new RoleStore<MyCustomRole>(new DataProvider(connString, DatabaseType.SqlServer2012));

            RawDB = new Database(connString, DatabaseType.SqlServer2012);
        }

        public void Dispose()
        {
            UserStore.Dispose();
            RoleStore.Dispose();
            UserStoreCustom.Dispose();
            RoleStoreCustom.Dispose();
            RawDB.Dispose();
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
