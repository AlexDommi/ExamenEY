using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VO;

namespace ExamenJASD.Pages.Usuarios
{
    public class ListaUsuariosModel : PageModel
    {
        private readonly BLLRegistroUsuarios _bllUsuarios = new BLLRegistroUsuarios();

        public JsonResult OnGetListaUsuarios()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioLogueado")))
            {
                Response.Redirect("/Login/Login");
            }

            var ListaUsuarios = _bllUsuarios.GetListaUsuarios();
            return new JsonResult(ListaUsuarios);
        }
        public JsonResult OnGetListaUsuariosId(int iId)
        {
            var ListaUsuarios = _bllUsuarios.GetListaUsuariosById(iId);
            return new JsonResult(ListaUsuarios.FirstOrDefault());
        }

        public IActionResult OnPostGuardarUsuario(UsuariosVO usuario)
        {
            if (usuario.iId == 0)
                _bllUsuarios.iInsertarUsuario(usuario.sNombre, usuario.sUsuario, usuario.sPassword, usuario.sEmail, usuario.sEstatus, usuario.dtFechaAlta,usuario.dtFechaModificacion);
            else
                _bllUsuarios.iActualizarUsuario(usuario.sNombre,usuario.sUsuario,usuario.sPassword,usuario.sEmail,usuario.sEstatus,usuario.dtFechaModificacion);

            return new JsonResult(usuario);
        }
    }
}
