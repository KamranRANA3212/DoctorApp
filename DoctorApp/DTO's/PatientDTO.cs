using System;

namespace DoctorApp.DTO_s
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string User_Id { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Location { get; set; }
    }
}