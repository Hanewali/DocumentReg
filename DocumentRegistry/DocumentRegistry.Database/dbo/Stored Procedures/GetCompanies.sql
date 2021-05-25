USE [DocumentRegistry]
GO

/****** Object:  StoredProcedure [dbo].[GetCompanies]    Script Date: 25.05.2021 17:53:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jakub Rumpel
-- Create date: 2021-05-23
-- Description:	Pobiera listę firm z bazy - to jest wstępna wersja procedury, żeby zweryfikać całą komunikację WebApp-WebApi-DB 
-- =============================================
CREATE PROCEDURE [dbo].[GetCompanies]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        *
    from
        Company
END
GO


