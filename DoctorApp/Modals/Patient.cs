using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location { get; set; }
        public string User_Id { get; set; }

        [ForeignKey("User_Id")]
        public ApplicationUser ApplicationUser { get; set; }

        public IList<Appointment> Appointment { get; set; }
    }
}