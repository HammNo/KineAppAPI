using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Exceptions
{
    public class TimeSlotException : Exception
    {
        public TimeSlotException(string message) 
            : base(message) { }
    }
}
