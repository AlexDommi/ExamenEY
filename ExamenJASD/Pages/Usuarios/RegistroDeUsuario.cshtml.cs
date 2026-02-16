using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using VO;
using CapaNegocio;
namespace ExamenJASD.Pages.Usuarios
{
    public class RegistroDeUsuarioModel : PageModel
    {
        private readonly BLLRegistroUsuarios _bllRegistroUsuarios = new BLLRegistroUsuarios();

        [BindProperty]
        public UsuariosVO Usuario { get; set; } = new UsuariosVO();

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
                return Page();

            _bllRegistroUsuarios.iInsertarUsuario(
                                                Usuario.sNombre,
                                                Usuario.sUsuario,
                                                Usuario.sPassword,
                                                Usuario.sEmail,
                                                "Activo",
                                                DateTime.Now,
                                                DateTime.Now);
            TempData["Success"] = "Usuario registrado correctamente";
            //string sNombre, string sUsuario, string sPassword, string sEmail, string sEstatus, DateTime dtFechaAlta, DateTime dtFechaModificacion
            return RedirectToPage("/Usuarios/Index");
        }
    }
}
