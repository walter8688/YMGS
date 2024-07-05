IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_sysAutoMsg]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_sysAutoMsg]
    GO
CREATE PROCEDURE pr_add_sysAutoMsg
    (
      @SENDTO_USERID INT ,
      @MESSAGE_CONTENT NVARCHAR(200) ,
      @MESSAGE_CONTENT_EN NVARCHAR(200) ,
      @SENDBY_SYSTEMID INT 
    )
AS 
    BEGIN
        INSERT  INTO dbo.TB_SYSTEM_AUTOMESSAGE
                ( SENDTO_USERID ,
                  MESSAGE_CONTENT ,
                  MESSAGE_CONTENT_EN ,
                  SENDBY_SYSTEMID ,
                  MESSAGE_SEND_DATE
                )
        VALUES  ( @SENDTO_USERID ,
                  @MESSAGE_CONTENT ,
                  @MESSAGE_CONTENT_EN ,
                  @SENDBY_SYSTEMID ,
                  GETDATE()
                )
    END
GO