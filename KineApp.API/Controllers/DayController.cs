using KineApp.BLL.DTO.Day;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                //! Tester droits d'administration
                //Si simple user : return Ok(_dayService.GetDay(query, false));
                return Ok(_dayService.GetDay(query, false));
            }
            catch(DayException de)
            {
                return BadRequest(de.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddDay(DayAddDTO command)
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
    }
}
