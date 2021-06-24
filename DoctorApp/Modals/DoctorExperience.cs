using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorExperience
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Designation { get; set; }
        public string Hospital { get; set; }

        public Doctor Doctor { get; set; }
    }
}