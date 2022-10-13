using KineApp.BLL.DTO.Day;
using KineApp.BLL.DTO.Week;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface IWeekService
    {
        WeekDTO GetWeek(WeekSearchDTO query, bool getAsAdmin);
        Guid AddWeek(WeekAddDTO command);
        Guid AddDay(DayAddDTO command);
    }
}
