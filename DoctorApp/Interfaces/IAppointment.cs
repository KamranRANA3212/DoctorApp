using DoctorApp.DTO_s;
using DoctorApp.Modals;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IAppointment
    {
        //Task<object> GetAppointment(string date, string userId, int cout);

        Task<object> GetAppintments();

        Task<ShortResponse> PostAppointment(Appointment dto);

        Task<ShortResponse> DeleteAppointment(int appointmentId);
    }
}