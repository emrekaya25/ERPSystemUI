using ERPSystemUI.Model.DTO.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.User
{
    public class UserDTO
    {
        public Int64 Id { get; set; } = 0;
        public string Name { get; set; } = "string";
        public string Email { get; set; } = "string";
        public string Phone { get; set; } = "string";
        public string Password { get; set; } = "string";
        public string Image { get; set; } = "string";
        public Int64 DepartmentId { get; set; } = 0;
        public Int64 CompanyId { get; set; } = 0;
        public List<RoleDTO> Roles { get; set; } = null;

        // response
        public string DepartmentName { get; set; } = null;
        public string CompanyName { get; set; } = "string";
    }
}
