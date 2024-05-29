using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.User;
using ERPSystemUI.Model.Model;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.ExceptionHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text.Json.Serialization;

namespace ERPSystemUI.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public ProductController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Products")]
        public async Task<IActionResult> Index()
        {

            ProductDTO productDTO = new ProductDTO();
            CategoryDTO categoryDTO = new CategoryDTO();

            ProductViewModel productViewModel = new ProductViewModel
            {
                Categories = await GetCategories(categoryDTO),
                Products = await GetProducts(productDTO)
            };



            return View(productViewModel);
        }

        public async Task<IActionResult> AddProduct(ProductDTO productDTO, IFormFile imageProductFile)
        {
            var url = "https://localhost:7150/AddProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (imageProductFile != null && imageProductFile.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await imageProductFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                productDTO.Image = base64String;
            }


            var body = JsonConvert.SerializeObject(productDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(response.Content);


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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> DeleteProduct(ProductDTO productDTO)
        {
            var url = "https://localhost:7150/DeleteProduct";

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

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(response.Content);

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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO, IFormFile imageProductFile)
        {
            var url = "https://localhost:7150/UpdateProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (imageProductFile != null && imageProductFile.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await imageProductFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                productDTO.Image = base64String;
            }


            var body = JsonConvert.SerializeObject(productDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(response.Content);


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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Product");
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

        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            var url = "https://localhost:7150/AddCategory";

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

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CategoryDTO>>(response.Content);

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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> DeleteCategory(CategoryDTO categoryDTO)
        {
            var url = "https://localhost:7150/DeleteCategory";

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
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CategoryDTO>>(response.Content);

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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            var url = "https://localhost:7150/UpdateCategory";

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
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CategoryDTO>>(response.Content);

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

                TempData["errorProduct"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseProduct"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Product");
        }
    }
}
