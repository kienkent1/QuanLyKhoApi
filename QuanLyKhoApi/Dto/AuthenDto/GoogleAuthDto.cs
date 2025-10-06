namespace QuanLyKhoApi.Dto.AuthenDto
{
    public class GoogleAuthDto
    {
        public string IdToken { get; set; }
    }
    public class GoogleResponse
    {
        public string GoogleSub {  get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? Picture {  get; set; }
    }
}
