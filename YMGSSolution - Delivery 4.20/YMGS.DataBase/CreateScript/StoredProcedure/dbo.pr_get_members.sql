/***
Create Date:2013/01/31
Description:获取所有角色为会员的User
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_members]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_members]
    GO
    
CREATE PROCEDURE pr_get_members
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   ROLE_ID = 4
                AND ACCOUNT_STATUS = 1
    END
    
GO