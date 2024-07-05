/***
Create Date:2013/01/22
Description:获取参数类型获取辅助参数
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_param_bytype]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_param_bytype]
GO

CREATE PROCEDURE [dbo].[pr_get_param_bytype] 
(@ParamType INT)
AS 
    SELECT  dbo.TB_PARAM_PARAM.PARAM_ID ,
            dbo.TB_PARAM_PARAM.PARAM_NAME ,
            dbo.TB_PARAM_PARAM.PARAM_TYPE
    FROM    dbo.TB_PARAM_PARAM
    WHERE   dbo.TB_PARAM_PARAM.PARAM_TYPE = @ParamType
            AND IS_USE = 1
    ORDER BY dbo.TB_PARAM_PARAM.PARAM_ORDER
GO