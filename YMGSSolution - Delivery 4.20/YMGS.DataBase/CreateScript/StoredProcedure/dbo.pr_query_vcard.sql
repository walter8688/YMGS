IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_query_vcard]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_query_vcard]
    GO
CREATE PROCEDURE pr_query_vcard
    (
      @VCard_Face_Value INT ,
      @VCard_Status INT ,
      @Start_Date DATETIME ,
      @End_Date DATETIME
    )
AS 
    BEGIN
        SELECT  TB_VCARD_DETAIL.* ,
                ISNULL(dbo.TB_SYSTEM_ACCOUNT.LOGIN_NAME, '') LOGIN_NAME
        FROM    dbo.TB_VCARD_DETAIL
                LEFT OUTER JOIN dbo.TB_SYSTEM_ACCOUNT ON dbo.TB_VCARD_DETAIL.ACTIVATE_USER_ID = dbo.TB_SYSTEM_ACCOUNT.USER_ID
        WHERE   ( VCARD_FACE_VALUE = @VCard_Face_Value
                  OR @VCard_Face_Value = -1
                  OR @VCard_Face_Value IS NULL
                )
                AND ( VCARD_STATUS = @VCard_Status
                      OR @VCard_Status = -1
                      OR @VCard_Status IS NULL
                    )
                AND ( TB_VCARD_DETAIL.CREATE_DATE >= @Start_Date
                      OR @Start_Date IS NULL
                    )
                AND ( TB_VCARD_DETAIL.CREATE_DATE <= @End_Date
                      OR @End_Date IS NULL
                    )
        ORDER BY CREATE_DATE ,
                ACTIVATE_DATE                    
    END
GO