/***
Create Date:2013/02/06
Description:���ô���Ӷ����&�ɷ�չ��Ա����
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_set_agent_detail]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_set_agent_detail]
    GO
CREATE PROCEDURE pr_set_agent_detail
    (
      @User_Id INT ,
      @Brokerage DECIMAL(4, 4) ,
      @Member_Count INT
    )
AS 
    BEGIN
        DECLARE @Count INT ,
            @Temp_Sec_Agent_Count INT ,
            @Temp_Root_Member_Count INT ,
            @Temp_Sec_Member_Count INT ,
            @Role_ID INT ,
            @Agent_ID INT ,
            @Temp_Brokerage DECIMAL(4, 4) ,
            @Min_Brokerage DECIMAL(4, 4)
        SELECT  @Count = COUNT(1)
        FROM    dbo.TB_AGENT_DETAIL
        WHERE   Agent_User_ID = @User_Id
        
        SELECT  @Role_ID = ROLE_ID
        FROM    dbo.TB_SYSTEM_ACCOUNT
        WHERE   [USER_ID] = @User_Id
        
        SELECT  @Min_Brokerage = MIN(Brokerage_Rate)
        FROM    dbo.TB_BROKERAGE_INTEGRAL_MAP WHERE Status = 1
        
        IF @Role_ID <> 3 --�ܴ�����Ҫ��֤��Ա�����Ƿ�����������ƥ��
            BEGIN
                SELECT  @Temp_Sec_Agent_Count = COUNT(1)
                FROM    dbo.TB_SYSTEM_ACCOUNT
                WHERE   AGENT_ID = @User_Id
            
                SELECT  @Temp_Sec_Member_Count = SUM(a.Member_Count)
                FROM    dbo.TB_AGENT_DETAIL a
                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT b ON a.Agent_User_ID = b.USER_ID
                WHERE   b.AGENT_ID = @User_Id
				
                IF @Member_Count < ( @Temp_Sec_Member_Count
                                     + @Temp_Sec_Agent_Count ) 
                    BEGIN
                        RAISERROR('�ܴ���ɷ�չ��Ա��������',16,1) WITH NOWAIT
                        RETURN
                    END
                IF @Brokerage >= @Min_Brokerage 
                    BEGIN
                        RAISERROR('���������ֵӦС��Ӷ���ʵ���Сֵ',16,1) WITH NOWAIT
                        RETURN
                    END
            END
        
        IF @Role_ID = 3 --1.������Ҫ��֤��Ա�����Ƿ񳬳�;2.������Ҫ��֤�������Ƿ񳬳��ܴ���ķ�����
            BEGIN
                SELECT  @Agent_ID = AGENT_ID
                FROM    dbo.TB_SYSTEM_ACCOUNT
                WHERE   [USER_ID] = @User_Id
                
                IF @Agent_ID IS NULL 
                    BEGIN
                        RAISERROR('�ܴ�������',16,1) WITH NOWAIT
                        RETURN
                    END
                    
                SELECT  @Temp_Brokerage = Brokerage
                FROM    dbo.TB_AGENT_DETAIL
                WHERE   Agent_User_ID = @Agent_ID
                
                --������Ҫ��֤�������Ƿ񳬳��ܴ���ķ�����
                IF @Temp_Brokerage IS NULL 
                    BEGIN
                        RAISERROR('��������ֵ���ܳ������ܴ�������ֵ',16,1) WITH NOWAIT
                        RETURN
                    END
                --PRINT CONVERT(NVARCHAR(10),@Brokerage) + ':' + CONVERT(NVARCHAR(10),@Temp_Brokerage )
                IF @Brokerage >= @Temp_Brokerage 
                    BEGIN
                        RAISERROR('��������ֵ���ܳ������ܴ�������ֵ',16,1) WITH NOWAIT
                        RETURN
                    END
                IF @Brokerage >= @Min_Brokerage 
                    BEGIN
                        RAISERROR('���������ֵӦС��Ӷ���ʵ���Сֵ',16,1) WITH NOWAIT
                        RETURN
                    END
				--�ܴ���ɷ�չ��Ա����
                SELECT  @Temp_Root_Member_Count = Member_Count
                FROM    dbo.TB_AGENT_DETAIL
                WHERE   Agent_User_ID = @Agent_ID
                
                --�ܴ��������������
                SELECT  @Temp_Sec_Agent_Count = COUNT(1)
                FROM    dbo.TB_SYSTEM_ACCOUNT
                WHERE   AGENT_ID = @Agent_ID
                
                --�ܴ����������ɷ�չ��Ա������
                SELECT  @Temp_Sec_Member_Count = ISNULL(SUM(a.Member_Count), 0)
                FROM    dbo.TB_AGENT_DETAIL a
                        INNER JOIN dbo.TB_SYSTEM_ACCOUNT b ON a.Agent_User_ID = b.USER_ID
                WHERE   b.AGENT_ID = @Agent_ID
                        AND a.Agent_User_ID <> @User_Id
                
                IF @Member_Count > ( @Temp_Root_Member_Count
                                     - @Temp_Sec_Agent_Count
                                     - @Temp_Sec_Member_Count ) 
                    BEGIN
                        RAISERROR('����ɷ�չ��Ա��������,����ϵ�ܴ���',16,1) WITH NOWAIT
                        RETURN
                    END
            END
        IF @Count = 0 
            BEGIN
                INSERT  INTO dbo.TB_AGENT_DETAIL
                        ( Agent_User_ID ,
                          Brokerage ,
                          Member_Count
                        )
                VALUES  ( @User_Id , -- Agent_User_ID - int
                          @Brokerage , -- Brokerage - decimal
                          @Member_Count  -- Member_Count - int
                        )
                
            END
        ELSE 
            BEGIN
                UPDATE  dbo.TB_AGENT_DETAIL
                SET     Brokerage = @Brokerage ,
                        Member_Count = @Member_Count
                WHERE   Agent_User_ID = @User_Id
            END
    END
GO