using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class Client:Person
    {
        public ICollection<Registration> Registrations { get; set; }
    }
}
