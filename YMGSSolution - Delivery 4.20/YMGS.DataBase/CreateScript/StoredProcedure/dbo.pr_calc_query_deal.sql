IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_calc_query_deal]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_calc_query_deal]
GO
CREATE PROCEDURE [dbo].[pr_calc_query_deal]
    (
	  @Match_Id int,
	  @Calc_Flag int --0�볡���� 1 ȫ������
    )
AS

BEGIN
	--��ѯ��ǰ���Խ��н�������д�Ͻ��׼�¼
	SELECT A.EXCHANGE_DEAL_ID,
		A.MATCH_ID,
		A.MARKET_ID,
		A.EXCHANGE_BACK_ID,
		A.EXCHANGE_LAY_ID,
		A.DEAL_AMOUNT,
		A.DEAL_TIME,
		A.STATUS,
		A.MATCH_TYPE,
		A.ODDS,
		C.MARKET_NAME,
		C.MARKET_FLAG,
		C.SCOREA MARKET_SCOREA,
		C.SCOREB MARKET_SCOREB,
		D.MARKET_TMP_TYPE,
		D.MARKET_TMP_NAME,
		D.BET_TYPE_ID
	FROM TB_EXCHANGE_DEAL A INNER JOIN TB_MATCH B ON A.MATCH_ID=B.MATCH_ID
	INNER JOIN TB_MATCH_MARKET C ON A.MARKET_ID=C.MARKET_ID
	LEFT JOIN TB_MARKET_TEMPLATE D ON C.MARKET_TMP_ID=D.MARKET_TMP_ID
	WHERE A.MATCH_ID=@Match_Id
	AND (
		(@Calc_Flag=0 AND B.STATUS in(select * from dbo.udf_get_footbal_halfcalc_status()) AND D.MARKET_TMP_TYPE=0) ---�볡���淨
		OR
		(@Calc_Flag=1 AND B.STATUS=4)----ȫ������ʱ�������еĴ�Ͻ��׼�¼
	)
	AND B.ADDITIONALSTATUS=1 --���������еı���
	AND A.STATUS=1 --�����Ĵ�Ͻ��׼�¼
	AND A.MATCH_TYPE=1
END
GO