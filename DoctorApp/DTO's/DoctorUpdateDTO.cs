using DoctorApp.Modals;
using System;
using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class DoctorUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenceNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int[] CertificationsId { get; set; }
        public string[] Address { get; set; }

        public List<DoctorCertificates> DoctorCertificates { get; set; }
        public List<DoctorAddress> DoctorAddress { get; set; }
    }
}