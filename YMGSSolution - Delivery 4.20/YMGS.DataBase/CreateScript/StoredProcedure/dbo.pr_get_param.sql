/***
Create Date:2013/01/10
Description:查询辅助参数
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_param]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_param]
GO
CREATE PROCEDURE pr_get_param
    (
      @Param_Type INT ,
      @Param_Name NVARCHAR(50)
    )
AS 
    BEGIN
		--@Param_Type=0查询所有数据
        SELECT  PARAM_ID ,
                PARAM_NAME ,
                PARAM_ORDER ,
                PARAM_TYPE ,
                PARAM_TYPE_NAME ,
                CASE IS_USE
                  WHEN 0 THEN '否'
                  WHEN 1 THEN '是'
                END AS IS_USE
        FROM    dbo.TB_PARAM_PARAM
                INNER JOIN dbo.TB_PARAM_TYPE ON dbo.TB_PARAM_PARAM.PARAM_TYPE = dbo.TB_PARAM_TYPE.PARAM_TYPE_ID
        WHERE   ( PARAM_TYPE_ID = @Param_Type
                  OR @Param_Type = -1
                )
                AND ( PARAM_NAME LIKE '%' + @Param_Name + '%'
                      OR @Param_Name = ''
                    )
        ORDER BY TB_PARAM_TYPE.PARAM_TYPE_NAME ,
                TB_PARAM_PARAM.PARAM_ORDER 
    END
GO