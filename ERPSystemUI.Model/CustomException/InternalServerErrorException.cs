using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.CustomException
{
    public class InternalServerErrorException:Exception
    {
        public InternalServerErrorException(string message="Ağ Geçidi Hatası!"):base(message)
        {
            
        }
    }
}
