using DoctorApp.DTO_s;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoctorApp.Interfaces
{
    public interface IAccount
    {
        Task<object> Register(SignUp signUp);

        Task<LoginResponse> Login(SignIn signIn);

        ObjectResult Refresh(string token);

        Task<object> GetAdmins();

        Task<object> DeleteAdmin(string id);

        Task<object> BlockUnlockAdmin(string email);

        Task<object> ChangePassword(ChnagePassword chnagePassword);

        Task<ShortResponse> CreateCard(DTO_s.Card card);

        Task<ShortResponse> ForgotPassword(ForgotPassword forgotPassword);
    }
}