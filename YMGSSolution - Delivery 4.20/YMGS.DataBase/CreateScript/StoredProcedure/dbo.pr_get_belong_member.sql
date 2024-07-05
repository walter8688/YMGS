/***
Create Date:2013/02/06
Description:获取下属会员
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_belong_member]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_belong_member]
    GO
CREATE PROCEDURE pr_get_belong_member ( @User_Id INT )
AS 
    BEGIN
        SELECT  [USER_NAME] ,
                EMAIL_ADDRESS ,
                CASE ROLE_ID
                  WHEN 2 THEN '总代理'
                  WHEN 3 THEN '代理'
                  WHEN 4 THEN '会员'
                END AS RoleName ,
                CASE ACCOUNT_STATUS
                  WHEN 1 THEN '活动'
                  WHEN 2 THEN '锁定'
                END AS ACCOUNT_STATUS
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   AGENT_ID = @User_Id
    END
GO