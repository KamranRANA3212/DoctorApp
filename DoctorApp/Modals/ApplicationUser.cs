using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public BankAccount BankAccount { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}