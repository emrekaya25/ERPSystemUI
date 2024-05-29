using ERPSystemUI.Model.DTO.Department;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Role;
using ERPSystemUI.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class UserViewModel
    {
        public List<UserDTO> Users { get; set; }
        public List<DepartmentDTO> Departments { get; set; }
        public List<RequestDTO> Requests { get; set; }
        public List<RoleDTO> Roles { get; set; }
        public UserDTO User { get; set; }
    }
}
