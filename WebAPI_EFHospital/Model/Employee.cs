using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class Employee:Person
    {

        public int? PositionId { get; set; }
        public Position? Position { get; set; }


        public int? EmplScheduleId { get; set; }
        public EmplSchedule? EmplSchedule { get; set; }
    }
}
