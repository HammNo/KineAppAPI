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
    public class TimeSlotRegistrationController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotRegistrationController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SetAppointement(TimeSlotRegistrationDTO command)
        {
            try
            {
                _timeSlotService.Register(command);
                return NoContent();
            }
            catch (TimeSlotException tse)
            {
                return BadRequest(tse.Message);
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
        public async Task<IActionResult> UnsetAppointement(Guid id)
        {
            try
            {
                await _timeSlotService.Unregister(id);
                return NoContent();
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

        [HttpPatch("book")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Book([FromBody] TimeSlotBookingDTO command)
        {
            try
            {
                //Récupérer userId à partir du HttpContext
                await _timeSlotService.TryRegister(command, new Guid("8034F963-2353-4C56-BDAE-C3BC1062E34C")/*remplacer par userId*/);
                return NoContent();
            }
            catch (TimeSlotException tse)
            {
                return BadRequest(tse.Message);
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

        [HttpPatch("{tsId}/confirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(Guid tsId)
        {
            try
            {
                await _timeSlotService.ConfirmRegistration(tsId);
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

        [HttpPatch("{tsId}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(Guid tsId)
        {
            try
            {
                await _timeSlotService.RejectRegistration(tsId);
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
