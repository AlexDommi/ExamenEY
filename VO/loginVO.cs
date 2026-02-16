using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VO
{
    public class loginVO
    {

        private string _sUsuario;
        private string _sPassword;

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string sUsuario
        {
            get => _sUsuario;
            set => _sUsuario = value;
        }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string sPassword
        {
            get => _sPassword;
            set => _sPassword = value;
        }

        public loginVO(DataRow dr)
        {
            sUsuario = dr["EY_USUARIO"].ToString();
            sPassword = dr["EY_PASSWORD"].ToString();
           
        }

        public loginVO()
        {
            
            sUsuario = string.Empty;
            sPassword = string.Empty;
           
        }
    }
}
