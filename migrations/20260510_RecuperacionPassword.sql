-- Migración: Recuperación de contraseña
-- Fecha: 2026-05-10
-- Agrega token y expiración para el flujo de reset de contraseña

ALTER TABLE usuarios
ADD COLUMN token_recuperacion_password VARCHAR(64),
ADD COLUMN fecha_expiracion_token_recuperacion TIMESTAMPTZ;

CREATE INDEX idx_usuarios_token_recuperacion
    ON usuarios (token_recuperacion_password)
    WHERE token_recuperacion_password IS NOT NULL;
