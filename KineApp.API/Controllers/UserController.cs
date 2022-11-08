using KineApp.BLL.DTO.User;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KineApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<UserDTO>))]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery] UserSearchDTO query)
        {
            try
            {
                return Ok(_userService.FindUsers(query).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddDTO command)
        {
            try
            {
                string? role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == "Admin")
                {
                    _userService.Add(command);
                }
                else if (role == "User")
                {
                    await _userService.Register(command);
                }
                else
                {
                    return BadRequest("Must be logged to add user");
                }
                return NoContent();
            }
            catch (UserException ue)
            {
                return BadRequest(ue.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _userService.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UserException ue)
            {
                return BadRequest(ue.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{uId}&{vCode}/validate")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Validate(Guid uId, Guid vCode)
        {
            try
            {
                _userService.Validate(uId, vCode);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UserException ue)
            {
                return BadRequest(ue.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
