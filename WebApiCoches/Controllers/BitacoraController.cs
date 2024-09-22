using Entidades;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace WebApiCoches.Controllers
{
    [ApiController]
    [Route("api/Bitacora")]
    public class BitacoraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(AgregarAccion))]
        public string AgregarAccion(Bitacora P_Accion)
        {
            return Logica.AgregarAccion(P_Accion);
        }
        [HttpGet]
        [Route(nameof(ConsultarAccionPorFiltro))]
        public Bitacora ConsultarAccionPorFiltro([FromHeader] string P_ID, [FromHeader] int P_ID_Usuario)
        {
            return Logica.ConsultarAccionPorFiltro(new Bitacora
            {
                ID = P_ID == null ? string.Empty : P_ID,
                ID_Usuario = P_ID_Usuario == 0 ? 0 : P_ID_Usuario
            });
        }

    }
}
