using ERPSystemUI.Model.DTO.Department;
using ERPSystemUI.Model.DTO.Product;
using ERPSystemUI.Model.DTO.Request;
using ERPSystemUI.Model.DTO.Role;
using ERPSystemUI.Model.DTO.User;
using ERPSystemUI.Model.DTO.UserRole;
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
    public class UserController : Controller
    {
        private readonly ResponseChecker _responseChecker;

        public UserController(ResponseChecker responseChecker)
        {
            _responseChecker = responseChecker;
        }

        //ana kullanıcılar sayfası
        [HttpGet("/Users")]
        public async Task<IActionResult> Index()
        {
            DepartmentDTO departmentDTO = new DepartmentDTO();
            UserDTO userDTO = new UserDTO();
            if (!(SessionRole.Roles.Contains("Admin")))
            {
                userDTO.CompanyId = Convert.ToInt64(HttpContext.Session.GetString("CompanyId"));
            }
            RoleDTO roleDTO = new RoleDTO();
            RequestDTO requestDTO = new RequestDTO();
            UserViewModel userViewModel = new UserViewModel()
            {
                Departments = await GetDepartments(departmentDTO),
                Users = await GetUsers(userDTO),
                Requests = await GetRequests(requestDTO),
                Roles = await GetRoles(roleDTO)
            };
            return View(userViewModel);
        }
        //detay sayfası
        public async Task<IActionResult> GetUser(UserDTO userDTO)
        {
            DepartmentDTO departmentDTO = new DepartmentDTO();
            RequestDTO requestDTO = new RequestDTO();
            RoleDTO roleDTO = new RoleDTO();
            requestDTO.ApproverId = userDTO.Id;
            UserViewModel userViewModel = new UserViewModel()
            {
                Departments = await GetDepartments(departmentDTO),
                User = await User(userDTO),
                Requests = await GetRequests(requestDTO),
                Roles = await GetRoles(roleDTO)
            };
            return View(userViewModel);
        }

        // sağ üstü profil sayfası
        public async Task<IActionResult> ProfilePage(UserDTO userDTO)
        {
            DepartmentDTO departmentDTO = new DepartmentDTO();
            RequestDTO requestDTO = new RequestDTO();
            RoleDTO roleDTO = new RoleDTO();
            requestDTO.ApproverId = userDTO.Id;
            UserViewModel userViewModel = new UserViewModel()
            {
                Departments = await GetDepartments(departmentDTO),
                User = await User(userDTO),
                Requests = await GetRequests(requestDTO),
                Roles = await GetRoles(roleDTO)
            };
            return View(userViewModel);
        }
        // profil sayfa güncelleme
        public async Task<IActionResult> UpdateProfile(UserDTO userDTO, IFormFile imageFile)
        {
            var url = "https://localhost:7150/UpdateUser";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                userDTO.Image = base64String;
            }


            var body = JsonConvert.SerializeObject(userDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(response.Content);


            if (responseObject.ErrorInformation != null)
            {
                TempData["errorUser"] = $"{responseObject.Message} {responseObject.ErrorInformation.ErrorDescription}";
            }
            else
            {
                TempData["responseUser"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "Home");
        }


        public async Task<UserDTO> User(UserDTO userDTO)
        {
            var url = "https://localhost:7150/User";

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

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(response.Content);

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
        public async Task<IActionResult> AddUserRole(UserRoleDTO userRoleDTO)
        {
            var url = "https://localhost:7150/AddUserRole";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));


            var body = JsonConvert.SerializeObject(userRoleDTO);

            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<UserRoleDTO>>(response.Content);

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

                TempData["errorUser"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseUser"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> DeleteUserRole(UserRoleDTO userRoleDTO)
        {
            var url = "https://localhost:7150/DeleteUserRole";

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            var body = JsonConvert.SerializeObject(userRoleDTO);

            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<UserRoleDTO>>(response.Content);

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

                TempData["errorUser"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseUser"] = $"{deserializeObject.Message}";
            }


            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> AddUser(UserDTO userDTO, IFormFile imageFile)
        {
            var url = "https://localhost:7150/AddUser";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                userDTO.Image = base64String;
            }


            var body = JsonConvert.SerializeObject(userDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(response.Content);


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

                TempData["errorUser"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseUser"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> UpdateUser(UserDTO userDTO, IFormFile imageFile)
        {
            var url = "https://localhost:7150/UpdateUser";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));

            string base64String = null;

            // Eğer dosya gönderilmişse
            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosyayı işlemek için gerekli kodları buraya ekleyin
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    // Dosyayı base64 string'e dönüştür
                    base64String = Convert.ToBase64String(fileBytes);
                }
                userDTO.Image = base64String;
            }


            var body = JsonConvert.SerializeObject(userDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                _responseChecker.Checker((int)response.StatusCode);
            }

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(response.Content);


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

                TempData["errorUser"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseUser"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> DeleteUser(UserDTO userDTO)
        {
            var url = "https://localhost:7150/DeleteUser";
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

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(response.Content);


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

                TempData["errorUser"] = $"{errorMessages}";
            }
            else
            {
                TempData["responseUser"] = $"{responseObject.Message}";
            }


            return RedirectToAction("Index", "User");
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

    }
}
