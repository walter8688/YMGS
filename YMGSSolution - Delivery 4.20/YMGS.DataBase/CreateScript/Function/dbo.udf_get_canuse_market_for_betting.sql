IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[udf_get_canuse_market_for_betting]') AND xtype IN (N'FN', N'IF', N'TF'))
BEGIN
	DROP FUNCTION dbo.[udf_get_canuse_market_for_betting]
END
GO

CREATE function [dbo].[udf_get_canuse_market_for_betting]
(
) 
returns @TABLE table 
(
    [Market_Id] int,
    [Match_Id] int
) 
as
begin 

	INSERT INTO @TABLE
	SELECT DISTINCT A.MARKET_ID,B.MATCH_ID
	FROM TB_MATCH_MARKET A
	INNER JOIN TB_MATCH B ON A.MATCH_ID=B.MATCH_ID	
	LEFT JOIN TB_MARKET_TEMPLATE C ON A.MARKET_TMP_ID=C.MARKET_TMP_ID
	WHERE B.STATUS IN(1,2,3,7) AND B.ADDITIONALSTATUS IN(1,3)  AND ISNULL(A.MARKET_STATUS,1)=1
	AND 
	( 
		(
			ISNULL(B.IS_ZOUDI,0)=1 --������ߵ�
			AND (
					(C.BET_TYPE_ID=1 AND (
						(MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --�볡��׼��ֻ������ǰ���ϰ볡������ע
						or
						(MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --ȫ����׼��ֻ������ǰ��������ע
						OR
						(MARKET_TMP_TYPE=2 AND B.STATUS IN(1)) --�볡/ȫ����׼��ֻ������ǰ��ע
					))
					OR
					(C.BET_TYPE_ID=2 AND B.STATUS IN(1)) --����:ֻ����ǰ��ע
					OR
					(C.BET_TYPE_ID=3 AND MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --��С��:�볡ֻ������ǰ���ϰ볡��������ע
					OR
					(C.BET_TYPE_ID=3 AND MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --��С��:ȫ����С���������ǰ���ж�������ע
					OR
					(C.BET_TYPE_ID=4 AND MARKET_TMP_TYPE=0 AND B.STATUS IN(1,2)) --������:��ǰ���ж�������ע	
					OR
					(C.BET_TYPE_ID=4 AND MARKET_TMP_TYPE=1 AND B.STATUS IN(1,2,3,7)) --������:��ǰ���ж�������ע
				)
		)
		
		OR
		
		(
			ISNULL(B.IS_ZOUDI,0)=0 --���ߵ�
			AND B.STATUS=1
		)
	)

    return
end
GO