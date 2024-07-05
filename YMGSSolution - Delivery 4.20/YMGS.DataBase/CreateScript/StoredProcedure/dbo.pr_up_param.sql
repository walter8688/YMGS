/***
Create Date:2013/01/10
Description:更新辅助参数
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_param]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_up_param
GO
CREATE PROCEDURE [dbo].[pr_up_param]
    (
      @Param_ID INT ,
      @Param_Type INT ,
      @Param_Name NVARCHAR(50) ,
      @Is_Use INT ,
      @Last_Update_User NVARCHAR(50) 
    )
AS 
    DECLARE @Count INT
    BEGIN
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_PARAM_PARAM
        WHERE   PARAM_NAME = @Param_Name
                AND dbo.TB_PARAM_PARAM.PARAM_TYPE = @Param_Type AND PARAM_ID <> @Param_ID
        IF @Count = 0 
            BEGIN
                UPDATE  dbo.TB_PARAM_PARAM
                SET     PARAM_TYPE = @Param_Type ,
                        PARAM_NAME = @Param_Name ,
                        IS_USE = @Is_Use ,
                        LAST_UPDATE_USER = @Last_Update_User ,
                        LAST_UPDATE_TIME = GETDATE()
                WHERE   PARAM_ID = @Param_ID
            END
        ELSE 
            BEGIN
                RETURN -1
            END
        
    END
GO