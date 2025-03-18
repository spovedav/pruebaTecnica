-- esta prueba tecnica de LPlus bnco g
create database Prueba07

/*
Nota.-
* Como suelo trabjar para multiPaises, no suelo poner GETDATE() por temas de fecha del Servidor de SQL en su lugar crea una funcion fn.GetDate(),, pero para este proyecto de juguete no se lo hara

*/
use Prueba07

CREATE TABLE Usuario(
	Id int identity(1,1) CONSTRAINT PK_Usuario_Id PRIMARY KEY,
	UserName NVARCHAR(100) NOT NULL,
	Password NVARCHAR(300) NOT NULL,
	Email NVARCHAR(100) NOT NULL,

	FechaCreacion datetime not null default GETDATE(),
	Estado bit default(0) not null
)

--USUARIO: admin
--CLAVE:   admin
INSERT INTO Usuario(UserName, Password, Email, FechaCreacion, Estado)
			VALUES ('admin', 'oXc3hMpvDMNnbIerJwbH5Q==', 'marcelo.poveda@mi-dominio.com', GETDATE(), 1)

CREATE TABLE Categoria (
	Id int identity(1,1) CONSTRAINT PK_Categoria_Id PRIMARY KEY ,
	Nombre NVARCHAR(50) NOT NULL
)

INSERT INTO Categoria (Nombre) 
VALUES ('Servicio'), ('Bien');


-- Se manda a crear los productos
-- Voy separar la cateogoria
-- Voy a poner datos de autoria
-- Podria poner más cosas pero lo quiero hacer muy complejo

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) CONSTRAINT PK_Persona_Id PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(500),
    CategoriaId int CONSTRAINT FK_ProductoIdCategoria_CategoriaId FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id),
    Imagen NVARCHAR(500), -- URL de la imagen
    Precio DECIMAL(10,2) NOT NULL CHECK (Precio >= 0),
    Stock INT NOT NULL CHECK (Stock >= 0),
    
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioCreacion NVARCHAR(50) NOT NULL,

	FechaModificacion DATETIME NULL,
	UsuarioModificacion NVARCHAR(50) NULL,

	FechaEliminacion DATETIME NULL,
	UsuarioEliminacion NVARCHAR(50) NULL,

	Estado bit default (1) -- Es una buena practica solo eliminar de forma logica y permante
);

INSERT INTO Productos (Nombre, Descripcion, CategoriaId, Imagen, Precio, Stock, FechaCreacion, UsuarioCreacion, Estado) 
VALUES ('Laptop Gamer', 'Laptop con procesador Intel i7 y 16GB RAM', 2,
    'https://fastly.picsum.photos/id/866/200/300.jpg?hmac=rcadCENKh4rD6MAp6V_ma-AyWv641M4iiOpe1RyFHeI', 1200.99, 10, GETDATE(), 'admin', 1
);

INSERT INTO Productos (Nombre, Descripcion, CategoriaId, Imagen, Precio, Stock, FechaCreacion, UsuarioCreacion, Estado) 
VALUES ('Viaje De Premiun', 'Viaje para vacacionar a lo grande', 1,
    'https://fastly.picsum.photos/id/637/200/300.jpg?hmac=_aCsxiL_H35K1JVhdKAxp9akei2mXbQ7N5N2zfAtCiE', 700.99, 140, GETDATE(), 'admin', 1
	);

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

-- Lo mismo no eliminar permante y llegar un constrol de quien hizo el moviento de trasnacion
CREATE TABLE Transacciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME DEFAULT GETDATE(),
    Tipo NVARCHAR(10) CHECK (Tipo IN ('compra', 'venta')) NOT NULL,
    ProductoId INT NOT NULL CONSTRAINT FK_TransaccionesProductId_ProductId FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10,2) NOT NULL CHECK (PrecioUnitario >= 0),
    PrecioTotal AS (Cantidad * PrecioUnitario) PERSISTED,
    Detalle NVARCHAR(500),

	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioCreacion NVARCHAR(50) NOT NULL,

	FechaModificacion DATETIME NULL,
	UsuarioModificacion NVARCHAR(50) NULL,

	FechaEliminacion DATETIME NULL,
	UsuarioEliminacion NVARCHAR(50) NULL,

	Estado bit default (1) -- Es una buena practica solo eliminar de forma logica y permante
);