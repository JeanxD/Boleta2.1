using Microsoft.EntityFrameworkCore;
using FloanyVisionWeb.Models;
namespace FloanyVisionWeb.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int NumeroBoleta { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        // ... el resto de tus propiedades
    }
}