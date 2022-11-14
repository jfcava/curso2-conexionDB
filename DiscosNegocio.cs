using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pictureBox
{
    internal class DiscosNegocio
    {
        public List<Discos> listar()
        {
            List<Discos> lista = new List<Discos>();

            // Necesito conectarme a algun lado...
            SqlConnection conexion = new SqlConnection();

            // Necesito realizar acciones...
            SqlCommand comando = new SqlCommand();

            // Cuando realizo la conexion voy a obtener un set de datos, lo voy a albergar en un lector datareader...
            // No voy a generar instancia, porque cuando realizo la lectura obtengo un objeto de tipo datareader...
            SqlDataReader lector;

            try
            {
                // Configuro los objetos creados...

                // Primero la cadena de conexion...
                // En "server" iria la IP de la base de datos.. si es local, se puede poner el punto
                // En "database" el nombre de la base de datos
                // Integrated security es para indicar que la seguridad de la base de datos esta por Windows Authentication
                // Si tuviera un usuario y contrasena pondria integrated security=false; user=blabla; password=blabla

                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";

                // Configuro el comando para realizar la lectura

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa From DISCOS";

                // Le indico que ejecute ese comando en esta conexion que declare...
                comando.Connection = conexion;

                // Abro la conexion...
                conexion.Open();

                // Realizo la lectura
                lector = comando.ExecuteReader();

                // lector.Read() me devuelve true si hay un registro a continuacion...
                // y tambien me va a posicionar el puntero en la siguiente posicion...
                while (lector.Read())
                {
                    Discos aux = new Discos();

                    // Empiezo a cargar el objeto con los datos del registro que me devuelve en la primera vuelta del while
                    // El cero sale del orden en el que cargue la consulta.. Select Id (0), Titulo (1), etc...
                    aux.Id = lector.GetInt32(0);
                    // Otra manera...
                    // Esto devuelve un objeto.. por eso lo casteo como string...
                    aux.Titulo = (string)lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];
                    aux.UrlImagen = (string)lector["UrlImagenTapa"];

                    // Agrego esos objetos a la lista...
                    lista.Add(aux);
                }

                // Cierro la conexion...
                conexion.Close();

                // Devuelvo la lista...
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
