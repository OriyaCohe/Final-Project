using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL;
using DTO;
using RestSharp;
using System.Net.Http;
using System.Runtime.InteropServices;
using GoogleMaps.Net;
using Newtonsoft.Json.Linq;
namespace BLL
{
    //public class Voluntreer
    //{
    //    string tel;
    //    int distance;
        
    //}
    public class SearchVolunteer
    {
        static VolunteerDAL VolunteerDAL = new VolunteerDAL();
        VolunteerDTO VolunteerDTO = new VolunteerDTO();
        //רישמת מתנדבים
        public static List<volunteers> GetVolunteers()
        {
            return VolunteerDAL.GetVolunteers();
        }
        //שליפת מס פל  שהסטוטס חיובי
        public List<string> GetRegisteringCellPhonesOfTheVolunteers()
        {
            return GetVolunteers().FindAll(x => x.status).Select(x => x.phone).ToList();
        }
        // נקראת מהאנגולר כל חצי שעה מעדכנת מיקומים בדטה בייס 
        public bool newLocation()
        {
            try
            {
                List<string> PhoneVolunteers = GetRegisteringCellPhonesOfTheVolunteers();
                foreach (string item in PhoneVolunteers)
                {
                    var v = GetCustomerLocation(item);
                    VolunteerDAL.UpDateLocation(item, v.ToString());
                    //VolunteerDAL.Location = GetCustomerLocation(item);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        // פונקציה לשליפת מיקום על פי מספר טלפון נקראת מהאנגולר כל חצי שעה 
        public async Task<string> GetCustomerLocation(string phoneNumber)
        {
            try
            {
                // יצירת מופע של HttpClient
                using (var client = new HttpClient())
                {
                    // מפתח API של גוגל מפות
                    string apiKey = "AIzaSyBNVjEXhyDOUvcCECJFY5x_OGKt38dxVBk";

                    // כתובת ה-URL לשירות ה-Geocoding של גוגל מפות
                    string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={phoneNumber}&key={apiKey}";

                    // ביצוע בקשת GET וקבלת התשובה
                    HttpResponseMessage response = await client.GetAsync(url);

                    // קריאה לתוכן של התשובה
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // החזרת התוצאה
                    return responseBody;
                }
            }
            catch (Exception ex)
            {
                // הדפסת הודעת שגיאה אם משהו השתבש
                return "error";
            }
        }
        //פונקציה שמוצאת מרחקים בין 2 כתובות
        public int GetDistance(string origin, string dest)
        {
            try
            {
                
                string key = "AIzaSyBNVjEXhyDOUvcCECJFY5x_OGKt38dxVBk";
                string uri = $"https://maps.googleapis.com/maps/api/distancematrix/xml?key={key}&origins={origin}&destinations={"אור לציון 63, נתיבות, ישראל"}&mode=driving";
                var client = new RestClient(uri);
                //client.Timeout = -1;
                var request = new RestRequest(Method.Get.ToString());
                RestResponse response = client.Execute(request);
                XElement element = XElement.Parse(response.Content);
                if (element != null)
                    return int.Parse(element.Descendants("duration").FirstOrDefault()?.Element("value").Value) / 60;
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        //public void monimSort(IDictionary<string, int> dv)
        //{
        //    int[] arr = new int[15];
        //    Voluntreer[] arrVol=new Voluntreer[dv.Count];
        //    int k = 0;
        //    foreach (var item in dv)
        //    {
        //        arr[item.Value]++;
        //    }
        //    for (int i=0;i<arr.Length; i++)
        //    {
        //        arrVol[k] = arr[i]>0?new Voluntreer(dv)
        //    }
        //} 
        //החזרת רשימת מספרי פל של המתנדבים ממויינת לפי מרחק מהלקוח   
        public string OptimalVolunteerList(string addressClient, string problem, int idR)
        {
            RequestDAL requestDAL = new RequestDAL();
            SMS sms = new SMS();
            List<volunteers> lv = VolunteerDAL.GetVolunteers();
            List<VolunteerDTO> optimaly = new List<VolunteerDTO>();
            List<int> Distance = new List<int>();
            IDictionary<string, int> dv = new Dictionary<string, int>();

            foreach (var item in lv)
            {//   שהמפתח יהיה מס הפלאפון של המתנדב והערך יהיה המרחק בינו ללקוחDictionary יוצרת  
                int dis = GetDistance(addressClient, item.Location);
                if(dis<=15)
                    dv.Add(item.phone,dis);
            }
            ///למיין לפי מרחק מהלקוח
            List<string> list = dv.OrderBy(x => x.Value).Select(x => x.Key).ToList();
            int i;
            for (i = 0; i < list.Count && !requestDAL.GetStatusOfRequest(idR); i++)
            {
                //עובר על הרשימה הממוינת ושולחת הודעות לפי הסדר הלולאה תעצר כאשר אחד המתנדבים אישר את הפניה
                //(GetStatusOfRequest(idR) חוזר מהפונקציה הזו)
                //או שנגמה הרשימה 
             //  sms.sms(list[i], " לקוח מתמודד עם " + problem + " בכתובת:" + addressClient + " קוד פניה:" + idR);
                //המתנה דקה /לתשובה
               // System.Threading.Thread.Sleep(60000);
               i = i + 1;
            }
            while (!requestDAL.GetStatusOfRequest(idR))
            {
                //אם הפניה לא נלקחה לחכות עד שתילקח  
            }
            //for (int j = i; j > 0; j++)
                //שליחת הודעה לכל המתנדבים 
               // sms.sms(list[j], "הפניה נלקחה");
            //החזרת הודעה ללקוח
            return "מתנדב בדרך אליך";
        }
    }
}
