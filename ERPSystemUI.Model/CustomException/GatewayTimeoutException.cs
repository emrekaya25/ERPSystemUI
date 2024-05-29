using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.CustomException
{
    public class GatewayTimeoutException:Exception
    {
        public GatewayTimeoutException(string message="Ağ Geçidi Zaman Aşımı Hatası!"):base(message)
        {
            
        }
    }
}
