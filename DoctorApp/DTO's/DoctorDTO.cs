using DoctorApp.Modals;
using System;
using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public int RegistrationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string AssistantName { get; set; }
        public string AssistantNumber { get; set; }
        public string FatherName { get; set; }
        public string LicenceNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsLicenceVerified { get; set; }
        public string HospitalLocation { get; set; }
        public string PostalCode { get; set; }
        public decimal CheckUpFee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Category_Id { get; set; }
        public int Speciality_Id { get; set; }
        public string User_Id { get; set; }
        public string Certificates { get; set; }
        public string Category { get; set; }
        public string[] Specialities { get; set; }
        public Qualification[] Qualifications { get; set; }
        public Experience[] Experience { get; set; }
        public string Image { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int[] CertificationsId { get; set; }
        public string[] Address { get; set; }
        public bool IsOnline { get; set; }
        public int Status { get; set; }
        public double Ratings { get; set; }

        public List<DoctorCertificates> DoctorCertificates { get; set; }
        public List<DoctorAddress> DoctorAddress { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}