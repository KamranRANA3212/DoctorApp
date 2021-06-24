using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Reviews
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Patient_Id { get; set; }
        public int Doctor_Id { get; set; }
        public int Appointment_Id { get; set; }

        [ForeignKey("Patient_Id")]
        public Patient GivenBy { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor GivenTo { get; set; }

        [ForeignKey("Appointment_Id")]
        public Appointment Appointment { get; set; }
    }
}