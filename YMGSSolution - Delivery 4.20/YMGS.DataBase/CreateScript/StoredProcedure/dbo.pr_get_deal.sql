IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_deal]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_deal]
go
CREATE PROCEDURE [dbo].[pr_get_deal]
    (
      @BETTYPE INT ,
      @BETID INT ,
      @MATCH_ID INT ,
      @MATCH_TYPE INT
    )
AS 
    BEGIN
        IF ( @BETTYPE = 1 ) 
            BEGIN
                SELECT  *
                FROM    TB_EXCHANGE_DEAL
                WHERE   MATCH_ID = @MATCH_ID
                        AND MATCH_TYPE = @MATCH_TYPE
                        AND EXCHANGE_BACK_ID = @BETID
            END
        IF ( @BETTYPE = 2 ) 
            BEGIN
                SELECT  *
                FROM    TB_EXCHANGE_DEAL
                WHERE   MATCH_ID = @MATCH_ID
                        AND MATCH_TYPE = @MATCH_TYPE
                        AND EXCHANGE_LAY_ID = @BETID
            END       
    END
GO