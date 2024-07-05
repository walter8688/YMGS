IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_SingleApplyProxyByAPID]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_SingleApplyProxyByAPID]
GO

CREATE PROCEDURE [dbo].[pr_get_SingleApplyProxyByAPID]
    (
      @Apply_Proxy_ID INT
    )
AS 
    BEGIN
		SELECT CnStatus = CASE  WHEN apply_status =0 THEN '申请中' 
								WHEN apply_status =1 THEN '审批中'
								WHEN apply_status =2 THEN '已批准'
								WHEN apply_status =3 THEN '已拒绝' 
								WHEN apply_status =4 THEN '已取消'
					 END,
				EnStatus = CASE  WHEN apply_status =0 THEN 'Appyling' 
								 WHEN apply_status =1 THEN 'Approval In Process'
								 WHEN apply_status =2 THEN 'Confirmed'
								 WHEN apply_status =3 THEN 'Rejected' 
								 WHEN apply_status =4 THEN 'Canceled bets'
					 END,
				 CnRoleName = tsr.ROLE_NAME,
				 EnRoleName = CASE  WHEN tsr.ROLE_ID = 2  THEN 'Agent1'
								    WHEN tsr.ROLE_ID = 8  THEN 'Agent2'
								    WHEN tsr.ROLE_ID = 9  THEN 'Agent3'
								    WHEN tsr.ROLE_ID = 10 THEN 'Agent4'
								    WHEN tsr.ROLE_ID = 11 THEN 'Agent5'
					 END,
					 Apply_DateCon = CONVERT(varchar(100), Apply_Date, 120),*
		FROM TB_APPLY_PROXY tap WITH(NOLOCK)
		INNER JOIN TB_SYSTEM_ROLE tsr WITH(NOLOCK) ON tap.Role_ID = tsr.ROLE_ID
		WHERE tap.Apply_Proxy_ID = @Apply_Proxy_ID
    END

GO

