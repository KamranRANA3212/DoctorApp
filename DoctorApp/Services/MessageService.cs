using DoctorApp.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DoctorApp.Services
{
    public class MessageService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var accountsid = ConfigurationManager.AppSettings["SMSAccountIdentification"];
            var authtoken = ConfigurationManager.AppSettings["SMSAccountPassword"];
            var fromnumber = ConfigurationManager.AppSettings["SMSAccountFrom"];
            TwilioClient.Init(accountsid, authtoken);
            MessageResource result = MessageResource.Create(
                new PhoneNumber(message.Destination),
                from: new PhoneNumber(fromnumber),
                body: message.Body
                );
            Trace.TraceInformation(result.Status.ToString());
            return Task.FromResult(0);
        }
    }

}
