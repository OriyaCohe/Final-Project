using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace BLL
{
    public class Transition
    {
        public int nextState;
        public int indexS;
        public int indexJ;
        public Transition()
        {
            nextState = -1;
            indexS = 0;
            indexJ = 0;
        }
    }
    public class State
    {
        public Dictionary<string, Transition> transitions =new Dictionary<string, Transition>();
        public int indexS { get; set; }
        public int indexJ { get; set; }
        public State(Dictionary<string, Transition> transitionsF)
        {
            this.transitions = CopyTransitions(transitionsF);
        }
        private Dictionary<string, Transition> CopyTransitions(Dictionary<string, Transition> transitionsF)
        {
            Dictionary<string, Transition> copiedTransitions = new Dictionary<string, Transition>();

            foreach (var item in transitionsF)
            {
                copiedTransitions.Add(item.Key, item.Value);
            }
            return copiedTransitions;
        }
    }
}