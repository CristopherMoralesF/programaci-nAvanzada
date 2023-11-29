using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace assetManagementApi.Controllers
{
    public class CuentasContablesController : Controller
    {
        private readonly ASSET_MANAGEMENTContext _context;


        public CuentasContablesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }


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
            var resultado = (from x in _context.CuentaContables
                             where x.IdCuenta == idCuenta
                             select x).FirstOrDefault();

            CuentaContableEnt resultadoCuenta = new CuentaContableEnt();

            if (resultado != null)
            {
                resultadoCuenta.idCuenta = resultado.IdCuenta;
                resultadoCuenta.descripcionCuenta = resultado.DescripcionCuenta;
                resultadoCuenta.categoriaCuenta = new CategoriaCuentaEnt { idCategoria = resultado.IdCategoria };
                resultadoCuenta.totalDebitos = Convert.ToDouble(resultado.TotalDebitos);
                resultadoCuenta.totalCreditos = Convert.ToDouble(resultado.TotalCreditos);
                resultadoCuenta.balance = Convert.ToDouble(resultado.Balance);
                resultadoCuenta.naturaleza = resultado.Naturaleza;

                return resultadoCuenta;

            }

            return null;
        }

        [Route("api/cuentas/validarCuentaContableClase")]
        [HttpGet]
        public Boolean validarCuentaContableClase(string idCuenta)
        {
            var resultado = (from x in _context.ClaseCuenta where x.IdCuenta == idCuenta select x).ToList();


            if (resultado.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

