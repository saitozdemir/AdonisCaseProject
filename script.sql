USE [CaseProject]
GO
/****** Object:  StoredProcedure [dbo].[CaseProject_sp_CreateReservation]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CaseProject_sp_CreateReservation]
(
 @GuestName   NVARCHAR(MAX),
 @DepartureDate  DATETIME,
 @ArrivalDate  DATETIME,
 @Status BIT
)
                                         
AS
BEGIN

INSERT INTO Reservation (GuestName,DepartureDate,ArrivalDate,[Status]) VALUES (@GuestName,@DepartureDate,@ArrivalDate,@Status)
	
END

GO
/****** Object:  StoredProcedure [dbo].[CaseProject_sp_DeleteReservation]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CaseProject_sp_DeleteReservation]
(
 @ReservationID   INT

)
                                         
AS
BEGIN

DELETE FROM Reservation WHERE ReservationId=@ReservationID
	
END

GO
/****** Object:  StoredProcedure [dbo].[CaseProject_sp_GetReservationByID]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CaseProject_sp_GetReservationByID]

@ReservationID int

AS
BEGIN

SELECT * FROM Reservation WHERE ReservationId=@ReservationID
	
END

GO
/****** Object:  StoredProcedure [dbo].[CaseProject_sp_GetReservationList]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CaseProject_sp_GetReservationList]

AS
BEGIN

SELECT * FROM Reservation
	
END

GO
/****** Object:  StoredProcedure [dbo].[CaseProject_sp_UpdateReservationByID]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CaseProject_sp_UpdateReservationByID]

@ReservationID int,
@GuestName   NVARCHAR(MAX),
@DepartureDate  DATETIME,
@ArrivalDate  DATETIME,
@Status BIT

AS
BEGIN

UPDATE Reservation SET


GuestName=@GuestName,
DepartureDate=@DepartureDate,
ArrivalDate=@ArrivalDate,
[Status]=@Status

WHERE ReservationId=@ReservationID
END
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 03.12.2022 02:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationId] [int] IDENTITY(1,1) NOT NULL,
	[GuestName] [nvarchar](max) NULL,
	[ArrivalDate] [datetime] NULL,
	[DepartureDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([ReservationId], [GuestName], [ArrivalDate], [DepartureDate], [Status]) VALUES (1, N'oNURRVurgun', CAST(N'2022-12-03 00:00:00.000' AS DateTime), CAST(N'2022-12-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Reservation] ([ReservationId], [GuestName], [ArrivalDate], [DepartureDate], [Status]) VALUES (3, N'merdadasdasd', CAST(N'2022-03-03 00:00:00.000' AS DateTime), CAST(N'2022-07-07 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Reservation] ([ReservationId], [GuestName], [ArrivalDate], [DepartureDate], [Status]) VALUES (4, N'DDDDDD', CAST(N'2022-03-03 00:00:00.000' AS DateTime), CAST(N'2022-05-05 00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Reservation] OFF
