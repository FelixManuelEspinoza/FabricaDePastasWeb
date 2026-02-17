using FabricaPastas.BD.Data.Entity;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FabricaPastas.Server.Servicios
{
    public class PdfServicio
    {
        public byte[] GenerarRecibo(Pedido pedido)
        {
            // Recomendado por QuestPDF para evitar warning de licencia
            QuestPDF.Settings.License = LicenseType.Community;

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    // ===== HEADER =====
                    page.Header().Column(header =>
                    {
                        header.Spacing(5);

                        header.Item().Text("Recibo de Pedido - La Nonna Pastas")
                            .FontSize(18).SemiBold();

                        header.Item().Text($"Código: {pedido.CodigoPedido}")
                            .FontSize(12);

                        header.Item().LineHorizontal(1);
                    });

                    // ===== CONTENT =====
                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Fecha pedido: {pedido.FechaPedido:dd/MM/yyyy}");
                        col.Item().Text($"Fecha entrega: {(pedido.FechaEntrega.HasValue ? pedido.FechaEntrega.Value.ToString("dd/MM/yyyy") : "-")}");
                        col.Item().Text($"Cliente (Usuario_Id): {pedido.Usuario_Id}");
                        col.Item().Text($"Total: $ {pedido.Total:n2}")
                            .FontSize(13).SemiBold();

                        col.Item().LineHorizontal(1);

                        col.Item().Text("Detalles")
                            .FontSize(13).SemiBold();

                        if (pedido.Detalles != null && pedido.Detalles.Any())
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();     // Nombre
                                    columns.ConstantColumn(60);   // Cant
                                    columns.ConstantColumn(90);   // Precio
                                    columns.ConstantColumn(90);   // Subtotal
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(CellHeader).Text("Producto");
                                    h.Cell().Element(CellHeader).AlignCenter().Text("Cant.");
                                    h.Cell().Element(CellHeader).AlignRight().Text("Precio");
                                    h.Cell().Element(CellHeader).AlignRight().Text("Subtotal");
                                });

                                foreach (var d in pedido.Detalles)
                                {
                                    table.Cell().Element(CellBody).Text(d.Nombre ?? "");
                                    table.Cell().Element(CellBody).AlignCenter().Text(d.Cantidad.ToString());
                                    table.Cell().Element(CellBody).AlignRight().Text($"$ {d.Precio_Unitario:n2}");
                                    table.Cell().Element(CellBody).AlignRight().Text($"$ {d.Subtotal:n2}");
                                }
                            });
                        }
                        else
                        {
                            col.Item().Text("No hay detalles cargados.").Italic();
                        }

                        col.Item().LineHorizontal(1);

                        col.Item().Text("¡Gracias por tu compra!")
                            .FontSize(12);
                    });

                    // ===== FOOTER =====
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Generado el ");
                        text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });
                });
            }).GeneratePdf();

            return pdfBytes;

            // Helpers de estilo
            static IContainer CellHeader(IContainer container) =>
                container
                    .Background(Colors.Grey.Lighten3)
                    .PaddingVertical(5)
                    .PaddingHorizontal(5)
                    .DefaultTextStyle(x => x.SemiBold());

            static IContainer CellBody(IContainer container) =>
                container
                    .PaddingVertical(4)
                    .PaddingHorizontal(5);
        }
    }
}
