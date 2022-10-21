using KineApp.BLL.DTO.User;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                //Si admin : _userService.Add(command)
                await _userService.Register(command);
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
