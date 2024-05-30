using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using ERPSystemUI.Model.DTO.Role;
using ERPSystemUI.Model.Model;

namespace ERPSystemUI.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public RoleController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Roles")]
        public async Task<IActionResult> Index()
        {
            RoleDTO roleDTO = new RoleDTO();
            RoleViewModel roleViewModel = new RoleViewModel()
            {
                Roles = await GetRoles(roleDTO)
            };
            return View(roleViewModel);
        }

        public async Task<List<RoleDTO>> GetRoles(RoleDTO roleDTO)
        {
            var url = "https://localhost:7150/Roles";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(roleDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<RoleDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            var url = "https://localhost:7150/AddRole";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(roleDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<RoleDTO>>(response.Content);
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

                TempData["errorRole"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRole"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Role");
        }
        public async Task<IActionResult> DeleteRole(RoleDTO roleDTO)
        {
            var url = "https://localhost:7150/DeleteRole";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(roleDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<RoleDTO>>(response.Content);
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

                TempData["errorRole"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRole"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Role");
        }
        public async Task<IActionResult> UpdateRole(RoleDTO roleDTO)
        {
            var url = "https://localhost:7150/UpdateRole";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(roleDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<RoleDTO>>(response.Content);
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

                TempData["errorRole"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseRole"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Role");
        }
    }
}
