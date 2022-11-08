using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KineApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly IDayService _dayService;
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotController(IDayService dayService, ITimeSlotService timeSlotService)
        {
            _dayService = dayService;
            _timeSlotService = timeSlotService;
        }

        [HttpGet("getWaiting")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetWaiting()
        {
            try
            {
                return Ok(_timeSlotService.GetAllWaiting());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(TimeSlotAddDTO command)
        {
            try
            {
                _dayService.AddTimeSlot(command);
                return NoContent();
            }
            catch (TimeSlotException tse)
            {
                return BadRequest(tse.Message);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _timeSlotService.Remove(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (TimeSlotException tse)
            {
                return BadRequest(tse.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
