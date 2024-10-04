using AutoMapper;
using HotelListingAPI.Model;
using HotelListingAPI.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingAPI.AuthManager
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration1;
        private APIUser _user;

        // global variable 
        private const string _loginProvider = "HotelListingApi";
        private const string _refreshToken = "RefreshToken";
        // implementing IMapper here because this AuthDTO is not been implemented directly to the database

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;

        }

        // this method createrefreshtoken so that we don't have to delete user while creating new token
        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user,"HotelListingApi", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = JwtSecurityTokenHandler.ReadJwtToken(request.RefreshToken);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.userId)
            {
                return null;
            }

            var IsValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.token);

            if (IsValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    token = token,
                    userId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);

            return null;

        }

        public async Task<AuthResponseDto> Login(LoginDTO loginDTO)
        {
            _user = await _userManager.FindByEmailAsync(loginDTO.Email);
            bool IsValidUser = await _userManager.CheckPasswordAsync(_user, loginDTO.Password);

            if (_user == null || IsValidUser == false)
            {
                return null;
            }

            var token = await GenerateToken();
            return new AuthResponseDto
            {
                token = token,
                userId = _user.Id
            };
        }



        public async Task<IEnumerable<IdentityError>> Register(APIUsersDTO usersDTO)
        {

            _user = _mapper.Map<APIUser>(usersDTO);
            _user.UserName = usersDTO.Email;

            var result = await _userManager.CreateAsync(_user, usersDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync( _user, "User");
            }

            return result.Errors;
        }



        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration1["JwSettings:Key"]));
            var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var UserClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
               new Claim (JwtRegisteredClaimNames.Sub, _user.Email),
               new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Email, _user.Email),
               new Claim("uid", _user.Id),
            }
            .Union(UserClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration1["JwtSettings:Issuer"],
                audience: _configuration1["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration1["Jwtsettings:DurationInMinutes"])),
                 signingCredentials: credentails
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
