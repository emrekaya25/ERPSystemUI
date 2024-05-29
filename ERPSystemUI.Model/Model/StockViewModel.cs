using ERPSystemUI.Model.DTO.Department;
using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Stock;
using ERPSystemUI.Model.DTO.Unit;
using ERPSystemUI.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class StockViewModel
    {
        public List<StockDTO> Stocks { get; set; }
        public List<DepartmentDTO> Departments { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<UnitDTO> Units { get; set; }
        public List<UserDTO> Users { get; set; }

    }
}
