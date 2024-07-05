/***
Create Date:2013/01/10
Description:新增辅助参数
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_param]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_add_param
GO
CREATE PROCEDURE [dbo].[pr_add_param]
    (
      @Param_Type INT ,
      @Param_Name NVARCHAR(50) ,
      @Is_Use INT ,
      @Create_User NVARCHAR(50) ,
      @Last_Update_User NVARCHAR(50) 
    )
AS 
    DECLARE @Count INT ,
        @Param_Order INT 
    BEGIN
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_PARAM_PARAM
        WHERE   dbo.TB_PARAM_PARAM.PARAM_NAME = @Param_Name
                AND dbo.TB_PARAM_PARAM.PARAM_TYPE = @Param_Type
        IF @Count = 0 
            BEGIN
				-- 获取@Param_Order
                SELECT  @Count = COUNT(*)
                FROM    dbo.TB_PARAM_PARAM
                WHERE   PARAM_TYPE = @Param_Type
                IF @Count = 0 
                    BEGIN
                        SET @Param_Order = 1
                    END
                ELSE 
                    BEGIN
                        SELECT  @Param_Order = MAX(PARAM_ORDER) + 1
                        FROM    dbo.TB_PARAM_PARAM
                        WHERE   PARAM_TYPE = @Param_Type
                    END
				
                INSERT  INTO dbo.TB_PARAM_PARAM
                        ( PARAM_TYPE ,
                          PARAM_NAME ,
                          PARAM_ORDER ,
                          IS_USE ,
                          CREATE_USER ,
                          CREATE_TIME ,
                          LAST_UPDATE_USER ,
                          LAST_UPDATE_TIME
                        )
                VALUES  ( @Param_Type , -- PARAM_TYPE - int
                          @Param_Name , -- PARAM_NAME - nvarchar(100)
                          @Param_Order , -- PARAM_ORDER - int
                          @Is_Use , -- IS_USE - int
                          @Create_User , -- CREATE_USER - int
                          GETDATE() , -- CREATE_TIME - datetime
                          @Last_Update_User , -- LAST_UPDATE_USER - int
                          GETDATE()  -- LAST_UPDATE_TIME - datetime
                        )
            END
        ELSE 
            BEGIN
                RETURN -1
            END
        
    END
GO