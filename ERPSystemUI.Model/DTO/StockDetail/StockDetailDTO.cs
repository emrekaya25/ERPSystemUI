using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.StockDetail
{
    public class StockDetailDTO
    {
        public Int64 Id { get; set; } = 0;
        public decimal Quantity { get; set; } = 0;
        public Int64 StockId { get; set; } = 0;
        public Int64 ProcessTypeId { get; set; } = 0;
        public Int64 ReceiverId { get; set; } = 0;
        public Int64 DelivererId { get; set; } = 0;
        public string ProcessTypeName { get; set; } = null;
        public string RecieverName { get; set; } = null;
        public string DelivererName { get; set; } = null;
        public DateTime AddedTime { get; set; }
        public string ProductName { get; set; } = null;
        public string StockDetailImage { get; set; } = null;
        public string ProductDescription { get; set; } = null;
        public string StockDetailUnitName { get; set; } = null;
    }
}
