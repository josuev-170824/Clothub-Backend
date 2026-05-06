START TRANSACTION;
ALTER TABLE tiendas DROP COLUMN plan;

ALTER TABLE tiendas DROP COLUMN stripe_account_id;

ALTER TABLE pedidos DROP COLUMN comision;

ALTER TABLE pedidos DROP COLUMN monto_vendedor;

ALTER TABLE pedidos DROP COLUMN stripe_payment_intent_id;

ALTER TABLE apartados DROP COLUMN stripe_payment_intent_id;

CREATE TABLE suscripciones (
    id uuid NOT NULL,
    tienda_id uuid NOT NULL,
    estado character varying(20) NOT NULL,
    fecha_inicio_prueba timestamp with time zone NOT NULL,
    fecha_fin_prueba timestamp with time zone NOT NULL,
    fecha_inicio_suscripcion timestamp with time zone,
    fecha_vencimiento timestamp with time zone,
    onvo_subscription_id character varying(100),
    CONSTRAINT "PK_suscripciones" PRIMARY KEY (id),
    CONSTRAINT "FK_suscripciones_tiendas_tienda_id" FOREIGN KEY (tienda_id) REFERENCES tiendas (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_suscripciones_tienda_id" ON suscripciones (tienda_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260504125540_SuscripcionYNuevoModeloNegocio', '10.0.7');

COMMIT;

