CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE etiquetas (
    id uuid NOT NULL,
    nombre character varying(100) NOT NULL,
    CONSTRAINT "PK_etiquetas" PRIMARY KEY (id)
);

CREATE TABLE usuarios (
    id uuid NOT NULL,
    nombre character varying(100) NOT NULL,
    apellidos character varying(100) NOT NULL,
    email character varying(255) NOT NULL,
    password_hash character varying(255),
    email_verificado boolean NOT NULL DEFAULT FALSE,
    token_verificacion_email character varying(100),
    proveedor_auth character varying(20) NOT NULL,
    google_id character varying(100),
    rol character varying(20) NOT NULL,
    fecha_registro timestamp with time zone NOT NULL,
    CONSTRAINT "PK_usuarios" PRIMARY KEY (id)
);

CREATE TABLE tiendas (
    id uuid NOT NULL,
    usuario_id uuid NOT NULL,
    nombre character varying(100) NOT NULL,
    logo_url character varying(500),
    descripcion character varying(1000),
    provincia character varying(50) NOT NULL,
    telefono character varying(20) NOT NULL,
    telefono_verificado boolean NOT NULL DEFAULT FALSE,
    instagram character varying(100),
    whatsapp character varying(20),
    reglas_de_la_tienda character varying(2000),
    acepta_apartados boolean NOT NULL DEFAULT FALSE,
    porcentaje_apartado numeric(5,2),
    tiempo_limite_apartado_horas integer,
    plan character varying(20) NOT NULL,
    estado character varying(20) NOT NULL,
    stripe_account_id character varying(100),
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_tiendas" PRIMARY KEY (id),
    CONSTRAINT "FK_tiendas_usuarios_usuario_id" FOREIGN KEY (usuario_id) REFERENCES usuarios (id) ON DELETE RESTRICT
);

CREATE TABLE prendas (
    id uuid NOT NULL,
    tienda_id uuid NOT NULL,
    nombre character varying(150) NOT NULL,
    marca character varying(100),
    tipo_prenda character varying(100) NOT NULL,
    talla character varying(20) NOT NULL,
    medida_cintura numeric(6,2),
    medida_largo numeric(6,2),
    medida_ancho numeric(6,2),
    condicion character varying(20) NOT NULL,
    descripcion character varying(1000),
    precio numeric(10,2) NOT NULL,
    estado character varying(20) NOT NULL,
    vistas integer NOT NULL DEFAULT 0,
    fecha_publicacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_prendas" PRIMARY KEY (id),
    CONSTRAINT "FK_prendas_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE RESTRICT
);

CREATE TABLE reportes (
    id uuid NOT NULL,
    comprador_id uuid NOT NULL,
    tienda_id uuid NOT NULL,
    motivo character varying(500) NOT NULL,
    estado character varying(20) NOT NULL,
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_reportes" PRIMARY KEY (id),
    CONSTRAINT "FK_reportes_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_reportes_usuarios_comprador_id" FOREIGN KEY (comprador_id) REFERENCES usuarios (id) ON DELETE RESTRICT
);

CREATE TABLE seguimientos (
    id uuid NOT NULL,
    usuario_id uuid NOT NULL,
    tienda_id uuid,
    etiqueta_id uuid,
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_seguimientos" PRIMARY KEY (id),
    CONSTRAINT "FK_seguimientos_etiquetas_etiqueta_id" FOREIGN KEY (etiqueta_id) REFERENCES etiquetas (id) ON DELETE CASCADE,
    CONSTRAINT "FK_seguimientos_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE CASCADE,
    CONSTRAINT "FK_seguimientos_usuarios_usuario_id" FOREIGN KEY (usuario_id) REFERENCES usuarios (id) ON DELETE CASCADE
);

CREATE TABLE apartados (
    id uuid NOT NULL,
    comprador_id uuid NOT NULL,
    prenda_id uuid NOT NULL,
    monto_pagado numeric(10,2) NOT NULL,
    monto_restante numeric(10,2) NOT NULL,
    fecha_limite timestamp with time zone NOT NULL,
    estado character varying(20) NOT NULL,
    stripe_payment_intent_id character varying(100),
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_apartados" PRIMARY KEY (id),
    CONSTRAINT "FK_apartados_prendas_prenda_id" FOREIGN KEY (prenda_id) REFERENCES prendas (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_apartados_usuarios_comprador_id" FOREIGN KEY (comprador_id) REFERENCES usuarios (id) ON DELETE RESTRICT
);

CREATE TABLE etiquetas_prenda (
    prenda_id uuid NOT NULL,
    etiqueta_id uuid NOT NULL,
    CONSTRAINT "PK_etiquetas_prenda" PRIMARY KEY (prenda_id, etiqueta_id),
    CONSTRAINT "FK_etiquetas_prenda_etiquetas_etiqueta_id" FOREIGN KEY (etiqueta_id) REFERENCES etiquetas (id) ON DELETE CASCADE,
    CONSTRAINT "FK_etiquetas_prenda_prendas_prenda_id" FOREIGN KEY (prenda_id) REFERENCES prendas (id) ON DELETE CASCADE
);

CREATE TABLE fotos_prenda (
    id uuid NOT NULL,
    prenda_id uuid NOT NULL,
    url character varying(500) NOT NULL,
    orden integer NOT NULL,
    CONSTRAINT "PK_fotos_prenda" PRIMARY KEY (id),
    CONSTRAINT "FK_fotos_prenda_prendas_prenda_id" FOREIGN KEY (prenda_id) REFERENCES prendas (id) ON DELETE CASCADE
);

CREATE TABLE pedidos (
    id uuid NOT NULL,
    comprador_id uuid NOT NULL,
    tienda_id uuid NOT NULL,
    prenda_id uuid NOT NULL,
    monto_total numeric(10,2) NOT NULL,
    monto_vendedor numeric(10,2) NOT NULL,
    comision numeric(10,2) NOT NULL,
    tipo_envio character varying(30) NOT NULL,
    costo_envio numeric(10,2) NOT NULL,
    provincia character varying(50) NOT NULL,
    canton character varying(100) NOT NULL,
    distrito character varying(100) NOT NULL,
    direccion_exacta character varying(500) NOT NULL,
    nombre_destinatario character varying(200) NOT NULL,
    telefono_destinatario character varying(20) NOT NULL,
    estado character varying(20) NOT NULL,
    codigo_rastreo character varying(100),
    stripe_payment_intent_id character varying(100),
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_pedidos" PRIMARY KEY (id),
    CONSTRAINT "FK_pedidos_prendas_prenda_id" FOREIGN KEY (prenda_id) REFERENCES prendas (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_pedidos_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_pedidos_usuarios_comprador_id" FOREIGN KEY (comprador_id) REFERENCES usuarios (id) ON DELETE RESTRICT
);

CREATE TABLE resenas (
    id uuid NOT NULL,
    comprador_id uuid NOT NULL,
    tienda_id uuid NOT NULL,
    pedido_id uuid NOT NULL,
    calificacion integer NOT NULL,
    comentario character varying(1000),
    fecha_creacion timestamp with time zone NOT NULL,
    CONSTRAINT "PK_resenas" PRIMARY KEY (id),
    CONSTRAINT "FK_resenas_pedidos_pedido_id" FOREIGN KEY (pedido_id) REFERENCES pedidos (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_resenas_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_resenas_usuarios_comprador_id" FOREIGN KEY (comprador_id) REFERENCES usuarios (id) ON DELETE RESTRICT
);

CREATE INDEX "IX_apartados_comprador_id" ON apartados (comprador_id);

CREATE UNIQUE INDEX "IX_apartados_prenda_id_estado" ON apartados (prenda_id, estado) WHERE estado = 'Activo';

CREATE UNIQUE INDEX "IX_etiquetas_nombre" ON etiquetas (nombre);

CREATE INDEX "IX_etiquetas_prenda_etiqueta_id" ON etiquetas_prenda (etiqueta_id);

CREATE UNIQUE INDEX "IX_fotos_prenda_prenda_id_orden" ON fotos_prenda (prenda_id, orden);

CREATE INDEX "IX_pedidos_comprador_id" ON pedidos (comprador_id);

CREATE INDEX "IX_pedidos_estado" ON pedidos (estado);

CREATE INDEX "IX_pedidos_prenda_id" ON pedidos (prenda_id);

CREATE INDEX "IX_pedidos_tienda_id" ON pedidos (tienda_id);

CREATE INDEX "IX_prendas_estado" ON prendas (estado);

CREATE INDEX "IX_prendas_fecha_publicacion" ON prendas (fecha_publicacion);

CREATE INDEX "IX_prendas_tienda_id" ON prendas (tienda_id);

CREATE INDEX "IX_reportes_comprador_id" ON reportes (comprador_id);

CREATE INDEX "IX_reportes_fecha_creacion" ON reportes (fecha_creacion);

CREATE INDEX "IX_reportes_tienda_id_estado" ON reportes (tienda_id, estado);

CREATE INDEX "IX_resenas_comprador_id" ON resenas (comprador_id);

CREATE UNIQUE INDEX "IX_resenas_pedido_id" ON resenas (pedido_id);

CREATE INDEX "IX_resenas_tienda_id" ON resenas (tienda_id);

CREATE INDEX "IX_seguimientos_etiqueta_id" ON seguimientos (etiqueta_id);

CREATE INDEX "IX_seguimientos_tienda_id" ON seguimientos (tienda_id);

CREATE UNIQUE INDEX "IX_seguimientos_usuario_id_etiqueta_id" ON seguimientos (usuario_id, etiqueta_id) WHERE etiqueta_id IS NOT NULL;

CREATE UNIQUE INDEX "IX_seguimientos_usuario_id_tienda_id" ON seguimientos (usuario_id, tienda_id) WHERE tienda_id IS NOT NULL;

CREATE UNIQUE INDEX "IX_tiendas_usuario_id" ON tiendas (usuario_id);

CREATE UNIQUE INDEX "IX_usuarios_email" ON usuarios (email);

CREATE UNIQUE INDEX "IX_usuarios_google_id" ON usuarios (google_id) WHERE google_id IS NOT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260502183518_InitialCreate', '10.0.7');

COMMIT;

