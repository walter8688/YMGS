IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_clear_market]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_clear_market]
GO
CREATE PROCEDURE pr_clear_market
    (
      @Match_Id INT ,
      @Match_Type INT 
    )
AS 
    BEGIN
        SET XACT_ABORT ON
        BEGIN TRANSACTION
        DECLARE @Error_Msg NVARCHAR(100) ,
            @Cur_Freezed_Fund DECIMAL(20,2)
        DECLARE ClearMarketCur CURSOR FORWARD_ONLY
        FOR
            SELECT  MATCH_AMOUNTS ,
                    TRADE_USER ,
                    EXCHANGE_BACK_ID AS Lay_Or_Back_Excange_Id
            FROM    dbo.TB_EXCHANGE_BACK
            WHERE   MATCH_ID = @Match_Id
                    AND MATCH_TYPE = @Match_Type
                    AND STATUS = 1
            UNION ALL
            SELECT  MATCH_AMOUNTS * ( ODDS - 1 ) ,
                    TRADE_USER ,
                    EXCHANGE_LAY_ID AS Lay_Or_Back_Excange_Id
            FROM    dbo.TB_EXCHANGE_LAY
            WHERE   MATCH_ID = @Match_Id
                    AND MATCH_TYPE = @Match_Type
                    AND STATUS = 1
        DECLARE @Unfreeze_Fund DECIMAL(20,2) ,
            @User_Id INT ,
            @User_Fund_Id INT ,
            @Lay_Or_Back_Excange_Id INT ,
            @EXType NVARCHAR(1)
        OPEN ClearMarketCur
        FETCH NEXT FROM ClearMarketCur INTO @Unfreeze_Fund, @User_Id,
            @Lay_Or_Back_Excange_Id
        WHILE @@FETCH_STATUS = 0 
            BEGIN
				--��ȡ��ǰ�ʽ��˻�������ʽ�
                SELECT  @Cur_Freezed_Fund = FREEZED_FUND ,@User_Fund_Id = USER_FUND_ID
                FROM    dbo.TB_USER_FUND WHERE   [USER_ID] = @User_Id
                --��֤�ⶳ�ʽ��Ƿ�С�ڵ��ڵ�ǰ�ʽ��˻�������ʽ�
                IF ( @Cur_Freezed_Fund < @Unfreeze_Fund ) 
                    BEGIN
                        SET @Error_Msg = '����ID:' + CONVERT(NVARCHAR(100),@Match_Id) + ';�û�ID:' + CONVERT(NVARCHAR(100),@User_Id)+ ';�ɽⶳ�ʽ���,�����ѻع�!'
                        RAISERROR(@Error_Msg,16,1) WITH NOWAIT
                        ROLLBACK TRANSACTION
                        RETURN
                    END
                ELSE 
                    BEGIN
						--�ⶳ�ʽ�
                        UPDATE  dbo.TB_USER_FUND
                        SET     CUR_FUND = CUR_FUND + @Unfreeze_Fund ,
                                FREEZED_FUND = FREEZED_FUND - @Unfreeze_Fund ,
                                LAST_UPDATE_TIME = GETDATE()
                        WHERE   [USER_ID] = @User_Id
                        --��¼�ʽ���ʷ
                        INSERT  INTO dbo.TB_FUND_HISTORY
                                ( USER_FUND_ID ,
                                  TRADE_TYPE ,
                                  TRADE_DESC ,
                                  TRADE_SERIAL_NO ,
                                  TRADE_FUND ,
                                  TRADE_DATE
                                )
                        VALUES  ( @User_Fund_Id , -- USER_FUND_ID - int
                                  4 , -- TRADE_TYPE - int
                                  N'�����г�ʱδ�ɽ���Ͷע�ⶳ���ʽ�' , -- TRADE_DESC - nvarchar(100)
                                  @Lay_Or_Back_Excange_Id , -- TRADE_SERIAL_NO - int
                                  @Unfreeze_Fund , -- TRADE_FUND - decimal
                                  GETDATE()  -- TRADE_DATE - datetime
                                )
                    END
                FETCH NEXT FROM ClearMarketCur INTO @Unfreeze_Fund, @user_id,
                    @Lay_Or_Back_Excange_Id
            END        
        CLOSE ClearMarketCur
        DEALLOCATE ClearMarketCur
        
        --����Ͷ/��ע״̬Ϊ�ѷ���
        UPDATE  dbo.TB_EXCHANGE_LAY
        SET     MATCH_AMOUNTS = 0, STATUS = 6
        WHERE   MATCH_ID = @Match_Id AND MATCH_TYPE = @Match_Type AND STATUS = 1

        UPDATE  dbo.TB_EXCHANGE_BACK
        SET     MATCH_AMOUNTS = 0, STATUS = 6
        WHERE   MATCH_ID = @Match_Id AND MATCH_TYPE = @Match_Type AND STATUS = 1
        
        IF @Match_Type = 1
        BEGIN
        	UPDATE dbo.TB_MATCH SET ADDITIONALSTATUS = 1 WHERE MATCH_ID = @Match_Id
        END

		--���»�������
		exec pr_up_cache_object 3

        --PRINT @@ERROR
        COMMIT TRANSACTION
    END
GO