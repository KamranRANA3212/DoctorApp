using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.enums;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class AppointmnetService : IAppointment
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private IConfiguration _config;

        private IWebHostEnvironment _env;

        public AppointmnetService(ApplicationDbContext context, IMapper mapper, IConfiguration config, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _env = env;
        }

        public async Task<ShortResponse> DeleteAppointment(int appointmentId)
        {
            var appointment = await _context.Appointment.FirstOrDefaultAsync(z => z.Id == appointmentId);

            if (appointment != null)
            {
                appointment.IsDeleted = true;

                //await _context.SaveChangesAsync();

                return new ShortResponse() { Status = "success", Error = "Appointment has been deleted!" };
            }
            return new ShortResponse() { Status = "fail", Error = "No Record Found!" };
        }

        public async Task<object> GetAppintments()
        {
            var reponse = await _context.Appointment.Select(dto => new AppointmentDTO()
            {
                Id = dto.Id,
                PatientName = dto.Patient.FirstName + " " + dto.Patient.LastName,
                DoctorName = dto.Doctor.FirstName + " " + dto.Doctor.LastName,
                HospitalLocation = dto.Doctor.HospitalLocation ?? "",
                PatientId = dto.Patient.Id,
                Problem = dto.Problem,
                Status = dto.Status == 1 ? "Approved" : dto.Status == 2 ? "InProcess" : "Cancel",
                Date = Convert.ToDateTime(dto.Date.ToShortDateString()),
                Days = EF.Functions.DateDiffDay(DateTime.UtcNow.AddHours(5), dto.Date),
                Time = dto.Time.ToString("hh:mm tt"),
                TotalAmountPaid = dto.TotalAmount,
                FormatedDate = dto.Date.ToString("dddd, dd MMMM yyyy"),
                ServiceCharges = dto.ServiceCharges,
            }).AsNoTracking().ToListAsync();

            return Utilities.Response<AppointmentDTO>.GenerateResponse("success", reponse, null, new List<string>(), "");
        }

        public async Task<ShortResponse> PostAppointment(Appointment model)
        {
            var doctor = await _context.Doctor.FirstOrDefaultAsync(z => z.Id == model.Doctor_Id);

            if (doctor != null)
            {
                if (!doctor.IsOnline)
                {
                    if (!_context.Appointment.Any(z => z.Doctor_Id == model.Doctor_Id && z.Patient_Id == model.Patient_Id && z.Date.Date == DateTime.UtcNow.AddHours(5).Date))
                    {
                        model.CreatedDate = DateTime.UtcNow.AddHours(5);
                        model.Status = (int)Status.InProcess;

                        model.Date = Convert.ToDateTime(model.DateInString);
                        model.Time = Convert.ToDateTime(model.TimeInString);

                        model.Notifications = new List<Notifications>();
                        model.Notifications.Add(new Notifications()
                        {
                            Message = "New Appointment",
                            CreatedDate = DateTime.UtcNow.AddHours(5),
                        });

                        _context.Appointment.Add(model);

                        var result = await _context.SaveChangesAsync() >= 1;

                        if (result)
                        {
                            // Deduction of amount  will happen here
                            //var stripeTransactionId = StripeService.DeductAmount(model.Doctor_Id,
                            //                                        Convert.ToInt64(model.TotalAmount),
                            //                                        Convert.ToInt64(model.ServiceCharges),
                            //                                        model.Id,
                            //                                        model.Patient.ApplicationUser.Email);

                            if (!String.IsNullOrEmpty("123"))
                            {
                                Transaction newTransaction = new Transaction()
                                {
                                    StripeTransactionId = "SP-14523622",
                                    Appointment_Id = model.Id,
                                    Code = new Random().Next(999, 9999),
                                    Type = TransctionTypes.BankType,
                                    CreatedDate = DateTime.UtcNow.AddDays(5),
                                    Status = (int)Status.Approved,
                                };

                                _context.Add(newTransaction);

                                var status = await _context.SaveChangesAsync() >= 1;

                                if (status)
                                {
                                    //Send Email to doctor
                                    SendEmail(model.Id);

                                    return new ShortResponse()
                                    {
                                        Status = "success",
                                        Error = "Appointment has been booked successfully!"
                                    };
                                }
                            }
                        }
                    }
                    else
                    {
                        return new ShortResponse() { Status = "success", Error = "Appointment already booked" };
                    }
                }
                else
                {
                    return new ShortResponse() { Status = "fail", Error = "Sorry doctor is not availabale" };
                }
            }

            return new ShortResponse() { Status = "fail", Error = "Doctor  not found" };
        }

        private void SendEmail(int appointmentId)
        {
            var appointment = _context.Appointment.Include(z => z.Patient).Include("Patient.ApplicationUser").FirstOrDefault(z => z.Id == appointmentId);

            var senderEmail = new MailAddress("noreply@sharingride.pk", "DoctorPatientApp");
            var receiverEmail = new MailAddress(appointment.Patient.ApplicationUser.Email, "Receiver");
            var password = "Bscsf13m046@123";
            string sub = "New appointment from DoctorPatientApp";

            string body = "";

            using (var file = new StreamReader(Path.Combine(_env.ContentRootPath, "EmailTemplate/BookingDetailForDoctor.html")))
            {
                body = file.ReadToEnd();
            };

            body = body.Replace("{patientname}", appointment.Patient.ApplicationUser.Name);
            body = body.Replace("{location}", appointment.Patient.Location);
            body = body.Replace("{date}", appointment.Date.ToString("dddd, dd MMMM yyyy"));
            body = body.Replace("{time}", appointment.Time.ToString("hh:mm tt"));
            body = body.Replace("{problem}", appointment.Problem);
            body = body.Replace("{fee}", (appointment.TotalAmount - appointment.ServiceCharges).ToString());

            var smtp = new SmtpClient
            {
                Host = "mail.sharingride.pk",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body,

                IsBodyHtml = true,
            })
            {
                try
                {
                    smtp.Send(mess);
                }
                catch (Exception ex)
                {
                    throw;
                }
            };
        }

        //public async Task<object> GetAppointment(string date, string userId, int count = 0)
        //{
        //    var stringToDate = Utilities.Utility.ConvertStringToDate(date);

        //    var appointments = await _context.Appointment.Where(z => z.Doctor.User_Id == userId && z.IsDeleted == false && z.Status == (int)Status.Approved && z.Date.Date == stringToDate).Select(z => new AppointmentDTO()
        //    {
        //        FormatedDate = z.Date.ToString("dd/MM/yyyy"),
        //        Time = z.Time.ToString("hh:mm tt"),
        //    }).AsNoTracking().ToListAsync();

        //    return Utilities.Response<AppointmentDTO>.GenerateResponse("success", appointments, null, new List<string>(), "");
        //}
    }
}