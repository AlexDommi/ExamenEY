using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VO;

namespace CapaDatos
{
    public class DALRegistroUsuarios
    {
        public static int iInsertarUsuario(string sNombre, string sUsuario, string sPassword, string sEmail, string sEstatus, DateTime dtFechaAlta, DateTime dtFechaModificacion)
        {

            return MetodoDatos.iExecuteNonQuery(
                                                "SP_INSERTAR_USUARIO",
                                                     "@EY_NOMBRE_COMPLETO", sNombre
                                                   , "@EY_USUARIO", sUsuario
                                                   , "@EY_PASSWORD", sPassword
                                                   , "@EY_EMAIL", sEmail
                                                   , "@EY_ESTATUS", sEstatus
                                                   , "@EY_FECHA_ALTA", dtFechaAlta
                                                   , "@EY_FECHA_MODIFICACION", dtFechaModificacion);
        }

        public static int iActualizarUsuario(string sNombre,string sUsuario, string sPassword, string sEmail, string sEstatus, DateTime dtFechaModificacion)
        {
            return MetodoDatos.iExecuteNonQuery(
                                                 "SP_ACTUALIZAR_USUARIO",
                                                     "@EY_NOMBRE_COMPLETO", sNombre
                                                   , "@EY_USUARIO", sUsuario
                                                   , "@EY_PASSWORD", sPassword
                                                   , "@EY_EMAIL", sEmail
                                                   , "@EY_ESTATUS", sEstatus
                                                   , "@EY_FECHA_MODIFICACION", dtFechaModificacion);
        }
        public static List<UsuariosVO> GetListaUsuarios(int? iId)
        {
            try
            {
                DataSet dsUsuarios;

                if (iId != null)
                {
                    dsUsuarios = MetodoDatos.dtExecuteDataSet("SP_LISTA_USUARIOS_ID", "@EY_ID",iId);
                }
                else
                {
                    dsUsuarios = MetodoDatos.dtExecuteDataSet("SP_LISTA_USUARIOS");
                }

                List<UsuariosVO> listaUsuarios = new List<UsuariosVO>();
                foreach (DataRow drUsuarios in dsUsuarios.Tables[0].Rows)
                {
                    listaUsuarios.Add(new UsuariosVO(drUsuarios));
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de clientes: {ex.Message}");
                throw;
            }
        }
    }
}
