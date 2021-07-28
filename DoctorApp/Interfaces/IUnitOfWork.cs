using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IUnitOfWork
    {
        IDashBoard DashBoard { get; }

        IAccount Account { get; }
        IDoctor Doctor { get; }
        ISpeciality Speciality { get; }
        IReviews Reviews { get; }
        IAppointment Appointment { get; }
        IDoctorTimeSchedule DoctorTimeSchedule { get; }
        IPatient Patient { get; }

       
        Task<bool> SaveAsync();
    }
}