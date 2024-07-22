using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Google.Protobuf.WellKnownTypes;
using Twilio;
using Twilio.Rest.Conversations.V1.Conversation;
using Google.Type;
using Twilio.TwiML.Messaging;
//using Twilio.Rest.Api.v2010.accout;

namespace BLL
{
    public class volunteersBLL
    {
        VolunteerDAL VolunteerDAL = new VolunteerDAL();
        //מחזיר רשימה
        public List<volunteers> GetVolunteers()
        {
            return VolunteerDAL.GetVolunteers();
        }
        //התחברות
        public VolunteerDTO login(int id, string password)
        {
            volunteers volunteer = VolunteerDAL.GetVolunteersById(id);
            return CONVERTER.VolunteerCONVERTER.converVolunteersDTO(VolunteerDAL.login(id, password));
        }
        //הרשמה
        public bool AddVolunteers(VolunteerDTO newVolunteers)
        {
            if (!DoesItExist(newVolunteers))
            {

                VolunteerDAL.AddVolunteer(CONVERTER.VolunteerCONVERTER.converVolunteers(newVolunteers));
                return true;
            }
            return false;
        }
        //פונקציה שבודקת האם המתנדב שרוצה להירשם קיים  
        public bool DoesItExist(VolunteerDTO newVolunteers)
        {
            int id = newVolunteers.Id;
            return VolunteerDAL.GetVolunteersById(id) == null ? false : true;
        }
        //עדכון סטטוס לשלילי
        public bool UpStatusNo(int id)
        {
            return VolunteerDAL.UpStatusNo(id);
        }
        //עדכון סטטוס לחיובי
        public bool UpStatusOk(int id)
        {
            return VolunteerDAL.UpStatusOk(id);
        }
    }
}
