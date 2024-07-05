/***
Create Date:2013/01/10
Description:维护辅助参数类型
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_handler_param_type]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 ) 
    DROP PROCEDURE pr_handler_param_type
    GO
CREATE PROCEDURE [dbo].[pr_handler_param_type]
    (
      @Handler_Type INT ,--0:获取数据;1:新增数据;2:更新数据;3:删除数据
      @Param_Type_ID INT ,
      @Param_Type_Name NVARCHAR(50)
    )
AS 
    DECLARE @Count INT
    BEGIN
		--0:获取数据
        IF @Handler_Type = 0 
            BEGIN
                SELECT  *
                FROM    dbo.TB_PARAM_TYPE
            END
		--1:新增数据
        ELSE 
            IF @Handler_Type = 1 
                BEGIN
					--判断类型是否存在
                    SELECT  @count = COUNT(*)
                    FROM    dbo.TB_PARAM_TYPE
                    WHERE   PARAM_TYPE_NAME = @Param_Type_Name
                    IF ( @Count < 1 ) 
                        BEGIN
                            INSERT  INTO dbo.TB_PARAM_TYPE
                                    ( PARAM_TYPE_NAME )
                            VALUES  ( @Param_Type_Name  -- PARAM_TYPE_NAME - nvarchar(50)
                                      )
                        END
                    ELSE 
                        BEGIN
                            RETURN -1
                        END
                    SET @Count = 0
                END
		--2:更新数据
            ELSE 
                IF @Handler_Type = 2 
                    BEGIN
						--判断类型是否存在
                        SELECT  @count = COUNT(*)
                        FROM    dbo.TB_PARAM_TYPE
                        WHERE   PARAM_TYPE_NAME = @Param_Type_Name
                        IF ( @Count < 1 ) 
                            BEGIN
                                UPDATE  dbo.TB_PARAM_TYPE
                                SET     PARAM_TYPE_NAME = @Param_Type_Name
                                WHERE   PARAM_TYPE_ID = @Param_Type_ID
                            END
                        ELSE 
                            BEGIN
                                RETURN -1
                            END
                        SET @Count = 0
                    END
		--3:删除数据
                ELSE 
                    IF @Handler_Type = 3 
                        BEGIN
							--删除TB_Param_Param表内对象类型数据
                            DELETE  FROM dbo.TB_PARAM_PARAM
                            WHERE   PARAM_TYPE = @Param_Type_ID
                            --删除TB_Param_Type相应类型
                            DELETE  FROM dbo.TB_PARAM_TYPE
                            WHERE   PARAM_TYPE_ID = @Param_Type_ID
                        END
    END
GO