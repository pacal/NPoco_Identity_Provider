using System;
using Microsoft.AspNet.Identity;
using NPoco;

namespace Pacal.NPoco_Idenity_Provider
{
    [TableName("AspNetRoles")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class IdentityRole : IRole
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string name) : this()
        {
            Name = name;
        }

        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }
  
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
