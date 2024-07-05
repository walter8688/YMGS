IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_record_match_score]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_record_match_score]
GO
CREATE PROCEDURE pr_record_match_score
    (
      @Match_Id INT ,
      @Home_Fir_Half_Score INT ,
      @Guest_Fir_Half_Score INT ,
      @Home_Sec_Half_Score INT ,
      @Guest_Sec_Half_Score INT ,
      @Home_OverTime_Score INT ,
      @Guest_OverTime_Score INT ,
      @Home_Point_Score INT ,
      @Guest_Point_Score INT ,
      @Home_Full_Score INT ,
      @Guest_Full_Score INT
    )
AS 
    BEGIN
        UPDATE  dbo.TB_MATCH
        SET     HOME_FIR_HALF_SCORE = @Home_Fir_Half_Score ,
                GUEST_FIR_HALF_SCORE = @Guest_Fir_Half_Score ,
                HOME_SEC_HALF_SCORE = @Home_Sec_Half_Score ,
                GUEST_SEC_HALF_SCORE = @Guest_Sec_Half_Score ,
                HOME_OVERTIME_SCORE = @Home_OverTime_Score ,
                GUEST_OVERTIME_SCORE = @Guest_OverTime_Score ,
                HOME_POINT_SCORE = @Home_Point_Score ,
                GUEST_POINT_SCORE = @Guest_Point_Score ,
                HOME_FULL_SCORE = @Home_Full_Score ,
                GUEST_FULL_SCORE = @Guest_Full_Score
        WHERE   MATCH_ID = @Match_Id
        
        --更新缓存对象表
		exec pr_up_cache_object 3
    END
GO