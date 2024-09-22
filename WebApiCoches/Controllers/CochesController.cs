using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Negocio;
using Entidades;

namespace WebApiCoches.Controllers
{
    [ApiController]
    [Route("api/Coches")]

    public class CochesController : Controller
    {
        private readonly ILogica _iLogica;

        public CochesController(ILogica iLogica)
        {
            _iLogica = iLogica;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(RegistrarCoche))]
        public bool RegistrarCoche([FromBody]Coche P_Coche)
        {
            return _iLogica.RegistrarCoche(P_Coche);
        }

        [HttpGet]
        [Route(nameof(ConsultarCoches))]
        public List<Coche> ConsultarCoches()
        {
            return _iLogica.ConsultarCoches();
        }

        [HttpGet]
        [Route(nameof(ConsultarCochesXMarca))]
        public List<Coche> ConsultarCochesXMarca([FromHeader] string P_Marca)
        {
            return _iLogica.ConsultarCochesXMarca(new Coche { marca = P_Marca });
        }

        [HttpGet]
        [Route(nameof(ConsultarCochesXPlaca))]
        public List<Coche> ConsultarCochesXPlaca([FromHeader] string P_Placa)
        {
            return _iLogica.ConsultarCochesXPlaca(new Coche { placa = P_Placa });
        }

    }
}
