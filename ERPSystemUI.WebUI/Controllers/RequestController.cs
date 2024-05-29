using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.DTO.Unit;
using ERPSystemUI.Model.DTO.User;
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
    public class RequestController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public RequestController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Requests")]
        public async Task<IActionResult> Index()
        {
            RequestDTO requestDTO = new RequestDTO();
            StatusDTO statusDTO = new StatusDTO();
            ProductDTO productDTO = new ProductDTO();
            UnitDTO unitDTO = new UnitDTO();
            UserDTO userDTO = new UserDTO();
            if ((SessionRole.Roles.Contains("Çalışan")))
            {
                var userId = HttpContext.Session.GetString("UserId");
                requestDTO.RequesterId = Convert.ToInt64(userId);
            }
            if ((SessionRole.Roles.Contains("Müdür")))
            {
                requestDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
                userDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }
            RequestViewModel requestViewModel = new RequestViewModel
            {
                Statuses = await GetStatuses(statusDTO),
                Requests = await GetRequests(requestDTO),
                Units = await GetUnits(unitDTO),
                Products = await GetProducts(productDTO)
            };
            return View(requestViewModel);
        }

        public async Task<List<RequestDTO>> GetRequests(RequestDTO requestDTO)
        {
            var url = "https://localhost:7150/Requests";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(requestDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<RequestDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<IActionResult> AddRequest(RequestDTO requestDTO)
        {
            var url = "https://localhost:7150/AddRequest";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(requestDTO);

            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<RequestDTO>>(response.Content);

            if (deserializeObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)deserializeObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join(", ", errorList);

                TempData["errorRequest"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRequest"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Request");
        }
        public async Task<IActionResult> UpdateRequest(RequestDTO requestDTO)
        {
            var url = "https://localhost:7150/UpdateRequest";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(requestDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<RequestDTO>>(response.Content);

            if (deserializeObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)deserializeObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join(", ", errorList);

                TempData["errorRequest"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRequest"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Request");
        }
        public async Task<IActionResult> DeleteRequest(RequestDTO requestDTO)
        {
            var url = "https://localhost:7150/DeleteRequest";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(requestDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<RequestDTO>>(response.Content);

            if (deserializeObject.ErrorInformation != null)
            {
                var errorList = new List<string>();

                // ErrorInformation.Error jarray nesnesi dönüyor.
                JArray errors = (JArray)deserializeObject.ErrorInformation.Error;
                foreach (var error in errors)
                {
                    errorList.Add(error.ToString());
                }
                // errorList içindeki hataları ve diğer bilgileri TempData'ya eklemek
                string errorMessages = string.Join(", ", errorList);

                TempData["errorRequest"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRequest"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Request");
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

        public async Task<List<UnitDTO>> GetUnits(UnitDTO unitDTO)
        {
            var url = "https://localhost:7150/Units";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(unitDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<UnitDTO>>>(response.Content);

            return responseObject.Data;
        }

        public async Task<List<UserDTO>> GetUsers(UserDTO userDTO)
        {
            var url = "https://localhost:7150/Users";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(userDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<UserDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<List<ProductDTO>> GetProducts(ProductDTO productDTO)
        {
            var url = "https://localhost:7150/Products";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(productDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<ProductDTO>>>(response.Content);

            return responseObject.Data;
        }

    }
}
