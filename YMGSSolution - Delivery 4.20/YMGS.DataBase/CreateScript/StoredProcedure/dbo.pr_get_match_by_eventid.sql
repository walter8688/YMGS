IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_match_by_eventid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_match_by_eventid]
GO
CREATE PROCEDURE [dbo].[pr_get_match_by_eventid] ( @EventID INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_MATCH
        WHERE   EVENT_ID = @EventID
    END
GO