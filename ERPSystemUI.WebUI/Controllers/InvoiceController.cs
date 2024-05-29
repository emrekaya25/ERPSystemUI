using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Invoice;
using ERPSystemUI.Model.DTO.InvoiceDetail;
using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.Model;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using ERPSystemUI.WebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ERPSystemUI.WebUI.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ResponseChecker _responseChecker;
        public InvoiceController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Invoices")]
        public async Task<IActionResult> Index()
        {
            InvoiceDTO invoiceDTO = new InvoiceDTO();
            if (SessionRole.Roles.Contains("Müdür"))
            {
                invoiceDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }

            InvoiceViewModel invoiceViewModel = new InvoiceViewModel
            {
                Invoices = await GetInvoices(invoiceDTO),
            };

            return View(invoiceViewModel);
        }
        public async Task<IActionResult> GetInvoiceDetail(InvoiceDTO invoiceDTO)
        {

            InvoiceViewModel invoiceViewModel = new InvoiceViewModel
            {
                Invoice = await GetInvoice(invoiceDTO),
            };

            return View(invoiceViewModel);
        }

        public async Task<IActionResult> AddInvoice(InvoiceDTO invoiceDTO)
        {
            var urlInvoice = "https://localhost:7150/AddInvoice";
            var clientInvoice = new RestClient(urlInvoice);
            var requestInvoice = new RestRequest(urlInvoice, Method.Post);
            requestInvoice.AddHeader("Content-Type", "application/json");
            requestInvoice.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            foreach (var detail in invoiceDTO.InvoiceDetails)
            {
                detail.Sum = detail.UnitPrice * detail.Quantity;
            }

            var bodyInvoice = JsonConvert.SerializeObject(invoiceDTO);
            requestInvoice.AddBody(bodyInvoice, "application/json");
            RestResponse responseInvoice = await clientInvoice.ExecuteAsync(requestInvoice);

            if (!(responseInvoice.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)responseInvoice.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<InvoiceDTO>>(responseInvoice.Content);
            if (responseObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)responseObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join("-", errorList);

                TempData["errorInvoicess"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseInvoice"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Invoice");
        }
        public async Task<IActionResult> DeleteInvoice(InvoiceDTO invoiceDTO)
        {
            var url = "https://localhost:7150/DeleteInvoice";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(invoiceDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<InvoiceDTO>>(response.Content);


            if (responseObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)responseObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join(", ", errorList);

                TempData["errorInvoice"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseInvoice"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Invoice");
        }
        public async Task<IActionResult> UpdateInvoice(InvoiceDTO invoiceDTO)
        {
            var url = "https://localhost:7150/UpdateInvoice";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));


            var body = JsonConvert.SerializeObject(invoiceDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<InvoiceDTO>>(response.Content);


            if (responseObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)responseObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join(", ", errorList);

                TempData["errorInvoice"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseInvoice"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Invoice");
        }
        public async Task<List<InvoiceDTO>> GetInvoices(InvoiceDTO invoiceDTO)
        {
            var url = "https://localhost:7150/Invoices";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(invoiceDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<InvoiceDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<InvoiceDTO> GetInvoice(InvoiceDTO invoiceDTO)
        {
            var url = "https://localhost:7150/Invoice";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(invoiceDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<InvoiceDTO>>(response.Content);

            return responseObject.Data;
        }
    }
}
