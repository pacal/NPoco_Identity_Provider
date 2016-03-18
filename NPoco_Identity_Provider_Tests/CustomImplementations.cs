using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPoco;
using Pacal.NPoco_Idenity_Provider;

namespace Pacal.NPoco_Identity_Provider_Tests
{
    [TableName("AspNetUsers")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class MyCustomUser : INPocoIdentity<MyCustomUser>
    {
        public MyCustomUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MyCustomUser(string userName)
            : this()
        {
            UserName = userName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Comment { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyCustomUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [TableName("AspNetRoles")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class MyCustomRole : IRole
    {
        public MyCustomRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MyCustomRole(string name) : this()
        {
            Name = name;
        }

        public MyCustomRole(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
