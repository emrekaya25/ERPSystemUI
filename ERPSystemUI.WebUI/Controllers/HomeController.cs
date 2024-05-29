using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Status;
using ERPSystemUI.Model.DTO.Stock;
using ERPSystemUI.Model.DTO.Unit;
using ERPSystemUI.Model.DTO.User;
using ERPSystemUI.Model.Model;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using ERPSystemUI.WebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ERPSystemUI.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ResponseChecker _responseChecker;
        public HomeController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Anasayfa")]
        public async Task<IActionResult> Index()
        {
            ProductDTO productDTO = new ProductDTO();
            RequestDTO requestDTO = new RequestDTO();


            if (!(SessionRole.Roles.Contains("Admin")))
            {
                if ((SessionRole.Roles.Contains("Müdür")))
                {
                    requestDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
                }
                else
                {
                    requestDTO.RequesterId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
                    HomeViewModel model = new HomeViewModel()
                    {
                        Requests = await GetRequests(requestDTO)
                    };
                    return View(model);
                }
            }


            StockDTO stockDTO = new StockDTO();
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Products = await GetProducts(productDTO),
                Requests = await GetRequests(requestDTO),
                Stocks = await GetStocks(stockDTO),

            };
            return View(homeViewModel);
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
        public async Task<List<StockDTO>> GetStocks(StockDTO stockDTO)
        {
            var url = "https://localhost:7150/Stocks";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            if (!(SessionRole.Roles.Contains("Admin")))
            {
                stockDTO.DepartmentId = Convert.ToInt64(HttpContext.Session.GetString("DepartmentId"));
            }

            var body = JsonConvert.SerializeObject(stockDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<StockDTO>>>(response.Content);

            return responseObject.Data;
        }
    }
}
