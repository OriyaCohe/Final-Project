using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class volunteersController : ApiController
    {
        volunteersBLL volunteersBLL = new volunteersBLL();
        RequestBLL requestBLL = new RequestBLL();

        [Route("api/Volunteer/OptimalVolunteerList"), HttpGet]

        public string OptimalVolunteerList(string addressClient, string problem, int idR)
        {
          return new SearchVolunteer().OptimalVolunteerList(addressClient, problem, idR);
        }
        [Route("api/volunteers/UpStatusOk"), HttpGet]

        public bool UpStatusOk(int id)
        {
            return volunteersBLL.UpStatusOk(id);
        }
        [Route("api/volunteers/UpStatusNo"), HttpGet]

        public bool UpStatusNo(int id)
        {
            return volunteersBLL.UpStatusNo(id);
        }
        [Route("api/volunteers/ConfirmationOfReferral"), HttpGet]
        public bool ConfirmationOfReferral(int id)
        {
            return requestBLL.ConfirmationOfReferral(id);
        }
        [Route("api/volunteers/login"), HttpGet]
        // GET: api/Nlp
        public VolunteerDTO login(int id, string password)
        {
            VolunteerDTO volunteerDTO = volunteersBLL.login(id, password);
            if (volunteerDTO == null)
                return new VolunteerDTO();
            return volunteerDTO;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }
        [Route("api/volunteers/AddVolunteers"), HttpPost]
        // GET: api/Login
        public bool AddVolunteers(VolunteerDTO volunteer)
        {
            return volunteersBLL.AddVolunteers(volunteer);
        }
        // POST: api/Login
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
