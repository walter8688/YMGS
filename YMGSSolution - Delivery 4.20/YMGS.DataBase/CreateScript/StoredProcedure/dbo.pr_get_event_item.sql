/***
Create Date:2013/01/17
Description:获取赛事类别
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_event_item]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_event_item]
go
CREATE PROCEDURE [dbo].[pr_get_event_item]
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_EVENT_ITEM
    END
GO