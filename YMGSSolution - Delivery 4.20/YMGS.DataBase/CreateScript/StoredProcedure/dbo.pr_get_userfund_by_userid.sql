/***
Create Date:2013/01/28
Description:更新用户资金账户信息
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_userfund_by_userid]')
                    AND OBJECTPROPERTY(ID, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_userfund_by_userid]
GO
CREATE PROCEDURE [dbo].[pr_get_userfund_by_userid] ( @UserID INT )
AS 
    BEGIN
        EXEC pr_inti_userfund @UserID
    
        SELECT  [USER_FUND_ID] ,
                [USER_ID] ,
                [BANK_NAME] ,
                [OPEN_BANK_NAME] ,
                [CARD_NO] ,
                [ACCOUNT_HOLDER] ,
                [CUR_FUND] ,
                [FREEZED_FUND] ,
                [CUR_INTEGRAL] ,
                [STATUS] ,
                [LAST_UPDATE_TIME]
        FROM    dbo.TB_USER_FUND
        WHERE   [USER_ID] = @UserID
    END
GO