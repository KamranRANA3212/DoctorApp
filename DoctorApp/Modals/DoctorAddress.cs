using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class DoctorAddress
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }
    }
}