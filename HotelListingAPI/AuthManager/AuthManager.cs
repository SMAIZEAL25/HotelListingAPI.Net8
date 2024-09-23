using AutoMapper;
using HotelListingAPI.Model;
using HotelListingAPI.Model.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.AuthManager
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser>_userManager;

        // implementing IMapper here because this AuthDTO is not been implemented directly to the database

        public AuthManager(IMapper mapper, UserManager <APIUser> userManager )
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
     

        public async Task<IEnumerable<IdentityError>> Register(APIUsersDTO usersDTO)
        {
            
           var user  = _mapper.Map<APIUser>( usersDTO);
            user.UserName = usersDTO.Email;

            var result = await _userManager.CreateAsync( user, usersDTO.Password );

            if ( result .Succeeded )
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}
