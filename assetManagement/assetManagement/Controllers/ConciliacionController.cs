using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace assetManagement.Controllers
{
    public class ConciliacionController : Controller
    {
        private readonly HttpClient _httpClient;

        public ConciliacionController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Conciliacion()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Conciliacion/consultarConciliacion"); 

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var conciliacion = JsonConvert.DeserializeObject<IEnumerable<ConciliacionEnt>>(content); 
                    return View(conciliacion);
                }
                {
                    return View(null);
                }

            }
            catch (Exception)
            {
                return View(null);
                throw;
            }
        }
    }
}
