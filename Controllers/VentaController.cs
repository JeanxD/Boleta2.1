using Microsoft.AspNetCore.Mvc;
using FloanyVisionWeb.Models;
using System.Threading.Tasks;

namespace FloanyVisionWeb.Controllers
{
    public class VentaController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> EmitirBoletaSUNAT([FromBody] PedidoDTO pedido)
        {
            
            
            // 2. Simularemos que enviamos a SUNAT y nos responde con éxito
            bool exitoSunat = true; 

            if (exitoSunat)
            {
                return Json(new { 
                    success = true, 
                    message = "¡Boleta registrada en BD y aceptada por SUNAT!", 
                    numeroBoleta = "B001-00059657" 
                });
            }
            else
            {
                return Json(new { success = false, message = "Error de conexión con SUNAT" });
            }
        }
    }                          
}