using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class SignUp
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public decimal Fee { get; set; }
        public string Description { get; set; }
        public double lat { get; set; }
        public double lang { get; set; }
        public string Location { get; set; }

        public string AssistantName { get; set; }
        public string AssistantNumber { get; set; }
        public string Phone { get; set; }
        public IFormFile image { get; set; }
        public int[] SpecialityIds { get; set; }
        public Qualification[] Qualification { get; set; }
        public Experience[] Experience { get; set; }
        public string LicenceNumber { get; set; }
        
        
       
    }
   
    

    public class Qualification
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Degree { get; set; }
        public string Institute { get; set; }
    }

    public class Experience
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Designation { get; set; }
        public string Hospital { get; set; }
    }

    public class SignIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ChnagePassword
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ViewModelSignIn
    {
       public string ImageName { get; set; }
        public IFormFile file { get; set; }
    }
}