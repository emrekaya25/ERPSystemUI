using ERPSystemUI.Model.DTO.InvoiceDetail;
using ERPSystemUI.Model.DTO.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Invoice
{
    public class InvoiceDTO
    {
        public Int64 Id { get; set; } = 0;
        public Int64 CompanyId { get; set; } = 0;
        public DateTime? InvoiceDate { get; set; } = default;
        public int InvoiceNo { get; set; } = 0;
        public string SupplierName { get; set; } = "string";
        public string SupplierPhone { get; set; } = "string";
        public string SupplierAddress { get; set; } = "string";
        public string SupplierMail { get; set; } = "string";
        public string CompanyName { get; set; } = "string";
        public string CompanyPhone { get; set; } = "string";
        public string CompanyMail { get; set; } = "string";

        public List<InvoiceDetailDTO> InvoiceDetails { get; set; } = new();
    }
}
