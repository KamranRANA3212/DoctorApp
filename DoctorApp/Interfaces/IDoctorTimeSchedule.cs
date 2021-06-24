using DoctorApp.Modals;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IDoctorTimeSchedule
    {
        Task<object> NewTimeSchedule(DoctorTimeSchedule model);

        Task<object> GetTimeSchedule(string doctorId);
    }
}