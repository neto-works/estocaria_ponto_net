namespace EstocariaNet.Shared.DTOs
{
    public class JwtObjectResultDTO
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiration { get; set; }

    }
}
