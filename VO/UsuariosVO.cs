using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace VO
{
    public class UsuariosVO
    {
        private int _iId;
        private string _sNombre;
        private string _sUsuario;
        private string _sPassword;
        private string _sEmail;
        private string _sEstatus;
        private DateTime _dtFechaAlta;
        private DateTime _dtFechaModificacion;
        private string _sRFC;
        public int iId 
        { 
            get => _iId;
            set => _iId = value;
        }
        
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 250 caracteres")]
        public string sNombre 
        {
            get => _sNombre;
            set => _sNombre = value;
        }

        [Required(ErrorMessage = "El usuario completo es obligatorio")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 50 caracteres")]
        public string sUsuario
        {
            get => _sUsuario;
            set => _sUsuario = value;
        }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{14,}$", ErrorMessage = "La contraseña debe tener mínimo 14 caracteres, mayúscula, minúscula, número y carácter especial")]
        public string sPassword 
        {
            get => _sPassword;
            set => _sPassword = value;
        }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string sEmail 
        { 
            get => _sEmail;
            set => _sEmail = value;
        }

        public string sEstatus 
        { 
            get => _sEstatus;
            set => _sEstatus = value;
        }

        public DateTime dtFechaAlta 
        { 
            get => _dtFechaAlta;
            set => _dtFechaAlta = value;
        }

        public DateTime dtFechaModificacion
        {
            get => _dtFechaModificacion;
            set => _dtFechaModificacion = value;
        }

        public string sRFC
        {
            get => _sRFC;
            set => _sRFC = value;
        }
        public UsuariosVO(DataRow dr)
        {
            iId = Convert.ToInt32(dr["EY_ID"]);
            sNombre = dr["EY_NOMBRE_COMPLETO"].ToString();
            sUsuario = dr["EY_USUARIO"].ToString();
            sPassword = dr["EY_PASSWORD"].ToString();
            sEmail = dr["EY_EMAIL"].ToString();
            sEstatus = dr["EY_ESTATUS"].ToString();
            dtFechaAlta = Convert.ToDateTime(dr["EY_FECHA_ALTA"]);
            dtFechaModificacion = Convert.ToDateTime(dr["EY_FECHA_MODIFICACION"]);
        }

        public UsuariosVO()
        {
            iId = 0;
            sNombre = string.Empty;
            sUsuario = string.Empty;
            sPassword = string.Empty;
            sEmail = string.Empty;
            sEstatus = string.Empty;
            dtFechaAlta = DateTime.Now;
            dtFechaModificacion = DateTime.Now;
        }
    }
}
