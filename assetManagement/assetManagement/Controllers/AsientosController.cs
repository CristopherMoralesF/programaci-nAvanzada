using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace assetManagement.Controllers
{
    public class AsientosController : Controller
    {
        private readonly HttpClient _httpClient;

        public AsientosController() {
            
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
            _httpClient.DefaultRequestHeaders.Accept.Clear(); 
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        
        }

        public async Task<IActionResult> Index()
        {

            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync("/api/Asiento/consultarAsientos");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var asientos = JsonConvert.DeserializeObject<IEnumerable<AsientoEnt>>(content);
                    return View(asientos);
                } else
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

        public async Task<IActionResult> detalleAsiento(int idAsiento)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Asiento/consultarAsiento?idAsiento="+idAsiento.ToString());

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(); 
                    var asiento = JsonConvert.DeserializeObject<AsientoEnt>(content);

                    return View(asiento);
                    
                } else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View(); 
            }
        }

    }
}
