using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.DTO.User;
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

        public void Register(TimeSlotRegistrationDTO command, bool hasAdminRights)
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
            if (timeSlot.Day.Date < DateTime.Today)
            {
                throw new TimeSlotException("To late to register");
            }
            if (!hasAdminRights && timeSlot.StartTime < (DateTime.Now.AddHours(2)).TimeOfDay)
            {
                throw new TimeSlotException("To late to register");
            }
            if (!timeSlot.Day.Visible)
            {
                throw new TimeSlotException("Can't reach linked day");
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

        public void Unregister(Guid id)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(id);
            if (timeSlot == null || timeSlot.Day == null)
            {
                throw new TimeSlotException("TimeSlot not implemented");
            }
            timeSlot.User = null;
            timeSlot.Booked = false;
            timeSlot.Note = null;
            _timeSlotRepository.Update(timeSlot);
        }

        public Guid Remove(Guid id)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(id);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.User != null)
            {
                throw new TimeSlotException("Can't remove a booked time slot");
            }
            _timeSlotRepository.Remove(timeSlot);
            return id;
        }

        public void BookByUser(TimeSlotBookingDTO command, Guid? userId = null)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(command.TimeSlotId);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.Booked)
            {
                throw new TimeSlotException("Time slot already booked");
            }
            //etc
        }
    }
}
