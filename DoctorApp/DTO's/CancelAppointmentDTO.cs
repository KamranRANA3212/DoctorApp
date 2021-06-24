namespace DoctorApp.DTO_s
{
    public class CancelAppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string CancelBy { get; set; }
        public string Reason { get; set; }
    }
}