IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_cancle_agent]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_cancle_agent]
GO
CREATE PROCEDURE pr_cancle_agent ( @User_ID INT )
AS 
    BEGIN
        SET XACT_ABORT ON
        BEGIN TRANSACTION
        --1.取消代理下面所有会员的归属关系
        UPDATE  dbo.TB_SYSTEM_ACCOUNT
        SET     AGENT_ID = NULL
        WHERE   USER_ID IN (
                SELECT  dbo.TB_SYSTEM_ACCOUNT.USER_ID
                FROM    dbo.TB_SYSTEM_ACCOUNT
                        INNER JOIN ( SELECT *
                                     FROM   dbo.TB_SYSTEM_ACCOUNT
                                     WHERE  AGENT_ID = @User_ID
                                            AND ROLE_ID = 3
                                   ) agents ON dbo.TB_SYSTEM_ACCOUNT.AGENT_ID = agents.USER_ID
                                               AND tb_system_account.ROLE_ID = 4
                UNION
                SELECT  USER_ID
                FROM    dbo.TB_SYSTEM_ACCOUNT
                WHERE   AGENT_ID = @User_ID
                        AND ROLE_ID = 4 )
		
		--2.取消下级代理角色，变更为会员
        UPDATE  dbo.TB_SYSTEM_ACCOUNT
        SET     AGENT_ID = NULL ,
                ROLE_ID = 4
        WHERE   USER_ID IN ( SELECT USER_ID
                             FROM   dbo.TB_SYSTEM_ACCOUNT
                             WHERE  AGENT_ID = @User_ID
                                    AND ROLE_ID = 3 )
		
		--3.取消代理角色，变更为会员    
        UPDATE  dbo.TB_SYSTEM_ACCOUNT
        SET     AGENT_ID = NULL ,
                ROLE_ID = 4
        WHERE   USER_ID = @User_ID
        COMMIT TRANSACTION            
    END
GO