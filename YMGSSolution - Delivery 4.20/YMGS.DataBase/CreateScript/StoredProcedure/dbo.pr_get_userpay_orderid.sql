IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_userpay_orderid]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_userpay_orderid]
    GO
CREATE PROCEDURE pr_get_userpay_orderid
    (
      @Trans_Date NVARCHAR(8)
    )
AS 
    BEGIN
        SELECT  MAX(ORDER_ID) AS ORDER_ID
        FROM    TB_USER_PAY
        WHERE   ORDER_ID LIKE '%' + @Trans_Date + '%'
    END
GO