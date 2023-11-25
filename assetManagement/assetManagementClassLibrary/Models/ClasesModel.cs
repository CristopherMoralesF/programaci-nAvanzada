using Proyecto_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace assetManagementClassLibrary.Models
{
    internal class ClasesModel
    {
        public int crearClase(ClaseEnt clase)
        {

            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/crearClase";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                JsonContent body = JsonContent.Create(clase);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }
                return 0;
            }
        }

        public List<ClaseEnt> consultarClase()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/obtenerListaClases";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());


                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",HttpContext.Current.)
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    //Get the list of provinces. 
                    var listaClases = res.Content.ReadFromJsonAsync<List<ClaseEnt>>().Result;

                    return listaClases;
                }
                return new List<ClaseEnt>();
            }
        }

        public ClaseEnt buscarInformacionClase(int idClase)
        {

            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/consultarClase?idClase=" + idClase.ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<ClaseEnt>().Result;
                }
                return new ClaseEnt();

            }

        }



        public List<SelectListItem> seleccionarClase()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/obtenerListaClases";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());


                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",HttpContext.Current.)
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    //Get the list of provinces. 
                    var listaClases = res.Content.ReadFromJsonAsync<List<ClaseEnt>>().Result;

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
        }

        public List<AuxiliarEnt> ejecutarDepreciacion(int idClase, string descripcionAsiento)
        {
            using (var client = new HttpClient())
            {

                //Create a class to be sent to the API. 
                ClaseEnt claseDepreciada = new ClaseEnt();
                claseDepreciada.asientoDepreciacion = new AsientoEnt();

                claseDepreciada.idClase = idClase;
                claseDepreciada.asientoDepreciacion.descripcion = descripcionAsiento;

                //Perform request to the API. 
                string url = "https://localhost:44328/api/ejecutarDepreciacionClase";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                JsonContent body = JsonContent.Create(claseDepreciada);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<AuxiliarEnt>>().Result;
                }

                return new List<AuxiliarEnt>();
            }
        }

        public int crearValidacionClase(ClaseEnt nuevaValidacion)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/crearValidacionClase";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                JsonContent body = JsonContent.Create(nuevaValidacion);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;

            }
        }

    }
}
