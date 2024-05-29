using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Offer
{
    public class OfferDTO
    {
        public Int64 Id { get; set; } = 0;
        public string Name { get; set; } = "string";
        public Int64 RequestId { get; set; }
        public string SupplierName { get; set; } = "string";
        public string Description { get; set; } = "string";
        public decimal Price { get; set; } = 0;
        public Int64 StatusId { get; set; } = 0;
        public string StatusName { get; set; } = null;
        public Int64? ApproverUserId { get; set; } = 0;
        public string ApproverUser { get; set; } = null;
    }
}
