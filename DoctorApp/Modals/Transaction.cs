using DoctorApp.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Transaction
    {
        public int Id { get; set; }
        public string StripeTransactionId { get; set; }
        public int Code { get; set; }
        public TransctionTypes Type { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int Appointment_Id { get; set; }

        [ForeignKey("Appointment_Id")]
        public Appointment Appointment { get; set; }
    }
}