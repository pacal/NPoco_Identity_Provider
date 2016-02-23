using System.Collections.Generic;
using NPoco;
using NPoco.Linq;

namespace Pacal.NPoco_Idenity_Provider
{
    class UserTable<TUser> where TUser : class, INPocoIdentity<TUser>
    {
        private DataProvider _database;

        public UserTable(DataProvider database)
        {
            _database = database;
        }


        public TUser GetUserById(string userId)
        {
            TUser user = null;
            user = _database.GetPocobyId<TUser>(userId);

            return user;
        }

        public TUser GetUserByName(string userName)
        {            
            return _database.GetPocoWhereSingle<TUser>(new Dictionary<string, string>() {{"UserName", userName}});
        }

        public TUser GetUserByEmail(string email)
        {
            return _database.GetPocoWhereSingle<TUser>(new Dictionary<string, string>() { { "Email", email } });
        }

        public string GetPasswordHash(string userId)
        {
            string pwHash;

            var user = GetUserById(userId);
            pwHash = user.PasswordHash;

            return pwHash;
        }

        public string Insert(TUser user)
        {
            return _database.Insert<string, TUser>(user);
        }

        public bool Delete(string userId)
        {
            return _database.Delete<TUser>(userId);
        }

        public bool Delete(TUser user)
        {
            return _database.Delete(user);
        }

        public bool Update(TUser user)
        {
            return _database.Update(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        public bool Update(TUser user, Snapshot<TUser> snapshot)
        {
            return _database.Update(user, snapshot);
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryProviderWithIncludes<TUser> Users => _database.GetNPocoIqProviderWithIncludes<TUser>();
    }
}