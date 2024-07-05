/***
Create Date:2013/01/31
Description:设置代理
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_set_members_as_agent]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_set_members_as_agent]
    GO
    
CREATE PROCEDURE pr_set_members_as_agent
    (
      @AgentId INT ,
      @UserId INT
    )
AS 
    BEGIN
        DECLARE @Account_Status INT
        SELECT  @Account_Status = ACCOUNT_STATUS
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   [USER_ID] = @UserId
        IF @Account_Status = 1 
            BEGIN
                UPDATE  dbo.TB_SYSTEM_ACCOUNT
                SET     AGENT_ID = @AgentId ,
                        ROLE_ID = 3
                WHERE   [USER_ID] = @UserId
            END
        ELSE
        BEGIN
        	RAISERROR('当前用户状态未激活,不可指定为代理',16,1) WITH NOWAIT
        	RETURN
        END
    END
    
GO