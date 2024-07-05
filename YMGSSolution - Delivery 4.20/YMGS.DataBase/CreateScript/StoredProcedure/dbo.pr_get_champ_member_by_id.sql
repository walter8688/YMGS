/***
Create Date:2013/01/31
Description:新增冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champ_member_by_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champ_member_by_id]
    GO
CREATE PROCEDURE pr_get_champ_member_by_id ( @Champ_Event_Id INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_Champ_Event_Member
        WHERE   Champ_Event_ID = @Champ_Event_Id
    END
GO