IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_check_grow_member_count]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_check_grow_member_count]
    GO
CREATE PROCEDURE pr_check_grow_member_count ( @Agent_Id INT )
AS 
    BEGIN
        DECLARE @Grow_Member_Count INT 
        DECLARE @Cur_Member_Count INT
        
        SELECT  @Grow_Member_Count = Member_Count
        FROM    dbo.TB_AGENT_DETAIL
        WHERE   Agent_User_ID = @Agent_Id
        
        
        SELECT  @Cur_Member_Count = COUNT(1)
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   AGENT_ID = @Agent_Id
                AND ROLE_ID = 4
        
        SELECT  @Cur_Member_Count = COUNT(1) + @Cur_Member_Count
        FROM    dbo.TB_SYSTEM_ACCOUNT
                INNER JOIN ( SELECT USER_ID
                             FROM   dbo.TB_SYSTEM_ACCOUNT
                             WHERE  AGENT_ID = @Agent_Id
                                    AND ROLE_ID = 3
                           ) agent ON dbo.TB_SYSTEM_ACCOUNT.AGENT_ID = agent.USER_ID   
                           
        SELECT  @Cur_Member_Count = COUNT(1) + @Cur_Member_Count
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   AGENT_ID = @Agent_Id
                AND ROLE_ID = 3

        --PRINT CONVERT(NVARCHAR(10),@Cur_Member_Count)

        IF @Cur_Member_Count >= @Grow_Member_Count 
            BEGIN
                RAISERROR('可发展会员数不足!',16,1) WITH NOWAIT
            END
    END
GO