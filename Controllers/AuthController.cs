using MediMitra.Data;
using MediMitra.DTO;
using MediMitra.Services;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediMitra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (ModelState.IsValid)
            {
                var result = await _authService.Signup(registerDTO);
                if (result.Status)
                {
                    return StatusCode(StatusCodes.Status201Created, result);
                }
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(ModelState);
        }
           

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginUser(loginDTO);
            if(result.Status)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
             return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> updatePassword(ChangePasswordDTO changePasswordDTO)
        {
            if(ModelState.IsValid)
            {
            var result = await _authService.changePassword(changePasswordDTO);
                if (result.Status)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> forgotEmailPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var result = await _authService.forgotPassword(forgotPasswordDTO.Email, HttpContext);
            if (result.Status)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);
        }

        [HttpPost]
        [Route("reset-password")]

        public async Task<IActionResult> ResetPasswordusingOTP(sendOtpDTO sendOtpDTO)
        {
            if (ModelState.IsValid)
            {
            var result = await _authService.ResetPassword(sendOtpDTO.Otp, sendOtpDTO.newPassword, sendOtpDTO.confirmPassword, HttpContext);

                if (result.Status)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return StatusCode(StatusCodes.Status400BadRequest, result);

            }
            return BadRequest(ModelState);
        }
    }

}
