using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.CustomException
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message="Aradığınız Sayfa Bulunamadı!"):base(message)
        {
            
        }
    }
}
