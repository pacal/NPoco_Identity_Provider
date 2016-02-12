using System;
using NPoco;
using Pacal.NPoco_Idenity_Provider;
using Xunit;

namespace Pacal.NPoco_Identity_Provider_Tests
{
    public class DatabaseFixture : IDisposable
    {

        readonly string connString = "server = localhost\\sqlexpress;initial catalog = aspnet-IdentityTest; persist security info=True;Integrated Security = SSPI;";        
        public UserStore<IdentityUser> UserStore { get; private set; }
        public RoleStore<IdentityRole> RoleStore { get; private set; }
        // used for setup
        public Database RawDB { get; private set; }

        public DatabaseFixture()
        {
            UserStore = new UserStore<IdentityUser>(new DataProvider(connString, DatabaseType.SqlServer2012));
            RoleStore = new RoleStore<IdentityRole>(new DataProvider(connString, DatabaseType.SqlServer2012));
            RawDB = new Database(connString, DatabaseType.SqlServer2012);
        }

        public void Dispose()
        {
            UserStore.Dispose();
            RoleStore.Dispose();
            RawDB.Dispose();
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
