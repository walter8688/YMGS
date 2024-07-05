/***
Create Date:2013/01/08
Description:新增区域参数数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_param_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_param_zone]
GO
CREATE PROCEDURE dbo.pr_add_param_zone
    (
      @Parent_Zone_ID INT ,
      @Zone_Name NVARCHAR(40)
    )
AS 
    DECLARE @Count INT ,
        @Zone_Order INT
    BEGIN
		--确定ZoneOrder
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_PARAM_ZONE
        WHERE   PARENT_ZONE_ID = @Parent_Zone_ID
        IF @Count = 0 
            BEGIN
                SET @Zone_Order = 1
            END
        ELSE 
            BEGIN
                SELECT  @Zone_Order = MAX(ZONE_ORDER) + 1
                FROM    dbo.TB_PARAM_ZONE
                WHERE   PARENT_ZONE_ID = @Parent_Zone_ID
            END
		--新增ParamZone
        SELECT  @Count = COUNT(*)
        FROM    dbo.TB_PARAM_ZONE
        WHERE   dbo.TB_PARAM_ZONE.ZONE_NAME = @Zone_Name
                AND PARENT_ZONE_ID = @Parent_Zone_ID
        IF @Count = 0 
            BEGIN
                INSERT  INTO dbo.TB_PARAM_ZONE
                        ( PARENT_ZONE_ID ,
                          ZONE_NAME ,
                          ZONE_ORDER
	                  )
                VALUES  ( @Parent_Zone_ID , -- PARENT_ZONE_ID - int
                          @Zone_Name , -- ZONE_NAME - nvarchar(40)
                          @Zone_Order  -- ZONE_ORDER - int
	                  )
            END
        ELSE 
            BEGIN
                RETURN -1
            END
        
    END
GO