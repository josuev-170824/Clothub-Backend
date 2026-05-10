-- Migración: Protección contra fuerza bruta en verificación de email
-- Fecha: 2026-05-10
-- Agrega contador de intentos fallidos al verificar el código OTP

ALTER TABLE usuarios
ADD COLUMN intentos_fallidos_verificacion INT NOT NULL DEFAULT 0;
