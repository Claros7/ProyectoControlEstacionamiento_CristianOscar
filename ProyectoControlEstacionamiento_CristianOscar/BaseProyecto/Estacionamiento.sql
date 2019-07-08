/* Base de Datos ESTACIONAMIENTO*/


/*Seleccionar la base de datos por defeto*/
USE tempdb
GO

--Crear la base de datos
IF NOT EXISTS(SELECT * FROM sys.databases WHERE [NAME] = 'Estacionamiento')
		BEGIN
			CREATE DATABASE Estacionamiento
	END
GO

--Seleccionar la base
USE Estacionamiento
GO

--Crear el esquema a utilizar
CREATE SCHEMA estac
GO

--Crear la tabala Vehiculo
CREATE TABLE  Vehiculo (
	marcaVehiculo INT NOT NULL IDENTITY
		CONSTRAINT PK_Zoologico_id PRIMARY KEY,
	 plcaVehiculo NVARCHAR (50) NOT NULL
	)
GO

--Crear la tabla Hora de Entrada
CREATE TABLE Hora_Entrada (
	horaEntrada INT NOT NULL,
	placaVehiculo NVARCHAR NOT NULL
	)
GO

--Crear la tabla Hora de Salida
CREATE TABLE Hora_Salida (
	horaSalida INT NOT NULL,
	placaVehiculo NVARCHAR NOT NULL
	)
GO

