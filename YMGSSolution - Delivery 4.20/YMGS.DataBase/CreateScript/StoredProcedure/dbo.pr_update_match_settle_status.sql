IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_update_match_settle_status]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_update_match_settle_status]
    GO
CREATE PROCEDURE pr_update_match_settle_status
    (
      @Match_Id INT ,
      @Match_Settle_Status INT
    )
AS 
    BEGIN
        UPDATE  dbo.TB_MATCH
        SET     SETTLE_STATUS = @Match_Settle_Status
        WHERE   MATCH_ID = @Match_Id
    END
GO