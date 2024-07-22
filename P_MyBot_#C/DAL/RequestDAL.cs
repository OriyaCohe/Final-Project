using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RequestDAL
    {
        public static bool GlobalString=false;
        DBMyBotEntities DB=new DBMyBotEntities();
        public bool GetStatusOfRequest(int id)
        {
            DBMyBotEntities DB = new DBMyBotEntities();

            bool status = DB.Request.Find(id).Status;
            return DB.Request.Find(id).Status;
        }
        public int newRequest(Request r)
        {
            try
            {
                DB.Request.Add(r);
                DB.SaveChanges();
                List<Request>rl=DB.Request.ToList();
                return rl.Last().Id;

            }
            catch (Exception e)
            {

                return 0;
            }
        }
        public List<Request> listRe() { 
            return DB.Request.ToList();
        }
        public bool ConfirmationOfReferral(int id)
        {
            try
            {
                DB.Request.Find(id).Status = true;
                DB.SaveChanges();
                GlobalString = true;
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
