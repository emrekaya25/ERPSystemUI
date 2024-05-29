using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.DTO.Unit;
using ERPSystemUI.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class RequestViewModel
    {
        public List<RequestDTO> Requests { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<UnitDTO> Units { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<StatusDTO> Statuses { get; set; }
    }
}
