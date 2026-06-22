using Microsoft.EntityFrameworkCore;
using FloanyVisionWeb.Models; // <--- ¡ESTA LÍNEA ES LA CLAVE!

namespace FloanyVisionWeb.Models // Asegúrate de que el namespace coincida
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}