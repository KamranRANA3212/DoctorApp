using AutoMapper;

using DoctorApp.DTO_s;
using DoctorApp.enums;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class DoctorService : IDoctor
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private IConfiguration _config;

        public DoctorService(ApplicationDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public Response<DoctorDTO> AddDoctor(string userId, DoctorDTO doctorDTO)
        {
            if (!_context.Doctor.Any(z => z.User_Id == userId))
            {
                List<DoctorCertificates> doctorCertificates = new List<DoctorCertificates>();

                List<DoctorAddress> addresses = new List<DoctorAddress>();

                doctorDTO.Image = doctorDTO.Image;

                doctorDTO.User_Id = userId;

                doctorDTO.CreatedDate = DateTime.UtcNow.AddHours(5);

                doctorDTO.UpdatedDate = DateTime.UtcNow.AddHours(5);

                doctorDTO.IsOnline = false;

                doctorDTO.Status = (int)Status.InProcess;

                Doctor doctor = new Doctor();

                doctorDTO.DoctorCertificates = new List<DoctorCertificates>();

                doctorDTO.DoctorAddress = new List<DoctorAddress>();

                foreach (var Id in doctorDTO.CertificationsId) //add certificates
                {
                    doctorCertificates.Add(new DoctorCertificates()
                    {
                        Certifiate_Id = Id
                    });
                }

                doctorDTO.DoctorCertificates.AddRange(doctorCertificates);

                foreach (var address in doctorDTO.Address) //add multiple address
                {
                    addresses.Add(new DoctorAddress() { Address = address });
                }

                doctorDTO.DoctorAddress.AddRange(addresses);

                _mapper.Map(doctorDTO, doctor);

                _context.Doctor.Add(doctor);

                _context.SaveChanges();

                return Utilities.Response<DoctorDTO>.GenerateResponse("success", new List<DoctorDTO>(), null, new List<string>(), "Profile has been Created!");
            }

            return Utilities.Response<DoctorDTO>.GenerateResponse("fail", new List<DoctorDTO>(), null, new List<string>(), "Profile exist already!");
        }

        public async Task<ShortResponse> DeleteDoctor(string id)
        {
            _context.Doctor.Remove(await _context.Doctor.FindAsync(id));

            var status = await _context.SaveChangesAsync() >= 1;

            if (status)
            {
                return new ShortResponse()
                {
                    Status = "success",
                    Error = "Doctor has been Deleted ",
                };
            }
            else
            {
                return new ShortResponse()
                {
                    Status = "fail",
                    Error = "Server Error!Try Again ",
                };
            }
        }

        public async Task<DoctorDTO> GetDoctor(int id)
        {
            var doctor = await _context.Doctor.Include(z => z.DoctorExperience).Include(z => z.DoctorQualification).Where(z => z.Id == id).Select(z => new DoctorDTO()
            {
                Id = z.Id,
                FirstName = z.FirstName,
                LastName = z.LastName,
                FatherName = z.FatherName,
                AssistantName = z.AssistantName,
                AssistantNumber = z.AssistantNumber,
                Certificates = String.Join(',', z.DoctorCertificates.Select(z => z.Certificates.Name)),
                Address = z.DoctorAddress.Select(z => z.Address).ToArray(),
                Image = z.Image,
                IsOnline = z.IsOnline,
                LicenceNumber = z.LicenceNumber,
                Description = z.Description,
                RegistrationCode = z.RegistrationCode,
                Ratings = 4.3,
                Email = z.ApplicationUser.Email,
                Phone = z.ApplicationUser.PhoneNumber,
                CheckUpFee = z.CheckUpFee,
            }).AsNoTracking().FirstOrDefaultAsync();

            return doctor;
        }

        public async Task<List<DoctorDTO>> GetDoctors()
        {
            return await Refresh();
        }

        private async Task<List<DoctorDTO>> Refresh()
        {
            List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();

            var doctorList = await _context.Doctor.Include(z => z.DoctorCertificates).Include(z => z.DoctorExperience).Include(z => z.DoctorQualification).Select(doctor => new DoctorDTO()
            {
                Id = doctor.Id,
                RegistrationCode = doctor.RegistrationCode,
                FullName = doctor.FirstName + " " + doctor.LastName,
                Email = doctor.ApplicationUser.Email,
                Status = doctor.Status,
                FatherName = doctor.FatherName,
                AssistantName = doctor.AssistantName,
                AssistantNumber = doctor.AssistantNumber,
                Certificates = String.Join(',', doctor.DoctorCertificates.Select(z => z.Certificates.Name).ToList()),
                HospitalLocation = doctor.HospitalLocation,
                PostalCode = doctor.PostalCode,
                Description = doctor.Description,
                CheckUpFee = doctor.CheckUpFee,
                LicenceNumber = doctor.LicenceNumber,
                IsLicenceVerified = doctor.IsLicenceVerified,
                User_Id = doctor.User_Id,
                Image = doctor.Image,
                Address = doctor.DoctorAddress.Select(z => z.Address).ToArray(),
                DoctorCertificates = new List<DoctorCertificates>(),
            }).AsNoTracking().ToListAsync();

            return doctorList;
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorDTO"></param>
        /// <returns></returns>
        public async Task<ShortResponse> UpdateDoctor(string id, DoctorUpdateDTO doctorDTO)
        {
            Doctor doctor = await _context.Doctor.FirstOrDefaultAsync(z => z.User_Id == id);
            List<DoctorCertificates> doctorCertificates = new List<DoctorCertificates>();

            List<DoctorAddress> addresses = new List<DoctorAddress>();

            if (doctor == null)
            {
                return new ShortResponse() { Status = "fail", Error = "Can not proceed request" };
            }
            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            
            doctorDTO.Image = doctorDTO.Image;

            doctorDTO.UpdatedDate = DateTime.UtcNow.AddHours(5);

            doctorDTO.DoctorCertificates = new List<DoctorCertificates>();

            doctorDTO.DoctorAddress = new List<DoctorAddress>();

            foreach (var Id in doctorDTO.CertificationsId) //add certificates
            {
                doctorCertificates.Add(new DoctorCertificates()
                {
                    Certifiate_Id = Id
                });
            }

            doctorDTO.DoctorCertificates.AddRange(doctorCertificates);

            foreach (var address in doctorDTO.Address) //add multiple address
            {
                addresses.Add(new DoctorAddress() { Address = address });
            }

            doctorDTO.DoctorAddress.AddRange(addresses);

            _mapper.Map(doctorDTO, doctor);

            var result = await _context.SaveChangesAsync() >= 1;

            if (result)
            {
                return new ShortResponse() { Status = "success", Message = "Doctor has been updated" };
            }

            return new ShortResponse() { Status = "fail", Error = _config["ApplicationSettings:ErrorMessage"] };
        }

        /// <summary>
        /// Verify the doctor if licence # is valid
        /// </summary>
        /// <param name="registrationCode"></param>
        /// <returns></returns>
        public async Task<DoctorDTO> VerifyLicenceNumber(int registrationCode)
        {
            DoctorDTO doctorDTO = new DoctorDTO();

            var doctor = await _context.Doctor.FirstOrDefaultAsync(z => z.RegistrationCode == registrationCode);

            doctor.IsLicenceVerified = doctor.IsLicenceVerified ? false : true;

            await _context.SaveChangesAsync();

            _mapper.Map(doctor, doctorDTO);

            return doctorDTO;
        }

        /// <summary>
        /// Handle online/offline functionality
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShortResponse> ToggleOnline(string userId)
        {
            var user = await _context.Doctor.FirstOrDefaultAsync(z => z.User_Id == userId);


            user.IsOnline = !user.IsOnline;

            var status = await _context.SaveChangesAsync() >= 1;

            if (status)
            {
                return new ShortResponse()
                {
                    Status = "success",
                    Message = user.IsOnline.ToString()
                };
            }

            return new ShortResponse()
            {
                Status = "fail",
                Message = _config["ApplicationSettings:ErrorMessage"]
            };
        }

        /// <summary>
        /// Return All appointments w.r.t current date
        /// </summary>
        /// <param name="userId">
        ///
        /// </param>
        /// <returns></returns>
        public async Task<object> Home(string userId)
        {
            var currentDate = DateTime.UtcNow.AddHours(5);

            var transactios = await _context.Appointment
                                            .Include(z => z.Patient)
                                            .Where(z => z.Doctor.User_Id == userId && z.IsDeleted == false && z.Status == (int)Status.Approved && z.Date.Date == currentDate.Date)
                                            .Select(z => new AppointmentDTO()
                                            {
                                                Id = z.Id,
                                                PatientName = z.Patient.FirstName + " " + z.Patient.LastName,
                                                PatientId = z.Patient.Id,
                                                PatientLocation = z.Patient.Location,
                                                PatientLat = z.Patient.Lat,
                                                PatientLong = z.Patient.Long,
                                                Problem = z.Problem ?? "",
                                                Status = z.Status == 1 ? "Approved" : z.Status == 2 ? "InProcess" : "Cancel",
                                                Date = Convert.ToDateTime(z.Date.ToShortDateString()),
                                                FormatedDate = z.Date.ToString("dddd, dd MMMM yyyy"),
                                                Time = z.Time.ToString("hh:mm tt"),
                                                TotalAmountPaid = z.TotalAmount,
                                                ServiceCharges = z.ServiceCharges,
                                            }).AsNoTracking().ToListAsync();

            return Utilities.Response<AppointmentDTO>.GenerateResponse("success", transactios, null, new List<string>(), "", transactios.Count);
        }

        public async Task<object> GetMyWallet(string userId)
        {
            var transactios = await _context.Transaction.Include(z => z.Appointment).Where(z => z.Appointment.Doctor.ApplicationUser.Id == userId &&
                              z.Status == (int)Status.Approved)
                .Select(z => new Transactions()
                {
                    Id = z.Id,
                    PatientName = z.Appointment.Patient.FirstName + " " + z.Appointment.Patient.LastName,
                    Image = z.Appointment.Patient.Image,
                    PatientId = z.Appointment.Patient.Id,
                    Problem = z.Appointment.Problem ?? "",
                    Status = z.Appointment.Status == 1 ? "Approved" : z.Appointment.Status == 2 ? "InProcess" : "Cancel",
                    Date = z.Appointment.Date.ToString("dddd, dd MMMM yyyy"),
                    Time = z.Appointment.Time.ToString("hh:mm tt"),
                    TotalAmountPaid = z.Appointment.TotalAmount,
                    ServiceCharges = z.Appointment.ServiceCharges,
                    AmountAfterServiceCharges = z.Appointment.TotalAmount - z.Appointment.ServiceCharges
                }).Distinct().AsNoTracking().ToListAsync();

            var doctordetail = await _context.Doctor.Include(z => z.Appointment).FirstOrDefaultAsync(z => z.User_Id == userId);

            TransactionDTO transactionDTO = new TransactionDTO()
            {
                Doctor = new DoctorRecord()
                {
                    Name = doctordetail.FirstName + " " + doctordetail.LastName,
                    WalletBalnce = 0,
                    TotalEarnings = transactios.Sum(z => z.AmountAfterServiceCharges),
                    SatisfiedPatient = 100,
                    TotalCancelRequest = doctordetail.Appointment.Count(z => z.Status == (int)Status.Cancel && z.CancelBy == "Doctor")
                },
                MyPayments = transactios
            };

            return Utilities.Response<TransactionDTO>.GenerateResponse("success", null, transactionDTO, new List<string>(), "");
        }

        public async Task<object> GetMyBookings(string userId, string filter)
        {
            var currentDate = DateTime.UtcNow.AddHours(5);

            var appointments = await _context.Appointment.Include(z => z.Patient).Where(z => z.Doctor.User_Id == userId &&
                               filter == "past" ? z.Date.Date < currentDate.Date :
                               filter == "today" ? z.Date.Date == currentDate.Date :
                               z.Date > currentDate.Date)
                 .Select(dto => new AppointmentDTO()
                 {
                     Id = dto.Id,
                     PatientName = dto.Patient.FirstName + " " + dto.Patient.LastName,
                     PatientId = dto.Patient.Id,
                     Problem = dto.Problem,
                     PatientLat = dto.Patient.Lat,
                     PatientLong = dto.Patient.Long,
                     Status = dto.Status == 1 ? "Approved" : dto.Status == 2 ? "InProcess" : "Cancel",
                     Date = Convert.ToDateTime(dto.Date.ToShortDateString()),
                     Days = EF.Functions.DateDiffDay(DateTime.UtcNow.AddHours(5), dto.Date),
                     Time = dto.Time.ToString("hh:mm tt"),
                     TotalAmountPaid = dto.TotalAmount,
                     FormatedDate = dto.Date.ToString("dddd, dd MMMM yyyy"),
                     ServiceCharges = dto.ServiceCharges,
                 }).AsNoTracking().ToListAsync();

            return Utilities.Response<AppointmentDTO>.GenerateResponse("success", appointments, null, new List<string>(), "", appointments.Count);
        }

        public async Task<ShortResponse> UpdateLatLng(LocationDTO location, string value)
        {
            var patient = await _context.Patient.FirstOrDefaultAsync(z => z.ApplicationUser.Id == value);

            if (patient != null)
            {
                return new ShortResponse() { Status = "fail", Error = "Can not proceed request" };
            }

            patient.Lat = location.Lat;
            patient.Long = location.Lng;
            patient.Location = location.Location;

            var result = await _context.SaveChangesAsync() >= 1;

            if (result)
            {
                return new ShortResponse() { Status = "success", Message = "Location Updated Successfully!" };
            }

            return new ShortResponse() { Status = "fail", Error = _config["ApplicationSettings:ErrorMessage"] };
        }

        public async Task<ShortResponse> CancelAppointment(CancelAppointmentDTO cancelAppointment)
        {
            var appointment = await _context.Appointment.FirstOrDefaultAsync(z => z.Id == cancelAppointment.AppointmentId);

            if (appointment != null)
            {
                //Cancel Appointment
                appointment.CancelBy = "Doctor";
                appointment.Status = (int)Status.Cancel;
                appointment.Reason = cancelAppointment.Reason;

                // cancel transaction
                var transactions = await _context.Transaction.FirstOrDefaultAsync(z => z.Appointment_Id == appointment.Id);
                transactions.Status = (int)Status.Cancel;

                var result = await _context.SaveChangesAsync() >= 1;

                if (result)
                {
                    return new ShortResponse() { Status = "success", Message = "Appointment has been cancelled!" };
                }
            }
            return new ShortResponse() { Status = "fail", Error = _config["ApplicationSettings:ErrorMessage"] };
        }

        public async Task<ShortResponse> ChangeStatus(ChangeStatusDTO changeStatusDTO)
        {
            var doctor = await _context.Doctor.FirstOrDefaultAsync(z => z.Id == changeStatusDTO.DoctorId);

            doctor.Status = changeStatusDTO.Status;

            await _context.SaveChangesAsync();

            return new ShortResponse()
            {
                Status = "success",
                Message = "Status updated successfully"
            };
        }
    }
}