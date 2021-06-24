using System.Collections.Generic;

namespace DoctorApp.DTO_s
{
    public class TransactionDTO
    {
        public DoctorRecord Doctor { get; set; }
        public List<Transactions> MyPayments { get; set; }
    }

    public class Transactions
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Image { get; set; }
        public string HospitalLocation { get; set; }
        public int PatientId { get; set; }
        public string Problem { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal AmountAfterServiceCharges { get; set; }
    }

    public class DoctorRecord
    {
        public string Name { get; set; }
        public int SatisfiedPatient { get; set; }
        public int TotalCancelRequest { get; set; }
        public decimal WalletBalnce { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}