using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class Appointment
    {
        public int Id { get; set; }

        public int EmplScheduleId { get; set; }
        public EmplSchedule EmplSchedule { get; set; }

        public int? RegistrationId { get; set; }
        public Registration? Registration { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }

        public DateTime StartReception { get; set; } 
        public DateTime EndReception { get; set; }
    }
}
