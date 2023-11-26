using Microsoft.AspNetCore.Mvc;
using assetManagementClassLibrary; 

namespace assetManagement.Controllers
{
    public class AsientosController : Controller
    {

        private readonly HttpClient _httpClient;

        public AsientosController(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Index()
        {
            try
            {

                HttpResponseMessage response = _httpClient.GetAsync("/api/Asiento/consultarAsientos").GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var asientos = response.Content.ReadFromJsonAsync<AsientoEnt>().Result;
                    return View(asientos);
                }
                else
                {
                    return View();
                }

            }
            catch (Exception)
            {

                return View();
                throw;
            }

        }




    }
}
