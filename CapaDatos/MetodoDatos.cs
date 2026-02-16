using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    internal class MetodoDatos
    {
        //Metodo que nos regresa informacion en un DataSet (TABLA O lISTA)
        public static DataSet dtExecuteDataSet(string sStoredProcedure, params object[] parametros)
        {
            DataSet ds = new DataSet();

            //Cadena de conexion
            Configuracion conexion = new();
            
            string sConexion = conexion.ObtenerConexion();
            
            SqlConnection conn = new SqlConnection(sConexion);

            try
            {
                SqlCommand cmd = new SqlCommand(sStoredProcedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sStoredProcedure;

                //Valida que los parametros esten completos
                if (parametros != null && parametros.Length % 2 != 0)
                {
                    throw new ApplicationException("Los parametros deben de ser pares");
                }
                else
                {
                    //Asigna los parametros al command
                    for (int i = 0; i < parametros.Length; i += 2)
                    {
                        cmd.Parameters.AddWithValue(parametros[i].ToString(), parametros[i + 1] ?? DBNull.Value);
                    }

                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    conn.Close();
                }

                //se retorna el dataset
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("L50 - No se pudo conectar MetodoDatos.cs" + ex.Message);
            }
            finally 
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static int iExecuteNonQuery(string sStoredProcedure, params object[] parametros)
        {
            int iExito = 0;

            //Cadena de conexion

            Configuracion conexion = new();

            string sConexion = conexion.ObtenerConexion();


            SqlConnection conn = new SqlConnection(sConexion);
            try
            {
                SqlCommand cmd = new SqlCommand(sStoredProcedure, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sStoredProcedure;

                //Valida que los parametros esten completos
                if (parametros != null && parametros.Length % 2 != 0)
                {
                    throw new ApplicationException("Los parametros deben de ser pares");
                }
                else
                {
                    //Asigna los parametros al command
                    for (int i = 0; i < parametros.Length; i += 2)
                    {
                        cmd.Parameters.AddWithValue(parametros[i].ToString(), parametros[i + 1] ?? DBNull.Value);
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    iExito = 1;
                    conn.Close();
                }
                //se retorna el resultado
                return iExito;
            }
            catch (Exception ex)
            {
                throw new Exception("L 102 - No se pudo conectar MetodoDatos.cs" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
     }
}
