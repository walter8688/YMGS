IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_sysAutoMsg]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_del_sysAutoMsg
GO
CREATE PROCEDURE [dbo].pr_del_sysAutoMsg 
( 
	@MESSAGEID INT 
)
AS 
    BEGIN
        DELETE  FROM dbo.TB_SYSTEM_AUTOMESSAGE
        WHERE   MESSAGEID = @MESSAGEID 
    END
GO