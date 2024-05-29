using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Request
{
    public class RequestDTO
    {
        public Int64 Id { get; set; } = 0;
        public string Title { get; set; } = "string";
        public string Description { get; set; } = "string";
        public decimal Quantity { get; set; } = 0;
        public Int64 ProductId { get; set; } = 0;
        public Int64 UnitId { get; set; } = 0;
        public Int64 RequesterId { get; set; } = 0;
        public Int64? ApproverId { get; set; } = 0; 
        public Int64 StatusId { get; set; } = 0;
        public Int64 CompanyId { get; set; } = 0;

        //Response
        public string ProductName { get; set; } = "string";
        public string UnitName { get; set; } = "string";
        public string RequesterName { get; set; } = "string";
        public string ApproverName { get; set; } = "string";
        public string StatusName { get; set; } = "string";
    }
}
