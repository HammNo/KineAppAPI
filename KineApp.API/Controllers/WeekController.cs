using KineApp.BLL.DTO.Week;
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
    public class WeekController : ControllerBase
    {
        private readonly IWeekService _weekService;

        public WeekController(IWeekService weekService)
        {
            _weekService = weekService;
        }

        [HttpGet]
        [Produces(typeof(WeekDTO))]
        public IActionResult Get([FromQuery] WeekSearchDTO query)
        {
            try
            {
                return Ok(_weekService.GetWeek(query, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin"));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] WeekAddDTO command)
        {
            try
            {
                _weekService.AddWeek(command);
                return NoContent();
            }
            catch (WeekException we)
            {
                return BadRequest(we.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}/reveal")]
        [Authorize(Roles = "Admin")]
        public IActionResult Reveal(Guid id)
        {
            try
            {
                _weekService.RevealWeek(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (WeekException we)
            {
                return BadRequest(we.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
