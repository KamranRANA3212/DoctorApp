using DoctorApp.DTO_s;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class StripeService
    {
        private static IConfiguration _config;

        public StripeService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// This method is for generete account and coustomer token
        /// </summary>
        /// <param name="card">
        /// Contain user account detail like bankname,number,title
        /// </param>
        /// <param name="accountToken">
        /// contain account token that has information about bank
        /// </param>
        /// <param name="customerToken">
        /// contain customer token that link with account
        /// </param>

        public static void CreateToken(DTO_s.Card card, out string accountToken, out string customerToken)
        {
            Token token = null;
            Customer customer = null;

            accountToken = "";
            customerToken = "";

            try
            {
                var newCard = new TokenCardOptions()
                {
                    Name = card.Name,
                    Number = card.CardNumber,
                    Cvc = card.CVV,
                    ExpYear = card.ExpYear,
                    ExpMonth = card.ExpMonth,
                };

                var bankAccount = new Stripe.TokenBankAccountOptions()
                {
                    AccountHolderName = card.Name,
                    AccountNumber = card.AccountNumber,
                };

                //add service  to generate token

                var options = new TokenCreateOptions()
                {
                    Card = newCard,
                    BankAccount = bankAccount
                };

                var service = new TokenService();

                token = service.Create(options);

                var customerOptions = new CustomerCreateOptions()
                {
                    Email = card.Email,
                    Source = token.Id,
                };

                var customerService = new CustomerService();

                customer = customerService.Create(customerOptions);

                accountToken = token.Id;
                customerToken = customer.Id;
            }
            catch (Stripe.StripeException e)
            {
                throw e;
            }
        }

        public static string DeductAmount(int doctorId, long totalAmount, long serviceCharges, int appointmentId, string receipentEmail)
        {
            //MetaData
            Dictionary<string, string> Metadata = new Dictionary<string, string>();

            Metadata.Add("Doctor", doctorId.ToString());
            Metadata.Add("DoctorFee", (totalAmount - serviceCharges).ToString());
            Metadata.Add("ServiceCharges", serviceCharges.ToString());
            Metadata.Add("AppointmentId", appointmentId.ToString());

            var customerOptions = new CustomerCreateOptions()
            {
                Source = _config["StripeConfig:Source"],
            };

            var options1 = new ChargeCreateOptions
            {
                Amount = totalAmount,
                Currency = "USD",
                Description = "Patient pay fee to doctor",

                //Customer = customer.Id,
                ReceiptEmail = receipentEmail,
                Metadata = Metadata
            };

            var service1 = new ChargeService();

            Charge charge = service1.Create(options1);

            return charge.Id;
        }

        public static void TranferAmountToBank(string SourceToken, string customerToken, string amount)
        {
        }
    }
}