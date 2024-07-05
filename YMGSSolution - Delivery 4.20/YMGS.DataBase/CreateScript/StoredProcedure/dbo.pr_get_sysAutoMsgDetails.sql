IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_sysAutoMsgDetails]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_sysAutoMsgDetails]
go
CREATE PROCEDURE [dbo].[pr_get_sysAutoMsgDetails] ( @MESSAGEID INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_AUTOMESSAGE
        WHERE   MESSAGEID = @MESSAGEID
    END

GO