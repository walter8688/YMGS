/***
Create Date:2013/02/05
Description:新增佣金积分对应数据
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_brokerage_integral_map]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_brokerage_integral_map]
GO
CREATE PROCEDURE pr_add_brokerage_integral_map
    (
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
                IF ( @Min_Integral >= @Temp_Min_Integral
                     AND @Min_Integral <= @Temp_Max_Integral
                   )
                    OR ( @Max_Integral >= @Temp_Min_Integral
                         AND @Max_Integral <= @Temp_Max_Integral
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
                RAISERROR('此积分区间已经存在或者重叠',16,1) WITH NOWAIT
                RETURN
            END
		
        INSERT  INTO dbo.TB_BROKERAGE_INTEGRAL_MAP
                ( Brokerage_Rate ,
                  [Status] ,
                  Min_Integral ,
                  Max_Integral ,
                  Create_User ,
                  Create_Time ,
                  Last_Update_User ,
                  Last_Update_Time
                )
        VALUES  ( @Brokerage_Rage , -- Brokerage_Rate - decimal
                  1 , -- Status - int
                  @Min_Integral , -- Min_Integral - int
                  @Max_Integral , -- Max_Integral - int
                  @Cur_User , -- Create_User - int
                  GETDATE() , -- Create_Time - datetime
                  @Cur_User , -- Last_Update_User - int
                  GETDATE()  -- Last_Update_Time - datetime
                )
    END
GO
