using Azure.Core;
using ERAMS.API.Dto;
using ERAMS.API.Helper;
using ERAMS.API.Service;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ERAMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService;


        public AuthController(IAuthService authservice)
        {
            _authService = authservice;
           
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                await _authService.Register(request);

                return Ok("User Registered Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Log-In")]
        public async Task<IActionResult> LogIn(LoginRequest request)
        {
            try
            {
                var token = await _authService.LogIn(request);

                return Ok(new
                {
                    Message = "User Logged In Successfully",
                    AccessToken = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

