/***
Create Date:2013/02/01
Description:通过冠军赛事ID查询冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champ_event_by_key]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champ_event_by_key]
    GO
CREATE PROCEDURE pr_get_champ_event_by_key
    (
      @Champ_Event_Id INT
    )
AS 
    BEGIN
        SELECT  [Champ_Event_ID] ,
                [Champ_Event_Type] ,
                [Event_ID] ,
                [Champ_Event_Name] ,
                [Champ_Event_Desc] ,
                [Champ_Event_StartDate] ,
                [Champ_Event_EndDate] ,
                [Champ_Event_Status] ,
                [Create_User] ,
                [Create_Time] ,
                [Last_Update_User] ,
                [Last_Update_Time]
        FROM    dbo.TB_Champ_Event
        WHERE   CHAMP_EVENT_ID=@Champ_Event_Id
    END
GO