using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class Configuracion
    {
        private readonly IConfiguration _Configuration;

        public Configuracion()
        {
            _Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
        }

        public string ObtenerConexion()
        {
            return _Configuration.GetConnectionString("ExamenEY");
        }
    }
}
