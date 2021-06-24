using DoctorApp.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Doctor
    {
        public int Id { get; set; }
        public int RegistrationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FatherName { get; set; }
        public string LicenceNumber { get; set; }
        public bool IsLicenceVerified { get; set; }
        public string HospitalLocation { get; set; }
        public double HospitalLat { get; set; }
        public double HospitalLong { get; set; }
        public string PostalCode { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public decimal CheckUpFee { get; set; }
        public string AssistantName { get; set; }
        public string AssistantNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string User_Id { get; set; }
        public string Image { get; set; }
        public bool IsOnline { get; set; }
        public int Status { get; set; }

        [ForeignKey("User_Id")]
        public ApplicationUser ApplicationUser { get; set; }

        public IList<Appointment> Appointment { get; set; }

        public List<DoctorSpeciality> DoctorSpeciality { get; set; }

        public List<DoctorCertificates> DoctorCertificates { get; set; }
        public List<DoctorAddress> DoctorAddress { get; set; }
        public List<DoctorQualification> DoctorQualification { get; set; }
        public List<DoctorExperience> DoctorExperience { get; set; }
    }
}