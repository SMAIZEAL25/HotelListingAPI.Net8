using HotelListingAPI.AuthManager;
using HotelListingAPI.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        // Post: // api/Account.register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromBody] APIUsersDTO aPIUsersDTO)
        {

            // To track the logger of users when trying to register using this post method 

            _logger.LogInformation($"Registration Attempt for {aPIUsersDTO.Email}");

            // track the logger using the email incase there are any errors 
            try
            {
                var error = await _authManager.Register(aPIUsersDTO); // different a model and modelstate

                if (error.Any())
                {
                    foreach (var errors in error)
                    {
                        ModelState.AddModelError(errors.Code, errors.Description);
                    }

                    return BadRequest(ModelState);

                }

                return Ok();
            }
            catch (Exception ex) 
            {
                // catct exception use the logerror method and reference the register method in our controller 
                _logger.LogError(ex, $"something went wrong in the {nameof(Register)} - user registration attempt for {aPIUsersDTO.Email}");
                // return error 500 indicating failure 
                return Problem($"Something went wrong in the {nameof(Register)}. please contact support or administrator", statusCode: 500);
            }

        }


        // Post: // api/Account.Login
        [HttpPost]
        [Route("api/Account.Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)] 

        public async Task<ActionResult> Login([FromBody]LoginDTO login)
        {
            _logger.LogInformation($"Login Attempt for {login.Email}");

            try
            {

                var authResponseDto = await _authManager.Login(login);

                if (authResponseDto == null)
                {
                    return Unauthorized();
                }

                return Ok(authResponseDto);
            }
        catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went worng in the {nameof(login)}");
                return Problem ($"Sometbhing went wrong in the {nameof(login)}. Please contact administrator ", statusCode: 500);
            }

        }

        // Post: // api/RefeshToken
        [HttpPost]
        [Route("api/RefeshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> RefeshToken([FromBody] AuthResponseDto request)
        {

            var authResponseDto = await _authManager.VerifyRefreshToken(request);

            if (authResponseDto == null)
            {
                return Unauthorized();
            }


            return Ok(authResponseDto);

        }

    }
}   
