/***
Create Date:2013/01/31
Description:获取代理数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_all_agent]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_all_agent]
    GO
CREATE PROCEDURE pr_get_all_agent
    (
      @Role_Id INT ,
      @User_Name NVARCHAR(40) ,
      @Agent_Name NVARCHAR(40)
    )
AS 
    BEGIN
        SELECT  agent.* ,
                ISNULL(TB_AGENT_DETAIL.Brokerage, 0) Brokerage ,
                ISNULL(TB_AGENT_DETAIL.Member_Count, 0) Member_Count
        FROM    ( SELECT    a.[USER_ID] ,
                            a.[USER_NAME] ,
                            a.ROLE_ID ,
                            a.LOGIN_NAME ,
                            r.ROLE_NAME ROLE_NAME ,
                            '' agentID ,
                            '' agentName
                  FROM      dbo.TB_SYSTEM_ACCOUNT a
                            INNER JOIN dbo.TB_SYSTEM_ROLE r ON a.ROLE_ID = r.ROLE_ID
                  WHERE     a.ACCOUNT_STATUS = 1
                            AND a.ROLE_ID IN (
                            SELECT  ROLE_ID
                            FROM    tb_system_role
                            WHERE   CHARINDEX('级总代理', ROLE_NAME, 0) > 1
                                    AND CONVERT(INT, REPLACE(ROLE_NAME, '级总代理',
                                                             '')) >= 1
                                    AND CONVERT(INT, REPLACE(ROLE_NAME, '级总代理',
                                                             '')) <= 5 )
                  UNION
                  SELECT    a.[USER_ID] ,
                            a.[USER_NAME] ,
                            a.ROLE_ID ,
                            a.LOGIN_NAME ,
                            CASE a.ROLE_ID
                              WHEN 2 THEN '总代理'
                              WHEN 3 THEN '代理'
                            END AS ROLE_NAME ,
                            b.[USER_ID] agentID ,
                            b.[USER_NAME] agentName
                  FROM      dbo.TB_SYSTEM_ACCOUNT a
                            LEFT OUTER JOIN dbo.TB_SYSTEM_ACCOUNT b ON a.AGENT_ID = b.USER_ID
                  WHERE     a.ROLE_ID = 3
                            AND a.ACCOUNT_STATUS = 1
                ) agent
                LEFT OUTER JOIN dbo.TB_AGENT_DETAIL ON agent.USER_ID = TB_AGENT_DETAIL.Agent_User_ID
        WHERE   ( agent.ROLE_ID = @Role_Id
                  OR @Role_Id = -1
                )
                AND ( agent.[USER_NAME] LIKE '%' + @User_Name + '%'
                      OR @User_Name = ''
                    )
                AND ( agent.agentName LIKE '%' + @Agent_Name + '%'
                      OR @Agent_Name = ''
                    )
    END
GO