/***
Create Date:2013/01/31
Description:获取总代理的下属代理数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_agent_By_id]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_agent_By_id]
    GO
    
CREATE PROCEDURE pr_get_agent_By_id
    (
      @General_Agent_Id INT
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   ROLE_ID = 3
                AND ACCOUNT_STATUS = 1
                AND AGENT_ID = @General_Agent_Id
    END
GO