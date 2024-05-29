using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.UserRole
{
    public class UserRoleDTO
    {
        public Int64 Id { get; set; } = 0;
        public Int64 UserId { get; set; } = 0;
        public Int64 RoleId { get; set; } = 0;
        public string UserName { get; set; } = "string";
        public string RoleName { get; set; } = "string";
    }
}
