using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int fiveStar { get; set; }
        public int FourStar { get; set; }
        public int ThreeStar { get; set; }
        public int TwoStar { get; set; }
        public int OneStar { get; set; }
        public int TotalReviews { get; set; }
        public double AvgRating { get; set; }
        public List<ReviewDetail> Reviews { get; set; }
    }

    public class ReviewDetail
    {
        public string PatientName { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}