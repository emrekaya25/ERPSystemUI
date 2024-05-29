using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.CustomException
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException(string message="Yetkisiz Erişim"):base(message)
        {
            
        }

    }
}
