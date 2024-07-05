IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_userpay_succeed_detail]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_userpay_succeed_detail]
    GO
CREATE PROCEDURE [dbo].[pr_get_userpay_succeed_detail] ( @Order_Id NVARCHAR(20) )
AS 
    BEGIN
        SELECT  dbo.TB_USER_PAY.ORDER_ID ,
                dbo.TB_USER_PAY.TRAN_AMOUNT ,
                dbo.TB_VCARD_DETAIL.VCARD_NO ,
                dbo.TB_VCARD_DETAIL.VCARD_ACTIVATE_NO
        FROM    dbo.TB_USER_PAY
                INNER JOIN dbo.TB_VCARD_DETAIL ON dbo.TB_USER_PAY.VCARD_ID = dbo.TB_VCARD_DETAIL.VCARD_ID
        WHERE   dbo.TB_USER_PAY.ORDER_ID = @Order_Id
    END
GO