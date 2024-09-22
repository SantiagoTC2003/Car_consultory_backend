using Entidades;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos
{
    public class AccesoMongoDB
    {

        #region Atributos

        //String de conexión con MONGODB 
        //Información deberia venir desde el archivo de configuración y encriptada
        private static readonly string CadenaConexion = @"mongodb+srv://@cluster0.ilhoxwf.mongodb.net/Hotel_Altissia?retryWrites=true&w=majority";

        //Crear instancia para conectar a la base de datos
        private static MongoClient InstanciaBD;
        private static IMongoDatabase BaseDatos;

        //Crear constantes con el nombre de la BD

        private const string NombreBD = "Hotel_Altissia";
        #endregion

        #region Métodos

        #region Privados

        private static void EstablecerConexion()
        {
            try
            {
                //Inicializa las variables de instancia y base de datos
                InstanciaBD = new MongoClient(CadenaConexion);
                BaseDatos = InstanciaBD.GetDatabase(NombreBD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Metodo para agregar una accion en la bitacora de MongoDB
        /// </summary>
        /// <param name="P_Accion">Entidad de tipo Accion</param>
        /// <returns>True = Correcto | False = Error</returns>
        public static bool AgregarAccion(Bitacora P_Accion)
        {
            bool resultado = false;

            try
            {
                EstablecerConexion();
                //Se crea un objeto con el resultado de la base de datos donde se obtiene la colección que se busca
                var Coleccion = BaseDatos.GetCollection<Bitacora>("Bitacora");

                //Ejecuta la accion
                Coleccion.InsertOne(P_Accion);
                resultado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (InstanciaBD != null)
                    InstanciaBD = null;
                if (BaseDatos != null)
                    BaseDatos = null;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo para consultar las acciones en la bitacora de MongoDB
        /// </summary>
        /// <param name="P_Accion">Entidad de tipo Accion</param>
        /// <returns>True = Correcto | False = Error</returns>
        public static Bitacora ConsultarAcciones(Bitacora P_Accion)
        {
            Bitacora resultado = new Bitacora();
            try
            {
                EstablecerConexion();
                //Se crea un objeto con el resultado de la base de datos donde se obtiene la colección que se busca
                var Coleccion = BaseDatos.GetCollection<Bitacora>("Bitacora");

                if (P_Accion.ID_Usuario != 0 && P_Accion.ID_Usuario > 0 && P_Accion.ID.Length == 0)
                {
                    resultado = Coleccion.Find(doc => doc.ID_Usuario.Equals(P_Accion.ID_Usuario)).FirstOrDefault();
                }
                else if (!string.IsNullOrEmpty(P_Accion.ID) && P_Accion.ID.Length > 0 && P_Accion.ID_Usuario == 0)
                {
                    resultado = Coleccion.Find(doc => doc.ID.Equals(P_Accion.ID)).FirstOrDefault();
                }
                else
                    resultado = Coleccion.Find(doc => doc.ID.Equals(P_Accion.ID)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (InstanciaBD != null)
                    InstanciaBD = null;
                if (BaseDatos != null)
                    BaseDatos = null;
            }
            return resultado;
        }
        #endregion

    }
}
