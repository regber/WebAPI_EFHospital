using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_EFHospital.Model;

namespace WebAPI_EFHospital
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (Context db = new Context())
            {
                if(db.People.Count() == 0)
                {
                    InitDBFill(db);
                }
            }
        }

        private void InitDBFill(Context db)
        {
            Position therapist = new Position { Name="��������", Salary=45000 };
            Position ent = new Position { Name="���", Salary=40000 };
            Position cardiologist = new Position { Name = "���������", Salary = 50000 };
            Position endocrinologist = new Position { Name = "������������", Salary = 48000 };
            Position traumatologist = new Position { Name = "�����������", Salary = 46000 };
            Position surgeon = new Position { Name = "������", Salary = 55000 };


            Office therapist1Office = new Office { Number = 1 };
            Office therapist2Office = new Office { Number = 2 };
            Office entOffice = new Office { Number = 101 };
            Office cardiologistOffice = new Office { Number = 201 };
            Office endocrinologistOffice = new Office { Number =301 };
            Office traumatologistOffice = new Office { Number = 401 };
            Office surgeonOffice = new Office { Number = 501 };


            var beginnWorkDayTime = DateTime.Parse(DateTime.Now.ToLongDateString() + "08:00:00");

            EmplSchedule therapist1Schedule = new EmplSchedule {Appointments = new Appointment[] 
            { 
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(300), EndReception=beginnWorkDayTime.AddMinutes(330)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(335), EndReception=beginnWorkDayTime.AddMinutes(365)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(370), EndReception=beginnWorkDayTime.AddMinutes(400)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(405), EndReception=beginnWorkDayTime.AddMinutes(435)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(440), EndReception=beginnWorkDayTime.AddMinutes(470)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(475), EndReception=beginnWorkDayTime.AddMinutes(505)},
                new Appointment{ Office=therapist1Office,StartReception=beginnWorkDayTime.AddMinutes(510), EndReception=beginnWorkDayTime.AddMinutes(540)}
            }};
            EmplSchedule therapist2Schedule = new EmplSchedule
            {
                Appointments = new Appointment[]
                {
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(300), EndReception=beginnWorkDayTime.AddMinutes(330)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(335), EndReception=beginnWorkDayTime.AddMinutes(365)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(370), EndReception=beginnWorkDayTime.AddMinutes(400)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(405), EndReception=beginnWorkDayTime.AddMinutes(435)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(440), EndReception=beginnWorkDayTime.AddMinutes(470)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(475), EndReception=beginnWorkDayTime.AddMinutes(505)},
                new Appointment{ Office=therapist2Office,StartReception=beginnWorkDayTime.AddMinutes(510), EndReception=beginnWorkDayTime.AddMinutes(540)}
                }
            };
            EmplSchedule entSchedule = new EmplSchedule
            {
                Appointments = new Appointment[]
                {
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=entOffice,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
                }
            };
            EmplSchedule cardiologistSchedule = new EmplSchedule
            {
                Appointments = new Appointment[]
    {
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=cardiologistOffice,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
    }
            };
            EmplSchedule endocrinologistSchedule = new EmplSchedule
            {
                Appointments = new Appointment[]
    {
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=endocrinologistOffice,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
    }
            };
            EmplSchedule traumatologistSchedule = new EmplSchedule
            {
                Appointments = new Appointment[]
    {
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=traumatologistOffice,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
    }
            };
            EmplSchedule surgeonSchedule = new EmplSchedule
            {
                Appointments = new Appointment[]
    {
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime, EndReception=beginnWorkDayTime.AddMinutes(30)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(35), EndReception=beginnWorkDayTime.AddMinutes(65)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(70), EndReception=beginnWorkDayTime.AddMinutes(100)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(105), EndReception=beginnWorkDayTime.AddMinutes(135)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(140), EndReception=beginnWorkDayTime.AddMinutes(170)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(175), EndReception=beginnWorkDayTime.AddMinutes(205)},
                new Appointment{ Office=surgeonOffice,StartReception=beginnWorkDayTime.AddMinutes(210), EndReception=beginnWorkDayTime.AddMinutes(240)},
    }
            };


            Employee therapistEmpl1 = new Employee { Age=40, Position= therapist, EmplSchedule= therapist1Schedule, FirstName="����", LastName="�������", MiddleName="����������" };
            Employee therapistEmpl2 = new Employee { Age = 32, Position = therapist, EmplSchedule = therapist2Schedule, FirstName = "����������", LastName = "���������", MiddleName = "���������" };
            Employee entEmpl = new Employee { Age = 52, Position = ent, EmplSchedule = entSchedule, FirstName = "������", LastName = "��������", MiddleName = "�������������" };
            Employee cardiologistEmpl = new Employee { Age = 28, Position = cardiologist, EmplSchedule = cardiologistSchedule, FirstName = "������", LastName = "����������", MiddleName = "����������" };
            Employee endocrinologistEmpl = new Employee { Age = 34, Position = endocrinologist, EmplSchedule = endocrinologistSchedule, FirstName = "����", LastName = "�����", MiddleName = "����������" };
            Employee traumatologistEmpl = new Employee { Age = 25, Position = traumatologist, EmplSchedule = traumatologistSchedule, FirstName = "�����", LastName = "���������", MiddleName = "��������" };
            Employee surgeonEmpl = new Employee { Age = 38, Position = surgeon, EmplSchedule = surgeonSchedule, FirstName = "�����", LastName = "����������", MiddleName = "���������" };


            Client client1 = new Client { Age=65, LastName = "�������", FirstName ="������",  MiddleName="�����������"  };
            Client client2 = new Client { Age = 50, LastName = "������������", FirstName = "������", MiddleName = "���������" };
            Client client3 = new Client { Age = 30, LastName = "�������", FirstName = "������", MiddleName = "���������" };
            Client client4 = new Client { Age = 40, LastName = "���������", FirstName = "�����", MiddleName = "���������" };
            Client client5 = new Client { Age = 45, LastName = "���������", FirstName = "�������", MiddleName = "���������" };
            Client client6 = new Client { Age = 55, LastName = "���������", FirstName = "����", MiddleName = "����������" };
            Client client7 = new Client { Age = 67, LastName = "����������", FirstName = "����", MiddleName = "���������" };
            Client client8 = new Client { Age = 38, LastName = "�������", FirstName = "������", MiddleName = "���������" };
            Client client9 = new Client { Age = 64, LastName = "������", FirstName = "������", MiddleName = "�������������" };
            Client client10 = new Client { Age = 50, LastName = "����������", FirstName = "���������", MiddleName = "����������" };
            Client client11 = new Client { Age = 67, LastName = "��������", FirstName = "��������", MiddleName = "���������" };
            Client client12 = new Client { Age = 48, LastName = "�������", FirstName = "�����", MiddleName = "���������" };
            Client client13 = new Client { Age = 32, LastName = "��������", FirstName = "��������", MiddleName = "�����������" };
            Client client14 = new Client { Age = 37, LastName = "���������", FirstName = "��������", MiddleName = "����������" };
            Client client15 = new Client { Age = 38, LastName = "��������", FirstName = "���������", MiddleName = "���������" };
            Client client16 = new Client { Age = 60, LastName = "�������", FirstName = "�����", MiddleName = "��������" };


            

            db.Clients.AddRange(new Client[] { client1, client2, client3, client4, client5, client6, client7, client8, client9, client10, client11, client12, client13, client14, client15, client16 });
            db.Employees.AddRange(new Employee[] {therapistEmpl1,therapistEmpl2, entEmpl , cardiologistEmpl, endocrinologistEmpl, traumatologistEmpl, surgeonEmpl });

            db.SaveChanges();

            Registration reg1 = new Registration { AppointmentId= therapistEmpl1.EmplSchedule.Appointments.ToList()[0].Id , ClientId= client1.Id };
            Registration reg2 = new Registration { AppointmentId = therapistEmpl1.EmplSchedule.Appointments.ToList()[1].Id, ClientId = client2.Id };
            Registration reg3 = new Registration { AppointmentId = therapistEmpl1.EmplSchedule.Appointments.ToList()[2].Id, ClientId = client3.Id };
            Registration reg4 = new Registration { AppointmentId = therapistEmpl1.EmplSchedule.Appointments.ToList()[6].Id, ClientId = client4.Id };
            Registration reg5 = new Registration { AppointmentId = therapistEmpl2.EmplSchedule.Appointments.ToList()[0].Id, ClientId = client5.Id };
            Registration reg6 = new Registration { AppointmentId = therapistEmpl2.EmplSchedule.Appointments.ToList()[1].Id, ClientId = client6.Id };
            Registration reg7 = new Registration { AppointmentId = therapistEmpl2.EmplSchedule.Appointments.ToList()[8].Id, ClientId = client7.Id };
            Registration reg8 = new Registration { AppointmentId = therapistEmpl2.EmplSchedule.Appointments.ToList()[9].Id, ClientId = client8.Id };
            Registration reg9 = new Registration { AppointmentId = entEmpl.EmplSchedule.Appointments.ToList()[0].Id, ClientId = client8.Id };
            Registration reg10 = new Registration { AppointmentId = entEmpl.EmplSchedule.Appointments.ToList()[5].Id, ClientId = client9.Id };
            Registration reg11= new Registration { AppointmentId = cardiologistEmpl.EmplSchedule.Appointments.ToList()[2].Id, ClientId = client10.Id };
            Registration reg12 = new Registration { AppointmentId = cardiologistEmpl.EmplSchedule.Appointments.ToList()[3].Id, ClientId = client11.Id };
            Registration reg13 = new Registration { AppointmentId = endocrinologistEmpl.EmplSchedule.Appointments.ToList()[1].Id, ClientId = client12.Id };
            Registration reg14 = new Registration { AppointmentId = endocrinologistEmpl.EmplSchedule.Appointments.ToList()[2].Id, ClientId = client13.Id };
            Registration reg15 = new Registration { AppointmentId = traumatologistEmpl.EmplSchedule.Appointments.ToList()[5].Id, ClientId = client14.Id };
            Registration reg16 = new Registration { AppointmentId = surgeonEmpl.EmplSchedule.Appointments.ToList()[0].Id, ClientId = client15.Id };
            Registration reg17 = new Registration { AppointmentId = surgeonEmpl.EmplSchedule.Appointments.ToList()[1].Id, ClientId = client16.Id };

            db.Registrations.AddRange(new Registration[] { reg1, reg2, reg3, reg4, reg5, reg6, reg7, reg8, reg9, reg10, reg11, reg12, reg13, reg14, reg15, reg16, reg17 });

            db.SaveChanges();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Swagger Demo API",
                    Description = "Demo API for showing Swagger",
                    Version = "v1"
                });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });

            services.AddDbContext<Context>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo API");
                options.RoutePrefix = "";
            });
        }
    }
}
