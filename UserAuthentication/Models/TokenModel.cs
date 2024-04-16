namespace TokenAuthentication.Models
{
    public class UserModel
    {
        public string? Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
    }
}
