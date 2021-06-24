using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Notifications
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Appointment_Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Ignore]
        [ForeignKey("Appointment_Id")]
        public Appointment Appointment { get; set; }
    }
}