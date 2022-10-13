using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Exceptions
{
    public class DayException : Exception
    {
        public DayException(string message)
            : base(message) { }
    }
}
