namespace CameraBase.DTO
{
    public class jwtDTO
    {
        public jwtDTO(int accountId, string accountName, string picture, string jwt, string role)
        {
            this.accountId = accountId;
            this.accountName = accountName;
            this.picture = picture;
            this.jwt = jwt;
            this.role = role;
        }

        public int accountId { get; set; }
        public string accountName { get; set; }
        public string picture { get; set; }
        public string jwt { get; set; }
        public string role { get; set; }
    }
}
