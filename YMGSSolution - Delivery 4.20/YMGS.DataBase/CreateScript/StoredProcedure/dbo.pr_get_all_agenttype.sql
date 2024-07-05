IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_all_agenttype]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_all_agenttype]
    GO
CREATE PROCEDURE pr_get_all_agenttype
AS 
    BEGIN
        SELECT  ROLE_NAME ,
                ROLE_ID
        FROM    tb_system_role
        WHERE   CHARINDEX('级总代理', ROLE_NAME, 0) > 1
                AND CONVERT(INT, REPLACE(ROLE_NAME, '级总代理', '')) >= 1
                AND CONVERT(INT, REPLACE(ROLE_NAME, '级总代理', '')) <= 5
        UNION ALL
        SELECT  ROLE_NAME ,
                ROLE_ID
        FROM    dbo.TB_SYSTEM_ROLE
        WHERE   ROLE_ID = 3
    END
GO