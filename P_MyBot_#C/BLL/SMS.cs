using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace BLL
{
    public class SMS
    {
        public void sms(string userPhone, string s)
        {
            // ה-Account SID וה-Auth Token מחשבון ה-Twilio שלך
            const string accountSid = "AC9a80d44edcaadbbe5867780ff75b469d";
            const string authToken = "b4ec901cf71e3765460712b29f1db643";
            // אתחול לקוח Twilio
            TwilioClient.Init(accountSid, authToken);
            //פרטי ההודעה
            //try
            //{
            //    string cellphoneumber = "+972" + userPhone.Substring(1);
            //    var message = MessageResource.Create(
            //        body: s,
            //        from: new PhoneNumber("+12232674681"),  // מספר הטלפון של Twilio
            //        to: new PhoneNumber(cellphoneumber)    // מספר הטלפון של הנמען
            //    );
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }
    }
}

