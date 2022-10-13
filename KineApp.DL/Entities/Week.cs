using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DL.Entities
{
    public class Week
    {
        #region columns
        public Guid Id { get; set; }

        public DateTime FirstDay { get; set; }

        public DateTime LastDay { get; set; }

        public string? Note { get; set; }
        #endregion

        #region relations
        public ICollection<Day> Days { get; set; } = new List<Day>();
        #endregion       
    }
}
