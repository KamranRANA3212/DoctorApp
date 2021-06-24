using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.enums;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class ReviewService : IReviews
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ReviewService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetReviews(string userId)
        {
            var FiveStar = _context.Reviews.Where(five => five.Level == 5 && five.GivenTo.User_Id == userId).Count();
            var FourStar = _context.Reviews.Where(four => four.Level == 4 && four.GivenTo.User_Id == userId).Count();
            var ThreeStar = _context.Reviews.Where(three => three.Level == 3 && three.GivenTo.User_Id == userId).Count();
            var TwoStar = _context.Reviews.Where(two => two.Level == 2 && two.GivenTo.User_Id == userId).Count();
            var OneStar = _context.Reviews.Where(one => one.Level == 1 && one.GivenTo.User_Id == userId).Count();

            var reviews = await _context.Reviews.Where(w => w.GivenTo.User_Id == userId).Select(z => new ReviewDTO()
            {
                Reviews = _context.Reviews.Where(z => z.GivenTo.User_Id == userId).Select(z => new ReviewDetail()
                {
                    PatientName = z.GivenBy.FirstName+" "+z.GivenBy.LastName,
                    Level = z.Level,
                    Description = z.Description,
                    Date = z.CreatedDate.ToString("dddd, dd MMMM yyyy"),
                }).AsNoTracking().ToList(),

                Id = z.Id,
                fiveStar = FiveStar,
                FourStar = FourStar,
                ThreeStar = ThreeStar,
                TwoStar = TwoStar,
                OneStar = OneStar,
                TotalReviews = FiveStar + FourStar + ThreeStar + TwoStar + OneStar,
            }).AsNoTracking().FirstOrDefaultAsync();

            if (reviews != null)
            {
                reviews.AvgRating = (double)((reviews.fiveStar * 5) + (reviews.FourStar * 4) + (reviews.ThreeStar * 3) + (reviews.TwoStar * 2) + (reviews.OneStar * 1)) / reviews.TotalReviews;

                return Utilities.Response<ReviewDTO>.GenerateResponse("success", new List<ReviewDTO>(), reviews, new List<string>(), "");
            }

            return Utilities.Response<ReviewDTO>.GenerateResponse("success", new List<ReviewDTO>(), reviews, new List<string>(), "No Record Found");
        }

        public double GetDoctorRatings(int doctorId)
        {
            //var FiveStar = _context.Reviews.Where(five => five.Level == 5 && five.GivenTo.Id == doctorId).Count();
            //var FourStar = _context.Reviews.Where(four => four.Level == 4 && four.GivenTo.Id == doctorId).Count();
            //var ThreeStar = _context.Reviews.Where(three => three.Level == 3 && three.GivenTo.Id == doctorId).Count();
            //var TwoStar = _context.Reviews.Where(two => two.Level == 2 && two.GivenTo.Id == doctorId).Count();
            //var OneStar = _context.Reviews.Where(one => one.Level == 1 && one.GivenTo.Id == doctorId).Count();

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

            var TotalReviews = FiveStar + FourStar + ThreeStar + TwoStar + OneStar;

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
                await Task.Run(() =>//Run Operation on seperate thread
              {
                  doctor.Ratings = Double.IsNaN(GetDoctorRatings(doctor.Id)) == true ? 0.0 : GetDoctorRatings(doctor.Id);
              });
            }

            //Appointments
            //var currentDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(5).ToShortDateString());

            //var appointments = await _context.Appointment.Where(z => z.Doctor.User_Id == userId).Select(z => new AppointmentDTO()
            //{
            //    Id = z.Id,
            //    PatientName = z.Doctor.FirstName ?? "",
            //    PatientId = z.Patient.Id,
            //    Problem = z.Problem ?? "",
            //    Status = z.Status == 1 ? "Approved" : z.Status == 2 ? "InProcess" : "Cancel",
            //    Date = Convert.ToDateTime(z.Date.ToShortDateString()),
            //    FormatedDate = z.Date.ToString("dddd, dd MMMM yyyy"),
            //    Time = z.Time.ToString("hh:mm tt"),
            //    TotalAmountPaid = z.TotalAmount,
            //    ServiceCharges = z.ServiceCharges,
            //}).AsNoTracking().ToListAsync();

            return doctors;
        }

        public async Task<object> PostReview(Reviews review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Utilities.Response<ReviewDTO>.GenerateResponse("success", new List<ReviewDTO>(), null, new List<string>(), "Your comments has been send to doctor!");
        }
    }
}