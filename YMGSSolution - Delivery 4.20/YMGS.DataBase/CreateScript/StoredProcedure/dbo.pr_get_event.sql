/***
Create Date:2013/01/21
Description:获取赛事
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_event]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_event]
go
CREATE PROCEDURE [dbo].[pr_get_event]
    (
      @EVENTZONE_ID INT ,
      @EVENT_NAME NVARCHAR(100) ,
      @EVENT_NAME_En NVARCHAR(100) ,
      @EVENT_DESC NVARCHAR(100) ,
      @START_DATE DATETIME ,
      @END_DATE DATETIME ,
      @STATUS INT ,
      @EVENT_TEAM_ID INT ,
      @EVENT_TEAM_NAME NVARCHAR(50)
    )
AS 
    BEGIN
        SELECT  TB_EVENT.EVENT_ID ,
                TB_EVENT.EVENTZONE_ID ,
                TB_EVENT_ZONE.EVENTZONE_NAME ,
                TB_EVENT.EVENT_NAME ,
                dbo.TB_EVENT.EVENT_NAME_EN ,
                TB_EVENT.EVENT_DESC ,
                TB_EVENT.[START_DATE] ,
                TB_EVENT.END_DATE ,
                CASE TB_EVENT.[STATUS]
                  WHEN 0 THEN '激活'
                  WHEN 1 THEN '暂停'
                  WHEN 2 THEN '终止'
                  WHEN 3 THEN '未激活'
                END AS STATUSNAME ,
                TB_EVENT.[STATUS] ,
                TB_EVENT_ITEM.EventItem_ID
        FROM    dbo.TB_EVENT
                INNER JOIN dbo.TB_EVENT_ZONE ON TB_EVENT.EVENTZONE_ID = TB_EVENT_ZONE.EVENTZONE_ID
                INNER JOIN dbo.TB_EVENT_ITEM ON TB_EVENT_ZONE.EVENTITEM_ID = TB_EVENT_ITEM.EventItem_ID
        WHERE   ( TB_EVENT.EVENTZONE_ID = @EVENTZONE_ID
                  OR @EVENTZONE_ID = -1
                )
                AND ( TB_EVENT.EVENT_NAME LIKE '%' + @EVENT_NAME + '%'
                      OR @EVENT_NAME = ''
                    )
                AND ( TB_EVENT.EVENT_DESC LIKE '%' + @EVENT_DESC + '%'
                      OR @EVENT_DESC = ''
                    )
                AND ( TB_EVENT.[START_DATE] >= @START_DATE
                      OR @START_DATE IS NULL
                    )
                AND ( TB_EVENT.END_DATE <= @END_DATE
                      OR @END_DATE IS NULL
                    )
                AND ( TB_EVENT.[STATUS] = @STATUS
                      OR @STATUS = -1
                    )
                AND ( dbo.TB_EVENT.EVENT_NAME_EN LIKE '%' + @EVENT_NAME_En
                      + '%'
                      OR @EVENT_NAME_En = ''
                    )
        ORDER BY dbo.TB_EVENT.[START_DATE] DESC
    END
GO