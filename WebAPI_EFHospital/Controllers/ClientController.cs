using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_EFHospital.Model;

namespace WebAPI_EFHospital.Controllers
{
    public class ClientController : ControllerBase
    {
        [HttpGet("GetClients")]
        public JsonResult GetClients()
        {
            using (Context db = new Context())
            {
                var clients = db.Clients.ToList();

                return new JsonResult(clients);
            }
        }

        [HttpGet("GetClient")]
        public JsonResult GetClient(int clientId)
        {
            using (Context db = new Context())
            {
                var client = db.Clients.FirstOrDefault(c => c.Id == clientId);

                return new JsonResult(client);
            }
        }

        [HttpPost("AddClient")]
        public bool AddClient(int age, string firstName, string lastName, string middleName)
        {
            try
            {
                using (Context db = new Context())
                {
                    var client = new Client { Age=age, FirstName=firstName, LastName=lastName, MiddleName=middleName };

                    db.Clients.Add(client);

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("DeleteClient")]
        public bool DeleteClient(int clientId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var client = db.Clients.FirstOrDefault(c => c.Id == clientId);

                    db.Clients.Remove(client);

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("EditClient")]
        public bool EditClient(int clientId, int age, string firstName, string lastName, string middleName)
        {
            try
            {
                using (Context db = new Context())
                {
                    var client = db.Clients.FirstOrDefault(c => c.Id == clientId);

                    client.Age = age == 0 ? client.Age : age;
                    client.FirstName = firstName == null ? client.FirstName : firstName;
                    client.LastName = lastName == null ? client.LastName : lastName;
                    client.MiddleName = middleName == null ? client.MiddleName : middleName;

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }




        [HttpGet("GetRegistrations")]
        public JsonResult GetRegistrations()
        {
            using (Context db = new Context())
            {
                var registrations = db.Registrations.ToList();

                return new JsonResult(registrations);
            }
        }

        [HttpGet("GetClientRegistrations")]
        public JsonResult GetClientRegistrations(int clientId)
        {
            using (Context db = new Context())
            {
                var registrations = db.Registrations.Include(r => r.Client).Where(r => r.ClientId == clientId).ToList();

                return new JsonResult(registrations);
            }
        }

        [HttpPost("AddClientRegistration")]
        public bool GetClientRegistrations(int clientId, int appointmentId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var registration = new Registration { ClientId = clientId, AppointmentId = appointmentId };

                    db.Registrations.Add(registration);

                    db.SaveChanges();

                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpDelete("DeleteRegistration")]
        public bool DeleteRegistration(int registrationId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var registration = db.Registrations.FirstOrDefault(r=>r.Id==registrationId);

                    db.Registrations.Remove(registration);

                    db.SaveChanges();

                    return true;
                } 
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("EditRegistration")]
        public bool EditRegistration(int registrationId, int clientId, int appointmentId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var registration = db.Registrations.FirstOrDefault(r=>r.Id== registrationId);
                    registration.ClientId = clientId == 0 ? registration.ClientId : clientId;
                    registration.AppointmentId = appointmentId == 0 ? registration.AppointmentId : appointmentId;

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
