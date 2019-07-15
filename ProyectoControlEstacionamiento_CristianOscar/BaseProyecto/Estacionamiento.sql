/* Base de Datos ESTACIONAMIENTO*/


/*Seleccionar la base de datos por defeto*/
USE tempdb
GO

IF EXISTS(SELECT * FROM sys.databases WHERE [NAME]='Estacionamiento')
	BEGIN
		DROP DATABASE Estacionamiento

--Crear la base de datos

CREATE DATABASE Estacionamiento
	END
GO
  

--Seleccionar la base
USE Estacionamiento
GO

--Crear el esquema a utilizar
CREATE SCHEMA Parqueo
GO

--Crear la tabla Vehiculo
CREATE TABLE  Parqueo.Vehiculo (
	 placaVehiculo NVARCHAR(20) NOT NULL
	 CONSTRAINT PK_placaVehiculo PRIMARY KEY,
	 idTipoVehiculo INT NOT NULL 
	)
GO

--Crear la Tabla TipoVehículo
CREATE TABLE Parqueo.TipoVehiculo (
	idTipoVehiculo INT NOT NULL IDENTITY (1,1)
		CONSTRAINT PK_idTipoVehiculo PRIMARY KEY,
	nombreTipo VARCHAR(20) NOT NULL
	)
GO

--Crear la tabla Detalle
CREATE TABLE Parqueo.Detalle (
	idDetalle INT IDENTITY (1,1) NOT NULL
	CONSTRAINT PK_idDetalle PRIMARY KEY CLUSTERED,
	placaVehiculo NVARCHAR(20) NOT NULL,
	horaEntrada DATETIME NOT NULL,
	horaSalida DATETIME NULL,
	tiempoTotal INT NOT NULL,
	costo DECIMAL NOT NULL
	)
GO

--Relaciones
ALTER TABLE Parqueo.Vehiculo
	ADD CONSTRAINT 
		FK_Parqueo_Vehiculo$PerteneceA$Parqueo_TipoVehiculo
		FOREIGN KEY (idTipoVehiculo) REFERENCES Parqueo.TipoVehiculo(idTipoVehiculo)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

--Validar que solo exista una placa en la tabla ya que la placa es única
ALTER TABLE Parqueo.Detalle
ADD UNIQUE (placaVehiculo)
GO



---Creacion de trigger para llenar la tabla de Detalle---
CREATE TRIGGER	TR_Detalle
ON Parqueo.Detalle FOR UPDATE
AS
begin
DECLARE @placaVehiculo NVarchar(20)
Select @placaVehiculo = placaVehiculo From deleted
DECLARE @idTipoVehiculo Varchar(20)
Select @idTipoVehiculo = idTipoVehiculo From Parqueo.Vehiculo where placaVehiculo = @placaVehiculo
DECLARE @horaEntrada DateTime
Select @horaEntrada = horaEntrada From Parqueo.Detalle where placaVehiculo =@placaVehiculo
DECLARE @horaSalida Datetime
Select @horaSalida = horaSalida from Parqueo.Detalle
Declare @tiempoTotal INT
SELECT @tiempoTotal=tiempoTotal From Parqueo.Detalle
set @tiempoTotal = DATEDIFF(hh,@horaEntrada,@horaSalida)
declare @costo int
set @costo = 0


if(@tiempoTotal) = 1 or (@tiempoTotal)=0 BEGIN
	set @costo=20
	set @tiempoTotal=1
END
if(@tiempoTotal) = 2  BEGIN
	set @costo=30
END
if(@tiempoTotal) = 3 or (@tiempoTotal) =4  BEGIN
	set @costo=70
END
if(@tiempoTotal) >= 4  BEGIN
	set @costo=15*@tiempoTotal
END
if(@idTipoVehiculo) = 'Camión' or (@idtipoVehiculo) = 'Bus' or (@idtipoVehiculo) = 'Rastra' BEGIN
	UPDATE Parqueo.Detalle
	set @costo= @costo*2
	END
	if(@idtipoVehiculo) = 'Motocicleta' or (@idtipoVehiculo) = 'Otros' BEGIN
	set @costo=@costo*0.5
	END

UPDATE Parqueo.Detalle
SET costo=@costo 


UPDATE Parqueo.Detalle
SET tiempoTotal=@tiempoTotal 
END
GO


INSERT INTO Parqueo.Detalle
VALUES ('HND123', GETDATE(), GETDATE(),0,0)

UPDATE Parqueo.Detalle
SET horaSalida=GETDATE() where placaVehiculo='HND123'

SELECT * FROM Parqueo.Detalle