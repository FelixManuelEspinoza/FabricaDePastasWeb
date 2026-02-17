using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricaPastas.BD.Migrations
{
    /// <inheritdoc />
    public partial class descuentos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Promocion_Activa_Fecha_Inicio_Fecha_Fin",
                table: "Promocion");

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento_Porcentaje",
                table: "Promocion",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descuento_Porcentaje",
                table: "Promocion");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Activa_Fecha_Inicio_Fecha_Fin",
                table: "Promocion",
                columns: new[] { "Activa", "Fecha_Inicio", "Fecha_Fin" });
        }
    }
}
