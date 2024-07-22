using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UpDateLocationVolunteerController : ApiController
    {
        [Route("api/SearchVolunteer/UpDateLocation"), HttpGet]
        // GET: api/FindVolunteer
        public bool UpDateLocation()
        {
            SearchVolunteer SearchVolunteer = new SearchVolunteer();
           return SearchVolunteer.newLocation();
        }
        [Route("api/SearchVolunteer/OptimalVolunteerList"), HttpGet]
        //public List<string> OptimalVolunteerList(string addressClient)
        //{
        //    SearchVolunteer searchVolunteer = new SearchVolunteer();
        //    return searchVolunteer.OptimalVolunteerList(addressClient);
        //}

        //[Route("api/SearchVolunteer/sendSms"), HttpGet]
        //public void sendSms()
        //{
        //    SearchVolunteer searchVolunteer = new SearchVolunteer();
        //    searchVolunteer.sendSms();
        //}
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FindVolunteer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FindVolunteer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FindVolunteer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FindVolunteer/5
        public void Delete(int id)
        {
        }
    }
}
