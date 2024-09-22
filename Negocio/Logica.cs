using AccesoDatos;
using Entidades;
using System.Collections.Generic;
using System.Transactions;

namespace Negocio
{
    public class Logica : ILogica
    {

        #region Miembro
        private readonly IAccesoDatos _iAccesoSQL;

        #endregion

        #region Propiedades
        private TransactionOptions TransaccionOptions { get; set; }

        #endregion

        #region Constructor

        public Logica(IAccesoDatos iAccesoSQL)
        {
            _iAccesoSQL = iAccesoSQL;
            TransaccionOptions = new TransactionOptions { Timeout = TransactionManager.DefaultTimeout, IsolationLevel = IsolationLevel.ReadUncommitted };
        }

        #endregion

        #region MONGODB
        public static string AgregarAccion(Bitacora P_Accion)
        {
            string resultado = "Accion registrada correctamente";

            //Regla de negocio - Verificar si el ID de la accion ya existe, en caso de ser asi indicarlo como resultado
            Bitacora encontrado = AccesoMongoDB.ConsultarAcciones(P_Accion);
            if (encontrado == null)//Si no encuentra resultados en la coleccion, significa que puede agregarse a la misma
            {
                AccesoMongoDB.AgregarAccion(P_Accion); // Se envia a registrar la accion
            }
            else resultado = "Accion a registrar con el ID de: " + P_Accion.ID + " ya existe en base de datos";
            return resultado;
        }

        public static Bitacora ConsultarAccionPorFiltro(Bitacora P_Accion)
        {
            return AccesoMongoDB.ConsultarAcciones(P_Accion);
        }

        #endregion

        #region SQL

        public bool RegistrarCoche(Coche P_Coche)
        {
            bool resultado = false;

            //Aqui se inicia la transacción de las operaciones por ejecutarse
            using (TransactionScope objtransacion = new TransactionScope(TransactionScopeOption.Required, TransaccionOptions))
            {
                if (_iAccesoSQL.RegistrarCoche(P_Coche) == true) //Si la ejecucion de guardar la venta fue correcta
                {
                    resultado = true;
                }

                if (resultado) //Si resultado es TRUE, quiere indicar que todo se realizo correctamente
                    objtransacion.Complete(); //Aplica los cambios en BD Real
            }

            return resultado;
        }

        public List<Coche> ConsultarCoches()
        {
            return _iAccesoSQL.ConsultarCoches();
        }

        public List<Coche> ConsultarCochesXPlaca(Coche P_Coche)
        {
            return _iAccesoSQL.ConsultarCochesXPlaca(P_Coche);
        }

        public List<Coche> ConsultarCochesXMarca(Coche P_Coche)
        {
            return _iAccesoSQL.ConsultarCochesXMarca(P_Coche);
        }

        #endregion

    }
}
