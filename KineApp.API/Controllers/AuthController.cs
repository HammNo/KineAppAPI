using KineApp.API.DTO;
using KineApp.BLL.DTO.Admin;
using KineApp.BLL.DTO.User;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace KineApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthenticationService authenticationService, IJwtService jwtService)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
        }

        [HttpPost("administrate")]
        [Produces(typeof(AdminTokenDTO))]
        public IActionResult Administrate(AdminLoginDTO dto)
        {
            try
            {
                AdminDTO connectedAdmin = _authenticationService.AdminLogin(dto);
                string token = _jwtService.CreateToken(connectedAdmin.Id.ToString(), connectedAdmin.Email, "Admin");
                return Ok(new AdminTokenDTO(token, connectedAdmin));
            }
            catch (AuthenticationException)
            {
                return BadRequest("Bad credentials");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        [Produces(typeof(UserTokenDTO))]
        public IActionResult Login(UserLoginDTO dto)
        {
            try
            {
                UserDTO connectedUser = _authenticationService.UserLogin(dto);
                string token = _jwtService.CreateToken(connectedUser.Id.ToString(), connectedUser.Email, "User");
                return Ok(new UserTokenDTO(token, connectedUser));
            }
            catch (AuthenticationException)
            {
                return BadRequest("Bad credentials");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
