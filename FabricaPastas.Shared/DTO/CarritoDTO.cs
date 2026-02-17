namespace FabricaPastas.Shared.DTO
{
    public class CarritoDTO
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}
