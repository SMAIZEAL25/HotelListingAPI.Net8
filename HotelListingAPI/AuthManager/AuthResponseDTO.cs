namespace HotelListingAPI.AuthManager
{
    public class AuthResponseDto
    {
        public string userId { get; set; }

        public string token { get; set; }

        public string RefreshToken { get; set; }
    }
}
