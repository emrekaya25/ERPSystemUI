using ERPSystemUI.Model.DTO.Role;
using ERPSystemUI.Model.DTO.Unit;
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
	public class UnitController : Controller
	{
		private readonly ResponseChecker _responseChecker;

		public UnitController(ResponseChecker responseChecker)
		{
			_responseChecker = responseChecker;
		}

		[HttpGet("/Units")]
		public async Task<IActionResult> Index()
		{
			UnitDTO unitDTO = new UnitDTO();
			UnitViewModel unitViewModel = new UnitViewModel()
			{
				Units = await GetUnits(unitDTO)
			};
			return View(unitViewModel);
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
		public async Task<IActionResult> AddUnit(UnitDTO unitDTO)
		{
			var url = "https://localhost:7150/AddUnit";
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

			var responseObject = JsonConvert.DeserializeObject<ApiResponse<UnitDTO>>(response.Content);
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

				TempData["errorUnit"] = $"{errorMessages}";
			}
			else
			{
				TempData["responseUnit"] = $"{responseObject.Message}";
			}


			return RedirectToAction("Index", "Unit");
		}
		public async Task<IActionResult> DeleteUnit(UnitDTO unitDTO)
		{
			var url = "https://localhost:7150/DeleteUnit";
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

			var responseObject = JsonConvert.DeserializeObject<ApiResponse<UnitDTO>>(response.Content);
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

				TempData["errorUnit"] = $"{errorMessages}";
			}
			else
			{
				TempData["responseUnit"] = $"{responseObject.Message}";
			}


			return RedirectToAction("Index", "Unit");
		}
		public async Task<IActionResult> UpdateUnit(UnitDTO unitDTO)
		{
			var url = "https://localhost:7150/UpdateUnit";
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

			var responseObject = JsonConvert.DeserializeObject<ApiResponse<UnitDTO>>(response.Content);
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

				TempData["errorUnit"] = $"{errorMessages}";
			}
			else
			{
				TempData["responseUnit"] = $"{responseObject.Message}";
			}


			return RedirectToAction("Index", "Unit");
		}
	}
}
