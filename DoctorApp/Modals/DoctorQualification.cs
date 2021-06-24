using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorQualification
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Degree { get; set; }
        public string Institute { get; set; }

        public Doctor Doctor { get; set; }
    }
}