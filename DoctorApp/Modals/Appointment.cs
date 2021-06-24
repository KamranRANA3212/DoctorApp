using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorApp.Modals
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Problem { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        [NotMapped]
        public string DateInString { get; set; }

        [NotMapped]
        public string TimeInString { get; set; }

        public int Status { get; set; }
        public string CancelBy { get; set; }
        public string Reason { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public decimal ServiceCharges { get; set; }
        public bool IsDeleted { get; set; }
        public int Doctor_Id { get; set; }
        public int Patient_Id { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient_Id")]
        public Patient Patient { get; set; }
        public Wallet Wallet { get; set; }
        public Examine Examine { get; set; }
        public List<Notifications> Notifications { get; set; }
    }
}