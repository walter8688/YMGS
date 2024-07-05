/***
Create Date:2013/02/01
Description:查询冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_champ_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_champ_event]
    GO
CREATE PROCEDURE pr_get_champ_event
    (
      @Champ_Event_Type INT ,
      @Champ_Event_Name NVARCHAR(100) ,
      @Champ_Event_Name_EN NVARCHAR(100) ,
      @Champ_Event_Desc NVARCHAR(100) ,
      @Champ_Start_Date DATETIME ,
      @Champ_End_Date DATETIME
    )
AS 
    BEGIN
        SELECT  [Champ_Event_ID] ,
                [Champ_Event_Type] ,
                [Event_ID] ,
                [Champ_Event_Name] ,
                [Champ_Event_Name_En] ,
                [Champ_Event_Desc] ,
                [Champ_Event_StartDate] ,
                [Champ_Event_EndDate] ,
                [Champ_Event_Status] ,
                [Create_User] ,
                [Create_Time] ,
                [Last_Update_User] ,
                [Last_Update_Time]
        FROM    dbo.TB_Champ_Event
        WHERE   ( Champ_Event_Type = @Champ_Event_Type
                  OR @Champ_Event_Type IS NULL
                  OR @Champ_Event_Type = -1
                )
                AND ( Champ_Event_Name LIKE '%' + @Champ_Event_Name + '%'
                      OR @Champ_Event_Name IS NULL
                      OR @Champ_Event_Name = ''
                    )
                AND ( Champ_Event_Desc LIKE '%' + @Champ_Event_Desc + '%'
                      OR @Champ_Event_Desc IS NULL
                      OR @Champ_Event_Desc = ''
                    )
                AND ( Champ_Event_StartDate >= @Champ_Start_Date
                      OR @Champ_Start_Date IS NULL
                    )
                AND ( Champ_Event_EndDate <= @Champ_End_Date
                      OR @Champ_End_Date IS NULL
                    )
                AND ( Champ_Event_Name_En LIKE '%' + @Champ_Event_Name_EN
                      + '%'
                      OR @Champ_Event_Name_EN = ''
                    )
        ORDER BY Champ_Event_StartDate DESC ,
                Champ_Event_Type ,
                Champ_Event_Name
    END
GO