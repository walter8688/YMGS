IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_user_pay]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_user_pay]
    GO
CREATE PROCEDURE pr_add_user_pay
    (
      @User_Id INT ,
      @Mer_Id NVARCHAR(15) ,
      @Order_Id NVARCHAR(16) ,
      @Trans_Amt DECIMAL(18, 2) ,
      @Trans_Status INT ,
      @Vcard_Id INT ,
      @Trans_Type INT
    )
AS 
    BEGIN
        INSERT  INTO dbo.TB_USER_PAY
                ( USER_ID ,
                  VCARD_ID ,
                  MER_ID ,
                  ORDER_ID ,
                  TRAN_AMOUNT ,
                  TRAN_DATE ,
                  TRAN_STATUS ,
                  TRAN_TYPE
                )
        VALUES  ( @User_Id , -- USER_ID - int
                  @Vcard_Id ,
                  @Mer_Id , -- MER_ID - nvarchar(15)
                  @Order_Id , -- ORDER_ID - nvarchar(16)
                  @Trans_Amt , -- TRAN_AMOUNT - decimal
                  GETDATE() , -- TRAN_DATE - datetime
                  @Trans_Status ,  -- TRAN_STATUS - int
                  @Trans_Type
                )
    END
GO