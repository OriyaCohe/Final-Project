using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ParsingController : ApiController
    {
       // List<ThreadWithId> threads = new List<ThreadWithId>();

        Parsing parsing = new Parsing(WebApiApplication.dfa, WebApiApplication.sultion, WebApiApplication.transitions);
        [Route("api/Parsing/sentenceAnalysis"), HttpGet]
        public async Task<string> sentenceAnalysis(string message)
        {
           // public async Task<string> sentenceAnalysis(string message, int id)
            //Thread thread = new Thread(() => parsing.sentenceAnalysis(message));
            //threads.Add(new ThreadWithId(thread, id));
            //thread.Start();
            //thread.Join();
            //return threads.Find(t => t.Id == id).Result;
            string s = await parsing.sentenceAnalysis(message);
            return s;
        }
        [Route("api/Parsing/AnalyzingCustomerResponse"), HttpGet]
        public async Task<string> AnalyzingCustomerResponse(string message)
        {
            string s = await parsing.AnalyzingCustomerResponse(message);
            return s;
        }
        // GET: api/Parsing
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Parsing/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Parsing
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Parsing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Parsing/5
        public void Delete(int id)
        {
        }
    }
}
