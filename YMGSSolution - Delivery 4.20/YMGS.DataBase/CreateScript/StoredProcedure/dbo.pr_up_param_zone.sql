/***
Create Date:2013/01/08
Description:更新区域参数数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_param_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_param_zone]
GO
CREATE PROCEDURE dbo.pr_up_param_zone
    (
      @Zone_ID INT ,
      @Parent_Zone_ID INT ,
      @Zone_Name NVARCHAR(40)
    )
AS 
    DECLARE @Count INT
    BEGIN
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_PARAM_ZONE
        WHERE   dbo.TB_PARAM_ZONE.ZONE_NAME = @Zone_Name
        IF @Count = 0 
            BEGIN
                UPDATE  dbo.TB_PARAM_ZONE
                SET     dbo.TB_PARAM_ZONE.PARENT_ZONE_ID = @Parent_Zone_ID ,
                        dbo.TB_PARAM_ZONE.ZONE_NAME = @Zone_Name
                WHERE   dbo.TB_PARAM_ZONE.ZONE_ID = @Zone_ID
            END
        ELSE 
            BEGIN
                RETURN -1
            END
    END
GO