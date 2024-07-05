IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_agent_commission]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_agent_commission]
GO
CREATE PROCEDURE [dbo].[pr_calc_agent_commission]
    (
		@User_Id int,
		@Benefits decimal(18,2),
		@Agent_Fund_Id int output,
		@Agent_Commission decimal(18,2) output,
		@Main_Agent_Fund_Id int output,
		@Main_Agent_Commission decimal(18,2) OUTPUT,
		@Agent_Commission_Rate DECIMAL(18,2) OUTPUT,
		@Main_Agent_Commission_Rate DECIMAL(18,2) OUTPUT
    )
AS

--declare @User_Id int
--declare @Benefits decimal(18,2)
--declare @Agent_Fund_Id int
--declare @Agent_Commission decimal(18,2)
--declare @Main_Agent_Fund_Id int
--declare @Main_Agent_Commission decimal(18,2)

--set @User_Id=22
--set @Benefits = 1000

BEGIN
	declare @Agent_Id int
	declare @Main_Agent_Id int
	declare @Agent_Brokerage decimal(18,4)

	--由于目前角色可以增加，所以目前采取递归的方式来
	--确定代理或总代�?

	--先计算代�?
	SELECT @Agent_Fund_Id=B.USER_FUND_ID,@Agent_Id=A.AGENT_ID
	FROM TB_SYSTEM_ACCOUNT A
	INNER JOIN TB_USER_FUND B ON A.AGENT_ID=B.USER_ID
	WHERE A.USER_ID=@User_Id

	IF(@Agent_Fund_Id is null or @Agent_Fund_Id=0
		or @Agent_Id is null)
		return

	SELECT @Agent_Commission=BROKERAGE*@Benefits,@Agent_Brokerage=BROKERAGE
	FROM TB_AGENT_DETAIL
	WHERE AGENT_USER_ID=@Agent_Id
	SET @Agent_Commission=isnull(@Agent_Commission,0)
	SET @Agent_Commission_Rate = ISNULL(@Agent_Brokerage,0)

	--再计算总代�?
	SELECT @Main_Agent_Fund_Id=B.USER_FUND_ID,@Main_Agent_Id=A.AGENT_ID
	FROM TB_SYSTEM_ACCOUNT A
	INNER JOIN TB_USER_FUND B ON A.AGENT_ID=B.USER_ID
	WHERE A.USER_ID=@Agent_Id

	IF(@Main_Agent_Fund_Id is null or @Main_Agent_Fund_Id=0
		or @Main_Agent_Id is null)
		return

	SET @Agent_Brokerage=isnull(@Agent_Brokerage,0)
	SELECT @Main_Agent_Commission=(BROKERAGE-@Agent_Brokerage)*@Benefits,
	@Main_Agent_Commission_Rate = (BROKERAGE-@Agent_Brokerage)
	FROM TB_AGENT_DETAIL
	WHERE AGENT_USER_ID=@Main_Agent_Id
	SET @Main_Agent_Commission_Rate = ISNULL(@Main_Agent_Commission_Rate,0)
	SET @Main_Agent_Commission=isnull(@Main_Agent_Commission,0)
	IF(@Main_Agent_Commission<0)
		SET @Main_Agent_Commission=0

END
GO