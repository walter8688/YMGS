/***
Create Date:2013/01/31
Description:获取获胜冠军赛事成员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champ_event_winmembers]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champ_event_winmembers]
    GO
CREATE PROCEDURE pr_get_champ_event_winmembers ( @Champ_Event_Id INT )
AS 
    BEGIN
        SELECT  dbo.TB_Champ_Win_Member.Champ_Win_Member_ID ,
                dbo.TB_Champ_Win_Member.Champ_Event_Member_ID ,
                dbo.TB_Champ_Win_Member.Champ_Event_ID ,
                dbo.TB_Champ_Event_Member.Champ_Event_Member_Name,
                dbo.TB_Champ_Event_Member.Champ_Event_Member_Name_En
        FROM    dbo.TB_Champ_Win_Member
                INNER JOIN dbo.TB_Champ_Event_Member ON dbo.TB_Champ_Win_Member.Champ_Event_Member_ID = dbo.TB_Champ_Event_Member.Champ_Event_Member_ID
        WHERE   dbo.TB_Champ_Win_Member.Champ_Event_ID = @Champ_Event_Id
        ORDER BY dbo.TB_Champ_Event_Member.Champ_Event_Member_Name
    END
    
GO