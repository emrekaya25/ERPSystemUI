using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Login
{
    public class LoginDTO
    {
        public string Email { get; set; } = "string";
        public string Password { get; set; } = "string";

        //response
        public string Name { get; set; } = "string";
        public string Token { get; set; } = "string";
        public List<string> RoleName { get; set; } = null;
        public Int64 CompanyId { get; set; } = 0;
        public Int64 UserId { get; set; } = 0;
        public Int64 DepartmentId { get; set; } = 0;
        public string DepartmentName { get; set; } = "string";
        public string CompanyName { get; set; } = "string";
        public string UserImage { get; set; } = "string";
    }
}
