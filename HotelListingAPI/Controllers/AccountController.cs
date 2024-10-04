﻿using HotelListingAPI.AuthManager;
using HotelListingAPI.Model;
using HotelListingAPI.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        // Post: // api/Account.register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromBody] APIUsersDTO aPIUsersDTO)
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


        // Post: // api/Account.Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)] 

        public async Task<ActionResult> Login([FromBody]LoginDTO login)
        {
         
            var authResponseDto = await _authManager.Login(login);

            if (authResponseDto  == null)
            {
                return Unauthorized();
            }
          

            return Ok();

        }

        // Post: // api/RefeshToken
        [HttpPost]
        [Route("RefreshToken")]
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
