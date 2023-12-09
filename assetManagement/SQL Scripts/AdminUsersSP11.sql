CREATE PROCEDURE ObtenerUsuariosActivos
AS
BEGIN
    SELECT * FROM USUARIO WHERE ESTADO = 1;
END


---------
CREATE PROCEDURE AgregarUsuario
    @NOMBRE VARCHAR(50),
    @CORREO VARCHAR(50),
    @CONTRASENNA VARCHAR(50),
    @ID_ROLE INT
AS
BEGIN
    INSERT INTO USUARIO (NOMBRE, CORREO, CONTRASENNA, ID_ROLE, ESTADO, ESTADO_CONTRASENNA)
    VALUES (@NOMBRE, @CORREO, @CONTRASENNA, @ID_ROLE, 1, 1);
END


--------------
CREATE PROCEDURE EliminarUsuario
    @ID_USUARIO INT
AS
BEGIN
    DELETE FROM USUARIO
    WHERE ID_USUARIO = @ID_USUARIO;
END



------------------
CREATE PROCEDURE EditarUsuario
    @ID_USUARIO INT,
    @NOMBRE VARCHAR(50),
    @CORREO VARCHAR(50),
    @CONTRASENNA VARCHAR(50),
    @ID_ROLE INT
AS
BEGIN
    UPDATE USUARIO
    SET NOMBRE = @NOMBRE, CORREO = @CORREO, CONTRASENNA = @CONTRASENNA, ID_ROLE = @ID_ROLE
    WHERE ID_USUARIO = @ID_USUARIO;
END


--------------------
use ASSET_MANAGEMENT;

DROP PROCEDURE IF EXISTS CambiarContrasena
GO
CREATE PROCEDURE CambiarContrasena
    @Correo VARCHAR(50),
    @CodigoVerificacion VARCHAR(6), -- Supongamos un código de 6 caracteres
    @NuevaContrasena VARCHAR(50)
AS
BEGIN
    DECLARE @CodigoCorrecto VARCHAR(6)

    -- Lógica para generar un código de verificación y almacenarlo en la base de datos
    SET @CodigoCorrecto = '123456' -- Código de ejemplo, deberías generar uno dinámicamente y almacenarlo

    IF @CodigoVerificacion = @CodigoCorrecto
    BEGIN
        UPDATE USUARIO
        SET CONTRASENNA = @NuevaContrasena
        WHERE CORREO = @Correo

        -- Resto de tu lógica, por ejemplo, desactivar el código de verificación usado
        -- y cualquier otro proceso que necesites realizar

        SELECT 'Contraseña actualizada exitosamente' AS Resultado
    END
    ELSE
    BEGIN
        SELECT 'Código de verificación incorrecto' AS Resultado
    END
END

