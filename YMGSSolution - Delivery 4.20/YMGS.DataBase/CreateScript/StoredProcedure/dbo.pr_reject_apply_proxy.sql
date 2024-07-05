IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_reject_apply_proxy]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_reject_apply_proxy]
GO
CREATE PROCEDURE pr_reject_apply_proxy
    (
      @Apply_Proxy_Id INT
    )
AS 
    BEGIN
        DECLARE @Cur_Status INT
        SELECT @Cur_Status = Apply_Status  FROM tb_apply_proxy WHERE Apply_Proxy_ID = @Apply_Proxy_Id
        IF(@Cur_Status <> 0 AND @Cur_Status <> 1)
        BEGIN
        	RAISERROR('当前状态不能改为已拒绝',16,1) WITH NOWAIT
        	RETURN 
        END
        UPDATE tb_apply_proxy SET Apply_Status = 3 WHERE Apply_Proxy_ID = @Apply_Proxy_Id
    END
GO