using ERPSystemUI.Model.DTO.Offer;
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
    public class OfferController : Controller
    {
        private readonly ResponseChecker _responseChecker;
        public OfferController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Offers")]
        public async Task<IActionResult> Index()
        {
            OfferDTO offerDTO = new OfferDTO();
            RequestDTO requestDTO = new RequestDTO();
            if (!(SessionRole.Roles.Contains("Admin")))
            {
                requestDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }
            StatusDTO statusDTO = new StatusDTO();
            UserDTO userDTO = new UserDTO();

            OfferViewModel offerViewModel = new OfferViewModel
            {
                Requests = await GetRequests(requestDTO),
                Offers = await GetOffers(offerDTO),
                Statuses = await GetStatuses(statusDTO),
                Users = await GetUsers(userDTO)

            };
            return View(offerViewModel);
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
            
            var confirmRequestList = new List<RequestDTO>();
            foreach (var item in responseObject.Data)
            {
                if (item.StatusId==2)
                {
                    confirmRequestList.Add(item);
                }
            }

            return confirmRequestList;
        }
        public async Task<List<OfferDTO>> GetOffers(OfferDTO offerDTO)
        {
            var url = "https://localhost:7150/Offers";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(offerDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<OfferDTO>>>(response.Content);

            return responseObject.Data;
        }
        public async Task<IActionResult> AddOffer(OfferDTO offerDTO)
        {
            var url = "https://localhost:7150/AddOffer";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            //onaylayan yok ve onay bekliyor durumu
            offerDTO.ApproverUserId = 0;
            offerDTO.StatusId = 1;

            var body = JsonConvert.SerializeObject(offerDTO);

            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<OfferDTO>>(response.Content);

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

                TempData["errorOffer"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseOffer"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Offer");
        }
        [HttpPost("/UpdateOffer")]
        public async Task<IActionResult> UpdateOffer(OfferDTO offerDTO)
        {
            var url = "https://localhost:7150/UpdateOffer";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(offerDTO);

            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<OfferDTO>>(response.Content);

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

                TempData["errorOffer"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseOffer"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Offer");
        }
        [HttpPost("/OffersByRequest")]
        public async Task<List<OfferDTO>> GetOffersByRequest(OfferDTO offerDTO)
        {
            var url = "https://localhost:7150/Offers";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(offerDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }


            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<OfferDTO>>>(response.Content);

            return responseObject.Data;
        }
    }
}
