USE BDHotel
GO
/*REPORTES*/
--LISTAOD DE HABITACIONES
EXEC ListarHabitacion
GO
--COMPROBANTE
--TABLAS CLEINTE PAGO PERSONA RESERVA
CREATE PROC ReporteComprobante
@IdPago int
AS
BEGIN
	SELECT Persona.Nombre + ' ' + Persona.ApePaterno + ' ' + PERSONA.ApeMaterno AS Cliente,
			Persona.NumeroDoc,
			Persona.Direccion,	
			Pago.TipoComprobante,
			Pago.NumComprobante,
			pago.FechaEmision,
			'Alojamiento' AS Descripcion,
			Reserva.CostoAlojamiento AS Precio,
			'1' AS Cantidad,
			pago.TotalPago
		FROM Persona
		INNER JOIN Cliente	ON Persona.IdPersona	= Cliente.IdPersona
		INNER JOIN Reserva	ON Cliente.IdPersona	= Reserva.IdCliente
		INNER JOIN Pago		ON Reserva.IdReserva	= Pago.IdReserva
		WHERE Pago.IdPago = @IdPago
		
		UNION

		SELECT Persona.Nombre + ' ' + Persona.ApePaterno + ' ' + PERSONA.ApeMaterno AS Cliente,
			Persona.NumeroDoc,
			Persona.Direccion,
			Pago.TipoComprobante,
			Pago.NumComprobante,
			pago.FechaEmision,
			Producto.Nombre AS Descripcion,
			Producto.Precio AS Precio,
			Consumo.Cantidad AS	Cantidad,
			Consumo.Cantidad * Producto.Precio AS TotalPago
		FROM Persona
		INNER JOIN Cliente	ON Persona.IdPersona	= Cliente.IdPersona
		INNER JOIN Reserva	ON Cliente.IdPersona	= Reserva.IdCliente
		INNER JOIN Pago		ON Reserva.IdReserva	= Pago.IdReserva
		INNER JOIN Consumo	ON Reserva.IdReserva	= Consumo.IdReserva
		INNER JOIN Producto	ON Consumo.IdProducto	= Producto.IdProducto
		WHERE Pago.IdPago = @IdPago
END