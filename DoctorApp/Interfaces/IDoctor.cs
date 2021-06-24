using DoctorApp.DTO_s;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IDoctor
    {
        Task<List<DoctorDTO>> GetDoctors();

        Task<DoctorDTO> GetDoctor(int id);

        Response<DoctorDTO> AddDoctor(string userId, DoctorDTO doctorDTO);

        Task<ShortResponse> UpdateDoctor(string id, DoctorUpdateDTO doctorDTO);

        Task<ShortResponse> DeleteDoctor(string id);

        Task<DoctorDTO> VerifyLicenceNumber(int registrationCode);

        Task<ShortResponse> ToggleOnline(string UserId);

        Task<object> Home(string userId);

        Task<object> GetMyWallet(string userId);

        Task<object> GetMyBookings(string userId, string filter);

        Task<ShortResponse> UpdateLatLng(LocationDTO location, string value);

        Task<ShortResponse> CancelAppointment(CancelAppointmentDTO cancelAppointment);

        Task<ShortResponse> ChangeStatus(ChangeStatusDTO changeStatusDTO);
    }
}