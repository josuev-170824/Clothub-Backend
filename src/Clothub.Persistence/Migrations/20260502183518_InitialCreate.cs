using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "etiquetas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etiquetas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_verificado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    token_verificacion_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    proveedor_auth = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    google_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tiendas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    logo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    provincia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    telefono_verificado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    whatsapp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    reglas_de_la_tienda = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    acepta_apartados = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    porcentaje_apartado = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    tiempo_limite_apartado_horas = table.Column<int>(type: "integer", nullable: true),
                    plan = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    stripe_account_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiendas", x => x.id);
                    table.ForeignKey(
                        name: "FK_tiendas_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prendas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    marca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tipo_prenda = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    talla = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    medida_cintura = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    medida_largo = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    medida_ancho = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    condicion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    precio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    vistas = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    fecha_publicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prendas", x => x.id);
                    table.ForeignKey(
                        name: "FK_prendas_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reportes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    motivo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportes", x => x.id);
                    table.ForeignKey(
                        name: "FK_reportes_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reportes_usuarios_comprador_id",
                        column: x => x.comprador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "seguimientos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: true),
                    etiqueta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seguimientos", x => x.id);
                    table.ForeignKey(
                        name: "FK_seguimientos_etiquetas_etiqueta_id",
                        column: x => x.etiqueta_id,
                        principalTable: "etiquetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seguimientos_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seguimientos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "apartados",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prenda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    monto_restante = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    fecha_limite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    stripe_payment_intent_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apartados", x => x.id);
                    table.ForeignKey(
                        name: "FK_apartados_prendas_prenda_id",
                        column: x => x.prenda_id,
                        principalTable: "prendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_apartados_usuarios_comprador_id",
                        column: x => x.comprador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "etiquetas_prenda",
                columns: table => new
                {
                    prenda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    etiqueta_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etiquetas_prenda", x => new { x.prenda_id, x.etiqueta_id });
                    table.ForeignKey(
                        name: "FK_etiquetas_prenda_etiquetas_etiqueta_id",
                        column: x => x.etiqueta_id,
                        principalTable: "etiquetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_etiquetas_prenda_prendas_prenda_id",
                        column: x => x.prenda_id,
                        principalTable: "prendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fotos_prenda",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    prenda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fotos_prenda", x => x.id);
                    table.ForeignKey(
                        name: "FK_fotos_prenda_prendas_prenda_id",
                        column: x => x.prenda_id,
                        principalTable: "prendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prenda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    monto_total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    monto_vendedor = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    comision = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    tipo_envio = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    costo_envio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    provincia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    canton = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    distrito = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    direccion_exacta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    nombre_destinatario = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    telefono_destinatario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    codigo_rastreo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    stripe_payment_intent_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_prendas_prenda_id",
                        column: x => x.prenda_id,
                        principalTable: "prendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_usuarios_comprador_id",
                        column: x => x.comprador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "resenas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tienda_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pedido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    calificacion = table.Column<int>(type: "integer", nullable: false),
                    comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resenas", x => x.id);
                    table.ForeignKey(
                        name: "FK_resenas_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_resenas_tiendas_tienda_id",
                        column: x => x.tienda_id,
                        principalTable: "tiendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_resenas_usuarios_comprador_id",
                        column: x => x.comprador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apartados_comprador_id",
                table: "apartados",
                column: "comprador_id");

            migrationBuilder.CreateIndex(
                name: "IX_apartados_prenda_id_estado",
                table: "apartados",
                columns: new[] { "prenda_id", "estado" },
                unique: true,
                filter: "estado = 'Activo'");

            migrationBuilder.CreateIndex(
                name: "IX_etiquetas_nombre",
                table: "etiquetas",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_etiquetas_prenda_etiqueta_id",
                table: "etiquetas_prenda",
                column: "etiqueta_id");

            migrationBuilder.CreateIndex(
                name: "IX_fotos_prenda_prenda_id_orden",
                table: "fotos_prenda",
                columns: new[] { "prenda_id", "orden" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_comprador_id",
                table: "pedidos",
                column: "comprador_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_estado",
                table: "pedidos",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_prenda_id",
                table: "pedidos",
                column: "prenda_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_tienda_id",
                table: "pedidos",
                column: "tienda_id");

            migrationBuilder.CreateIndex(
                name: "IX_prendas_estado",
                table: "prendas",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_prendas_fecha_publicacion",
                table: "prendas",
                column: "fecha_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_prendas_tienda_id",
                table: "prendas",
                column: "tienda_id");

            migrationBuilder.CreateIndex(
                name: "IX_reportes_comprador_id",
                table: "reportes",
                column: "comprador_id");

            migrationBuilder.CreateIndex(
                name: "IX_reportes_fecha_creacion",
                table: "reportes",
                column: "fecha_creacion");

            migrationBuilder.CreateIndex(
                name: "IX_reportes_tienda_id_estado",
                table: "reportes",
                columns: new[] { "tienda_id", "estado" });

            migrationBuilder.CreateIndex(
                name: "IX_resenas_comprador_id",
                table: "resenas",
                column: "comprador_id");

            migrationBuilder.CreateIndex(
                name: "IX_resenas_pedido_id",
                table: "resenas",
                column: "pedido_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resenas_tienda_id",
                table: "resenas",
                column: "tienda_id");

            migrationBuilder.CreateIndex(
                name: "IX_seguimientos_etiqueta_id",
                table: "seguimientos",
                column: "etiqueta_id");

            migrationBuilder.CreateIndex(
                name: "IX_seguimientos_tienda_id",
                table: "seguimientos",
                column: "tienda_id");

            migrationBuilder.CreateIndex(
                name: "IX_seguimientos_usuario_id_etiqueta_id",
                table: "seguimientos",
                columns: new[] { "usuario_id", "etiqueta_id" },
                unique: true,
                filter: "etiqueta_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_seguimientos_usuario_id_tienda_id",
                table: "seguimientos",
                columns: new[] { "usuario_id", "tienda_id" },
                unique: true,
                filter: "tienda_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tiendas_usuario_id",
                table: "tiendas",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_google_id",
                table: "usuarios",
                column: "google_id",
                unique: true,
                filter: "google_id IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apartados");

            migrationBuilder.DropTable(
                name: "etiquetas_prenda");

            migrationBuilder.DropTable(
                name: "fotos_prenda");

            migrationBuilder.DropTable(
                name: "reportes");

            migrationBuilder.DropTable(
                name: "resenas");

            migrationBuilder.DropTable(
                name: "seguimientos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "etiquetas");

            migrationBuilder.DropTable(
                name: "prendas");

            migrationBuilder.DropTable(
                name: "tiendas");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
