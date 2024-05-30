using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using ERPSystemUI.Model.DTO.ProcessType;
using ERPSystemUI.Model.Model;

namespace ERPSystemUI.WebUI.Controllers
{
    public class ProcessTypeController : Controller
    {
        private readonly ResponseChecker _responseChecker;
        public ProcessTypeController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/ProcessTypes")]
        public async Task<IActionResult> Index()
        {
            ProcessTypeDTO processTypeDTO = new ProcessTypeDTO();
            ProcessTypeViewModel processTypeViewModel = new ProcessTypeViewModel()
            {
                ProcessTypes = await GetProcessTypes(processTypeDTO)
            };
            return View(processTypeViewModel);
        }
        public async Task<List<ProcessTypeDTO>> GetProcessTypes(ProcessTypeDTO processTypeDTO)
        {
            var url = "https://localhost:7150/ProcessTypes";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(processTypeDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<ProcessTypeDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<IActionResult> AddProcessType(ProcessTypeDTO processTypeDTO)
        {
            var url = "https://localhost:7150/AddProcessType";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(processTypeDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<ProcessTypeDTO>>(response.Content);
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

                TempData["errorProcessType"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProcessType"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "ProcessType");
        }
        public async Task<IActionResult> DeleteProcessType(ProcessTypeDTO processTypeDTO)
        {
            var url = "https://localhost:7150/DeleteProcessType";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(processTypeDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<ProcessTypeDTO>>(response.Content);
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

                TempData["errorProcessType"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProcessType"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "ProcessType");
        }
        public async Task<IActionResult> UpdateProcessType(ProcessTypeDTO processTypeDTO)
        {
            var url = "https://localhost:7150/UpdateProcessType";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(processTypeDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<ProcessTypeDTO>>(response.Content);
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

                TempData["errorProcessType"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProcessType"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "ProcessType");
        }
    }
}
