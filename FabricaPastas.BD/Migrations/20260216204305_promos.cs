using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricaPastas.BD.Migrations
{
    /// <inheritdoc />
    public partial class promos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Estado_Pedido_Id",
                table: "Pedido",
                column: "Estado_Pedido_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Forma_Pago_Id",
                table: "Pedido",
                column: "Forma_Pago_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Metodo_Entrega_Id",
                table: "Pedido",
                column: "Metodo_Entrega_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Rango_Promocion_Id_Tipo_Cliente_Id_MinPedidos_MaxPedidos",
                table: "Promocion_Rango",
                columns: new[] { "Promocion_Id", "Tipo_Cliente_Id", "MinPedidos", "MaxPedidos" });

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Rango_Tipo_Cliente_Id",
                table: "Promocion_Rango",
                column: "Tipo_Cliente_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Estado_Pedido_Estado_Pedido_Id",
                table: "Pedido",
                column: "Estado_Pedido_Id",
                principalTable: "Estado_Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Forma_Pago_Forma_Pago_Id",
                table: "Pedido",
                column: "Forma_Pago_Id",
                principalTable: "Forma_Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Metodo_Entrega_Metodo_Entrega_Id",
                table: "Pedido",
                column: "Metodo_Entrega_Id",
                principalTable: "Metodo_Entrega",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Usuario_Usuario_Id",
                table: "Pedido",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Estado_Pedido_Estado_Pedido_Id",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Forma_Pago_Forma_Pago_Id",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Metodo_Entrega_Metodo_Entrega_Id",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Usuario_Usuario_Id",
                table: "Pedido");

            migrationBuilder.DropTable(
                name: "Promocion_Rango");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_Estado_Pedido_Id",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_Forma_Pago_Id",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_Metodo_Entrega_Id",
                table: "Pedido");
        }
    }
}
