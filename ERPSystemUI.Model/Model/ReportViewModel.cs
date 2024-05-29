using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Invoice;
using ERPSystemUI.Model.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class ReportViewModel
    {
        public List<InvoiceDTO> Invoices { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        
        
    }
}
