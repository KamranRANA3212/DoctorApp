using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<DoctorQualification> DoctorQualification { get; set; }
        public DbSet<DoctorExperience> DoctorExperience { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Examine> Examine { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<DoctorAddress> DoctorAddress { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<DoctorTimeSchedule> DoctorTimeSchedule { get; set; }
        public DbSet<DoctorCertificates> DoctorCertificates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}