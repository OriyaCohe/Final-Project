using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RequestBLL
    {
        RequestDAL requestDAl = new RequestDAL();
        public int NewRequest()
        {
            Request r = new Request()
            {
                Status = false
            };
            return new RequestDAL().newRequest(r);
        }
        public List<Request> RequestsWhoseStatusIsNegative()
        {
            return requestDAl.listRe().FindAll(x => x.Status).ToList();
        }
        public bool ConfirmationOfReferral(int id)
        {
            return requestDAl.ConfirmationOfReferral(id);
        }
    }
}
