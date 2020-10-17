USE BDHotel
GO
/*****************************************************/
/*********************HABITACIÓN**********************/
/*****************************************************/
CREATE PROC ListarHabitacion
AS
BEGIN
	SELECT * FROM Habitacion
END
--
EXEC ListarHabitacion
GO
/*****************************************************/
CREATE PROC ListarHabDisponibles
AS
BEGIN
	SELECT * FROM Habitacion
	WHERE Estado = 'Disponible'
END
--
EXEC ListarHabDisponibles
GO
/*****************************************************/
CREATE PROC RegistrarHabitacion
@Numero				varchar(4),
@Piso				varchar(2),
@Descripcion		varchar(255),
@Caracteristicas	varchar(512),
@PrecioDiario		decimal(7,2),
@Estado				varchar(15),
@TipoHabitacion		varchar(20)
AS
BEGIN
	INSERT Habitacion (Numero, Piso, Descripcion, Caracteristicas, PrecioDiario, Estado, TipoHabitacion)
	VALUES (@Numero, @Piso, @Descripcion, @Caracteristicas, @PrecioDiario, @Estado, @TipoHabitacion)
END
--
EXEC RegistrarHabitacion '512', '5', 'Habitación con vista a la plaza', 'Cama matrimonial de 2 plazas, internet wifi y TV por cable', 150, 'Mantenimiento', 'Matrimonial'
GO
/*****************************************************/
CREATE PROC EditarHabitacion
@IdHabitacion		int,
@Numero				varchar(4),
@Piso				varchar(2),
@Descripcion		varchar(255),
@Caracteristicas	varchar(512),
@PrecioDiario		decimal(7,2),
@Estado				varchar(15),
@TipoHabitacion		varchar(20)
AS
BEGIN
	UPDATE Habitacion
	SET Numero = @Numero,
		Piso = @Piso,
		Descripcion = @Descripcion,
		Caracteristicas = @Caracteristicas,
		PrecioDiario = @PrecioDiario,
		Estado = @Estado,
		TipoHabitacion = @TipoHabitacion
	WHERE IdHabitacion = @IdHabitacion
END
--
EXEC EditarHabitacion '1', '512', '5', 'Habitación con vista a la plaza', 'Cama matrimonial de 2 plazas, internet wifi y TV por cable', 150, 'Disponible', 'Matrimonial'
GO
/*****************************************************/
CREATE PROC DesocuparHabitacion
@IdHabitacion int
AS
BEGIN
	UPDATE Habitacion
	SET Estado = 'Disponible'
	WHERE IdHabitacion = @IdHabitacion
END
GO
/*****************************************************/
CREATE PROC OcuparHabitacion
@IdHabitacion int
AS
BEGIN
	UPDATE Habitacion
	SET Estado = 'Ocupado'
	WHERE IdHabitacion = @IdHabitacion
END
GO
/*****************************************************/
CREATE PROC EliminarHabitacion
@IdHabitacion int
AS
BEGIN
	DELETE FROM Habitacion
	WHERE IdHabitacion = @IdHabitacion
END
--
EXEC EliminarHabitacion 1
GO
/*****************************************************/
CREATE PROC ListarHabPorNum
@Numero varchar(4)
AS
BEGIN
	SELECT * FROM Habitacion
	WHERE Numero LIKE @Numero + '%'
END
--
EXEC ListarHabPorNum '1'
GO
select * from Habitacion
GO
/*****************************************************/
/*********************PRODUCTO************************/
/*****************************************************/
CREATE PROC ListarProducto
AS
BEGIN
	SELECT * FROM Producto
END
--
EXEC ListarProducto
GO
/*****************************************************/
CREATE PROC RegistrarProducto
@Nombre			varchar(45),
@Descripcion	varchar(255),
@Unidad			varchar(20),
@Precio			decimal(7,2)
AS
BEGIN
	INSERT Producto (Nombre, Descripcion, Unidad, Precio)
	VALUES (@Nombre, @Descripcion, @Unidad, @Precio)
END
--
EXEC RegistrarProducto 'Producto2', 'Decripción2', 'Unidad2', 2.2
GO
/*****************************************************/
CREATE PROC EditarProducto
@IdProducto		int,
@Nombre			varchar(45),
@Descripcion	varchar(255),
@Unidad			varchar(20),
@Precio			decimal(7,2)
AS
BEGIN
	UPDATE Producto
	SET Nombre = @Nombre,
		Descripcion = @Descripcion,
		Unidad = @Unidad,
		Precio = @Precio
	WHERE IdProducto = @IdProducto
END
--
EXEC EditarProducto 2, 'Producto2', 'Decripción2', 'Unidad2', 5.5
GO
/*****************************************************/
CREATE PROC EliminarProducto
@IdProducto int
AS
BEGIN
	DELETE FROM Producto
	WHERE IdProducto = @IdProducto
END
--
EXEC EliminarProducto 1
GO
/*****************************************************/
CREATE PROC BuscarProductoPorNombre
@Nombre varchar(50)	
AS
BEGIN
	SELECT * FROM Producto
	WHERE Nombre LIKE '%' + @Nombre + '%'
END
--
EXEC BuscarProductoPorNombre 'llam'
GO
/*****************************************************/
/*********************CLIENTE*************************/
/*****************************************************/
CREATE PROC ListarCliente
AS
BEGIN
	SELECT Cliente.IdPersona, IdCliente, Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email
	FROM Cliente
	INNER JOIN Persona ON Cliente.IdPersona = Persona.IdPersona
END
--
EXEC ListarCliente
GO
/*****************************************************/
CREATE PROC RegistrarPersona
@Nombre		varchar(20),
@ApePaterno	varchar(20),
@ApeMaterno	varchar(20),
@TipoDoc	varchar(45),
@NumeroDoc	varchar(8),
@Direccion	varchar(45),
@Telefono	varchar(9),
@Email		varchar(30),
@IdPersona	int output
AS
BEGIN
	INSERT Persona (Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email)
	VALUES (@Nombre, @ApePaterno, @ApeMaterno, @TipoDoc, @NumeroDoc, @Direccion, @Telefono, @Email)
	
	SET @IdPersona = SCOPE_IDENTITY()
	RETURN @IdPersona
END
GO
/*****************************************************/
CREATE PROC RegistrarCliente
@IdPersona	int,
@IdCliente	varchar(10)
AS
BEGIN 
	INSERT Cliente (IdPersona, IdCliente)
	VALUES (@IdPersona, @IdCliente)
END
--
EXEC RegistrarCliente 1, 'ID_CLIENTE1'
GO
/*****************************************************/
CREATE PROC EditarPersona
@IdPersona	int,
@Nombre		varchar(20),
@ApePaterno	varchar(20),
@ApeMaterno	varchar(20),
@TipoDoc	varchar(45),
@NumeroDoc	varchar(8),
@Direccion	varchar(45),
@Telefono	varchar(9),
@Email		varchar(30)
AS
BEGIN
	UPDATE Persona
	SET Nombre = @Nombre,
		ApePaterno = @ApePaterno,
		ApeMaterno = @ApeMaterno,
		TipoDoc = @TipoDoc,
		NumeroDoc = @NumeroDoc,
		Direccion = @Direccion,
		Telefono = @Telefono,
		Email = @Email
	WHERE IdPersona = @IdPersona
END
--
EXEC EditarPersona 1, 'Cliente1', 'Cliente1', 'Cliente1', 'Cliente1', 11111111, 'Cliente1', 'Cliente1', 'Email1'
GO
/*****************************************************/
CREATE PROC EditarCliente
@IdPersona	int,
@IdCliente	varchar(10)
AS
BEGIN
	UPDATE Cliente
	SET IdCliente = @IdCliente
	WHERE IdPersona = @IdPersona
END
--
EXEC EditarCliente 1, 'id_Cliente1'
GO
/*****************************************************/
CREATE PROC EliminarPersona
@IdPersona int
AS
BEGIN
	DELETE FROM Persona
	WHERE IdPersona = @IdPersona
END
GO
/*****************************************************/
CREATE PROC EliminarCliente
@IdPersona int
AS
BEGIN
	DELETE FROM Cliente
	WHERE IdPersona = @IdPersona
END

select * from Persona
SELECT*FROM Cliente
GO
/*****************************************************/
CREATE PROC	BuscarCliente
@NumeroDoc varchar(8)
AS
BEGIN
	SELECT Cliente.IdPersona, IdCliente, Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email
	FROM Cliente
	INNER JOIN Persona ON Cliente.IdPersona = Persona.IdPersona
	WHERE NumeroDoc LIKE @NumeroDoc + '%'
END
--
EXEC BuscarCliente '7'
GO
/*****************************************************/
/******************TRABAJADOR*************************/
/*****************************************************/
CREATE PROC ListarTrabajador
AS
BEGIN
	SELECT Trabajador.IdPersona, Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email, Sueldo, Acceso, Sesion, Contrasenia, Estado
	FROM Trabajador
	INNER JOIN Persona ON Trabajador.IdPersona = Persona.IdPersona
END
--
EXEC ListarTrabajador
GO
/*****************************************************/
--Registrar persona
CREATE PROC RegistrarTrabajador
@IdPersona		int,
@Sueldo			decimal(7,2),
@Acceso			varchar(15),
@Sesion			varchar(15),
@Contrasenia	varchar(20),
@Estado			varchar(1)
AS
BEGIN
	INSERT Trabajador (IdPersona, Sueldo, Acceso, Sesion, Contrasenia, Estado)
	VALUES (@IdPersona, @Sueldo, @Acceso, @Sesion, @Contrasenia, @Estado)
END
GO
/*****************************************************/
--EditarPersona
CREATE PROC EditarTrabajador
@IdPersona		int,
@Sueldo			decimal(7,2),
@Acceso			varchar(15),
@Sesion			varchar(15),
@Contrasenia	varchar(20),
@Estado			varchar(1)
AS
BEGIN
	UPDATE Trabajador
	SET Sueldo =@Sueldo,
		Acceso = @Acceso,
		Sesion = @Sesion,
		Contrasenia = @Contrasenia,
		Estado = @Estado
	WHERE IdPersona = @IdPersona
END
GO
/*****************************************************/
CREATE PROC EliminarTrabajador
@IdPersona int
AS
BEGIN
	DELETE FROM Trabajador
	WHERE IdPersona = @IdPersona
END

select * from Persona
SELECT*FROM Cliente
GO
/*****************************************************/
CREATE PROC	BuscarTrabajador
@NumeroDoc varchar(8)
AS
BEGIN
	SELECT Trabajador.IdPersona, Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email, Sueldo, Acceso, Sesion, Contrasenia, Estado
	FROM Trabajador
	INNER JOIN Persona ON Trabajador.IdPersona = Persona.IdPersona
	WHERE NumeroDoc LIKE @NumeroDoc + '%'
END
--
EXEC BuscarTrabajador '7'
GO
/*****************************************************/
/************************LOGIN************************/
/*****************************************************/
--Tabla TRABAJADOR es mi tabla de usuarios
EXEC ListarTrabajador
GO
CREATE PROC	LoginTrabajador
@Sesion			varchar(15),
@Contrasenia	varchar(20)
AS
BEGIN
	SELECT Trabajador.IdPersona, Nombre, ApePaterno, ApeMaterno, TipoDoc, NumeroDoc, Direccion, Telefono, Email, Sueldo, Acceso, Sesion, Contrasenia, Estado
	FROM Trabajador
	INNER JOIN Persona ON Trabajador.IdPersona = Persona.IdPersona
	WHERE Sesion = @Sesion and Contrasenia COLLATE Latin1_General_CS_AS = @Contrasenia
END
--
EXEC LoginTrabajador 'admin', 'admin'
GO
/*****************************************************/
/************************RESERVA**********************/
/*****************************************************/
CREATE PROC ListarReserva
AS
BEGIN
	SELECT IdReserva,
			Reserva.IdHabitacion,
			Habitacion.Numero,
			IdCliente,
			(SELECT Nombre FROM Persona WHERE IdPersona = IdCliente) + ' ' +
			(SELECT ApePaterno FROM Persona WHERE IdPersona = IdCliente) + ' ' +
			(SELECT ApeMaterno FROM Persona WHERE IdPersona = IdCliente) AS NomCompCliente,
			IdTrabajador,
			(SELECT Nombre FROM Persona WHERE IdPersona = IdTrabajador) + ' ' +
			(SELECT ApePaterno FROM Persona WHERE IdPersona = IdTrabajador) + ' ' +
			(SELECT ApeMaterno FROM Persona WHERE IdPersona = IdTrabajador) AS	NomCompTrabajador,
			TipoReserva,
			FechaReserva,
			FechaIngreso,
			FechaSalida,
			CostoAlojamiento,
			Reserva.Estado
	FROM Reserva
	INNER JOIN Habitacion ON Reserva.IdHabitacion = Habitacion.IdHabitacion
END
--
EXEC ListarReserva
GO
/*****************************************************/
CREATE PROC RegistrarReserva
@IdHabitacion		int,
@IdCliente			int,
@IdTrabajador		int,
@TipoReserva		varchar(20),
@FechaReserva		date,
@FechaIngreso		date,
@FechaSalida		date,
@CostoAlojamiento	decimal(7,2),
@Estado				varchar(15)
AS
BEGIN 
	INSERT Reserva (IdHabitacion, IdCliente, IdTrabajador, TipoReserva, FechaReserva, FechaIngreso, FechaSalida, CostoAlojamiento, Estado)
	VALUES (@IdHabitacion, @IdCliente, @IdTrabajador, @TipoReserva, @FechaReserva, @FechaIngreso, @FechaSalida, @CostoAlojamiento, @Estado)
END
GO
/*****************************************************/
CREATE PROC EditarReserva
@IdReserva			int,
@IdHabitacion		int,
@IdCliente			int,
@IdTrabajador		int,
@TipoReserva		varchar(20),
@FechaReserva		date,
@FechaIngreso		date,
@FechaSalida		date,
@CostoAlojamiento	decimal(7,2),
@Estado				varchar(15)
AS
BEGIN
	UPDATE Reserva
	SET IdHabitacion = @IdHabitacion,
		IdCliente = @IdCliente,
		IdTrabajador = @IdTrabajador,
		TipoReserva = @TipoReserva,
		FechaReserva = @FechaReserva,
		FechaIngreso = @FechaIngreso,
		FechaSalida = @FechaSalida,
		CostoAlojamiento = @CostoAlojamiento,
		Estado = @Estado
	WHERE IdReserva = @IdReserva
END
GO
/*****************************************************/
CREATE PROC EditarEstadoReserva
@IdReserva int
AS
BEGIN
	UPDATE Reserva
	SET Estado = 'Pagado'
	WHERE IdReserva = @IdReserva
END
GO
/*****************************************************/
CREATE PROC EliminarReserva
@IdReserva int
AS
BEGIN
	DELETE FROM Reserva
	WHERE IdReserva = @IdReserva
END
GO
/*****************************************************/
CREATE PROC BuscarReserva
@FechaReserva date
AS
BEGIN
	SELECT IdReserva,
			Reserva.IdHabitacion,
			Habitacion.Numero,
			IdCliente,
			(SELECT Nombre FROM Persona WHERE IdPersona = IdCliente) + ' ' +
			(SELECT ApePaterno FROM Persona WHERE IdPersona = IdCliente) + ' ' +
			(SELECT ApeMaterno FROM Persona WHERE IdPersona = IdCliente) AS NomCompCliente,
			IdTrabajador,
			(SELECT Nombre FROM Persona WHERE IdPersona = IdTrabajador) + ' ' +
			(SELECT ApePaterno FROM Persona WHERE IdPersona = IdTrabajador) + ' ' +
			(SELECT ApeMaterno FROM Persona WHERE IdPersona = IdTrabajador) AS	NomCompTrabajador,
			TipoReserva,
			FechaReserva,
			FechaIngreso,
			FechaSalida,
			CostoAlojamiento,
			Reserva.Estado
	FROM Reserva
	INNER JOIN Habitacion ON Reserva.IdHabitacion = Habitacion.IdHabitacion
	where FechaReserva = @FechaReserva
END
GO
/*****************************************************/
CREATE PROC ListarConsumo
@IdReserva int
AS
BEGIN
	SELECT IdConsumo, IdReserva, Consumo.IdProducto, Nombre, Cantidad, Precio, Estado
	FROM Consumo
	INNER JOIN Producto ON Consumo.IdProducto = Producto.IdProducto
	WHERE IdReserva = @IdReserva
END
--
EXEC ListarConsumo 2
GO
/*****************************************************/
CREATE PROC RegistrarConsumo
@IdReserva		int,
@IdProducto		int,
@Cantidad		decimal(7,2),
@PrecioVenta	decimal(7,2),
@Estado			varchar(20)
AS
BEGIN
	INSERT Consumo (IdReserva, IdProducto, Cantidad, PrecioVenta, Estado)
	VALUES (@IdReserva, @IdProducto, @Cantidad, @PrecioVenta, @Estado)
END
GO
/*****************************************************/
CREATE PROC EditarConsumo
@IdConsumo		int,
@IdReserva		int,
@IdProducto		int,
@Cantidad		decimal(7,2),
@PrecioVenta	decimal(7,2),
@Estado			varchar(20)
AS
BEGIN
	UPDATE Consumo
	SET IdReserva = @IdReserva,
		IdProducto = @IdProducto,
		Cantidad = @Cantidad,
		PrecioVenta = @PrecioVenta,
		Estado = @Estado
	WHERE IdConsumo = @IdConsumo
END
GO
/*****************************************************/
CREATE PROC EliminarConsumo
@IdConsumo int
AS
BEGIN
	DELETE FROM Consumo
	WHERE IdConsumo = @IdConsumo
END
GO
/*****************************************************/
/************************PAGO*************************/
/*****************************************************/
CREATE PROC ListarPago
@IdReserva int
AS
BEGIN
	 SELECT * FROM Pago
	 WHERE IdReserva = @IdReserva
END
--
EXEC ListarPago 1
GO
/*****************************************************/
CREATE PROC RegistrarPago
@IdReserva			int,
@TipoComprobante	varchar(20),
@NumComprobante		varchar(12),
@Igv				decimal(9,2),
@TotalPago			decimal(9,2),
@FechaEmision		date,
@FechaPago			date
AS
BEGIN
	INSERT Pago (IdReserva, TipoComprobante, NumComprobante, Igv, TotalPago, FechaEmision, FechaPago)
	VALUES (@IdReserva, @TipoComprobante, @NumComprobante, @Igv, @TotalPago, @FechaEmision, @FechaPago)
END
GO
/*****************************************************/
CREATE PROC EditarPago
@IdPago				int,
@IdReserva			int,
@TipoComprobante	varchar(20),
@NumComprobante		varchar(12),
@Igv				decimal(9,2),
@TotalPago			decimal(9,2),
@FechaEmision		date,
@FechaPago			date
AS
BEGIN
	UPDATE Pago
	SET IdReserva = @IdReserva,
		TipoComprobante = @TipoComprobante,
		NumComprobante = @NumComprobante,
		Igv = @Igv,
		TotalPago = @TotalPago,
		FechaEmision = @FechaEmision,
		FechaPago = @FechaPago
	WHERE IdPago = @IdPago
END
GO
/*****************************************************/
CREATE PROC EliminarPago
@IdPago int
AS
BEGIN
	DELETE FROM Pago
	WHERE IdPago = @IdPago
END

SELECT * FROM Trabajador
SELECT * FROM Persona
SELECT * FROM Cliente
SELECT * FROM Reserva
SELECT * FROM Consumo
SELECT * FROM Pago


truncate table trabajador
truncate table cliente
truncate table persona
truncate table consumo
truncate table pago



