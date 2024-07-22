using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CONVERTER
{
    public class RequestCONVERTER
    {
        public static Request converRequest(RequestDTO request)
        {
            return new Request()
            {
                Id = request.Id,
                Status = request.Status,
                Idvolunteer = request.Idvolunteer,
            };
        }
        public static RequestDTO converRequestDTO(Request request)
        {
            if (request == null) return null;
            return new RequestDTO()
            {
                Id = request.Id,
                Status = request.Status,
                //Idvolunteer = request.Idvolunteer,
            };
        }
        public static List<RequestDTO> converRequestDTO(List<Request> votes)
        {
            return votes.Select(x => converRequestDTO(x)).ToList();
        }
    }
}
