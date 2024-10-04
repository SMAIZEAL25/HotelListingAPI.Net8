using HotelListingAPI.Model.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.AuthManager
{
    public interface IAuthManager
    {
        // Here the kind of operation this IAuthManager should carryout anytime it's called on 

        Task<IEnumerable<IdentityError>> Register(APIUsersDTO usersDTO);

        Task<AuthResponseDto> Login(LoginDTO loginDTO);

        Task<string> CreateRefreshToken();

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
