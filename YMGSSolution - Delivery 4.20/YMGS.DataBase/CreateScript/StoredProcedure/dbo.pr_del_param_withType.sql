/***
Create Date:2013/01/10
Description:通过参数类型删除辅助参数
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_param_withType]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE pr_del_param_withType
GO
CREATE PROCEDURE [dbo].pr_del_param_withType ( @Param_Type INT )
AS 
    BEGIN
        DELETE  FROM dbo.TB_PARAM_PARAM
        WHERE   PARAM_TYPE = @Param_Type 
    END
GO