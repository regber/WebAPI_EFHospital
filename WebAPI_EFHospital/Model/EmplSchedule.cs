using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class EmplSchedule
    {
        public int Id { get; set; }

        public ICollection<Window> Windows { get; set; }
    }
}
