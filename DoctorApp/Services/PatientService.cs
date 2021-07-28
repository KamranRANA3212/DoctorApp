using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.enums;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class PatientService : IPatient
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public static IWebHostEnvironment _envirnment;

        public PatientService(ApplicationDbContext context, IMapper mapper,IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _envirnment = environment;
        }

        public async Task<object> AddPatientAsync(PatientDTO patientDTO)
        {
            if (patientDTO!=null)
               try {
                    {
                        string uniquefile = ImageUpload(patientDTO);
                        Patient patient = new Patient();
                        patient.Image = uniquefile;
                        patient.CreatedDate = DateTime.UtcNow.AddHours(5);
                        patient.UpdatedDate = default;
                        /*patient.User_Id = userId;*/
                        
                      //  patient.Image = uniquefile;
                       // _mapper.Map(patientDTO, patient);

                        _context.Patient.Add(patient);

                        await _context.SaveChangesAsync();

                        return Utilities.Response<PatientDTO>.GenerateResponse("succcess", new List<PatientDTO>(), null, new List<string>(), "Patient Added Successfully!");
                    }
                    
            }
                catch(Exception ex)
                {
                    throw ex;
                }
            else
            {
                return Utilities.Response<PatientDTO>.GenerateResponse("succcess", new List<PatientDTO>(), null, new List<string>(), "Profile Already Exist!");
            }
        }
           private string ImageUpload([FromForm] PatientDTO img)
        {
            string uniqueFileName = null;

            if (img.Image != null)
            {
                string uploadsFolder = Path.Combine(_envirnment.WebRootPath, "images");
                uniqueFileName =  Guid.NewGuid().ToString() + "_" + img.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<object> CancelAppointment(CancelAppointmentDTO appointmentDTO)
        {
            var appointment = await _context.Appointment.FirstOrDefaultAsync(z => z.Id == appointmentDTO.AppointmentId);

            if (appointment != null)
            {
                appointment.IsDeleted = true;
                appointment.Status = (int)Status.Cancel;
                appointment.CancelBy = "Patient";
                appointment.Reason = appointmentDTO.Reason;

                await _context.SaveChangesAsync();

                return new { Status = "success", Message = "Appointment has been cancelled!" };
            }
            return new { Status = "fail", Message = "No appointment found!" };
        }

        public async Task<object> DeletePatientAsync(string id)
        {
            _context.Patient.Remove(await _context.Patient.FirstOrDefaultAsync(z => z.ApplicationUser.Id == id));

            await _context.SaveChangesAsync();

            return Utilities.Response<PatientDTO>.GenerateResponse("succcess", new List<PatientDTO>(), null, new List<string>(), "Patient Deleted!");
        }

        public async Task<object> Home(LocationDTO location)
        {
            PateintHomeDTO home = new PateintHomeDTO()
            {
                TopRatedDoctors = await GetTopRatedDoctors(),
                DoctorsNearMe = await DoctorsNearMe(location),
                NewDoctors = await NewDoctors()
            };

            return Utilities.Response<PateintHomeDTO>.GenerateResponse("succcess", null, home, new List<string>(), "");
        }

        private async Task<IList<DoctorDTO>> NewDoctors()
        {
            try
            {
                var doctors = await (from doctor in _context.Doctor

                                     select new DoctorDTO()
                                     {
                                         Id = doctor.Id,
                                         FullName = doctor.FirstName + " " + doctor.LastName,
                                         Image = doctor.Image,

                                         IsLicenceVerified = doctor.IsLicenceVerified,
                                         Certificates = String.Join(',', doctor.DoctorCertificates.Select(z => z.Certificates.Name)),
                                         Ratings = 0,
                                     }).OrderByDescending(z => z.Id).Take(10).Distinct().AsNoTracking().ToListAsync();

                foreach (var doctor in doctors)
                {
                    doctor.Ratings = Double.IsNaN(GetDoctorRatings(doctor.Id)) == true ? 0.0 : GetDoctorRatings(doctor.Id);
                }
                return doctors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// this routine give doctor near the patient
        /// </summary>
        /// <param name="location">
        /// Lat,lng,radius=1km
        /// </param>
        /// <returns></returns>
        private async Task<List<DoctorDTO>> DoctorsNearMe(LocationDTO location)
        {
            List<DoctorDTO> doctorsNearMe = new List<DoctorDTO>();

            var doctors = await _context.Doctor.Include(z => z.DoctorCertificates)
                                                .Include(z => z.DoctorExperience)
                                                .Include(z => z.DoctorQualification)
                                                .Where(z => z.ApplicationUser.IsActive == true)
                                                .Select(doctor => new DoctorDTO()
                                                { 
                                                    Id = doctor.Id,
                                                    FullName = doctor.FirstName + " " + doctor.LastName,
                                                    IsLicenceVerified = doctor.IsLicenceVerified,
                                                    Description = doctor.Description,
                                                    Image = doctor.Image,
                                                    Certificates = String.Join(',', doctor.DoctorCertificates.Select(z => z.Certificates.Name)),
                                                    Ratings = 0,
                                                }).AsNoTracking().ToListAsync();

            foreach (var doctor in doctors)
            {
                //First two are patient lat and lng
                double distanceBetweenInKm = GetDistance(location.Lat, location.Lng, doctor.Lat, doctor.Long);

                if (distanceBetweenInKm <= location.Radius)
                {
                    doctor.Ratings = Double.IsNaN(GetDoctorRatings(doctor.Id)) == true ? 0.0 : GetDoctorRatings(doctor.Id);
                    doctorsNearMe.Add(doctor);
                }
            }

            return doctorsNearMe;
        }

        /// <summary>
        /// This routine give the distance between doctor and patient
        /// </summary>
        /// <param name="lat">
        /// patient latitude
        /// </param>
        /// <param name="lng">patient longitude</param>
        /// <param name="hospitalLat">doctor latitude</param>
        /// <param name="hospitalLong">doctor longitude</param>
        /// <returns></returns>
        private double GetDistance(double lat, double lng, double hospitalLat, double hospitalLong)
        {
            GeoCoordinate pin1 = new GeoCoordinate(lat, lng);
            GeoCoordinate pin2 = new GeoCoordinate(hospitalLat, hospitalLong);

            double distanceBetween = pin1.GetDistanceTo(pin2); //return distance in meter

            return Math.Ceiling(distanceBetween / 1000); //convert distance In Km
        }

        public async Task<object> GetPatientAsync(string userId)
        {
            PatientDTO patientDTO = new PatientDTO();

            var patient = await _context.Patient.FirstOrDefaultAsync(z => z.ApplicationUser.Id == userId);

            if (patient != null)
            {
                _mapper.Map(patient, patientDTO);

                return Utilities.Response<PatientDTO>.GenerateResponse("succcess", new List<PatientDTO>(), patientDTO, new List<string>(), "");
            }
            return Utilities.Response<PatientDTO>.GenerateResponse("succcess", new List<PatientDTO>(), null, new List<string>(), "No Record Found!");
        }

        public async Task<object> GetPatientsAsync()
        {
            var patient = await _context.Patient.Include(z => z.ApplicationUser).Select(patiet => new PatientDTO()
            {
                //Id = patiet.Id,
                FullName = patiet.FirstName + " " + patiet.LastName,
                Phone = patiet.ApplicationUser.PhoneNumber,
                Email = patiet.ApplicationUser.Email,
                IsActive = patiet.ApplicationUser.IsActive
            }).AsNoTracking().ToListAsync();

            List<PatientDTO> patientDTOs = new List<PatientDTO>();

            _mapper.Map(patient, patientDTOs);

            return Utilities.Response<PatientDTO>.GenerateResponse("succcess", patientDTOs, null, new List<string>(), "");
        }

        public async Task<object> MyAppointments(string userId, string filter)
        {
            var currentDate = DateTime.UtcNow.AddHours(5).Date;

            var data = await _context.Appointment.Where(z => z.Patient.ApplicationUser.Id == userId &&
                       filter == "past" ? z.Date.Date <= currentDate :
                       filter == "today" ? z.Date.Date == currentDate :
                       z.Date.Date > currentDate)
                 .Select(z => new AppointmentDTO()
                 {
                     Id = z.Id,
                     PatientName = z.Patient.FirstName + " " + z.Patient.LastName,
                     PatientId = z.Patient_Id,
                     DoctorId = z.Doctor_Id,
                     DoctorName = z.Doctor.FirstName + " " + z.Doctor.LastName,
                     HospitalLocation = z.Doctor.HospitalLocation,
                     DoctorCertifications = String.Join(",", z.Doctor.DoctorCertificates.Select(z => z.Certificates.Name).ToList()),
                     Experience = z.Doctor.Experience,
                     Problem = z.Problem,
                     Status = z.Status == 1 ? "Approved" : z.Status == 2 ? "InProcess" : "Cancel",
                     Date = Convert.ToDateTime(z.Date.ToShortDateString()),
                     Days = EF.Functions.DateDiffDay(DateTime.UtcNow.AddHours(5), z.Date),
                     Time = z.Time.ToString("hh:mm tt"),
                     TotalAmountPaid = z.TotalAmount,
                     FormatedDate = z.Date.ToString("dddd, dd MMMM yyyy"),
                     ServiceCharges = z.ServiceCharges,
                 }).AsNoTracking().ToListAsync();

            return Utilities.Response<AppointmentDTO>.GenerateResponse("succcess", data, null, new List<string>(), "");
        }

        public async Task<object> MyPayemts(string userId)
        {
            var payments = await _context.Appointment.Where(z => z.Status == (int)Status.Approved && z.IsDeleted == false).Select(z => new PaymentDTO()
            {
                Id = z.Id,
                Name = z.Patient.FirstName + " " + z.Patient.LastName,
                Price = z.TotalAmount,
                Date = Utilities.Utility.FormatDate(z.Date),
                Image = z.Patient.Image,
            }).AsNoTracking().ToListAsync();

            return Utilities.Response<PaymentDTO>.GenerateResponse("success", payments, null, new List<string>(), "");
        }

        public async Task<object> UpdateLatLng(LocationDTO location, string userId)
        {
            var patient = await _context.Patient.FirstOrDefaultAsync(z => z.ApplicationUser.Id == userId);

            if (patient != null)
            {
                patient.Lat = location.Lat;
                patient.Long = location.Lng;
                patient.Location = location.Location;

                await _context.SaveChangesAsync();

                return new { Status = "success", Message = "Location Updated Successfully!" };
            }
            return new { Status = "fail", Message = "No Record Found" };
        }

        public async Task<object> UpdatePatientAsync(string id, PatientUpdateDTO patient)
        {
            var patientDetail = await _context.Patient.FirstOrDefaultAsync(z => z.User_Id == id);

            if (patientDetail != null)
            {
                patientDetail.UpdatedDate = DateTime.UtcNow.AddHours(5);

                patientDetail.FirstName = patient.FirstName;
                patientDetail.LastName = patient.LastName;
                patientDetail.UpdatedDate = DateTime.UtcNow.AddHours(5);
                patientDetail.Description = patient.Description;

                if (!String.IsNullOrEmpty(patient.Image))
                {
                    patientDetail.Image = patient.Image;
                }

                await _context.SaveChangesAsync();

                return Utilities.Response<PatientUpdateDTO>.GenerateResponse("succcess", new List<PatientUpdateDTO>(), patient, new List<string>(), "Record Updated!");
            }
            return Utilities.Response<PatientUpdateDTO>.GenerateResponse("succcess", new List<PatientUpdateDTO>(), null, new List<string>(), "No Record Found!");
        }

        public async Task<object> SearchDoctor(string filter)
        {
            try
            {
                var doctors = await _context.Doctor.Include(z => z.DoctorCertificates)
                              .Where(z => z.ApplicationUser.IsActive == false && (z.HospitalLocation.Contains(filter) || z.FirstName.Contains(filter) || z.LastName.Contains(filter)))
                            .Select(doctor => new DoctorDTO()
                            {
                                Id = doctor.Id,
                                FullName = doctor.FirstName + " " + doctor.LastName,
                                Certificates = String.Join(',', doctor.DoctorCertificates.Select(z => z.Certificates.Name).ToList()),
                                HospitalLocation = doctor.HospitalLocation,
                                Description = doctor.Description,
                                CheckUpFee = doctor.CheckUpFee,
                                LicenceNumber = doctor.LicenceNumber,
                                IsLicenceVerified = doctor.IsLicenceVerified,
                                User_Id = doctor.User_Id,
                                Image = doctor.Image,
                                Lat = doctor.HospitalLat,
                                Long = doctor.HospitalLong,
                                Address = doctor.DoctorAddress.Select(z => z.Address).ToArray(),
                                DoctorCertificates = new List<DoctorCertificates>(),
                            }).AsNoTracking().ToListAsync();

                foreach (var doctor in doctors)
                {
                    doctor.Ratings = Double.IsNaN(GetDoctorRatings(doctor.Id)) == true ? 0.0 : GetDoctorRatings(doctor.Id);
                }

                return Utilities.Response<DoctorDTO>.GenerateResponse("succcess", doctors, null, new List<string>(), "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
          //  return null;
        }

        public double GetDoctorRatings(int doctorId)
        {
            var a = (from i in _context.Reviews.Where(z => z.GivenTo.Id == doctorId).AsEnumerable()
                     group i by i.Id into g
                     select new
                     {
                         FiveStar = g.Count(z => z.Level == 5),
                         FourStar = g.Count(z => z.Level == 4),
                         ThreeStar = g.Count(z => z.Level == 3),
                         TwoStar = g.Count(z => z.Level == 2),
                         OneStar = g.Count(z => z.Level == 1),
                     }).ToList();

            var FiveStar = a.Sum(z => z.FiveStar);
            var FourStar = a.Sum(z => z.FourStar);
            var ThreeStar = a.Sum(z => z.ThreeStar);
            var TwoStar = a.Sum(z => z.TwoStar);
            var OneStar = a.Sum(z => z.OneStar);

            var TotalReviews = FiveStar +
                               FourStar +
                               ThreeStar +
                               TwoStar +
                               OneStar;

            return (double)((FiveStar * 5) + (FourStar * 4) + (ThreeStar * 3) + (TwoStar * 2) + (OneStar * 1)) / TotalReviews;
        }

        public async Task<IList<DoctorDTO>> GetTopRatedDoctors()
        {
            var doctors = await (from doctor in _context.Doctor
                                 join reviews in _context.Reviews
                                 on doctor.Id equals reviews.GivenTo.Id
                                 select new DoctorDTO()
                                 {
                                     Id = doctor.Id,
                                     FullName = doctor.FirstName + " " + doctor.LastName,
                                     Image = doctor.Image,
                                     Certificates = String.Join(',', doctor.DoctorCertificates.Select(z => z.Certificates.Name)),
                                     Ratings = 0,
                                 }).Distinct().AsNoTracking().ToListAsync();

            foreach (var doctor in doctors)
            {
                doctor.Ratings = Double.IsNaN(GetDoctorRatings(doctor.Id)) == true ? 0.0 : GetDoctorRatings(doctor.Id);
            };

            return doctors;
        }

       
    }
}