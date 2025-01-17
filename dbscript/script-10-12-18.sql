USE [master]
GO
/****** Object:  Database [YllariFm]    Script Date: 12/10/2018 10:13:38 PM ******/
CREATE DATABASE [YllariFm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YllariFm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\YllariFm.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'YllariFm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\YllariFm_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [YllariFm] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YllariFm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YllariFm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YllariFm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YllariFm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YllariFm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YllariFm] SET ARITHABORT OFF 
GO
ALTER DATABASE [YllariFm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YllariFm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YllariFm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YllariFm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YllariFm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YllariFm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YllariFm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YllariFm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YllariFm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YllariFm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YllariFm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YllariFm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YllariFm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YllariFm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YllariFm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YllariFm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YllariFm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YllariFm] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YllariFm] SET  MULTI_USER 
GO
ALTER DATABASE [YllariFm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YllariFm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YllariFm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YllariFm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [YllariFm] SET DELAYED_DURABILITY = DISABLED 
GO
USE [YllariFm]
GO
/****** Object:  Table [dbo].[Agencia]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agencia](
	[IdAgencia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[CorreoContacto] [varchar](80) NOT NULL,
	[CorreoExtra] [varchar](80) NULL,
	[Pais] [varchar](50) NULL,
	[Ciudad] [varchar](150) NULL,
 CONSTRAINT [PK_Agencia] PRIMARY KEY CLUSTERED 
(
	[IdAgencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Biblia]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Biblia](
	[IdBiblia] [int] IDENTITY(1,1) NOT NULL,
	[Mes] [int] NOT NULL,
	[Anho] [int] NOT NULL,
 CONSTRAINT [PK_Biblia] PRIMARY KEY CLUSTERED 
(
	[IdBiblia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudad](
	[IdCiudad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Pais] [varchar](50) NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[IdCiudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Tipo] [varchar](5) NOT NULL,
	[CorreoContacto] [varchar](80) NULL,
	[NumeroContacto] [varchar](30) NULL,
	[NumeroAdicional] [varchar](30) NULL,
	[Pais] [varchar](50) NULL,
	[Ciudad] [varchar](50) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [nombre unico] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[File]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[IdFile] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NOT NULL,
	[IdBiblia] [int] NOT NULL,
	[Descripcion] [varchar](750) NULL,
	[IdAgencia] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[IdCliente] [int] NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[IdFile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[IdHotel] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Estrellas] [tinyint] NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[IdHotel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orden]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orden](
	[IdOrden] [int] IDENTITY(1,1) NOT NULL,
	[IdServicio] [int] NOT NULL,
	[RecordatorioEnviado] [bit] NOT NULL,
	[Recordatorio2Enviado] [bit] NOT NULL,
 CONSTRAINT [PK_Orden] PRIMARY KEY CLUSTERED 
(
	[IdOrden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pasajero]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pasajero](
	[IdPasajero] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](520) NULL,
 CONSTRAINT [PK_Pasajero] PRIMARY KEY CLUSTERED 
(
	[IdPasajero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[TipoProveedor] [varchar](5) NULL,
	[Correo] [varchar](80) NOT NULL,
	[NumeroContacto] [varchar](30) NULL,
	[NumeroCntctAdicional] [varchar](30) NULL,
	[CorreoAdicional] [varchar](50) NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroRecordatorio]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroRecordatorio](
	[IdServicio] [int] NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[IdRegistroRecordatorio] [int] NOT NULL,
 CONSTRAINT [PK_RegistroRecordatorio] PRIMARY KEY CLUSTERED 
(
	[IdRegistroRecordatorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 12/10/2018 10:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio](
	[IdServicio] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[TipoServicio] [varchar](5) NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[IdFile] [int] NOT NULL,
	[HoraRecojo] [time](7) NULL,
	[HoraSalida] [time](7) NULL,
	[Vuelo] [varchar](50) NULL,
	[Pasajeros] [int] NOT NULL,
	[VR] [varchar](50) NULL,
	[TC] [varchar](50) NULL,
	[IdProveedor] [int] NULL,
	[Observaciones] [varchar](750) NULL,
	[Ciudad] [varchar](150) NULL,
	[NombrePasajero] [varchar](50) NULL,
	[Tren] [varchar](50) NULL,
	[ALM] [varchar](50) NULL,
	[Transporte] [varchar](50) NULL,
	[Hotel] [nchar](10) NULL,
 CONSTRAINT [PK_Servicio] PRIMARY KEY CLUSTERED 
(
	[IdServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [unico mes anho biblia]    Script Date: 12/10/2018 10:13:38 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [unico mes anho biblia] ON [dbo].[Biblia]
(
	[Anho] ASC,
	[Mes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [codigo unico]    Script Date: 12/10/2018 10:13:38 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [codigo unico] ON [dbo].[File]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Agencia] FOREIGN KEY([IdAgencia])
REFERENCES [dbo].[Agencia] ([IdAgencia])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Agencia]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Biblia] FOREIGN KEY([IdBiblia])
REFERENCES [dbo].[Biblia] ([IdBiblia])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Biblia]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Cliente]
GO
ALTER TABLE [dbo].[RegistroRecordatorio]  WITH CHECK ADD  CONSTRAINT [FK_RegistroRecordatorio_Proveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedor] ([IdProveedor])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegistroRecordatorio] CHECK CONSTRAINT [FK_RegistroRecordatorio_Proveedor]
GO
ALTER TABLE [dbo].[RegistroRecordatorio]  WITH CHECK ADD  CONSTRAINT [FK_RegistroRecordatorio_Servicio] FOREIGN KEY([IdServicio])
REFERENCES [dbo].[Servicio] ([IdServicio])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegistroRecordatorio] CHECK CONSTRAINT [FK_RegistroRecordatorio_Servicio]
GO
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_File] FOREIGN KEY([IdFile])
REFERENCES [dbo].[File] ([IdFile])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Servicio_File]
GO
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Proveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedor] ([IdProveedor])
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Servicio_Proveedor]
GO
USE [master]
GO
ALTER DATABASE [YllariFm] SET  READ_WRITE 
GO
