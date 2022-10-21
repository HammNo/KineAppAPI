using KineApp.BLL.DTO.Day;
using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using KineApp.BLL.Mappers;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public DayService(IDayRepository dayRepository, ITimeSlotRepository timeSlotRepository)
        {
            _dayRepository = dayRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        #region public methods
        public DayDTO GetDay(DaySearchDTO query, bool getAsAdmin)
        {
            DateTime date;
            if (query.Date == null) date = DateTime.Today;
            else date = (DateTime)query.Date;
            Day? day;
            if (getAsAdmin) day = _dayRepository.GetDayWithTimeSlotsAndUsers(date, true);
            else day = _dayRepository.GetDayWithTimeSlots(date, false);
            if (day == null)
            {
                throw new KeyNotFoundException();
            }
            return new DayDTO(day);
        }

        public void RevealDay(Guid id)
        {
            Day? day = _dayRepository.GetById(id);
            if (day == null)
            {
                throw new KeyNotFoundException();
            }
            if (day.Date < DateTime.Today)
            {
                throw new DayException("Can't reveal past day");
            }
            day.Visible = true;
            _dayRepository.Update(day);
        }

        public Guid AddTimeSlot(TimeSlotAddDTO command)
        {
            Day? day = _dayRepository.GetDayWithTimeSlots(new DateTime(command.Date.Year, command.Date.Month, command.Date.Day), true);
            if (day == null)
            {
                throw new DayException("Day not implemented");
            }
            if (day.Date < DateTime.Today)
            {
                throw new DayException("Target day can't be located in the past");
            }
            if (command.EndTime - command.StartTime < TimeSpan.FromMinutes(15) || (command.EndTime - command.StartTime > TimeSpan.FromMinutes(90)))
            {
                throw new TimeSlotException("Time slot must be bewteen 15 and 90 minutes");
            }
            if (!IsOkTimeByDay(day, command.StartTime, command.EndTime))
            {
                throw new TimeSlotException("Time slot already occupied");
            }
            TimeSlot ts = new TimeSlot
            {
                Id = new Guid(),
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                DayId = day.Id
            };
            _timeSlotRepository.Add(ts);
            day.TimeSlots.Add(ts);
            _dayRepository.Update(day);
            return ts.Id;
        }
        #endregion

        #region private methods
        private bool IsOkTimeByDay(Day targetDay, TimeSpan startTime, TimeSpan endTime)
        {
            TimeSlot? ts = targetDay.TimeSlots.SingleOrDefault(t => (startTime >= t.StartTime && startTime <= t.EndTime)
                                                                    || (endTime >= t.StartTime && endTime <= t.EndTime));
            if (ts != null) return false;
            return true;
        } 
        #endregion
    }
}
