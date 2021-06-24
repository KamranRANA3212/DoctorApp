using DoctorApp.DTO_s;
using DoctorApp.Modals;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Utilities
{
    public class Utility
    {
        //public static string ExecuteSP(string spName, IList<string> parameter, IList<object> values, ApplicationDbContext context)
        //{
        //    DataTable dt = new DataTable();

        //    var connection = (SqlConnection)context.Database.GetDbConnection();

        //    var command = connection.CreateCommand();
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = spName;

        //    if (parameter != null)
        //    {
        //        for (int i = 0; i < parameter.Count(); i++)
        //        {
        //            command.Parameters.AddWithValue(parameter[i], values[i]);
        //        }
        //    }

        //    connection.Open();

        //    using (SqlDataReader dr = command.ExecuteReader())
        //    {
        //        dt.Load(dr);
        //    }

        //    connection.Close();

        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryDTO>(dt);
        //}

        public static string FormatDate(DateTime date)
        {
            return date.ToString("D");
        }
    }
}