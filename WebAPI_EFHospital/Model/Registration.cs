using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EFHospital.Model
{
    public class Registration
    {
        public int Id { get; set; }

        public int WindowId { get; set; }
        public Window Window { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
