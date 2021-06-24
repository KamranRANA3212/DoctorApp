namespace DoctorApp.DTO_s
{
    public class DashBoardDTO
    {
        public int Doctors { get; set; }
        public int Patients { get; set; }
        public int Bookings { get; set; }
        public int NewRequest { get; set; }
        public int PendingRequest { get; set; }
        public int VerifiedRequest { get; set; }
        public int CancelledRequest { get; set; }
    }
}