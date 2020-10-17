----CREATE DATABASE BDHotel
----GO
USE BDHotel
GO
/****************************************************/
/*CREANDO VALIDACIONES DE CONSTRAINT*/
/****************************************************/
ALTER TABLE Cliente
DROP CONSTRAINT IF EXISTS FK_ClientePersona
GO
ALTER TABLE Trabajador
DROP CONSTRAINT IF EXISTS FK_TrabajadorPersona
GO
ALTER TABLE Reserva
DROP CONSTRAINT IF EXISTS FK_ReservaHabitacion
GO
ALTER TABLE Reserva
DROP CONSTRAINT IF EXISTS FK_ReservaCliente
GO
ALTER TABLE Reserva
DROP CONSTRAINT IF EXISTS FK_ReservaTrabajador
GO
ALTER TABLE Consumo
DROP CONSTRAINT IF EXISTS FK_ConsumoProducto
GO
ALTER TABLE Consumo
DROP CONSTRAINT IF EXISTS FK_ConsumoReserva
GO
ALTER TABLE Pago
DROP CONSTRAINT IF EXISTS FK_PagoReserva
GO
/****************************************************/
/*CREACIÓN DE LAS TABLAS*/
/****************************************************/
DROP TABLE IF EXISTS Habitacion
GO
CREATE TABLE Habitacion
(
IdHabitacion	int primary key identity,
Numero			varchar(4) not null,
Piso			varchar(2) not null,
Descripcion		varchar(255),
Caracteristicas	varchar(512),
PrecioDiario	decimal(7,2) not null, --Precio por día
Estado			varchar(15) not null,
TipoHabitacion	varchar(20) not null
)
GO
DROP TABLE IF EXISTS Persona
GO
CREATE TABLE Persona
(
IdPersona	int primary key identity,
Nombre		varchar(20) not null,
ApePaterno	varchar(20) not null,
ApeMaterno	varchar(20) not null,
TipoDoc		varchar(45) not null,
NumeroDoc	varchar(8) unique not null,
Direccion	varchar(45),
Telefono	varchar(9) unique not null,
Email	varchar(30) unique
)
GO
DROP TABLE IF EXISTS Cliente
GO
CREATE TABLE Cliente
(
IdPersona	int primary key, --Es un SUbTIpo de persona tendra la misma llave primaria
IdCliente	varchar(10) unique not null,
)
GO
DROP TABLE IF EXISTS Trabajador
GO
CREATE TABLE Trabajador
(
IdPersona	int primary key,
Sueldo		decimal(7,2) not null,
Acceso		varchar(15) not null,
Sesion		varchar(15) not null,
Contrasenia	varchar(20) not null,
Estado		varchar(1) not null
)
GO
DROP TABLE IF EXISTS Producto
GO
CREATE TABLE Producto
(
IdProducto	int primary key identity,
Nombre		varchar(45) not null,
Descripcion	varchar(255),
Unidad		varchar(20) not null,
Precio		decimal(7,2) not null
)
GO
DROP TABLE IF EXISTS Reserva
GO
CREATE TABLE Reserva
(
IdReserva			int primary key identity,
IdHabitacion		int not null,
IdCliente			int not null,
IdTrabajador		int not null,
TipoReserva			varchar(20) not null,
FechaReserva		date not null,
FechaIngreso		date not null,
FechaSalida			date not null,
CostoAlojamiento	decimal(7,2) not null,
Estado				varchar(15)
)
GO
DROP TABLE IF EXISTS Consumo
GO
CREATE TABLE Consumo
(
IdConsumo	int primary key identity,
IdReserva	int not null,
IdProducto	int not null,
Cantidad	decimal(7,2) not null,
PrecioVenta	decimal(7,2) not null,
Estado		varchar(20) not null
)
GO
DROP TABLE IF EXISTS Pago
GO
CREATE TABLE Pago
(
IdPago			int primary key identity,
IdReserva		int not null,
TipoComprobante	varchar(20) not null,
NumComprobante	varchar(12) not null,
Igv				decimal(9,2) not null,
TotalPago		decimal(9,2) not null,
FechaEmision	date not null,
FechaPago		date not null
--Estado			varchar(20) not null
)
GO
/*****************************************************/
/*CREANDO CONSTRAINTS*/
/*****************************************************/
ALTER TABLE Cliente
ADD CONSTRAINT FK_ClientePersona
FOREIGN KEY (IdPersona) REFERENCES Persona
GO
ALTER TABLE Trabajador
ADD CONSTRAINT FK_TrabajadorPersona
FOREIGN KEY (IdPersona) REFERENCES Persona
GO
ALTER TABLE Reserva
ADD CONSTRAINT FK_ReservaHabitacion
FOREIGN KEY (IdHabitacion) REFERENCES Habitacion
GO
ALTER TABLE Reserva
ADD CONSTRAINT FK_ReservaCliente
FOREIGN KEY (IdCliente) REFERENCES Cliente
GO
ALTER TABLE Reserva
ADD CONSTRAINT FK_ReservaTrabajador
FOREIGN KEY (IdTrabajador) REFERENCES Trabajador
GO
ALTER TABLE Consumo
ADD CONSTRAINT FK_ConsumoProducto
FOREIGN KEY (IdProducto) REFERENCES Producto
GO
ALTER TABLE Consumo
ADD CONSTRAINT FK_ConsumoReserva
FOREIGN KEY (IdReserva) REFERENCES Reserva
GO
ALTER TABLE Pago
ADD CONSTRAINT FK_PagoReserva
FOREIGN KEY (IdReserva) REFERENCES Reserva
GO