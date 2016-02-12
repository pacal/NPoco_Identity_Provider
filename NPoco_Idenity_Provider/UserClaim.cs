using NPoco;

namespace Pacal.NPoco_Idenity_Provider
{
    [TableName("AspNetUserClaims")]
    [PrimaryKey("Id")]
    public class UserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
