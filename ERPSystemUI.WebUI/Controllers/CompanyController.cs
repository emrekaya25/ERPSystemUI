using ERPSystemUI.Model.CustomException;
using ERPSystemUI.Model.DTO.Category;
using ERPSystemUI.Model.DTO.Company;
using ERPSystemUI.Model.DTO.Department;
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
    public class CompanyController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public CompanyController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        [HttpGet("/Companies")]
        public async Task<IActionResult> Index()
        {
            CompanyDTO companyDTO = new CompanyDTO();
            if (!(SessionRole.Roles.Contains("Admin")))
            {
                companyDTO.Id = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }
            CompanyViewModel viewModel = new CompanyViewModel()
            {
                Companies = await GetCompanies(companyDTO)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetDepartment(DepartmentDTO departmentDTO)
        {
            var companyDTO = new CompanyDTO();
            companyDTO.Id = departmentDTO.CompanyId;
            DepartmentViewModel viewModel = new DepartmentViewModel()
            {
                Company = await GetCompany(companyDTO),
                Departments = await GetDepartments(departmentDTO)
            };
            return View(viewModel);
        }


        public async Task<List<CompanyDTO>> GetCompanies(CompanyDTO companyDTO)
        {
            var url = "https://localhost:7150/Companies";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(companyDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<CompanyDTO>>>(response.Content);

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

        public async Task<CompanyDTO> GetCompany(CompanyDTO companyDTO)
        {
            var url = "https://localhost:7150/Company";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(companyDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<CompanyDTO>>(response.Content);

            return responseObject.Data;
        }

        public async Task<IActionResult> AddCompany(CompanyDTO companyDTO, IFormFile companyImage)
        {
            var url = "https://localhost:7150/AddCompany";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (companyImage != null && companyImage.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await companyImage.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                companyDTO.Image = base64String;
            }

            var body = JsonConvert.SerializeObject(companyDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CompanyDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Company");
        }
        public async Task<IActionResult> DeleteCompany(CompanyDTO companyDTO)
        {
            var url = "https://localhost:7150/DeleteCompany";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            var body = JsonConvert.SerializeObject(companyDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CompanyDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Company");
        }
        public async Task<IActionResult> UpdateCompany(CompanyDTO companyDTO, IFormFile companyImage)
        {
            var url = "https://localhost:7150/UpdateCompany";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (companyImage != null && companyImage.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await companyImage.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                companyDTO.Image = base64String;
            }

            var body = JsonConvert.SerializeObject(companyDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<CompanyDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Company");
        }

        public async Task<IActionResult> AddDepartment(DepartmentDTO departmentDTO)
        {
            var url = "https://localhost:7150/AddDepartment";

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

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<DepartmentDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Company");
        }
        public async Task<IActionResult> DeleteDepartment(DepartmentDTO departmentDTO)
        {
            var url = "https://localhost:7150/DeleteDepartment";

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

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<DepartmentDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }



            return RedirectToAction("Index", "Company");
        }
        public async Task<IActionResult> UpdateDepartment(DepartmentDTO departmentDTO)
        {
            var url = "https://localhost:7150/UpdateDepartment";

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
            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<DepartmentDTO>>(response.Content);

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

                TempData["errorCompany"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseCompany"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "Company");
        }
    }
}
