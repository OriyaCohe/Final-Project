using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class VolunteerDAL
    {
        DBMyBotEntities DB = new DBMyBotEntities();
        public  List<volunteers> GetVolunteers()
        {//מחזיר רשימה של כל המתנדבים
            return DB.volunteers.ToList();
        }
        public volunteers GetVolunteersById(int id)
        {//מחזיר מתנדב לפי ID
            return DB.volunteers.Find(id);
        }
        public void UpDateLocation(string Phone, string newLocation)
        {
            //פןנקציה שמעדכנת מיקום 
            DB.volunteers.FirstOrDefault(a=>a.phone.Trim()==Phone).Location=newLocation;
            DB.SaveChanges();
        }
        public bool UpStatusOk(int id)
        {
            try
            {
                DB.volunteers.Find(id).status = true;
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool UpStatusNo(int id)
        {
            try
            {
                DB.volunteers.Find(id).status = false;
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        public volunteers login(int id, string password)
        {
            return GetVolunteers().Find(x => x.Id == id && x.Password.Trim() == password);
        }
        public void AddVolunteer(volunteers volunteer)
        {

            DB.volunteers.Add(volunteer);
            DB.SaveChanges();
        }

    }
}