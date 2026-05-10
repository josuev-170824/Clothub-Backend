-- Migración: Verificación de email por código de 6 dígitos
-- Fecha: 2026-05-10
-- Agrega columna para la expiración del token de verificación

ALTER TABLE usuarios
ADD COLUMN fecha_expiracion_token_verificacion TIMESTAMPTZ;
