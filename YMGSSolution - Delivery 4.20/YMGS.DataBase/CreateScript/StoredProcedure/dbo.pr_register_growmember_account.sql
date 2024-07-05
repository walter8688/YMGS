IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_register_growmember_account]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_register_growmember_account]
GO

CREATE PROCEDURE pr_register_growmember_account
    (
      @USER_NAME NVARCHAR(40) ,
      @BORN_YEAR NVARCHAR(4) ,
      @BORN_MONTH NVARCHAR(2) ,
      @BORN_DAY NVARCHAR(2) ,
      @COUNTRY INT ,
      @CITY NVARCHAR(50) ,
      @ADDRESS NVARCHAR(100) ,
      @ZIP_CODE NVARCHAR(10) ,
      @PHONE_TYPE INT ,
      @PHONE_ZONE NVARCHAR(5) ,
      @PHONE_NUMBER NVARCHAR(20) ,
      @LOGIN_NAME NVARCHAR(20) ,
      @PASSWORD NVARCHAR(200) ,
      @SQUESTION1 INT ,
      @SANSWER1 NVARCHAR(100) ,
      @ACCOUNT_STATUS INT 
    )
AS 
    BEGIN
        UPDATE  dbo.TB_SYSTEM_ACCOUNT
        SET     [USER_NAME] = @USER_NAME ,
                BORN_YEAR = @BORN_YEAR ,
                BORN_MONTH = @BORN_MONTH ,
                BORN_DAY = @BORN_DAY ,
                COUNTRY = @COUNTRY ,
                CITY = @CITY ,
                [ADDRESS] = @ADDRESS ,
                ZIP_CODE = @ZIP_CODE ,
                PHONE_TYPE = @PHONE_TYPE ,
                PHONE_ZONE = @PHONE_ZONE ,
                PHONE_NUMBER = @PHONE_NUMBER ,
                [PASSWORD] = @PASSWORD ,
                SQUESTION1 = @SQUESTION1 ,
                SANSWER1 = @SANSWER1 ,
                ACCOUNT_STATUS = @ACCOUNT_STATUS
        WHERE   LOGIN_NAME = @LOGIN_NAME ;
    END

GO