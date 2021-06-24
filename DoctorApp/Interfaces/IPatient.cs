using DoctorApp.DTO_s;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IPatient
    {
        Task<object> AddPatientAsync(PatientDTO patient, string userId);

        Task<object> GetPatientAsync(string id);

        Task<object> GetPatientsAsync();

        Task<object> UpdatePatientAsync(string id, PatientUpdateDTO patient);

        Task<object> DeletePatientAsync(string id);

        Task<object> UpdateLatLng(LocationDTO location, string userId);

        Task<object> MyAppointments(string userId, string filter);

        Task<object> CancelAppointment(CancelAppointmentDTO appointmentDTO);

        Task<object> MyPayemts(string value);

        Task<object> Home(LocationDTO location);

        Task<object> SearchDoctor(string filter);

        Task<IList<DoctorDTO>> GetTopRatedDoctors();
    }
}