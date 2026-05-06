using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SuscripcionYNuevoModeloNegocio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "plan",
                table: "tiendas");

            migrationBuilder.DropColumn(
                name: "stripe_account_id",
                table: "tiendas");

            migrationBuilder.DropColumn(
                name: "comision",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "monto_vendedor",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intent_id",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "stripe_payment_intent_id",
                table: "apartados");

            migrationBuilder.CreateTable(
                name: "suscripciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_inicio_prueba = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_fin_prueba = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_inicio_suscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_vencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    onvo_subscription_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suscripciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_suscripciones_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_suscripciones_tienda_id",
                table: "suscripciones",
                column: "tienda_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suscripciones");

            migrationBuilder.AddColumn<string>(
                name: "plan",
                table: "tiendas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "stripe_account_id",
                table: "tiendas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "comision",
                table: "pedidos",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "monto_vendedor",
                table: "pedidos",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intent_id",
                table: "pedidos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_payment_intent_id",
                table: "apartados",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
