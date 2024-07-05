IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_settlement_rollback_getusers]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_settlement_rollback_getusers]
GO
CREATE PROCEDURE [dbo].[pr_calc_settlement_rollback_getusers]
    (
		@Match_Id int,--����ID
		@Calc_Flag int, --0�볡���� 1 ȫ������
		@Match_Type int --1 �������� 2 �ھ�����
    )
AS

BEGIN

	declare @Log_Id int	--������־ID
	
	--���Ȼ�õ�ǰ����������־ID
	select top 1 @Log_Id = log_id from tb_settlement_log
	where match_id=@Match_Id and match_type=@Match_Type 
	and (
		(@Match_Type=2 and calc_flag=@Calc_Flag)	-- �ھ�����
		or		
		(@Match_Type=1 and @Calc_Flag=0 and calc_flag=@Calc_Flag)	--�������°볡���½���
		or
		(@Match_Type=1 and @Calc_Flag=1) --��������ȫ�����½���
	)
	order by begintime desc	

	select @Log_Id Log_Id
	
	select distinct trade_user User_Id
	from tb_settlement_log_detail
	where log_id = @Log_Id
	
END
GO