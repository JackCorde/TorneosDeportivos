using System.Data;
using TorneosDeportivos.Models;
using Microsoft.Data.SqlClient;

namespace TorneosDeportivos.Data.Servicios
{
    public class GeneralServicio
    {
        private readonly Contexto _contexto;

        public GeneralServicio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void NumeroIntento(int id)
        {
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("AgregarIntentoFallido", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery(); // Ejecutar la consulta sin retorno de datos
                }
                connection.Close(); // Cerrar la conexión después de terminar
            }
        }

        public void LimpiarNumeroIntento(int id)
        {
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("LimpiarIntentoFallido", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery(); // Ejecutar la consulta sin retorno de datos
                }
                connection.Close(); // Cerrar la conexión después de terminar
            }
        }

    }
}
