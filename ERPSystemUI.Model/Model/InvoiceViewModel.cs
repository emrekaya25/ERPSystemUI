using ERPSystemUI.Model.DTO.Invoice;
using ERPSystemUI.Model.DTO.InvoiceDetail;
using ERPSystemUI.Model.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class InvoiceViewModel
    {
        public List<InvoiceDTO> Invoices { get; set; }
        public InvoiceDTO Invoice { get; set; }
    }
}
