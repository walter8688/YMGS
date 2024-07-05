IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_all_country]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_all_country]
    GO
CREATE PROCEDURE pr_get_all_country
AS 
BEGIN
	SELECT COUNTRY_ID,COUNTRY_NAME_CN,COUNTRY_NAME_EN 
	FROM TB_COUNTRY
	ORDER BY COUNTRY_NAME_CN
END
GO