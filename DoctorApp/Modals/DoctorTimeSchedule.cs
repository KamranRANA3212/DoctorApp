using AutoMapper;
using DoctorApp.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorTimeSchedule
    {
        public int Id { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor ApplicationUser { get; set; }

        [ForeignKey("Day_Id")]
        public Days Day { get; set; }

        public int Doctor_Id { get; set; }
        public int Day_Id { get; set; }
        public int ShiftType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}