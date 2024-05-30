using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Invoice;
using ERPSystemUI.Model.Model;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using ERPSystemUI.WebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERPSystemUI.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public ReportController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Rapor")]
        public async Task<IActionResult> Index()
        {
            InvoiceDTO invoiceDTO = new InvoiceDTO();
            CategoryDTO categoryDTO = new CategoryDTO();
            ReportViewModel reportViewModel = new ReportViewModel()
            {
                Invoices = await GetInvoicesByDate(),
                Categories = await GetCategories(categoryDTO)
            };
            return View(reportViewModel);
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
        public async Task<List<InvoiceDTO>> GetInvoicesByDate(string datefilter = "string")
        {
            {

                if (datefilter == null)
                {
                    var url = "https://localhost:7150/InvoicesByDate?datefilter=" + datefilter;

                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (!(response.StatusCode == HttpStatusCode.OK))
                    {
                        _responseChecker.Checker((int)response.StatusCode);
                    }

                    var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<InvoiceDTO>>>(response.Content);

                    return responseObject.Data;
                }
                else
                {
                    var date1 = datefilter.Replace(" ", "");
                    var datee = date1.Replace("/", ".");
                    var date2 = datee.Replace(@"\", "");
                    var url = "https://localhost:7150/InvoicesByDate?datefilter=" + date2;

                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (!(response.StatusCode == HttpStatusCode.OK))
                    {
                        _responseChecker.Checker((int)response.StatusCode);
                    }

                    var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<InvoiceDTO>>>(response.Content);

                    // giriş yapan kullanıcının şirketine göre çekme yeri
                    if (!(SessionRole.Roles.Contains("Admin")))
                    {
                        List<InvoiceDTO> invoices = new List<InvoiceDTO>();

                        foreach (var item in responseObject.Data)
                        {
                            if (item.CompanyId == Convert.ToInt64(HttpContext.Session.GetString("CompanyId")))
                            {
                                invoices.Add(item);
                            }
                        }
                        return invoices;
                    }

                    return responseObject.Data;

                }

            }


        }

        // tarihe göre ekran getirme
        public async Task<IActionResult> GetInvoiceByDate(string datefilter)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            ReportViewModel reportViewModel = new()
            {
                Invoices = await GetInvoicesByDate(datefilter),
                Categories = await GetCategories(categoryDTO)
            };

            if (datefilter == null || !(reportViewModel.Invoices.Count >= 1))
            {
                return RedirectToAction("Index", "Report");
            }

            else
            {
                return View(reportViewModel);
            }



        }
        public async Task<List<CategoryDTO>> GetCategories(CategoryDTO categoryDTO)
        {
            var url = "https://localhost:7150/Categories";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(categoryDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<CategoryDTO>>>(response.Content);

            return responseObject.Data;
        }
    }
}
