﻿using Entidades;
using System.Collections.Generic;

namespace AccesoDatos
{
    public interface IAccesoDatos
    {

        bool RegistrarCoche(Coche P_Coche);

        List<Coche> ConsultarCoches();

        List<Coche> ConsultarCochesXPlaca(Coche P_Coche);

        List<Coche> ConsultarCochesXMarca(Coche P_Coche);

    }
}
