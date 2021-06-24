using System;

namespace DoctorApp.DTO_s
{
    public class DoctorTimeScheduleDTO
    {
        public int Id { get; set; }
        public int Doctor_Id { get; set; }
        public int Day_Id { get; set; }
        public string DayName { get; set; }
        public string ShiftType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class Result
    {
        public string Day { get; set; }
    }
}