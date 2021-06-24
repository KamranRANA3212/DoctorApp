using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorApp.DTO_s
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}