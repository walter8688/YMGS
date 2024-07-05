/***
Create Date:2013/01/31
Description:新增冠军赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_champ_win_members]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_champ_win_members]
    GO
CREATE PROCEDURE pr_add_champ_win_members
    (
      @Champ_Event_Id INT ,
      @Champ_Win_Members NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @SplitStr NVARCHAR(2) ,
            @Champ_Event_Member_Id INT
        BEGIN            
            DELETE  FROM dbo.TB_Champ_Win_Member
            WHERE   Champ_Event_ID = @Champ_Event_Id                
            SET @SplitStr = ','
            DECLARE cur_champ_member CURSOR
            FOR
                SELECT  [Value]
                FROM    [dbo].[SplitString](@Champ_Win_Members, @SplitStr, 1) 
            OPEN cur_champ_member
            FETCH NEXT FROM cur_champ_member INTO @Champ_Event_Member_Id 
            WHILE @@FETCH_STATUS = 0 
                BEGIN
                    INSERT  INTO dbo.TB_Champ_Win_Member
                            ( Champ_Event_Member_ID ,
                              Champ_Event_ID
                            )
                    VALUES  ( @Champ_Event_Member_Id , -- Champ_Event_Member_ID - int
                              @Champ_Event_Id  -- Champ_Event_ID - int
                            )
                    FETCH NEXT FROM cur_champ_member INTO @Champ_Event_Member_Id 
                END
            CLOSE cur_champ_member
            DEALLOCATE cur_champ_member
        END
    END
GO