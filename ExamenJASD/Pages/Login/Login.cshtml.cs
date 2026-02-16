using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VO;

namespace ExamenJASD.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly BLLlogin _bllLogin = new BLLlogin();

        [BindProperty]
        public string Usuario { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Mensaje { get; set; }

        // Método que se ejecuta al hacer POST (submit)
        public IActionResult OnPost()
        {
            loginVO usuarioLogueado = _bllLogin.Login(Usuario, Password);

            if (!string.IsNullOrEmpty(usuarioLogueado.sUsuario))
            {
                // Login correcto → redirigir a lista de usuarios
                HttpContext.Session.SetString("UsuarioLogueado", usuarioLogueado.sUsuario);
                return RedirectToPage("/Usuarios/ListaUsuarios");
            }
            else
            {
                // Login incorrecto → mostrar mensaje en la misma página
                Mensaje = "Usuario o contraseña incorrectos";
                return Page();
            }
        }

        // Método que se ejecuta al hacer GET (abrir página)
        public void OnGet()
        {
            // Solo muestra el formulario
        }
    }
}
