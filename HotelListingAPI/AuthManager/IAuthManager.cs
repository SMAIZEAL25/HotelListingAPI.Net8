using HotelListingAPI.Model.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.AuthManager
{
    public interface IAuthManager
    {
        // Here the kind of operation this IAuthManager should carryout anytime it's called on 

        public Task <IEnumerable<IdentityError>> Register(APIUsersDTO usersDTO);
    }
}
