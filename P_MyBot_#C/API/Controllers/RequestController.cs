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

    public class RequestController : ApiController
    {
        // GET: api/Request
        [Route("api/Request/NewRequest"),HttpGet]
        public int NewRequest()
        {
           return new RequestBLL().NewRequest();
        }
            // GET: api/Request/5
            public string Get(int id)
        {
            return "value";
        }

        // POST: api/Request
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Request/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Request/5
        public void Delete(int id)
        {
        }
    }
}
