using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace assetManagement.Controllers
{
    public class ClaseController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ASSET_MANAGEMENTContext _context;

        public ClaseController(ASSET_MANAGEMENTContext context)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _context = context;


        }

        [HttpGet]
        public IActionResult crearClase()
        {
            var nuevaClase = new ClaseEnt();
            return View("CREAR_CLASE", nuevaClase);
        }


        [HttpPost]
        public async Task<IActionResult> crearClase(string clase, int vidaUtil, string activo, string gasto, string depAc)
        {
        try
    {
                ClaseEnt nuevaClase = new ClaseEnt();
                {
                    nuevaClase.descripcionClase = clase;
                    nuevaClase.vidaUtil = vidaUtil;
                    nuevaClase.cuentaActivo = new CuentaContableEnt { idCuenta = activo };
                    nuevaClase.cuentaGasto = new CuentaContableEnt { idCuenta = gasto };
                    nuevaClase.cuentaDepAcumulada = new CuentaContableEnt { idCuenta = depAc };
                };


        var response = await _httpClient.PostAsJsonAsync("api/crearClase", nuevaClase);
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadAsStringAsync();
            return Json(resultado);
        }
        else
        {
            return Json(new { error = "Error al crear la clase" });
        }
    }
    catch (Exception ex)
    {
        return Json(new { error = "Error al crear la clase" });
    }


}
        
        

        public async Task<IActionResult> ListarClases()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/obtenerListaClases");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var clases = JsonConvert.DeserializeObject<IEnumerable<ClaseEnt>>(content);
                    return View(clases);
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


        public async Task<List<SelectListItem>> seleccionarClase()
        {
           try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/obtenerListaClases");

                if (response.IsSuccessStatusCode)
                {
                    
                    var listaClases = response.Content.ReadFromJsonAsync<List<ClaseEnt>>().Result;

                    List<SelectListItem> listaDropdown = new List<SelectListItem>();

                    listaDropdown.Add(new SelectListItem
                    {
                        Text = "Seleccione la clase",
                        Value = "0"
                    });

                    foreach (var clase in listaClases)
                    {
                        listaDropdown.Add(new SelectListItem
                        {
                            Text = clase.descripcionClase,
                            Value = clase.idClase.ToString()
                        });
                    }

                    return listaDropdown;
                }

                return new List<SelectListItem>();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AuxiliarEnt>> ejecutarDepreciacion(int idClase, string descripcionAsiento)
        {
            try
            {
               //Create a class to be sent to the API. 
                ClaseEnt claseDepreciada = new ClaseEnt();
                claseDepreciada.asientoDepreciacion = new AsientoEnt();

                claseDepreciada.idClase = idClase;
                claseDepreciada.asientoDepreciacion.descripcion = descripcionAsiento;

                //Perform request to the API. 
                HttpResponseMessage response = await _httpClient.GetAsync("api/ejecutarDepreciacionClase");

                JsonContent body = JsonContent.Create(claseDepreciada);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<List<AuxiliarEnt>>().Result;
                }

                return new List<AuxiliarEnt>();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> crearValidacionClase(string idClase, string descripcionValidacion)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync("api/crearValidacionClase");
        //        ClaseEnt nuevaValidacion = new ClaseEnt
        //        {
        //            idClase = Convert.ToInt32(idClase),
        //            validacionClase = new ValidacionClaseEnt { descripcionValidacion = descripcionValidacion }
        //        };
        //        JsonContent body = JsonContent.Create(nuevaValidacion);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("ListarClases");
        //        }
        //        return View("ListarClases") ;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> crearValidacionClase(string idClase, string descripcionValidacion)
        {
            try
            {
                ClaseEnt nuevaValidacion = new ClaseEnt
                {
                    idClase = Convert.ToInt32(idClase),
                    validacionClase = new ValidacionClaseEnt { descripcionValidacion = descripcionValidacion }
                };


                var response = await _httpClient.PostAsJsonAsync("api/crearValidacionClase", nuevaValidacion);
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return Json(resultado);
                }
                else
                {
                    return Json(new { error = "Error al crear la validacion" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al crear la validacion" });
            }
        }

        public List<ValidacionClaseEnt> consultarValidacionesClase(int idClase)
        {

            var validacionesClase = (from x in _context.TipoValidacions
                                     where x.IdClase == idClase
                                     select x).ToList();

            List<ValidacionClaseEnt> listaValidaciones = new List<ValidacionClaseEnt>();


            foreach (var validacion in validacionesClase)
            {

                listaValidaciones.Add(new ValidacionClaseEnt
                {
                    idValidacion = validacion.IdTipoValidacion,
                    descripcionValidacion = validacion.DescripcionValidacion
                });
            }


            return listaValidaciones;

        }

        [HttpGet]
        public ClaseEnt buscarInformacionClase(int idClase)
        {

            try
            {
                var clase = (from x in _context.Clases
                             where x.IdClase == idClase
                             select x).FirstOrDefault();

                ClaseEnt claseOutput = new ClaseEnt();

                claseOutput.idClase = clase.IdClase;
                claseOutput.descripcionClase = clase.DescripcionClase;
                claseOutput.vidaUtil = (int)clase.VidaUtil;
                claseOutput.listaValidaciones = consultarValidacionesClase(clase.IdClase);

                return claseOutput;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
