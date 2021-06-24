using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class Response<T> where T : class
    {
        public string Status { get; set; }
        public IList<T> DataList { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }

    public class ShortResponse
    {
        public string Status { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }

    public class User
    {
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class LoginResponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
    }
}