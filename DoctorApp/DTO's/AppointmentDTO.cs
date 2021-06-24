using System;

namespace DoctorApp.DTO_s
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientLocation { get; set; }
        public double PatientLat { get; set; }
        public double PatientLong { get; set; }
        public string DoctorName { get; set; }
        public string HospitalLocation { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Problem { get; set; }
        public string DoctorCertifications { get; set; }
        public string Experience { get; set; }
        public int Days { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string FormatedDate { get; set; }
        public string Time { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal ServiceCharges { get; set; }
    }

    public class TopRatedDoctors
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Certifications { get; set; }
        public double Rating { get; set; }
    }
}