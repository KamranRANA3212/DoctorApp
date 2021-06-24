using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Utilities
{
    public static class Response<T> where T:class
    {
        public static DoctorApp.DTO_s.Response<T> GenerateResponse(string status, IList<T> dataList, T data, List<string> errors, string message, int count = 0)
        {
            DoctorApp.DTO_s.Response<T> response = new DoctorApp.DTO_s.Response<T>()
            {
                Status = status,
                DataList = dataList,
                Data = data,
                Errors = errors,
                Message = message,
                Count = count,
            };

            return response;
        }
    }
}