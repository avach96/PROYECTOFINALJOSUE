---SCRIPT DEL PROYECTO FINAL SIDNEY ALVAREZ CASTRO-----
CREATE DATABASE PROYECTOFINAL

---------------------------------------------- TABLA DE TRANSACCION -----------------------------------------------------------

CREATE TABLE TRANSACCION
(
	CODIGO INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE VARCHAR(30),
	CORREO VARCHAR(30)unique,
	CLAVE VARCHAR(30),
	TIPO NVARCHAR(20) CHECK (TIPO = 'Ingreso' or TIPO = 'Gasto'),
	DESCRIPCION VARCHAR(50),
	FECHA DATETIME,
	MONTO MONEY
)

 --INGRESAR REGISTROS--
INSERT INTO TRANSACCION 
VALUES ('Ingreso', 'Pago quincenal', '3-12-2022', '250000'),
	   ('Gasto', 'Arreglo de computadora', '3-12-2022', '15000'),
	   ('Ingreso', 'Intereses de ahorros', '4-12-2022', '80000')

--SELECT * FROM TRANSACCION


---------------------------------------------- TABLA DE USUARIOS --------------------------------------------------------------

/*
Usuarios
trasacciones
tipo_usuario
TipoMoneda
TipoCambio
*/




use PROYECTOFINAL
create table Persona
(
   id int identity(1,1),
   cedula varchar (20) ,
   Nombre varchar(50) not null,
   Apellido1 varchar(50)  null,
   Apellido2 varchar(50) null,
   Direccion nvarchar(100) null,
   telefono varchar(20),
   constraint uq_cedula Unique (cedula),
   constraint pk_idPersona primary key (id)
)
select * from persona
insert into Persona values ('2','Sofia','Castillo','Castro','Heredia','222')

use PROYECTOFINAL
create table TipoUsuario
(
   id int identity(1,1),
   Descripcion varchar(50)
   constraint pk_idTipoUsuario primary key (id)
)


insert into TipoUsuario (Descripcion) values ('Administrador'),('Regular')
select * from TipoUsuario

use PROYECTOFINAL
create table Usuario
( 
  usuario varchar(30),
  email varchar(20),
  idusuario int,
  tipoUsuario int,
  clave varchar(30) not null,
  constraint pk_email primary key (email), /* LLave primaria */
  constraint fk_id Foreign key (idusuario) References Persona(id), /* LLave forenea */
  constraint fk_idTipoUsuario Foreign key (tipoUsuario) References TipoUsuario(id) /* LLave forenea */
)


insert into usuario values ('juan@gmail.com',1,1,'123')
insert into usuario values ('Sofia@gmail.com',3,1,'456')
select * from usuario

use PROYECTOFINAL

create table Tipo_Trasaccion
(
  id int identity,
  Descripcion varchar(50) not null,
  TipoContable varchar (10) not null,
  constraint pk_idTipotrasaccion primary key (id), /* LLave primaria */
  Constraint Ck_tipocontable Check (TipoContable ='Activo' or TipoContable='Pasivo'),
  
)
insert into Tipo_Trasaccion values ('Salario','Activo'),('Pago de luz','Pasivo'),('Internet','Pasivo')
select * from Tipo_Trasaccion

use PROYECTOFINAL
Create table Transacciones
(
  id int identity,
  idTipoTransaccion int,
  emailtrasaccion varchar(20),
  Descripcion varchar(100),
  monto money not null default 0,
  Fecha datetime
  constraint fk_idusuariotrasaccion foreign key (emailtrasaccion) references Usuario(email)
  constraint pk_idtrasaccion primary key (id), /* LLave primaria */
  constraint fk_idTipoTransaccion Foreign key (idTipoTransaccion) References Tipo_Trasaccion(id) /* LLave forenea */
)

insert into Transacciones values (1,'juan@gmail.com','Ingreso mensual',1000,'2022/07/12')

select * from Transacciones



CREATE table TiposUsuario_Auditoria
(
   id int ,
   Descripcion varchar(50),
   Usuario varchar(50),
   Tipo varchar(20),
   fecha datetime
)

CREATE TABLE TRANSACCIONAuditoria
(
	CODIGO INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE VARCHAR(30),
	CORREO VARCHAR(30)unique,
	CLAVE VARCHAR(30),
	TIPO NVARCHAR(20) CHECK (TIPO = 'Ingreso' or TIPO = 'Gasto'),
	DESCRIPCION VARCHAR(50),
	FECHA DATETIME,
	MONTO MONEY,
	MOTIVO VARCHAR(20)
)

Create trigger Trigger_Auditoria_TipoUsuario
  on TipoUsuario
  After Insert, Delete
   
    as 
   begin
   SET NOCOUNT ON;
   insert into TiposUsuario_Auditoria  (id, Descripcion, usuario, Tipo, Fecha)
   select i.id, i.Descripcion, 'DBA','Ingreso',GETDATE() from inserted i
   union all
   select d.id, d.Descripcion, 'DBA','Borrado',GETDATE() from deleted d
  
   end

CREATE trigger Trigger_Auditoria_Transaccion
  on TRANSACCION
  After Insert, Delete
   
    as 
   begin
   SET NOCOUNT ON;
   insert into TRANSACCIONAuditoria (NOMBRE, CORREO, CLAVE, TIPO, DESCRIPCION,FECHA,MONTO,MOTIVO)
   select i.NOMBRE, i.CORREO, i.CLAVE, i.TIPO, i.DESCRIPCION, i.FECHA, i.MONTO, 'AGREGADO' from inserted i
   union all
   select d.NOMBRE, d.CORREO, d.CLAVE, d.TIPO, d.DESCRIPCION, d.FECHA, d.MONTO, 'ELIMINADO' from deleted d
  
   end

  insert into TipoUsuario(Descripcion) values ('Reportes')
  delete TipoUsuario where id=2
  select * from TRANSACCION
  select * from TRANSACCIONAuditoria
  delete TRANSACCION where NOMBRE='JOSUE'
  insert into TRANSACCION (NOMBRE, CORREO, CLAVE, TIPO, DESCRIPCION,FECHA,MONTO) values ('JOSUE','tes4t@gmail.com','123','Ingreso','Pago Horas','2022-12-05','300000')


--------------------------------------------- FUNCIONES P.ALMACENADOS -------------------------------------------------------------

--SELECT TIPO, DESCRIPCION, FECHA, MONTO from TRANSACCION
--INNER JOIN USUARIO ON USUARIO.CODIGO = TRANSACCION.CODIGO

create PROC AGREGARUSUARIOS1
	@USUARIO varchar(30),
	@CORREO varchar(20),
	@CLAVE varchar(30)

 AS
	BEGIN
		INSERT INTO Usuario (usuario,email,clave) values (@USUARIO,@CORREO,@CLAVE)
	END 

exec AGREGARUSUARIOS 'josue01','test0129@gmail.com','123456'

Select * FROM Usuario

CREATE PROC LISTARUSUARIOS
 AS
	BEGIN
		SELECT NOMBRE, CORREO, CLAVE, TIPO, DESCRIPCION, FECHA, MONTO FROM TRANSACCION
	END 

create PROC ELIMINARUSUARIOS
	@NOMBRE varchar(30)
 AS
	BEGIN
		DELETE Usuario WHERE usuario = @NOMBRE
	END 

create PROCEDURE AGREGARUSUARIOS
	@NOMBRE varchar(30),
	@CORREO varchar(30),
	@CLAVE varchar(30),
	@TIPO NVARCHAR(20),
	@DESCRIPCION VARCHAR(50),
	@FECHA DATETIME,
	@MONTO MONEY

 AS
	BEGIN
		INSERT INTO TRANSACCION values (@NOMBRE, @CORREO, @CLAVE, @TIPO, @DESCRIPCION, @FECHA, @MONTO)
	END 

CREATE PROC ACTUALIZARUSUARIOS 
	@NOMBRE varchar(30),
	@CORREO varchar(30),
	@CLAVE varchar(30),
	@TIPO NVARCHAR(20),
	@DESCRIPCION VARCHAR(50),
	@FECHA DATETIME,
	@MONTO MONEY
	
 AS
	BEGIN
		UPDATE TRANSACCION SET CORREO = @CORREO, CLAVE = @CLAVE, TIPO = @TIPO, DESCRIPCION = @DESCRIPCION, FECHA = @FECHA, MONTO=@MONTO WHERE NOMBRE = @NOMBRE
 END


SELECT * FROM TRANSACCION

------------------------------------------------------- FILTROS ---------------------------------------------------------
CREATE PROCEDURE TRANSACCION_FILTRO
@TIPO VARCHAR(20)

 AS
	BEGIN 
		SELECT * FROM TRANSACCION 
		WHERE TIPO = @TIPO 
	 END
-----------------------------------------------SUMA----------------------------------------------------------------------
CREATE PROCEDURE CONTADOR
 AS
  BEGIN 
		SELECT COUNT (CODIGO) AS COUNT
		FROM TRANSACCION
  END

 CREATE PROCEDURE SUMINGRESO
 AS
  BEGIN 
		SELECT SUM(MONTO) TOTAL
		FROM TRANSACCION WHERE TIPO = 'Ingreso'
  END

 CREATE PROCEDURE SUMGASTO
 AS
  BEGIN 
		SELECT SUM(MONTO) SUMGASTO
		FROM TRANSACCION WHERE TIPO = 'GASTO'
		RETURN
  END

CREATE PROCEDURE Registro
@cedula varchar(20),
@nombre varchar(30),
@apellido varchar(50)=null,
@email varchar(30),
@usuario varchar(30),
@clave varchar(30)
as
	begin
		insert into Persona(cedula,Nombre,Apellido1) values (@cedula,@nombre,@apellido)
		insert into Usuario(usuario,email,idusuario,tipoUsuario,clave)
		select @usuario, @email, id, 1, @clave from Persona where cedula = @cedula
	end

Select * from Usuario



