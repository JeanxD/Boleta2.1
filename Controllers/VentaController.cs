using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FloanyVisionWeb.Models;

public class VentaController : Controller
{
    private readonly ApplicationDbContext _context;

    public VentaController(ApplicationDbContext context)
    {
        _context = context;
        
        // SEED DATA: Cargamos los tres tipos de documentos automáticamente al iniciar
        if (!_context.Clientes.Any())
        {
            _context.Clientes.AddRange(
                // 1. Caso DNI (8 dígitos)
                new Cliente { Id = 1, Nombre = "SANTAMARIA SILUPU GIANFRANK", NumeroDocumento = "75539814", Direccion = "AV. LAS FLORES 456 - SJL" },
                
                // 2. Caso RUC (11 dígitos)
                new Cliente { Id = 2, Nombre = "DISTRIBUIDORA OPTICA LIMA S.A.C.", NumeroDocumento = "20601234567", Direccion = "AV. PRÓCERES DE LA INDEPENDENCIA 1234 - SJL" },
                
                // 3. Caso Carnet de Extranjería - CE (9 dígitos)
                new Cliente { Id = 3, Nombre = "JEAN PIERRE SILVA GONZALEZ", NumeroDocumento = "001234567", Direccion = "JR. CHURCAMPA 321 - LIMA" }
            );
            _context.SaveChanges();
        }
    }

    // 1. BUSCADOR POR DOCUMENTO (AJAX) - Jala cualquier tipo de documento
    [HttpGet]
    public IActionResult BuscarCliente(string documento)
    {
        try
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.NumeroDocumento == documento);
            if (cliente == null)
            {
                return Json(null); // Si no existe, activa la alerta en el navegador
            }
            return Json(new { nombre = cliente.Nombre, direccion = cliente.Direccion });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 2. PROCESAR VENTA (Auto-incrementa boleta y guarda en memoria)
    [HttpPost]
    public IActionResult EmitirBoletaSUNAT([FromBody] VentaRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Dni))
            {
                return Json(new { success = false, message = "Datos del comprobante inválidos." });
            }

            // Si es un cliente nuevo (no está en el Seed Data), lo guarda automáticamente
            var clienteExistente = _context.Clientes.FirstOrDefault(c => c.NumeroDocumento == request.Dni);
            if (clienteExistente == null)
            {
                var nuevoCliente = new Cliente
                {
                    Nombre = request.NombreCliente,
                    NumeroDocumento = request.Dni,
                    Direccion = request.Direccion
                };
                _context.Clientes.Add(nuevoCliente);
            }

            // Lógica del correlativo autoincremental
            int ultimoNumero = _context.Ventas.Any() ? _context.Ventas.Max(v => v.NumeroBoleta) : 0;
            int nuevoNumero = ultimoNumero + 1;

            var nuevaVenta = new Venta
            {
                NumeroBoleta = nuevoNumero,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.Dni
            };
            _context.Ventas.Add(nuevaVenta);
            _context.SaveChanges();

            string correlativoString = nuevoNumero.ToString().PadLeft(8, '0');

            return Json(new { 
                success = true, 
                nuevoNumeroBoleta = correlativoString, 
                message = "Comprobante emitido con éxito." 
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error interno: " + ex.Message });
        }
    }
}

public class VentaRequest
{
    public string TipoDocumento { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
}