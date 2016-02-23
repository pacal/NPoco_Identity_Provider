using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPoco;

namespace Pacal.NPoco_Idenity_Provider
{
    [TableName("AspNetUsers")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class IdentityUser : INPocoIdentity<IdentityUser>
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string userName)
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
        [Ignore]
        public ICollection<UserClaim> Claims { get; set; }
        [Ignore]
        public ICollection<IdentityRole> Roles { get; set; }
        [Ignore]
        public ICollection<UserLogin> Logins { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
