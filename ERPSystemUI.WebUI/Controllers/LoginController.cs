using ERPSystemUI.Model.DTO.Login;
using ERPSystemUI.Model.Result;
using ERPSystemUI.WebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ERPSystemUI.WebUI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("/Login")]
        public IActionResult Index()
        {
            // session dolu ise kişi login sayfasına giderse anasayfaya yönlendirilecek.
            if (HttpContext.Session.GetString("UserId") != null)
            {

                HttpContext.Response.Redirect("/Anasayfa");
            }
            return View();


        }

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var url = "https://localhost:7150/LoginAsync";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<LoginDTO>>(restResponse.Content);


            if (responseObject.StatusCode == (int)HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("Name", responseObject.Data.Name);
                HttpContext.Session.SetString("Token", responseObject.Data.Token);
                HttpContext.Session.SetString("Email", responseObject.Data.Email);
                HttpContext.Session.SetString("CompanyId", responseObject.Data.CompanyId.ToString());
                HttpContext.Session.SetString("DepartmentId", responseObject.Data.DepartmentId.ToString());
                HttpContext.Session.SetString("UserId", responseObject.Data.UserId.ToString());
                HttpContext.Session.SetString("Company", responseObject.Data.CompanyName);
                HttpContext.Session.SetString("Department", responseObject.Data.DepartmentName);
                HttpContext.Session.SetString("UserImage", responseObject.Data.UserImage);


                SessionRole.Roles = responseObject.Data.RoleName;


                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = $"{responseObject.Message}";
            }



            return RedirectToAction("Index", "Login");


        }

        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

    }
}
