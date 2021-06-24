using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorSpeciality
    {
        public int Id { get; set; }
        public int doctorId { get; set; }
        public int SpecialtiesId { get; set; }
        public Specialties Specialties { get; set; }
        public Doctor Doctor { get; set; }
    }
}