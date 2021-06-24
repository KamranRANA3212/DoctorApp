using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorCertificates
    {
        public int Id { get; set; }
        public int Doctor_Id { get; set; }
        public int Certifiate_Id { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }

        [ForeignKey("Certifiate_Id")]
        public Certificates Certificates { get; set; }
    }
}