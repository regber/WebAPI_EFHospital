using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_EFHospital.Model;

namespace WebAPI_EFHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {









        [HttpGet("GetEmployees")]
        public JsonResult GetEmployees()
        {
            using (Context db = new Context())
            {
                var employees = db.Employees.Include(e => e.Position).ToList();

                return new JsonResult(employees);
            }
        }

        [HttpGet("GetEmployee")]
        public JsonResult GetEmployee(int employeeId)
        {
            using (Context db = new Context())
            {
                var employee = db.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == employeeId);

                return new JsonResult(employee);
            }
        }

        [HttpGet("GetEmployeeFreeAppointment")]
        public JsonResult GetEmployeeFreeAppointments(int employeeId)
        {
            int? scheduleId;

            using (Context db = new Context())
            {
                scheduleId = db.Employees.Include(e => e.EmplSchedule).FirstOrDefault(e => e.Id == employeeId).EmplScheduleId;
            }

            using (Context db = new Context())
            {
                var appointments = db.Appointments.Include(a => a.Registration).Where(a => a.Registration == null);

                var freeAppointments = appointments.Where(a => a.EmplScheduleId == scheduleId).ToList();

                return new JsonResult(freeAppointments);
            }
        }

        [HttpGet("GetEmployeeOccupiedAppointment")]
        public JsonResult GetEmployeeOccupiedAppointments(int employeeId)
        {
            int? scheduleId;

            using (Context db = new Context())
            {
                scheduleId = db.Employees.Include(e => e.EmplSchedule).FirstOrDefault(e => e.Id == employeeId).EmplScheduleId;
            }

            using (Context db = new Context())
            {
                var appointments = db.Appointments.Include(a => a.Registration).ThenInclude(r => r.Client).Where(a => a.Registration != null);

                var freeAppointments = appointments.Where(a => a.EmplScheduleId == scheduleId).ToList();

                return new JsonResult(freeAppointments);
            }
        }



        [HttpPost("AddEmployee")]
        public bool AddEmployee(int age, string firstName, string lastName, string middleName, int? positionId, int? emplScheduleId)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Employees.Add(new Model.Employee { Age = age, FirstName = firstName, LastName = lastName, MiddleName = middleName, PositionId = positionId, EmplScheduleId = emplScheduleId });

                    db.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpDelete("DeleteEmployee")]
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var employee = db.Employees.FirstOrDefault(e => e.Id == employeeId);

                    if (employee != null)
                    {
                        db.Employees.Remove(employee);

                        db.SaveChanges();
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPut("EditEmployee")]
        public bool EditEmployee(int employeeId,int age, string firstName, string lastName, string middleName, int? positionId, int? emplScheduleId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var employee = db.Employees.FirstOrDefault(e=>e.Id== employeeId);

                    employee.Age = age == 0 ? employee.Age: age;
                    employee.FirstName = firstName == null ? employee.FirstName : firstName;
                    employee.LastName = lastName == null ? employee.LastName : lastName;
                    employee.MiddleName = middleName == null ? employee.MiddleName : middleName;
                    employee.PositionId = positionId == null ? employee.PositionId : positionId;
                    employee.EmplScheduleId = emplScheduleId == null ? employee.EmplScheduleId : emplScheduleId;

                    db.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }




        [HttpGet("GetPositions")]
        public JsonResult GetPositions()
        {
            using (Context db = new Context())
            {
                var positions = db.Positions.ToList();

                return new JsonResult(positions);
            }
        }
        [HttpGet("GetPosition")]
        public JsonResult GetPosition(int positionId)
        {
            using (Context db = new Context())
            {
                var positions = db.Positions.FirstOrDefault(p => p.Id == positionId);

                return new JsonResult(positions);
            }
        }

        [HttpPost("AddPosition")]
        public bool AddPosition(string positionName, int salary)
        {
            try
            {
                using (Context db = new Context())
                {
                    var position = new Position { Name = positionName, Salary = salary };

                    db.Positions.Add(position);

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("DeletePosition")]
        public bool DeletePosition(int positionId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var position = db.Positions.FirstOrDefault(p => p.Id == positionId);

                    db.Positions.Remove(position);

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpPut("EditPosition")]
        public bool EditPosition(int positionId, string name, int salary)
        {
            try
            {
                using (Context db = new Context())
                {
                    var position = db.Positions.FirstOrDefault(e => e.Id == positionId);

                    position.Name = name == null ? position.Name : name;
                    position.Salary = salary == 0 ? position.Salary : salary;

                    db.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPut("SetEmployeePosition")]
        public bool SetEmployeePosition(int employeeId, int positionId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var employee = db.Employees.FirstOrDefault(e => e.Id == employeeId);
                    var position = db.Positions.FirstOrDefault(s => s.Id == positionId);

                    if (employee != null && position != null)
                    {
                        employee.Position = position;

                        db.SaveChanges();

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }



        [HttpGet("GetSchedules")]
        public JsonResult GetSchedules()
        {
            using (Context db = new Context())
            {
                var schedule = db.EmplSchedules.Include(s => s.Appointments).ToList();

                return new JsonResult(schedule);
            }
        }

        [HttpGet("GetSchedule")]
        public JsonResult GetSchedule(int scheduleId)
        {
            using (Context db = new Context())
            {
                var schedule = db.EmplSchedules.Include(s => s.Appointments).FirstOrDefault(s => s.Id == scheduleId);

                return new JsonResult(schedule);
            }
        }

        [HttpPost("AddSchedule")]
        public int? AddSchedule()
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = new EmplSchedule();

                    db.EmplSchedules.Add(schedule);

                    db.SaveChanges();

                    return schedule.Id;
                }

            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("DeleteSchedule")]
        public bool DeleteSchedule(int scheduleId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = db.EmplSchedules.FirstOrDefault(p => p.Id == scheduleId);

                    db.EmplSchedules.Remove(schedule);

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpPut("SetEmployeeSchedule")]
        public bool SetEmployeeSchedule(int employeeId, int scheduleId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var employee = db.Employees.FirstOrDefault(e => e.Id == employeeId);
                    var schedule = db.EmplSchedules.FirstOrDefault(s => s.Id == scheduleId);

                    if (employee != null && schedule != null)
                    {
                        employee.EmplSchedule = schedule;

                        db.SaveChanges();

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }





        [HttpGet("GetOffices")]
        public JsonResult GetOffices()
        {
            using (Context db = new Context())
            {
                var offices = db.Offices.Include(o => o.Appointments).ToList();

                return new JsonResult(offices);
            }
        }

        [HttpGet("GetOffice")]
        public JsonResult GetOffice(int officeId)
        {
            using (Context db = new Context())
            {
                var office = db.Offices.Include(o => o.Appointments).FirstOrDefault(o => o.Id == officeId);

                return new JsonResult(office);
            }
        }

        [HttpPost("AddOffice")]
        public bool AddOffice(int officeNumber)
        {
            try
            {
                using (Context db = new Context())
                {
                    var newOffice = new Office { Number = officeNumber };

                    db.Offices.Add(newOffice);

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("DeleteOffice")]
        public bool DeleteOffice(int officeId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var office = db.Offices.FirstOrDefault(o => o.Id == officeId);

                    db.Offices.Remove(office);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("EditOffice")]
        public bool EditOffice(int officeId, int officeNumber)
        {
            try
            {
                using (Context db = new Context())
                {
                    var office = db.Offices.FirstOrDefault(o => o.Id == officeId);

                    office.Number = officeNumber == 0 ? office.Number : officeNumber;

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }




        [HttpGet("GetFreeAppointments")]
        public JsonResult GetFreeAppointments()
        {
            using (Context db = new Context())
            {
                var appointments = db.Appointments.Include(a => a.Registration).Where(r => r.Registration == null).ToList();

                return new JsonResult(appointments);
            }
        }

        [HttpGet("GetOccupiedAppointments")]
        public JsonResult GetOccupiedAppointments()
        {
            using (Context db = new Context())
            {
                db.Clients.ToList();
                var appointments = db.Appointments.Include(a => a.Registration).ThenInclude(r => r.Client).Where(r => r.Registration != null).ToList();

                return new JsonResult(appointments);
            }
        }

        [HttpGet("GetAppointment")]
        public JsonResult GetAppointment(int appointmentId)
        {
            using (Context db = new Context())
            {
                db.Clients.ToList();
                var appointment = db.Appointments.Include(a => a.Registration).Include(a => a.Office).FirstOrDefault(a => a.Id == appointmentId);

                return new JsonResult(appointment);
            }
        }

        [HttpPost("AddAppointmentToSchedule")]
        public bool AddAppointmentToSchedule(int scheduleId, int officeId, string startReception, string endReception)
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = db.EmplSchedules.FirstOrDefault(p => p.Id == scheduleId);

                    schedule.Appointments.Add(new Appointment { OfficeId = officeId, StartReception = DateTime.Parse(startReception), EndReception = DateTime.Parse(endReception) });

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("RemoveAppointment")]
        public bool RemoveAppointment(int appointmentId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var appointment = db.Appointments.FirstOrDefault(p => p.Id == appointmentId);

                    db.Appointments.Remove(appointment);

                    db.SaveChanges();

                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        [HttpPut("EditAppointment")]
        public bool EditAppointment(int appointmentId, int officeId, string startReception, string endReception)
        {
            try
            {
                using (Context db = new Context())
                {
                    var appointment = db.Appointments.Include(a => a.Registration).FirstOrDefault(p => p.Id == appointmentId);

                    appointment.OfficeId = officeId == 0 ? appointment.OfficeId : officeId;
                    appointment.StartReception = startReception == null ? appointment.StartReception : DateTime.Parse(startReception);
                    appointment.EndReception = endReception == null ? appointment.EndReception : DateTime.Parse(endReception);

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
