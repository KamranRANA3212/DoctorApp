using DoctorApp.DTO_s;
using DoctorApp.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IReviews
    {
        Task<object> GetReviews(string userId);

        Task<object> PostReview(Reviews review);

        Task<IList<DoctorDTO>> GetTopRatedDoctors();
    }
}