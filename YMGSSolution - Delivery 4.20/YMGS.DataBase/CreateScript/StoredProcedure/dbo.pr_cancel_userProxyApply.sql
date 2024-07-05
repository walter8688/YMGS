-- =============================================
-- 取消代理申请
-- =============================================
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_cancel_userProxyApply]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_cancel_userProxyApply]
    GO
CREATE PROCEDURE pr_cancel_userProxyApply
    (
      @Apply_Proxy_ID INT 
    )
AS 
    BEGIN
        DECLARE @Apply_Status INT
        SELECT @Apply_Status = Apply_Status FROM TB_APPLY_PROXY tap WITH(NOLOCK)
        WHERE tap.Apply_Proxy_ID=@Apply_Proxy_ID
        IF @Apply_Status <>0
            BEGIN
                RAISERROR('当前状态不能取消!',16,1) WITH NOWAIT
                RETURN
            END
         ELSE
         	BEGIN
         		UPDATE TB_APPLY_PROXY SET Apply_Status = 4
         		WHERE Apply_Proxy_ID = @Apply_Proxy_ID
         	END
    END
GO
