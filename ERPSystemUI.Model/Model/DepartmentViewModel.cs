using ERPSystemUI.Model.DTO.Company;
using ERPSystemUI.Model.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class DepartmentViewModel
    {
        public CompanyDTO Company { get; set; }
        public List<DepartmentDTO> Departments { get; set; }

    }
}
