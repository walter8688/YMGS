/***
Create Date:2013/01/08
Description:获取区域参数数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_param_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_param_zone]
GO

CREATE PROCEDURE [dbo].[pr_get_param_zone]
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_PARAM_ZONE
    END
go