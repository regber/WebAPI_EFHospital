using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class Office
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
