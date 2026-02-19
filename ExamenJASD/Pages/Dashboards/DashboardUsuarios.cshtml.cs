using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExamenJASD.Pages.Dashboards
{
    public class DashboardUsuariosModel : PageModel
    {
        public int iTotalUsuarios { get; set; }
        public int iUsuariosActivos { get; set; }
        public int iUsuariosInactivos { get; set; }

        public void OnGet()
        {
            iTotalUsuarios = 12;
            iUsuariosActivos = 7;
            iUsuariosInactivos = 5;
        }
    }
}
