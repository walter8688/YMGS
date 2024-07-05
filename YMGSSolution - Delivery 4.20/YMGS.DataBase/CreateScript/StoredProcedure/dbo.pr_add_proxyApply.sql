IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_add_proxyApply]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_add_proxyApply]
    GO
CREATE PROCEDURE pr_add_proxyApply
    (
		@Apply_Proxy_ID INT OUTPUT,
		@User_ID INT,
		@Role_ID INT,
		@User_Telephone NVARCHAR(50),
		@User_Country NVARCHAR(50),
		@User_Province NVARCHAR(50),
		@User_City NVARCHAR(50),
		@User_BankAddress NVARCHAR(50),
		@User_BankNO NVARCHAR(50),
		@Apply_Status INT,
		@Apply_Date DATETIME
    )
AS 
    BEGIN
		INSERT INTO [TB_APPLY_PROXY](
			[User_ID],[Role_ID],[User_Telephone],[User_Country],[User_Province],[User_City],
			[User_BankAddress],[User_BankNO],[Apply_Status],[Apply_Date]
		)VALUES(
			@User_ID,@Role_ID,@User_Telephone,@User_Country,@User_Province,@User_City,
			@User_BankAddress,@User_BankNO,@Apply_Status,@Apply_Date
		)
		SET @Apply_Proxy_ID = @@IDENTITY
    END
GO
