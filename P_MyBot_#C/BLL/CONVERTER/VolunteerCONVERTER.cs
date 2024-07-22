using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.CONVERTER
{
    public class VolunteerCONVERTER
    {
        public static volunteers converVolunteers(VolunteerDTO volunteers)
        {
            return new volunteers()
            {
                Id = volunteers.Id,
                Name = volunteers.Name,
                phone = volunteers.phone,
                status = volunteers.status,
                Password = volunteers.Password,
                Location = volunteers.Location,
            };
        }
        public static VolunteerDTO converVolunteersDTO(volunteers volunteers)
        {
            if (volunteers == null) return null;
            return new VolunteerDTO()
            {
                Id = volunteers.Id,
                Name = volunteers.Name,
                phone = volunteers.phone,
                status = volunteers.status,
                Password = volunteers.Password,
                Location = volunteers.Location,
            };
        }
        public static List<VolunteerDTO> converVolunteersDTO(List<volunteers> votes)
        {
            return votes.Select(x => converVolunteersDTO(x)).ToList();
        }
    }
}
