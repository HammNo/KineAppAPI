using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
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
    }
}
