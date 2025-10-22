namespace QuanLyKhoApi.Dto
{
    public class ComfirmEmailUser
    {
        public Guid IdTaiKhoan { get; set; }

        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
