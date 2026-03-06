using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricaPastas.BD.Migrations
{
    /// <inheritdoc />
    public partial class somee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria_Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Producto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forma_Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metodo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forma_Pago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lista_Precio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista_Precio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metodo_Entrega",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metodo_Entrega", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Fecha_Inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Fecha_Fin = table.Column<DateOnly>(type: "date", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    Descuento_Porcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaProductoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PrecioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_Producto_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria_Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promocion_Rango",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Promocion_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Cliente_Id = table.Column<int>(type: "int", nullable: true),
                    MinPedidos = table.Column<int>(type: "int", nullable: false),
                    MaxPedidos = table.Column<int>(type: "int", nullable: true),
                    Descuento_Porcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion_Rango", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocion_Rango_Promocion_Promocion_Id",
                        column: x => x.Promocion_Id,
                        principalTable: "Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Promocion_Rango_Tipo_Cliente_Tipo_Cliente_Id",
                        column: x => x.Tipo_Cliente_Id,
                        principalTable: "Tipo_Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Cliente_Id = table.Column<int>(type: "int", nullable: false),
                    Lista_Precio_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cuit_Cuil = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Lista_Precio_Lista_Precio_Id",
                        column: x => x.Lista_Precio_Id,
                        principalTable: "Lista_Precio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_Rol_Id",
                        column: x => x.Rol_Id,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Tipo_Cliente_Tipo_Cliente_Id",
                        column: x => x.Tipo_Cliente_Id,
                        principalTable: "Tipo_Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Lista_Precio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lista_Precio_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Lista_Precio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalle_Lista_Precio_Lista_Precio_Lista_Precio_Id",
                        column: x => x.Lista_Precio_Id,
                        principalTable: "Lista_Precio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_Lista_Precio_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promocion_Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Promocion_Id = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocion_Producto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Promocion_Producto_Promocion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promocion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false),
                    Estado_Pedido_Id = table.Column<int>(type: "int", nullable: false),
                    Metodo_Entrega_Id = table.Column<int>(type: "int", nullable: false),
                    Forma_Pago_Id = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacionesCatering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodigoPedido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Estado_PedidoId = table.Column<int>(type: "int", nullable: true),
                    Forma_PagoId = table.Column<int>(type: "int", nullable: true),
                    Metodo_EntregaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Estado_Pedido_Estado_PedidoId",
                        column: x => x.Estado_PedidoId,
                        principalTable: "Estado_Pedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Estado_Pedido_Estado_Pedido_Id",
                        column: x => x.Estado_Pedido_Id,
                        principalTable: "Estado_Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Forma_Pago_Forma_PagoId",
                        column: x => x.Forma_PagoId,
                        principalTable: "Forma_Pago",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Forma_Pago_Forma_Pago_Id",
                        column: x => x.Forma_Pago_Id,
                        principalTable: "Forma_Pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Metodo_Entrega_Metodo_EntregaId",
                        column: x => x.Metodo_EntregaId,
                        principalTable: "Metodo_Entrega",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Metodo_Entrega_Metodo_Entrega_Id",
                        column: x => x.Metodo_Entrega_Id,
                        principalTable: "Metodo_Entrega",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pedido_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio_Unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalle_Pedido_Pedido_Pedido_Id",
                        column: x => x.Pedido_Id,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_Pedido_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Producto_Nombre",
                table: "Categoria_Producto",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Lista_Precio_Lista_Precio_Id_Producto_Id",
                table: "Detalle_Lista_Precio",
                columns: new[] { "Lista_Precio_Id", "Producto_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Lista_Precio_ProductoId",
                table: "Detalle_Lista_Precio",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Pedido_Pedido_Id",
                table: "Detalle_Pedido",
                column: "Pedido_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Pedido_Producto_Id",
                table: "Detalle_Pedido",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Pedido_ProductoId",
                table: "Detalle_Pedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_Pedido_Descripcion",
                table: "Estado_Pedido",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forma_Pago_Metodo",
                table: "Forma_Pago",
                column: "Metodo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lista_Precio_Tipo",
                table: "Lista_Precio",
                column: "Tipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metodo_Entrega_Descripcion",
                table: "Metodo_Entrega",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_CodigoPedido",
                table: "Pedido",
                column: "CodigoPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Estado_Pedido_Id",
                table: "Pedido",
                column: "Estado_Pedido_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Estado_PedidoId",
                table: "Pedido",
                column: "Estado_PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_FechaPedido",
                table: "Pedido",
                column: "FechaPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Forma_Pago_Id",
                table: "Pedido",
                column: "Forma_Pago_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Forma_PagoId",
                table: "Pedido",
                column: "Forma_PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Metodo_Entrega_Id",
                table: "Pedido",
                column: "Metodo_Entrega_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Metodo_EntregaId",
                table: "Pedido",
                column: "Metodo_EntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Usuario_Id",
                table: "Pedido",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_UsuarioId",
                table: "Pedido",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaId",
                table: "Producto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaProductoId",
                table: "Producto",
                column: "CategoriaProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Nombre",
                table: "Producto",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Producto_ProductoId",
                table: "Promocion_Producto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Producto_Promocion_Id_Producto_Id",
                table: "Promocion_Producto",
                columns: new[] { "Promocion_Id", "Producto_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Producto_PromocionId",
                table: "Promocion_Producto",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Rango_Promocion_Id_Tipo_Cliente_Id_MinPedidos_MaxPedidos",
                table: "Promocion_Rango",
                columns: new[] { "Promocion_Id", "Tipo_Cliente_Id", "MinPedidos", "MaxPedidos" });

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Rango_Tipo_Cliente_Id",
                table: "Promocion_Rango",
                column: "Tipo_Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Nombre",
                table: "Rol",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Cliente_Descripcion",
                table: "Tipo_Cliente",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Lista_Precio_Id",
                table: "Usuario",
                column: "Lista_Precio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Rol_Id",
                table: "Usuario",
                column: "Rol_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Tipo_Cliente_Id",
                table: "Usuario",
                column: "Tipo_Cliente_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_Lista_Precio");

            migrationBuilder.DropTable(
                name: "Detalle_Pedido");

            migrationBuilder.DropTable(
                name: "Promocion_Producto");

            migrationBuilder.DropTable(
                name: "Promocion_Rango");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "Estado_Pedido");

            migrationBuilder.DropTable(
                name: "Forma_Pago");

            migrationBuilder.DropTable(
                name: "Metodo_Entrega");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria_Producto");

            migrationBuilder.DropTable(
                name: "Lista_Precio");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Tipo_Cliente");
        }
    }
}
