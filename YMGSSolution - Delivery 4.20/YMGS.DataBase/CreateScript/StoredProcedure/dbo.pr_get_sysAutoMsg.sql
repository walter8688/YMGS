IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_sysAutoMsg]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_sysAutoMsg]
go
CREATE PROCEDURE [dbo].[pr_get_sysAutoMsg]
    (
      @Msg_SDate DATETIME ,
      @Msg_EDate DATETIME ,
      @User_Id INT
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_AUTOMESSAGE
        WHERE   ( MESSAGE_SEND_DATE >= @Msg_SDate
                  OR @Msg_SDate IS NULL
                )
                AND ( MESSAGE_SEND_DATE <= @Msg_EDate
                      OR @Msg_EDate IS NULL
                    )
                AND ( SENDTO_USERID = @User_Id
                      OR @User_Id = ''
                    )
        ORDER BY MESSAGE_SEND_DATE
    END

GO