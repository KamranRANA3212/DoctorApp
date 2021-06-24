using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class PateintHomeDTO
    {
        public IList<DoctorDTO> TopRatedDoctors { get; set; }
        public IList<DoctorDTO> DoctorsNearMe { get; set; }
        public IList<DoctorDTO> NewDoctors { get; set; }
    }
}