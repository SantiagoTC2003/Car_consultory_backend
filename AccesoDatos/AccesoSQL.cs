using Dapper;
using Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace AccesoDatos
{
    public class AccesoSQl : IAccesoDatos
    {

        #region Miembro 
        private readonly IConfiguration _iConfiguration;

        #endregion

        #region Constructor
        public AccesoSQl(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        #endregion

        public bool RegistrarCoche(Coche P_Coche)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Marca", P_Coche.marca, DbType.String, ParameterDirection.Input, 25);
            parametros.Add("@Modelo", P_Coche.modelo, DbType.String, ParameterDirection.Input, 25);
            parametros.Add("@GTransmision", P_Coche.transmision, DbType.String, ParameterDirection.Input, 20);
            parametros.Add("@Capacidad", P_Coche.capacidad, DbType.Int32, ParameterDirection.Input, 2);
            parametros.Add("@Placa", P_Coche.placa, DbType.String, ParameterDirection.Input, 15);

            using (var conexionSQLServer = new SqlConnection(_iConfiguration.GetConnectionString("ConexionSQL")))
            {
                return conexionSQLServer.Execute("PA_RegistrarCoche", parametros, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public List<Coche> ConsultarCoches()
        {
            DynamicParameters parametros = new DynamicParameters();

            using (var conexionSQLServer = new SqlConnection(_iConfiguration.GetConnectionString("ConexionSQL")))
            {
                return (List<Coche>)conexionSQLServer.Query<Coche>("PA_ConsultarCoches", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Coche> ConsultarCochesXPlaca(Coche P_Coche)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Placa", P_Coche.placa, DbType.String, ParameterDirection.Input, 5);
            using (var conexionSQLServer = new SqlConnection(_iConfiguration.GetConnectionString("ConexionSQL")))
            {
                return (List<Coche>)conexionSQLServer.Query<Coche>("PA_ConsultarCochesPorPlaca", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Coche> ConsultarCochesXMarca(Coche P_Coche)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Marca", P_Coche.marca, DbType.String, ParameterDirection.Input, 5);
            using (var conexionSQLServer = new SqlConnection(_iConfiguration.GetConnectionString("ConexionSQL")))
            {
                return (List<Coche>)conexionSQLServer.Query<Coche>("PA_ConsultarCochesPorMarca", parametros, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
