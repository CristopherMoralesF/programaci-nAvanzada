using assetManagement.Entities;
using Proyecto_API.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace assetManagementClassLibrary.Models
{
    internal class ActivoModel
    {
        private readonly object HttpContext;

        public List<AuxiliarEnt> ConsultarAuxiliarActivos()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/activo/consultarAuxiliarActivos";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<List<AuxiliarEnt>>().Result;
                }

                return new List<AuxiliarEnt>();
            }
        }

        public ActivoEnt ConsultarActivo(int idActivo)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://localhost:44328/api/activo/consultarActivo?idActivo={idActivo}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<ActivoEnt>().Result;
                }

                return new ActivoEnt();
            }
        }

        public int EditarValidacion(ValidacionClaseEnt nuevaValidacion)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/modificarValidacionActivo";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                JsonContent body = JsonContent.Create(nuevaValidacion);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int AgregarValidacion(ValidacionClaseEnt nuevaValidacion)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/agregarValidacionActivo";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                JsonContent body = JsonContent.Create(nuevaValidacion);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int CrearActivo(ActivoEnt nuevoActivo)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/activo/crearActivo";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                JsonContent body = JsonContent.Create(nuevoActivo);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int ModificarClase(ActivoEnt modificarActivo)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/activo/modificarClase";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));

                JsonContent body = JsonContent.Create(modificarActivo);

                HttpResponseMessage res = client.PutAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<int>().Result;
                }
                return 0;
            }
        }
    }
}
