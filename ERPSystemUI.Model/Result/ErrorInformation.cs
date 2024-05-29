using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Result
{
    public class ErrorInformation
    {
        public object Error { get; set; }
        public string ErrorDescription { get; set; }


        public static ErrorInformation GlobalError(string errorDescription = "Bir hata oluştu!!", object? error = null)
        {
            return new ErrorInformation { ErrorDescription = errorDescription, Error = error };
        }

        public static ErrorInformation FieldValidationError(object? error = null, string errorDescription = "Zorunlu alanlarda eksiklikler var!!")
        {
            return new ErrorInformation { Error = error, ErrorDescription = errorDescription };
        }

        public static ErrorInformation NotFound(string errorDescription = "Sonuç bulunamadı!!", object? error = null)
        {
            return new ErrorInformation { Error = error, ErrorDescription = errorDescription };
        }
    }
}
