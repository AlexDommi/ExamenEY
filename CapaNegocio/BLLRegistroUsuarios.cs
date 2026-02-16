using CapaDatos;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VO;

namespace CapaNegocio
{
    public class BLLRegistroUsuarios
    {
        public int iInsertarUsuario(string sNombre, string sUsuario, string sPassword, string sEmail, string sEstatus, DateTime dtFechaAlta, DateTime dtFechaModificacion)
        {
            EncriptaPassword encriptar = new EncriptaPassword();
            string sPasswordEncriptado = encriptar.sHashPassword(sPassword);

            return DALRegistroUsuarios.iInsertarUsuario(sNombre, sUsuario, sPasswordEncriptado, sEmail, sEstatus, dtFechaAlta, dtFechaModificacion);
        }

        public int iActualizarUsuario(string sNombre,string sUsuario, string sPassword, string sEmail, string sEstatus, DateTime dtFechaModificacion)
        {
            if (!string.IsNullOrEmpty(sPassword))
            {
                EncriptaPassword encriptar = new EncriptaPassword();

                sPassword = encriptar.sHashPassword(sPassword);
            }

            return DALRegistroUsuarios.iActualizarUsuario(sNombre, sUsuario, sPassword, sEmail, sEstatus, dtFechaModificacion);
        }
        public List<UsuariosVO> GetListaUsuarios()
        {
            return DALRegistroUsuarios.GetListaUsuarios(null);
        }

        public List<UsuariosVO> GetListaUsuariosById(int iId)
        {
            return DALRegistroUsuarios.GetListaUsuarios(iId);
        }
        public class EncriptaPassword
        {
            private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

            public string sHashPassword(string password)
            {
                return _hasher.HashPassword(null, password);
            }

            public bool bVerifyPassword(string sHashedPassword, string sPassword)
            {
                var resultado = _hasher.VerifyHashedPassword(null, sHashedPassword, sPassword);
                return resultado == PasswordVerificationResult.Success;

            }
        }
    }
}
