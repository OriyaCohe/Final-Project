using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using BLL;
namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static DFA dfa = new DFA();
        public static String[][] sultion = new string[5][];
        public static Dictionary<string, Transition> transitions = new Dictionary<string, Transition>();
        protected void Application_Start()
        {
            Parsing p = new Parsing(dfa,sultion,transitions);
            p.fillTransitions();
            p.solution();
            p.fillDFA();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
