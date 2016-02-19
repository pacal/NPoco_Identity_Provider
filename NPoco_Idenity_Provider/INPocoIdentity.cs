using System;
using Microsoft.AspNet.Identity;

namespace Pacal.NPoco_Idenity_Provider
{
    public interface INPocoIdentity : IUser
    {
         string Email { get; set; }
         bool EmailConfirmed { get; set; }
         string PasswordHash { get; set; }
         string SecurityStamp { get; set; }
         string PhoneNumber { get; set; }
         bool PhoneNumberConfirmed { get; set; }
         bool TwoFactorEnabled { get; set; }
         DateTime? LockoutEndDateUtc { get; set; }
         bool LockoutEnabled { get; set; }
         int AccessFailedCount { get; set; }
    }
}
