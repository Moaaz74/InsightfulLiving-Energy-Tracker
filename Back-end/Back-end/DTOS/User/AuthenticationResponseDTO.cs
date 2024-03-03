namespace Back_end.DTOS.User
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int HomeId { get; set; }
    }
}
