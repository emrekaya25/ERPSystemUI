using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Stock
{
    public class StockDTO
    {
        public Int64 Id { get; set; } = 0;
        public decimal Quantity { get; set; } = 0;
        public Int64 ProductId { get; set; } = 0;
        public Int64 UnitId { get; set; } = 0;
        public Int64 DepartmentId { get; set; } = 0;

        public string ProductImage { get; set; } = null;
        public string ProductName { get; set; } = null;
        public string CompanyName { get; set; } = null;
        public string UnitName { get; set; } = null;
        public string DepartmentName { get; set; } = null;
    }
}
