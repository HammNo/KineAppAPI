using KineApp.BLL.DTO.Day;
using KineApp.BLL.DTO.Week;
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
    public class WeekService : IWeekService
    {
        private readonly IWeekRepository _weekRepository;
        private readonly IDayRepository _dayRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public WeekService(IWeekRepository weekRepository, IDayRepository dayRepository, ITimeSlotRepository timeSlotRepository)
        {
            _weekRepository = weekRepository;
            _dayRepository = dayRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        #region public methods
        public WeekDTO GetWeek(WeekSearchDTO query, bool getAsAdmin)
        {
            DateTime refDate;
            if (query.FirstDayOfRefWeek == null)
            {
                refDate = FirstDayOfWeek(DateTime.Today);
            }
            else
            {
                refDate = FirstDayOfWeek((DateTime)query.FirstDayOfRefWeek);
            }
            Week? week;
            if (getAsAdmin) week = _weekRepository.GetDetailedWeekWithUsers(refDate, true);
            else week = _weekRepository.GetDetailedWeek(refDate, false);
            if (week == null)
            {
                throw new WeekException("Week not implemented");
            }
            return new WeekDTO(week);
        }

        public Guid AddWeek(WeekAddDTO command)
        {
            DateTime cWeekFDay;

            if (command.DayOfCreationWeek == null)
            {
                cWeekFDay = FirstDayOfWeek(DateTime.Today.AddDays(7));
            }
            else cWeekFDay = FirstDayOfWeek((DateTime)command.DayOfCreationWeek);

            if (cWeekFDay < FirstDayOfWeek(DateTime.Today))
            {
                throw new WeekException("Target week can't be in the past");
            }
            if (cWeekFDay > FirstDayOfWeek(DateTime.Today.AddYears(1)))
            {
                throw new WeekException("Target week must be in the limit of 1 year");
            }
            if (_weekRepository.Any(w => w.FirstDay == cWeekFDay))
            {
                throw new WeekException("Target week already exists");
            }

            Week newWeek;
            if (command.DayOfModelWeek == null)
            {
                newWeek = CreateWeek(cWeekFDay, command.Note);
            }
            else
            {
                Week? modelWeek = _weekRepository.GetDetailedWeek(FirstDayOfWeek((DateTime)command.DayOfModelWeek), true);
                if (modelWeek == null)
                {
                    throw new WeekException("Model week doesn't exist");
                }
                newWeek = CreateWeek(cWeekFDay, command.Note);
                FillExistingWeek(newWeek, modelWeek);
            }
            return newWeek.Id;
        } 
        #endregion

        public Guid AddDay(DayAddDTO command)
        {
            DateTime cDayDate;

            if (command.Date == null)
            {
                cDayDate = DateTime.Today;
            }
            else cDayDate = new DateTime(((DateTime)command.Date).Year, ((DateTime)command.Date).Month, ((DateTime)command.Date).Day);

            if (cDayDate.Date < DateTime.Today)
            {
                throw new WeekException("Target day can't be in the past");
            }
            if (cDayDate > cDayDate.AddYears(1))
            {
                throw new WeekException("Target day must be in the limit of 1 year");
            }
            if (_dayRepository.Any(d => d.Date == cDayDate))
            {
                throw new WeekException("Target day already exists");
            }
            Week? weekOfCDay = _weekRepository.FindOne(w => w.FirstDay == FirstDayOfWeek(cDayDate));
            if (weekOfCDay == null)
            {
                throw new WeekException("Week of target day doesn't exist yet");
            }
            Day newDay = CreateDay(cDayDate, command.Note, weekOfCDay.Id, false);
            return newDay.Id;
        }

        #region private methods
        private DateTime FirstDayOfWeek(DateTime refDate)
        {
            int daysTillTargetDay = DayOffset(refDate);
            return (new DateTime(refDate.Year, refDate.Month, refDate.Day)).AddDays(-daysTillTargetDay);
        }

        private int DayOffset(DateTime targetDate)
        {
            int targetDay = (targetDate.DayOfWeek == DayOfWeek.Sunday) ? 7 : (int)targetDate.DayOfWeek;
            return targetDay - (int)DayOfWeek.Monday;
        }

        private Week CreateWeek(DateTime fDayOfTWeek, string? note)
        {
            Week newWeek = new Week
            {
                Id = Guid.NewGuid(),
                FirstDay = new DateTime(fDayOfTWeek.Date.Year, fDayOfTWeek.Date.Month, fDayOfTWeek.Date.Day),
                LastDay = (new DateTime(fDayOfTWeek.Date.Year, fDayOfTWeek.Date.Month, fDayOfTWeek.Date.Day)).AddDays(6),
                Note = note
            };
            _weekRepository.Add(newWeek);
            return newWeek;
        }

        private Day CreateDay(DateTime date, string? note, Guid weekId, bool visible)
        {
            Day newDay = new Day
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(date.Year, date.Month, date.Day),
                Note = note,
                WeekId = weekId,
                Visible = visible
            };
            _dayRepository.Add(newDay);
            return newDay;
        }

        private void FillExistingWeek(Week baseWeek, Week modelWeek)
        {
            foreach (Day day in modelWeek.Days)
            {
                int dayOffset = DayOffset(day.Date);
                Day dayToAdd = CreateDay(baseWeek.FirstDay.AddDays(dayOffset), null, baseWeek.Id, false);
                foreach (TimeSlot timeSlot in day.TimeSlots)
                {
                    TimeSlot tsToAdd = new TimeSlot
                    {
                        Id = Guid.NewGuid(),
                        StartTime = timeSlot.StartTime,
                        EndTime = timeSlot.EndTime,
                        Note = timeSlot.Note,
                        DayId = dayToAdd.Id
                    };
                    _timeSlotRepository.Add(tsToAdd);
                };
            };
        } 
        #endregion
    }
}
