using System;

namespace DoctorApp.DTO_s
{
    public class PatientUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}