IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_atuo_freeze_start_match]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_atuo_freeze_start_match]
    GO
CREATE PROCEDURE [dbo].[pr_get_atuo_freeze_start_match]
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_MATCH
        WHERE   [STATUS] = 1
                AND ADDITIONALSTATUS <> 2
    END
GO