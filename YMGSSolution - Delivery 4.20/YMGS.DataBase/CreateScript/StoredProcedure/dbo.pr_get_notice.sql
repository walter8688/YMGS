IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_notice]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_notice]
    GO
CREATE PROCEDURE pr_get_notice
    (
      @op_flag INT ,
      @id INT ,
      @title NVARCHAR(100),
      @entitle NVARCHAR(100),
      @content NVARCHAR(300),
      @encontent NVARCHAR(300),
      @isv int
    )
AS 
    BEGIN
        IF ( @op_flag = 0 ) 
            BEGIN
                SELECT  *
                FROM TB_AD_NOTICE
				where (title like '%'+@title+'%') and (isv=@isv or @isv is null or @isv ='');
            END
        IF ( @op_flag = 1 ) 
            BEGIN
                INSERT  INTO TB_AD_NOTICE
                        ( TITLE,CONTENT,ENTITLE,ENCONTENT,ISV)
                VALUES  (@title, @content,@entitle,@encontent,@isv) ;
				--更新缓存对象表
				exec pr_up_cache_object 7
            END
        IF ( @op_flag = 2 ) 
            BEGIN
                DELETE  FROM TB_AD_NOTICE
                WHERE   PID = @id ;
				--更新缓存对象表
				exec pr_up_cache_object 7
            END
        IF ( @op_flag = 3 ) 
            BEGIN
                UPDATE  TB_AD_NOTICE
                SET     TITLE = @title ,
                        CONTENT = @content,
                        ENTITLE=@entitle,
                        ENCONTENT=@encontent,
                        ISV=@isv
                WHERE   PID = @id ;
				--更新缓存对象表
				exec pr_up_cache_object 7
            END

    END
GO