using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Examine
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime NextVisitDate { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Appointment_Id")]
        public Appointment Appointment { get; set; }
    }
}