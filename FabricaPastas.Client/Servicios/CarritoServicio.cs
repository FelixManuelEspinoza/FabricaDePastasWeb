using FabricaPastas.Shared.DTO;

namespace FabricaPastas.Client.Servicios
{
    public class CarritoServicio
    {
        private List<CarritoDTO> _items = new();

        public IReadOnlyList<CarritoDTO> Items => _items.AsReadOnly();

        // 🔹 Método de pago seleccionado (por defecto: efectivo)
        public string MetodoPago { get; private set; } = "Efectivo";

        // 🔹 Observaciones opcionales del cliente
        public string? Observaciones { get; private set; }

        #region Métodos principales del carrito

        public void Agregar(CarritoDTO item)
        {
            var existente = _items.FirstOrDefault(p => p.ProductoId == item.ProductoId);
            if (existente != null)
            {
                existente.Cantidad += item.Cantidad;
            }
            else
            {
                _items.Add(item);
            }
        }

        public void Eliminar(int idProducto)
        {
            var item = _items.FirstOrDefault(p => p.ProductoId == idProducto);
            if (item != null)
                _items.Remove(item);
        }

        public void Vaciar()
        {
            _items.Clear();
            MetodoPago = "Efectivo";
            Observaciones = null;
        }

        public decimal ObtenerTotal() => _items.Sum(p => p.Subtotal);

        #endregion

        #region Métodos adicionales

        /// <summary>
        /// Permite establecer el método de pago (Efectivo o Transferencia)
        /// </summary>
        public void AsignarMetodoPago(string metodo)
        {
            if (metodo == "Efectivo" || metodo == "Transferencia")
                MetodoPago = metodo;
        }

        /// <summary>
        /// Permite agregar observaciones del pedido (opcional)
        /// </summary>
        public void AsignarObservaciones(string? texto)
        {
            Observaciones = texto;
        }

        /// <summary>
        /// Convierte los datos del carrito en un objeto pedido listo para enviar al servidor
        /// </summary>
        public CrearPedidoDTO GenerarPedidoDTO()
        {
            return new CrearPedidoDTO
            {
                Fecha_Pedido = DateTime.Now,
                Fecha_Entrega = DateTime.Now.AddDays(2),
                Total = ObtenerTotal(),
                MetodoPago = MetodoPago,
                Observaciones = Observaciones,
                Productos = _items.Select(item => new CrearDetalle_PedidoDTO
                {
                    Nombre = item.Nombre,
                    Cantidad = item.Cantidad,
                    Precio_Unitario = item.PrecioUnitario
                }).ToList()
            };
        }

        public void ActualizarCantidad(int idProducto, int nuevaCantidad)
        {
            var item = _items.FirstOrDefault(p => p.ProductoId == idProducto);
            if (item != null)
            {
                item.Cantidad = nuevaCantidad;
            }
        }

        #endregion
    }
}
