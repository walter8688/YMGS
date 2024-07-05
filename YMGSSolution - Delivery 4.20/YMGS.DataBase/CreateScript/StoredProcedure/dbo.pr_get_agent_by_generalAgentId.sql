/***
Create Date:2013/01/31
Description:获取总代理的下属代理数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_agent_by_generalAgentId]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_agent_by_generalAgentId]
    GO
CREATE PROCEDURE pr_get_agent_by_generalAgentId
    (
      @General_Agent_Id INT
    )
AS 
    BEGIN
        SELECT  agent.* ,
                ISNULL(TB_AGENT_DETAIL.Brokerage, 0) Brokerage ,
                ISNULL(TB_AGENT_DETAIL.Member_Count, 0) Member_Count
        FROM    ( SELECT    a.[USER_ID] ,
                            a.[USER_NAME] ,
                            a.LOGIN_NAME,
                            a.ROLE_ID ,
                            CASE a.ROLE_ID
                              WHEN 2 THEN '总代理'
                              WHEN 3 THEN '代理'
                            END AS ROLE_NAME ,
                            0 agentID ,
                            '' agentName
                  FROM      dbo.TB_SYSTEM_ACCOUNT a
                  WHERE     a.ACCOUNT_STATUS = 1
                            AND a.ROLE_ID = 3
                            AND a.AGENT_ID = @General_Agent_Id
                ) agent
                LEFT OUTER JOIN dbo.TB_AGENT_DETAIL ON agent.USER_ID = TB_AGENT_DETAIL.Agent_User_ID
    END
GO