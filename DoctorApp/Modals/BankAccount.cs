using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string User_Id { get; set; }
        public long ExpiryYear { get; set; }
        public long ExpiryMonth { get; set; }

        public string CVV { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Stripe_Token { get; set; }
        public string Stripe_Customer_Token { get; set; }

        [ForeignKey("User_Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}