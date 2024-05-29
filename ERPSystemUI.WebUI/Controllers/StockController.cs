using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Department;
using ERPSystemUI.Model.DTO.ProcessType;
using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Stock;
using ERPSystemUI.Model.DTO.StockDetail;
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
using System.ComponentModel.Design;
using System.Net;

namespace ERPSystemUI.WebUI.Controllers
{
    public class StockController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public StockController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Stocks")]
        public async Task<IActionResult> Index()
        {

            ProductDTO productDTO = new ProductDTO();
            StockDTO stockDTO = new StockDTO();
            UnitDTO unitDTO = new UnitDTO();
            UserDTO userDTO = new UserDTO();
            DepartmentDTO departmentDTO = new DepartmentDTO();

            if (!(SessionRole.Roles.Contains("Admin")))
            {
                departmentDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
                var companyId = HttpContext.Session.GetString("CompanyId");
                stockDTO.DepartmentId = Convert.ToInt64(companyId);
                userDTO.CompanyName = HttpContext.Session.GetString("Company");
                userDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }


            StockViewModel stockViewModel = new StockViewModel
            {
                Products = await GetProducts(productDTO),
                Departments = await GetDepartments(departmentDTO),
                Stocks = await GetStocks(stockDTO),
                Units = await GetUnits(unitDTO),
                Users = await GetUsers(userDTO)
            };

            return View(stockViewModel);
        }

        public async Task<IActionResult> AddStock(StockDTO stockDTO)
        {
            var url = "https://localhost:7150/AddStock";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Stock");
        }

        public async Task<IActionResult> DeleteStock(StockDTO stockDTO)
        {
            var url = "https://localhost:7150/DeleteStock";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Stock");
        }

        public async Task<IActionResult> UpdateStock(StockDTO stockDTO)
        {
            var url = "https://localhost:7150/UpdateStock";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Stock");
        }

        public async Task<List<StockDTO>> GetStocks(StockDTO stockDTO)
        {
            var url = "https://localhost:7150/Stocks";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
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
        public async Task<List<DepartmentDTO>> GetDepartments(DepartmentDTO departmentDTO)
        {
            var url = "https://localhost:7150/Departments";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(departmentDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<DepartmentDTO>>>(response.Content);

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

        // Detay Sayfası için gerekli olanlar
        public async Task<IActionResult> GetStockDetail(StockDTO stockDTO)
        {
            ProcessTypeDTO processTypeDTO = new ProcessTypeDTO();
            StockDetailDTO stockDetailDTO = new StockDetailDTO();
            stockDetailDTO.StockId = stockDTO.Id;
            UserDTO userDTO = new UserDTO();
            StockDetailViewModel stockDetailViewModel = new StockDetailViewModel()

            {
                Stocks = await GetStocks(stockDTO),
                StockDetails = await GetStockDetails(stockDetailDTO),
                ProcessTypes = await GetProcessTypes(processTypeDTO),
                Recievers = await GetRecievers(userDTO),
                Deliverers = await GetDeliverers(userDTO)
            };


            return View(stockDetailViewModel);
        }

        public async Task<List<StockDetailDTO>> GetStockDetails(StockDetailDTO stockDetailDTO)
        {
            var url = "https://localhost:7150/StockDetails";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<StockDetailDTO>>>(response.Content);

            return responseObject.Data;
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
        public async Task<List<UserDTO>> GetDeliverers(UserDTO userDTO)
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
        public async Task<List<UserDTO>> GetRecievers(UserDTO userDTO)
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
        public async Task<IActionResult> AddStockDetail(StockDetailDTO stockDetailDTO)
        {
            var url = "https://localhost:7150/AddStockDetail";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDetailDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Stock");
        }
        public async Task<IActionResult> DeleteStockDetail(StockDetailDTO stockDetailDTO)
        {
            var url = "https://localhost:7150/DeleteStockDetail";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDetailDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "StockDetail");
        }
        public async Task<IActionResult> UpdateStockDetail(StockDetailDTO stockDetailDTO)
        {
            var url = "https://localhost:7150/UpdateStockDetail";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(stockDetailDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<StockDetailDTO>>(response.Content);

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

                TempData["errorStock"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseStock"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "StockDetail");
        }
    }
}
