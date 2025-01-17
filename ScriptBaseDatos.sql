USE [BD-RESERVAS]
GO
/****** Object:  Table [dbo].[Comercio]    Script Date: 5/16/2024 8:25:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comercio](
	[id_comercio] [int] IDENTITY(1,1) NOT NULL,
	[nom_comercio] [nvarchar](250) NOT NULL,
	[aforo_maximo] [int] NULL,
 CONSTRAINT [PK_Comercio] PRIMARY KEY CLUSTERED 
(
	[id_comercio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 5/16/2024 8:25:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[id_comercio] [int] NOT NULL,
	[nom_servicio] [nvarchar](250) NOT NULL,
	[hora_apertura] [time](7) NOT NULL,
	[hora_cierre] [time](7) NOT NULL,
	[duracion] [time](7) NULL,
 CONSTRAINT [PK_Servicio] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turno]    Script Date: 5/16/2024 8:25:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turno](
	[id_turno] [int] IDENTITY(1,1) NOT NULL,
	[id_servicio] [int] NOT NULL,
	[fecha_turno] [datetime] NOT NULL,
	[hora_inicio] [time](7) NOT NULL,
	[hora_fin] [time](7) NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Turno] PRIMARY KEY CLUSTERED 
(
	[id_turno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comercio] ON 

INSERT [dbo].[Comercio] ([id_comercio], [nom_comercio], [aforo_maximo]) VALUES (1, N'Mrs Bono', 10)
SET IDENTITY_INSERT [dbo].[Comercio] OFF
GO
SET IDENTITY_INSERT [dbo].[Servicio] ON 

INSERT [dbo].[Servicio] ([id_servicio], [id_comercio], [nom_servicio], [hora_apertura], [hora_cierre], [duracion]) VALUES (1, 1, N'Comida Rapidda', CAST(N'08:00:00' AS Time), CAST(N'18:00:00' AS Time), CAST(N'01:00:00' AS Time))
INSERT [dbo].[Servicio] ([id_servicio], [id_comercio], [nom_servicio], [hora_apertura], [hora_cierre], [duracion]) VALUES (2, 1, N'Servicio Para dama', CAST(N'16:00:00' AS Time), CAST(N'00:00:00' AS Time), CAST(N'06:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Servicio] OFF
GO

ALTER TABLE [dbo].[Comercio] ADD  DEFAULT ((1)) FOR [aforo_maximo]
GO
ALTER TABLE [dbo].[Turno] ADD  DEFAULT ((0)) FOR [estado]
GO
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Comercio] FOREIGN KEY([id_comercio])
REFERENCES [dbo].[Comercio] ([id_comercio])
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Comercio]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Servicio] FOREIGN KEY([id_servicio])
REFERENCES [dbo].[Servicio] ([id_servicio])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Servicio]
GO
/****** Object:  StoredProcedure [dbo].[GenerarTurnos]    Script Date: 5/16/2024 8:25:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerarTurnos]
    @ServicioId INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Duracion TIME,
			@FechaActual DATE,
			@HoraActual TIME,
			@HoraFin TIME,
			@HoraApertura TIME,
			@HoraCierre TIME,
			@ContadorDias INT = 0;

	DELETE FROM Turno WHERE fecha_turno >=  @FechaInicio AND fecha_turno <= @FechaFin AND estado = 0

	SET @FechaActual = @FechaInicio;
	
    SELECT 
		@Duracion = duracion, 
		@HoraApertura = hora_apertura,
		@HoraCierre = hora_cierre 
    FROM Servicio 
    WHERE id_servicio = @ServicioId;

	 
    DECLARE @NumeroDias INT = DATEDIFF(DAY, @FechaInicio, @FechaFin) + 1;	  
	   
    WHILE (@ContadorDias < @NumeroDias)
    BEGIN 
        SET @HoraActual = @HoraApertura;  

        WHILE  DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00:00', @Duracion), @HoraActual) <= @HoraCierre
        BEGIN
		
            SET @HoraFin = DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00:00', @Duracion), @HoraActual);
		 
            INSERT INTO Turno (id_servicio, fecha_turno, hora_inicio, hora_fin, estado)
            VALUES (@ServicioId, @FechaActual, @HoraActual, @HoraFin, 0);
          
            SET @HoraActual = @HoraFin;			
        END
		 
        SET @FechaActual = DATEADD(DAY, 1, @FechaActual);
		SET @ContadorDias = @ContadorDias + 1;
    END

	SELECT 
	 [id_turno],
	 [id_servicio], 
	 [fecha_turno], 
	 [hora_inicio], 
	 [hora_fin],
	 [estado]
	FROM Turno 
	where fecha_turno >=  @FechaInicio AND fecha_turno <= @FechaFin
END


GO
