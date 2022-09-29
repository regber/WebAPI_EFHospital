using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                var employee = db.Employees.Include(e=>e.Position).ToList();

                return new JsonResult(employee);
            }
        }

        [HttpGet("GetPositiosn")]
        public JsonResult GetPositions()
        {
            using (Context db = new Context())
            {
                var positions = db.Positions.ToList();

                return new JsonResult(positions);
            }
        }

        [HttpGet("GetSchedule")]
        public JsonResult GetSchedule(int scheduleId)
        {
            using (Context db = new Context())
            {
                var schedule = db.EmplSchedules.Include(s=>s.Appointments).ToList();

                return new JsonResult(schedule);
            }
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
