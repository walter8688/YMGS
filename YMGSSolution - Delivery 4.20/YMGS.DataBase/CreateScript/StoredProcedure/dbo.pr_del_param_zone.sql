/***
Create Date:2013/01/08
Description:获取区域参数数据
***/

IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_del_param_zone]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_del_param_zone]
GO

CREATE PROCEDURE dbo.pr_del_param_zone ( @Zone_ID INT )
AS 
    BEGIN
		DECLARE @Temp_Count INT
		SELECT @Temp_Count = COUNT(1) FROM dbo.TB_EVENT_TEAM 
		WHERE EXISTS ( SELECT temp.ZONE_ID FROM 
		(SELECT p3.ZONE_ID FROM dbo.TB_PARAM_ZONE p1 INNER JOIN dbo.TB_PARAM_ZONE p2
		ON p1.ZONE_ID = p2.PARENT_ZONE_ID
	    INNER JOIN dbo.TB_PARAM_ZONE p3 ON p3.PARENT_ZONE_ID = p2.ZONE_ID
        WHERE p1.ZONE_ID = @Zone_ID
        UNION ALL
        SELECT p2.ZONE_ID FROM dbo.TB_PARAM_ZONE p1 INNER JOIN dbo.TB_PARAM_ZONE p2
        ON p1.ZONE_ID = p2.PARENT_ZONE_ID WHERE p1.ZONE_ID = @Zone_ID
        UNION ALL
        SELECT  p1.ZONE_ID FROM TB_PARAM_ZONE p1 WHERE p1.ZONE_ID = @Zone_ID) temp 
        WHERE temp.ZONE_ID = TB_EVENT_TEAM.PARAM_ZONE_ID )
		IF @Temp_Count > 0
		BEGIN
			RAISERROR('当前区域已被引用不能删除!',16,1) WITH NOWAIT
			RETURN
		END
		-- 删除Zone_ID = @Zone_ID的数据
        DELETE  FROM dbo.TB_PARAM_ZONE
        WHERE   dbo.TB_PARAM_ZONE.ZONE_ID = @Zone_ID
        
        DECLARE Cur_Del_ParamZone CURSOR
        FOR
            SELECT  ZONE_ID
            FROM    dbo.TB_PARAM_ZONE
            WHERE   PARENT_ZONE_ID = @Zone_ID
        OPEN Cur_Del_ParamZone
        DECLARE @Temp_ZoneId INT
        FETCH NEXT FROM Cur_Del_ParamZone INTO @Temp_ZoneId
        WHILE @@FETCH_STATUS = 0 
            BEGIN
                DELETE  FROM dbo.TB_PARAM_ZONE
                WHERE   dbo.TB_PARAM_ZONE.ZONE_ID = @Temp_ZoneId
                FETCH NEXT FROM Cur_Del_ParamZone INTO @Temp_ZoneId
            END
        CLOSE Cur_Del_ParamZone
        DEALLOCATE Cur_Del_ParamZone   
    END
GO
