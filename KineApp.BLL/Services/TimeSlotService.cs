using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IUserRepository _userRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository, IUserRepository userRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _userRepository = userRepository;
        }

        public void Register(TimeSlotRegistrationDTO command)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(command.TimeSlotId);
            if (timeSlot == null || timeSlot.Day == null)
            {
                throw new TimeSlotException("TimeSlot not implemented");
            }
            if (timeSlot.Booked)
            {
                throw new TimeSlotException("TimeSlot already booked");
            }
            if (timeSlot.Day.Date < DateTime.Today && timeSlot.StartTime < (DateTime.Now.AddHours(2)).TimeOfDay)
            {
                throw new TimeSlotException("To late to register");
            }
            User? user = _userRepository.FindOne(u => u.Id == command.UserId);
            if (user == null)
            {
                throw new UserException("Can't find user");
            }
            timeSlot.User = user;
            timeSlot.Booked = true;
            timeSlot.Note = command.Note;
            _timeSlotRepository.Update(timeSlot);
        }
    }
}
