using KineApp.BLL.DTO.Day;
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

        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

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
                throw new DayException("Day not implemented");
            }
            return new DayDTO(day);
        }
    }
}
