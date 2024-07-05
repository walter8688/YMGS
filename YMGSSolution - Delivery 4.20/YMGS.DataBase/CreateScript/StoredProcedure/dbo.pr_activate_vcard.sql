IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_activate_vcard]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_activate_vcard]
    GO
CREATE PROCEDURE pr_activate_vcard
    (
      @VCard_No NVARCHAR(100) ,
      @VCard_Activate_No NVARCHAR(100) ,
      @Activate_User_Id INT 
    )
AS 
    BEGIN
        DECLARE @Temp_Count INT 
        DECLARE @VCard_Face_Value INT
        DECLARE @Vard_Id INT
        SELECT  @Temp_Count = COUNT(1)
        FROM    dbo.TB_VCARD_DETAIL
        WHERE   VCARD_NO = @VCard_No
                AND VCARD_ACTIVATE_NO = @VCard_Activate_No         
       
        IF @Temp_Count <> 1 
            BEGIN
                RAISERROR('V网卡卡号密码不匹配!|V-Card No and V-Card activate No is mismatch!',16,1) WITH NOWAIT
                RETURN
            END
        SELECT  @Temp_Count = COUNT(1)
        FROM    dbo.TB_VCARD_DETAIL
        WHERE   VCARD_NO = @VCard_No
                AND VCARD_ACTIVATE_NO = @VCard_Activate_No
                AND VCARD_STATUS = 0
                
        IF @Temp_Count <> 1 
            BEGIN
                RAISERROR('此V网卡已被激活!|This V-Card has been activated!',16,1) WITH NOWAIT
                RETURN
            END
        SELECT  @VCard_Face_Value = VCARD_FACE_VALUE ,
                @Vard_Id = VCARD_ID
        FROM    dbo.TB_VCARD_DETAIL
        WHERE   VCARD_NO = @VCard_No
                AND VCARD_ACTIVATE_NO = @VCard_Activate_No
                AND VCARD_STATUS = 0
        --1.更新V网卡状态
        UPDATE  dbo.TB_VCARD_DETAIL
        SET     VCARD_STATUS = 1 ,
                ACTIVATE_USER_ID = @Activate_User_Id ,
                ACTIVATE_DATE = GETDATE()
        WHERE   VCARD_NO = @VCard_No
                AND VCARD_ACTIVATE_NO = @VCard_Activate_No
                
        --2.用户账户资金增加相应金额
        
        UPDATE  dbo.TB_USER_FUND
        SET     CUR_FUND = CUR_FUND + @VCard_Face_Value
        WHERE   USER_ID = @Activate_User_Id
        SELECT  @VCard_Face_Value
        
        --新增资金记录
        DECLARE @User_Fund_Id INT
        SELECT  @User_Fund_Id = USER_FUND_ID
        FROM    dbo.TB_USER_FUND
        WHERE   USER_ID = @Activate_User_Id
        
        INSERT  INTO dbo.TB_FUND_HISTORY
                ( USER_FUND_ID ,TRADE_TYPE ,TRADE_DESC ,TRADE_SERIAL_NO ,TRADE_FUND ,TRADE_DATE)
        VALUES  ( @User_Fund_Id ,0 ,N'V网卡激活,卡号:' + @VCard_Activate_No ,@Vard_Id ,@VCard_Face_Value ,GETDATE())
    END
GO