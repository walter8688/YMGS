IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[query_vcard_detail]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[query_vcard_detail]
    GO
CREATE PROCEDURE query_vcard_detail
    (
      @vcard_no NVARCHAR(100) ,
      @vcard_activate_no NVARCHAR(100)
    )
AS 
    BEGIN
        SELECT  *
        FROM    dbo.TB_VCARD_DETAIL
        WHERE   VCARD_NO = @vcard_no
                AND VCARD_ACTIVATE_NO = @vcard_activate_no
    END
GO