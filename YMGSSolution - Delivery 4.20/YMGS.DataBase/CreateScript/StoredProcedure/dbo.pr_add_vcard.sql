IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_vcard]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_vcard]
    GO
CREATE PROCEDURE pr_add_vcard
    (
      @VCard_No NVARCHAR(100) ,
      @VCard_Activate_No NVARCHAR(100) ,
      @VCard_Face_Value INT ,
      @Create_User_Id INT 
    )
AS 
    BEGIN
        DECLARE @Temp_Count INT
		
        SELECT  @Temp_Count = COUNT(1)
        FROM    dbo.TB_VCARD_DETAIL
        WHERE   VCARD_NO = @VCard_No
		
        IF @Temp_Count = 0 
            BEGIN
                INSERT  INTO dbo.TB_VCARD_DETAIL
                        ( VCARD_NO ,
                          VCARD_ACTIVATE_NO ,
                          VCARD_FACE_VALUE ,
                          VCARD_STATUS ,
                          CREATE_USER_ID ,
                          CREATE_DATE 
		            )
                VALUES  ( @VCard_No , -- VCARD_NO - nvarchar(100)
                          @VCard_Activate_No , -- VCARD_ACTIVATE_NO - nvarchar(100)
                          @VCard_Face_Value , -- VCARD_FACE_VALUE - int
                          0 , -- VCARD_STATUS - int
                          @Create_User_Id , -- CREATE_USER_ID - int
                          GETDATE()  -- CREATE_DATE - datetime
		            )
		         SELECT SCOPE_IDENTITY()
            END
        ELSE 
            BEGIN
                SELECT  -1 
            END
		
    END
GO