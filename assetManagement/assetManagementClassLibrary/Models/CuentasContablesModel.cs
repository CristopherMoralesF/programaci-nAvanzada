using Proyecto_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace assetManagementClassLibrary.Models
{
    internal class CuentasContablesModel
    {
        public List<CuentaContableEnt> consultarCuentas()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/cuentas/optenerCuentas";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {

                    return res.Content.ReadFromJsonAsync<List<CuentaContableEnt>>().Result;
                }

                return new List<CuentaContableEnt>();
            }
        }


        public CuentaContableEnt validarCuentaDuplicada(string idCuentaContable)
        {
            using (var client = new HttpClient())
            {

                string url = "https://localhost:44328/api/cuentas/buscarCuenta?idCuenta=" + idCuentaContable;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<CuentaContableEnt>().Result;
                }

                return null;

            }
        }


        public Boolean validarCuentaContableClase(string idCuentaContable)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/cuentas/validarCuentaContableClase?idCuenta=" + idCuentaContable;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<Boolean>().Result;
                }

                return false;

            }
        }

        public int CrearCuentaContable(CuentaContableEnt cuentaContable)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44328/api/cuentas/crearCuenta";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());

                JsonContent body = JsonContent.Create(cuentaContable);

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
