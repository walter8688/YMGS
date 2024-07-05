IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_confirm_apply_proxy]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_confirm_apply_proxy]
GO
CREATE PROCEDURE pr_confirm_apply_proxy
    (
      @Apply_Proxy_Id INT
    )
AS 
    BEGIN
        DECLARE @Cur_Status INT,@UserId INT,@Role_Id INT
        SELECT @Cur_Status = Apply_Status,@UserId = [User_ID],@Role_Id = Role_ID FROM tb_apply_proxy WHERE Apply_Proxy_ID = @Apply_Proxy_Id
        IF(@Cur_Status <> 1)
        BEGIN
        	RAISERROR('当前状态不能改为已批准',16,1) WITH NOWAIT
        	RETURN 
        END
        UPDATE tb_apply_proxy SET Apply_Status = 2 WHERE Apply_Proxy_ID = @Apply_Proxy_Id
        UPDATE dbo.TB_SYSTEM_ACCOUNT SET ROLE_ID = @Role_Id WHERE [USER_ID] = @UserId
    END
GO