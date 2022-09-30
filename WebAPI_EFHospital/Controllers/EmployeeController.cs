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

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEmployees")]
        public JsonResult GetEmployees()
        {
            using (Context db = new Context())
            {
                var employees = db.Employees.Include(e => e.Position).ToList();

                return new JsonResult(employees);
            }
        }

        /// <summary>
        /// Получить информацию по сотруднику
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <returns></returns>
        [HttpGet("GetEmployee")]
        public JsonResult GetEmployee(int employeeId)
        {
            using (Context db = new Context())
            {
                var employee = db.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == employeeId);

                return new JsonResult(employee);
            }
        }

        /// <summary>
        /// Получить список свободных для приема записей
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <returns></returns>
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


        /// <summary>
        /// Получить список занятых для приема записей
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="age">Возраст</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="positionId">Id должности</param>
        /// <param name="emplScheduleId">Id расписания сотрудника</param>
        /// <returns></returns>
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

        /// <summary>
        /// Удалить сотрудника из БД
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <returns></returns>
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

        /// <summary>
        /// Изменить данные сотрудника
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="age">Возраст</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="positionId">Id должности</param>
        /// <param name="emplScheduleId">Id расписания сотрудника</param>
        /// <returns></returns>
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



        /// <summary>
        /// Получить список должностей
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPositions")]
        public JsonResult GetPositions()
        {
            using (Context db = new Context())
            {
                var positions = db.Positions.ToList();

                return new JsonResult(positions);
            }
        }

        /// <summary>
        /// Получить информацию по должности
        /// </summary>
        /// <param name="positionId">Id должности</param>
        /// <returns></returns>
        [HttpGet("GetPosition")]
        public JsonResult GetPosition(int positionId)
        {
            using (Context db = new Context())
            {
                var positions = db.Positions.FirstOrDefault(p => p.Id == positionId);

                return new JsonResult(positions);
            }
        }

        /// <summary>
        /// Добавить должность в ябд
        /// </summary>
        /// <param name="positionName">Название должности</param>
        /// <param name="salary">Зарплата</param>
        /// <returns></returns>
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

        /// <summary>
        /// Удалить должность из БД
        /// </summary>
        /// <param name="positionId">Id должности</param>
        /// <returns></returns>
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

        /// <summary>
        /// Изменить должность
        /// </summary>
        /// <param name="positionId">Id должности</param>
        /// <param name="positionName">Название должности</param>
        /// <param name="salary">Зарплата</param>
        /// <returns></returns>
        [HttpPut("EditPosition")]
        public bool EditPosition(int positionId, string positionName, int salary)
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


        /// <summary>
        /// Назначить сотрудника на должность
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="positionId">Id должности</param>
        /// <returns></returns>
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


        /// <summary>
        /// Получить список расписаний сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSchedules")]
        public JsonResult GetSchedules()
        {
            using (Context db = new Context())
            {
                var schedule = db.EmplSchedules.Include(s => s.Appointments).ToList();

                return new JsonResult(schedule);
            }
        }

        /// <summary>
        /// Получить информацию по расписанию сотрудника
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [HttpGet("GetSchedule")]
        public JsonResult GetSchedule(int scheduleId)
        {
            using (Context db = new Context())
            {
                var schedule = db.EmplSchedules.Include(s => s.Appointments).FirstOrDefault(s => s.Id == scheduleId);

                return new JsonResult(schedule);
            }
        }


        /// <summary>
        /// Добавить расписание сотрудника
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Удалить расписание сотрудника
        /// </summary>
        /// <param name="scheduleId">Id расписания</param>
        /// <returns></returns>
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

        /// <summary>
        /// Назначить раписание сотруднику
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="scheduleId">id расписания</param>
        /// <returns></returns>
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




        /// <summary>
        /// Получить список кабинетов
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOffices")]
        public JsonResult GetOffices()
        {
            using (Context db = new Context())
            {
                var offices = db.Offices.Include(o => o.Appointments).ToList();

                return new JsonResult(offices);
            }
        }

        /// <summary>
        /// Получить информацию по кабинету
        /// </summary>
        /// <param name="officeId">Id кабинета</param>
        /// <returns></returns>
        [HttpGet("GetOffice")]
        public JsonResult GetOffice(int officeId)
        {
            using (Context db = new Context())
            {
                var office = db.Offices.Include(o => o.Appointments).FirstOrDefault(o => o.Id == officeId);

                return new JsonResult(office);
            }
        }

        /// <summary>
        /// Добавить кабинет
        /// </summary>
        /// <param name="officeNumber">Номер кабинета</param>
        /// <returns></returns>
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


        /// <summary>
        /// Удалить кабинет из БД
        /// </summary>
        /// <param name="officeId">Id кабинета</param>
        /// <returns></returns>
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

        /// <summary>
        /// Изменить данные кабинета
        /// </summary>
        /// <param name="officeId">Id кабинета</param>
        /// <param name="officeNumber">Номер кабинета</param>
        /// <returns></returns>
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



        /// <summary>
        /// Получить список приемов
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFreeAppointments")]
        public JsonResult GetFreeAppointments()
        {
            using (Context db = new Context())
            {
                var appointments = db.Appointments.Include(a => a.Registration).Where(r => r.Registration == null).ToList();

                return new JsonResult(appointments);
            }
        }

        /// <summary>
        /// Получить список записей на прием
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Получить информацию по приему
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавить прием в расписание
        /// </summary>
        /// <param name="scheduleId">Id расписания</param>
        /// <param name="officeId">Id кабинета</param>
        /// <param name="startReception">Время начала приема</param>
        /// <param name="endReception">Время окончания приема</param>
        /// <returns></returns>
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


        /// <summary>
        /// Удалить прием из БД
        /// </summary>
        /// <param name="appointmentId">Id приема</param>
        /// <returns></returns>
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

        /// <summary>
        /// Изменить прием
        /// </summary>
        /// <param name="appointmentId">Id приема</param>
        /// <param name="officeId">Id кабинета</param>
        /// <param name="startReception">Время начала приема</param>
        /// <param name="endReception">Время окончания приема</param>
        /// <returns></returns>
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
