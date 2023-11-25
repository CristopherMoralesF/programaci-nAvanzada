using assetManagement.Models;
using Proyecto_API.Entities;
using assetManagementApi.Models;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    [System.Web.Mvc.Authorize]
    public class CuentasContablesController
    {

        private readonly ASSET_MANAGEMENTContext _context;


        public CuentasContablesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }


        CuentaContableModel cuentaContableModel = new CuentaContableModel();

        [Route("api/cuentas/optenerCuentas")]
        [HttpGet]
        public List<CuentaContableEnt> consultarCuentas()
        {
           
                List<CuentaContableEnt> cuentasContables = new List<CuentaContableEnt>();

                foreach (var cuentaContable in _context.CuentaContables.ToList())
                {

                    CategoriaCuentaEnt nuevaCategoria = new CategoriaCuentaEnt();
                    nuevaCategoria.idCategoria = cuentaContable.IdCategoria;

                    cuentasContables.Add(new CuentaContableEnt
                    {

                        idCuenta = cuentaContable.IdCuenta,
                        descripcionCuenta = cuentaContable.DescripcionCuenta,
                        categoriaCuenta = nuevaCategoria,
                        totalDebitos = Convert.ToDouble(cuentaContable.TotalDebitos),
                        totalCreditos = Convert.ToDouble(cuentaContable.TotalCreditos),
                        balance = Convert.ToDouble(cuentaContable.Balance),
                        naturaleza = cuentaContable.Naturaleza
                    });

                }

                return cuentasContables;
        }

        [Route("api/cuentas/crearCuenta")]
        [HttpPost]
        public int crearCuenta(CuentaContableEnt nuevaCuenta)
        {

            CuentaContable cuenta = new CuentaContable();

                cuenta.IdCuenta = nuevaCuenta.idCuenta;
                cuenta.DescripcionCuenta = nuevaCuenta.descripcionCuenta;
                cuenta.IdCategoria = nuevaCuenta.categoriaCuenta.idCategoria;
                cuenta.Naturaleza = nuevaCuenta.naturaleza;
                cuenta.Balance = 0;
                cuenta.TotalDebitos = 0;
                cuenta.TotalCreditos = 0;

                _context.Add(cuenta);
                return _context.SaveChanges();

            }
          

        [Route("api/cuentas/buscarCuenta")]
        [HttpGet]
        public CuentaContableEnt buscarCuenta(string idCuenta)
        {
            return cuentaContableModel.buscarCuenta(idCuenta);
        }

        [Route("api/cuentas/validarCuentaContableClase")]
        [HttpGet]
        public Boolean validarCuentaContableClase(string idCuenta)
        {
            return cuentaContableModel.validarCuentaContableClase(idCuenta);
        }

    }
}
