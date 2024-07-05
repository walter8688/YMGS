if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_test_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_test_ins]
GO
CREATE PROCEDURE dbo.pr_test_ins
(
    @Names nvarchar(50),
    @Description nvarchar(max)
)
AS

BEGIN
    INSERT INTO YMGS_TEST
    (
        [NAMES],
		[DESCRIPTION]
    )
    VALUES
    (       
        @Names,
		@Description
    ) 
    
    select SCOPE_IDENTITY(); 
END

GO