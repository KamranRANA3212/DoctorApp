using DoctorApp.DTO_s;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IDashBoard
    {
        Task<Response<DashBoardDTO>> GetDashBoardData();
    }
}