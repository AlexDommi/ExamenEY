using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VO;

namespace CapaDatos
{
    public class DALLogin
    {
        public static loginVO GetUsuarioByUserPassword(string sUsuario)
        {
            try
            {
                DataSet ds = MetodoDatos.dtExecuteDataSet("SP_OBTENER_USUARIO", "@EY_USUARIO", sUsuario);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    loginVO loginVO = new loginVO(dr);
                    return loginVO;
                }
                else
                {
                    loginVO loginVO = new loginVO();
                    return loginVO;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
