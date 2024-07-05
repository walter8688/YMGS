IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_edit_role_func]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_edit_role_func]
GO
CREATE PROCEDURE [dbo].[pr_edit_role_func]
    (
      @STATUS NVARCHAR(10) ,
      @ROLE_ID INT ,
      @ROLENAME NVARCHAR(40) ,
      @ROLEDESC NVARCHAR(100) ,
      @CREATER INT ,
      @LASTEUPDATEUSER INT ,
      @FUNCLIST NVARCHAR(MAX)
    )
AS 
    BEGIN
        DECLARE @tempCount INT
        DECLARE @NEWROLEID INT
        IF ( @STATUS = 'add' ) 
            BEGIN
                SELECT  @tempCount = COUNT(1)
                FROM    dbo.TB_SYSTEM_ROLE
                WHERE   ROLE_NAME = @ROLENAME
                IF @tempCount > 0 
                    BEGIN
                        RAISERROR('当前角色名已经存在!',16,1) WITH NOWAIT 
                        RETURN
                    END
	  
                INSERT  INTO TB_SYSTEM_ROLE
                        ( ROLE_NAME ,
                          ROLE_DESC ,
                          CREATE_USER ,
                          CREATE_TIME ,
                          LAST_UPDATE_USER ,
                          LAST_UPDATE_TIME
                        )
                VALUES  ( @ROLENAME ,
                          @ROLEDESC ,
                          @CREATER ,
                          GETDATE() ,
                          @LASTEUPDATEUSER ,
                          GETDATE()
                        ) ;
                SELECT  @NEWROLEID = SCOPE_IDENTITY() ;
                INSERT  INTO TB_ROLE_FUNC_MAP
                        ( ROLE_ID ,
                          FUNC_ID
                        )
                        SELECT  @NEWROLEID ,
                                [Value]
                        FROM    [dbo].[SplitString](@FUNCLIST, ',', 1) ;
                SELECT  @NEWROLEID ;
            END

        IF ( @STATUS = 'edit' ) 
            BEGIN
                SELECT  @tempCount = COUNT(1)
                FROM    dbo.TB_SYSTEM_ROLE
                WHERE   ROLE_NAME = @ROLENAME
                        AND ROLE_ID <> @ROLE_ID
                IF @tempCount > 0 
                    BEGIN
                        RAISERROR('当前角色名已经存在!',16,1) WITH NOWAIT 
                        RETURN
                    END
	 
                UPDATE  TB_SYSTEM_ROLE
                SET     ROLE_NAME = @ROLENAME ,
                        ROLE_DESC = @ROLEDESC ,
                        LAST_UPDATE_USER = @LASTEUPDATEUSER ,
                        LAST_UPDATE_TIME = GETDATE()
                WHERE   ROLE_ID = @ROLE_ID ;
                DELETE  FROM TB_ROLE_FUNC_MAP
                WHERE   ROLE_ID = @ROLE_ID ;
                INSERT  INTO TB_ROLE_FUNC_MAP
                        ( ROLE_ID ,
                          FUNC_ID
                        )
                        SELECT  @ROLE_ID ,
                                [Value]
                        FROM    [dbo].[SplitString](@FUNCLIST, ',', 1) ;
                SELECT  @ROLE_ID ;
            END

        IF ( @STATUS = 'del' ) 
            BEGIN
                DECLARE @Temp_Role_Id INT ,
                    @Count INT ,
                    @RaisErrorCode NVARCHAR(50) ;
                SELECT  @Temp_Role_Id = ROLE_ID
                FROM    TB_ROLE_FUNC_MAP
                WHERE   ROLE_ID = @ROLE_ID ;
                SELECT  @Count = COUNT(1)
                FROM    dbo.TB_SYSTEM_ACCOUNT
                WHERE   ROLE_ID = @Temp_Role_Id
                IF @Count > 0 
                    BEGIN
                        SET @RaisErrorCode = '当前角色已和用户关联不能删除!'
                        RAISERROR (@RaisErrorCode,16,1) WITH NOWAIT
                        RETURN 
                    END
                ELSE 
                    BEGIN
                        DELETE  FROM TB_SYSTEM_ROLE
                        WHERE   ROLE_ID = @ROLE_ID ;
                        DELETE  FROM dbo.TB_ROLE_FUNC_MAP
                        WHERE   ROLE_ID = @ROLE_ID
                    END
                SELECT  @ROLE_ID ;
            END
    END
 GO