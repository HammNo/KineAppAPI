﻿using KineApp.BLL.DTO.Week;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetWeek([FromQuery] WeekSearchDTO query)
        {
            try
            {
                //! Tester droits d'administration
                //Si simple user : return Ok(_weekService.GetWeek(query, false));
                return Ok(_weekService.GetWeek(query, true));
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

        [HttpPost]
        public IActionResult AddWeek([FromBody] WeekAddDTO command)
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
    }
}