namespace FabricaPastas.Shared.DTO
{
    public class DescuentoActualDTO
    {
        public int UsuarioId { get; set; }
        public int CantidadPedidos { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public string? PromocionTitulo { get; set; }
        public int? PromocionId { get; set; }
    }
}
