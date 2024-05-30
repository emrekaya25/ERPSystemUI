using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.Model;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ERPSystemUI.WebUI.Controllers
{
	public class StatusController : Controller
	{
        private readonly ResponseChecker _responseChecker;

        public StatusController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Statuses")]
		public async Task<IActionResult> Index()
		{
			StatusDTO statusDTO = new StatusDTO();
            StatusViewModel statusViewModel = new StatusViewModel()
            {
                Statuses = await GetStatuses(statusDTO)
            };
			return View(statusViewModel);
		}

        public async Task<List<StatusDTO>> GetStatuses(StatusDTO statusDTO)
        {
            var url = "https://localhost:7150/Statuses";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(statusDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<StatusDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<IActionResult> AddStatus(StatusDTO statusDTO)
        {
            var url = "https://localhost:7150/AddStatus";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(statusDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<StatusDTO>>(response.Content);
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

                TempData["errorStatus"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStatus"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Status");
        }
        public async Task<IActionResult> DeleteStatus(StatusDTO statusDTO)
        {
            var url = "https://localhost:7150/DeleteStatus";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(statusDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<StatusDTO>>(response.Content);
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

                TempData["errorStatus"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStatus"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Status");
        }
        public async Task<IActionResult> UpdateStatus(StatusDTO statusDTO)
        {
            var url = "https://localhost:7150/UpdateStatus";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(statusDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<StatusDTO>>(response.Content);
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

                TempData["errorStatus"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStatus"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Status");
        }
    }
}
