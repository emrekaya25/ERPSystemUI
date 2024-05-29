using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Department
{
    public class DepartmentDTO
    {
        public Int64 Id { get; set; } = 0;
        public Int64 CompanyId { get; set; } = 0;
        public string Name { get; set; } = "string";

        //response

        public string CompanyName { get; set; } = null;
    }
}
