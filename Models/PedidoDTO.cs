namespace FloanyVisionWeb.Models
{
    public class PedidoDTO
    {
        // El signo '?' indica que aceptan valores nulos temporalmente
        public string? Dni { get; set; }
        public string? NombreCliente { get; set; }
        public string? Direccion { get; set; }
        public string? ProductoDesc { get; set; }
        
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal TotalPagar { get; set; }
    }
}