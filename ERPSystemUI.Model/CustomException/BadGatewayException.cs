using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.CustomException
{
    public class BadGatewayException:Exception
    {
        public BadGatewayException(string message="Kötü Ağ Geçidi!"):base(message)
        {
            
        }
    }
}
