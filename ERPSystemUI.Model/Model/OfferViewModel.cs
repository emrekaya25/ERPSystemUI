using ERPSystemUI.Model.DTO.Offer;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class OfferViewModel
    {
        public List<OfferDTO> Offers { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<StatusDTO> Statuses { get; set; }
        public List<RequestDTO> Requests { get; set; }
    }
}
