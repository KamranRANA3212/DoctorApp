using DoctorApp.DTO_s;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface ISpeciality
    {
        Task<object> AddNewSpeciality(SpecialityDTO specialityDTO);

        Task<object> GetSpecialities();

        Task<object> SearchSpeciality(string value);
    }
}