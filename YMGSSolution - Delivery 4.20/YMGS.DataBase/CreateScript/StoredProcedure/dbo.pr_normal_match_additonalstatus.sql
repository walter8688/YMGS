IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_normal_match_additonalstatus]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_normal_match_additonalstatus]
GO
CREATE PROCEDURE [dbo].[pr_normal_match_additonalstatus]
    (
      @MatchID INT ,
      @LastUpdateUser INT
    )
AS 
    BEGIN
        DECLARE @temp_addtional_status INT
        SELECT  @temp_addtional_status = ADDITIONALSTATUS
        FROM    dbo.TB_MATCH
        WHERE   MATCH_ID = @MatchID
        IF @temp_addtional_status = 2 
            BEGIN
                UPDATE  dbo.TB_MATCH
                SET     ADDITIONALSTATUS = 1
                WHERE   MATCH_ID = @MatchID
                        AND LAST_UPDATE_USER = @LastUpdateUser

				--���»�������
				exec pr_up_cache_object 3
            END
        ELSE 
            BEGIN
                RAISERROR('��ǰ״̬�²����л�������״̬!',16,1) WITH NOWAIT
            END
    END
GO