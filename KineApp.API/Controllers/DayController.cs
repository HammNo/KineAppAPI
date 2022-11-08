using KineApp.BLL.DTO.Day;
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
    public class DayController : ControllerBase
    {
        private readonly IDayService _dayService;
        private readonly IWeekService _weekService;

        public DayController(IDayService dayService, IWeekService weekService)
        {
            _dayService = dayService;
            _weekService = weekService;
        }

        [HttpGet]
        [Produces(typeof(DayDTO))]
        public IActionResult Get([FromQuery]DaySearchDTO query)
        {
            try
            {
                return Ok(_dayService.GetDay(query, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin"));
            }
            catch (KeyNotFoundException )
            {
                return NotFound();
            }
            catch (DayException de)
            {
                return BadRequest(de.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(DayAddDTO command)
        {
            try
            {
                _weekService.AddDay(command);
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
                _dayService.RevealDay(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DayException we)
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
