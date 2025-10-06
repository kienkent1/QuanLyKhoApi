namespace QuanLyKhoApi.Data
{
    public class RoleClaim
    {
        public string RoleId { get; set; }
        public Role Role { get; set; }

        public int ClaimId { get; set; }
        public Claims Claim { get; set; }
    }
}
