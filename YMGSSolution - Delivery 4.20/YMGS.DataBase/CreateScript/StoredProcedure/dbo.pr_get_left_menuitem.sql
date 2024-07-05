if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_get_left_menuitem]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_get_left_menuitem]
GO

CREATE PROCEDURE [dbo].[pr_get_left_menuitem]
AS
BEGIN
select m.MATCH_ID,m.MATCH_NAME,m.STARTDATE,m.EVENT_ID,E.EVENT_NAME,E.EVENTZONE_ID,EZ.EVENTZONE_NAME,EZ.EVENTITEM_ID,EI.EventItem_Name 
from dbo.TB_Match m
left join dbo.TB_Event E on E.EVENT_ID=m.EVENT_ID
left join dbo.TB_EVENT_ZONE EZ on EZ.EVENTZONE_ID=E.EVENTZONE_ID
left join dbo.TB_EVENT_ITEM EI on ei.EventItem_ID=ez.EVENTITEM_ID
 where  m.STATUS in (2,3,4) and EI.EventItem_ID is not null and ez.EVENTZONE_ID is not null and E.EVENT_ID is not null;
 
 END

GO