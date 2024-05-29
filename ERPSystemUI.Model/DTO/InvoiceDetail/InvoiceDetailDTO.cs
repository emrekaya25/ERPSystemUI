using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.InvoiceDetail
{
    public class InvoiceDetailDTO
    {
        public Int64 Id { get; set; } = 0;
        public Int64 InvoiceId { get; set; } = 0;
        public string ProductName { get; set; } = "string";
        public string ProductDescription { get; set; } = "string";
        public decimal Quantity { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;
        public decimal Sum { get; set; } = 0;


    }
}
