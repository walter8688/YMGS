/***
Create Date:2013/02/05
Description:����Ӷ����ֶ�Ӧ����
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_up_brokerage_integral_map]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_up_brokerage_integral_map]
GO
CREATE PROCEDURE pr_up_brokerage_integral_map
    (
      @Brokerage_Rage_Id INT ,
      @Brokerage_Rage DECIMAL(4, 4) ,
      @Min_Integral INT ,
      @Max_Integral INT ,
      @Cur_User INT
    )
AS 
    BEGIN
        DECLARE @IsExists INT
        SET @IsExists = 0
        DECLARE Cur_Brokerage CURSOR
        FOR
            SELECT  Min_Integral ,
                    Max_Integral
            FROM    dbo.TB_BROKERAGE_INTEGRAL_MAP
            WHERE   [Status] = 1
        DECLARE @Temp_Min_Integral INT ,
            @Temp_Max_Integral INT
        OPEN Cur_Brokerage
        FETCH NEXT FROM Cur_Brokerage INTO @Temp_Min_Integral,
            @Temp_Max_Integral
        WHILE @@FETCH_STATUS = 0 
            BEGIN
                IF ( @Min_Integral > @Temp_Min_Integral
                     AND @Min_Integral < @Temp_Max_Integral
                   )
                    OR ( @Max_Integral > @Temp_Min_Integral
                         AND @Max_Integral < @Temp_Max_Integral
                       ) 
                    BEGIN
                        SET @IsExists = 1	
                        BREAK
                    END
                    
                FETCH NEXT FROM Cur_Brokerage INTO @Temp_Min_Integral,
                    @Temp_Max_Integral
            END
        CLOSE Cur_Brokerage
        DEALLOCATE Cur_Brokerage
		
        IF @IsExists = 1 
            BEGIN
                RAISERROR('�˻��������Ѿ����ڻ����ص�',16,1) WITH NOWAIT
                RETURN
            END
    
        UPDATE  dbo.TB_BROKERAGE_INTEGRAL_MAP
        SET     Brokerage_Rate = @Brokerage_Rage ,
                Min_Integral = @Min_Integral ,
                Max_Integral = @Max_Integral ,
                Last_Update_User = @Cur_User ,
                Last_Update_Time = GETDATE()
        WHERE   Brokerage_Rate_ID = @Brokerage_Rage_Id
    END
    
GO