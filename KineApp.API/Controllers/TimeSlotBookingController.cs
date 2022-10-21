using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KineApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotBookingController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly IUserService _userService;

        public TimeSlotBookingController(ITimeSlotService timeSlotService, IUserService userService)
        {
            _timeSlotService = timeSlotService;
            _userService = userService;
        }

        //[HttpPost]
        //public IActionResult Add(TimeSlotBookingDTO command)
        //{
        //    try
        //    {
        //        if (command.NewUser == null)
        //        {
        //            return BadRequest();
        //        }
        //        _userService.Register(command.NewUser);
        //        return NoContent();
        //    }
        //    catch (UserException ue)
        //    {
        //        return BadRequest(ue.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
