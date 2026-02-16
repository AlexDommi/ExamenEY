using Microsoft.AspNetCore.Identity;
using CapaDatos;
using VO;

namespace CapaNegocio
{
    public class BLLlogin
    {
        public loginVO Login(string sUsuario, string sPassword)
        {

            loginVO usuario = DALLogin.GetUsuarioByUserPassword(sUsuario);

            if (usuario == null || string.IsNullOrEmpty(usuario.sPassword))
            {
                return new loginVO(); 
            }


            EncriptaPassword encripta = new EncriptaPassword();
            bool valid = encripta.bVerifyPassword(usuario.sPassword, sPassword);

            if (valid)
            {
                return usuario; 
            }
            else
            {
                return new loginVO();
            }
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
