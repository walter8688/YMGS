IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_members_by_general_agentId]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_get_members_by_general_agentId
GO

CREATE PROCEDURE pr_get_members_by_general_agentId ( @General_AgentId INT )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   AGENT_ID = @General_AgentId
                AND ROLE_ID = 4
    END
GO
