using Entidades;
using System.Collections.Generic;

namespace Negocio
{
    public interface ILogica
    {

        bool RegistrarCoche(Coche P_Coche);

        List<Coche> ConsultarCoches();

        List<Coche> ConsultarCochesXPlaca(Coche P_Coche);

        List<Coche> ConsultarCochesXMarca(Coche P_Coche);

    }
}
