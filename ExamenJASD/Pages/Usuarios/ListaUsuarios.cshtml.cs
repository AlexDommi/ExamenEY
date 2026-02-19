using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VO;

namespace ExamenJASD.Pages.Usuarios
{
    public class ListaUsuariosModel : PageModel
    {
        private readonly BLLRegistroUsuarios _bllUsuarios = new BLLRegistroUsuarios();
     
        public IActionResult OnGetListaUsuarios()
        {
            var usuario = HttpContext.Session.GetString("UsuarioLogueado");

            if (string.IsNullOrEmpty(usuario))
            {
                //return RedirectToPage("/Login/Login");
                //se modifica porque la tabla se genera con JS
                return new JsonResult(new { noAuth = true });
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
