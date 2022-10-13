using KineApp.BLL.DTO.User;
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserAddDTO command)
        {
            try
            {
                _userService.Add(command);
                return NoContent();
            }
            catch(Exception e)
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
