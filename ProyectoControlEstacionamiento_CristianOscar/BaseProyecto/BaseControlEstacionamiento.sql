/* Base de Datos ESTACIONAMIENTO*/


/*Seleccionar la base de datos por defeto*/

USE tempdb
GO

IF EXISTS(SELECT * FROM sys.databases WHERE [name]='ControlDeEstacionamiento')
	BEGIN
		DROP DATABASE ControlDeEstacionamiento
	END
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE [name]='ControlEstacionamiento')
	BEGIN
		CREATE DATABASE ControlEstacionamiento
	END
GO

USE ControlEstacionamiento
GO

use model
GO

drop table INVENTARIO.INV_LABORATORIOS


CREATE SCHEMA Estacionamiento
GO


-- Creación de la tabla Vehículo
CREATE TABLE Estacionamiento.Vehiculo (
	Placa NVARCHAR(8) NOT NULL
		CONSTRAINT PK_Estacionamiento_Vehiculo_Placa PRIMARY KEY CLUSTERED,
	TipoDeVehiculo VARCHAR(20) NOT NULL,
)


--Creacion tabla detalle---
CREATE TABLE Estacionamiento.Detalle(
idDetalles INT IDENTITY(1,1) NOT NULL
			CONSTRAINT PK_idDetalles PRIMARY KEY CLUSTERED,
horaDeEntrada DATETIME NOT NULL,
horaDeSalida DATETIME NOT NULL,
placaDeVehiculo NVARCHAR(8) NOT NULL
)
GO


-- Creacion tabla reportes
l

-- Creamos las llaves foráneas
ALTER TABLE Estacionamiento.Reporte
	ADD CONSTRAINT
		FK_Estacionamiento_Vehiculo$TieneUnaOMas$Estacionamiento_HoraDeEntrada
		FOREIGN KEY (Placa) REFERENCES Estacionamiento.Reporte(HoraDeEntrada)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

ALTER TABLE Estacionamiento.Detalle
	ADD CONSTRAINT
		FK_Estacionamiento_Detalles$TieneUna$Estacionamiento_placaDeVehiculo
		FOREIGN KEY (placaDeVehiculo) REFERENCES Estacionamiento.Vehiculo(Placa)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

ALTER TABLE Estacionamiento.Detalle
ADD UNIQUE (placaDeVehiculo);

---Creacion de trigger para llenar la tabla de reporte---
INSERT INTO Estacionamiento.Reporte VALUES(@Placa,@TipoDeVehiculo,@HoraDeEntrada,@HoraDeSalida,DATEDIFF(hh,@HoraDeEntrada,@HoraDeSalida),0)

if(@TiempoTotal) = 1 or (@TiempoTotal)=0 BEGIN
	UPDATE Estacionamiento.Reporte 
	SET Costo = 20 where Placa = @Placa
ENDl
if(@TiempoTotal) = 2  BEGIN
	UPDATE Estacionamiento.Reporte
	SET Costo = 30 where Placa = @Placa
END
if(@TiempoTotal) = 3 or (@TiempoTotal) =4  BEGIN
	UPDATE Estacionamiento.Reporte
	SET Costo = 70 where Placa = @Placa
END
if(@TiempoTotal) >= 4  BEGIN
	UPDATE Estacionamiento.Reporte
	SET Costo = 15*TiempoTotal where Placa = @Placa
END
if(@TipoDeVehiculo) = 'Camion' or (@TipoDeVehiculo) = 'Bus' or (@TipoDeVehiculo) = 'Rastra' BEGIN
	UPDATE Estacionamiento.Reporte
	SET Costo = Costo*2 where Placa = @Placa
	END
	
	if(@TipoDeVehiculo) = 'Motocicleta' or (@TipoDeVehiculo) = 'Otros' BEGIN
	UPDATE Estacionamiento.Reporte
	SET Costo = Costo*0.5 where Placa = @Placa
	END
end

