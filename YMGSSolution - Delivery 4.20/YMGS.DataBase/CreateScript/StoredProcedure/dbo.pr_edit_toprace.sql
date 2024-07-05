IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_edit_toprace]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_edit_toprace]
    GO
CREATE PROCEDURE pr_edit_toprace
    (
      @op_flag INT ,
      @matchid INT ,
      @cnpic VARBINARY(MAX) ,
      @enpic VARBINARY(MAX) ,
      @cntitle NVARCHAR(100) ,
      @entitle NVARCHAR(100) ,
      @cncontent NVARCHAR(300) ,
      @encontent NVARCHAR(300)
    )
AS 
    BEGIN
        IF ( @op_flag = 0 ) 
            BEGIN
                SELECT  TOPRACEID ,
                        MARCHID ,
                        CNPIC ,
                        ENPIC ,
                        CNTITLE ,
                        ENTITLE ,
                        CNCONTENT ,
                        ENCONTENT
                FROM    TB_AD_TOPRACE
            END
        IF ( @op_flag = 1 ) 
            BEGIN
                DELETE  FROM TB_AD_TOPRACE ;
                INSERT  INTO TB_AD_TOPRACE
                        ( MARCHID ,
                          CNPIC ,
                          ENPIC ,
                          CNTITLE ,
                          ENTITLE ,
                          CNCONTENT ,
                          ENCONTENT
                        )
                VALUES  ( @matchid ,
                          @cnpic ,
                          @enpic ,
                          @cntitle ,
                          @entitle ,
                          @cncontent ,
                          @encontent
                        ) ;
				--更新缓存对象表
				exec pr_up_cache_object 8
            END
        IF ( @op_flag = 2 ) 
            BEGIN
                DELETE  FROM TB_AD_TOPRACE
				--更新缓存对象表
				exec pr_up_cache_object 8
            END
        IF ( @op_flag = 3 ) 
            BEGIN
                UPDATE  TB_AD_TOPRACE
                SET     MARCHID = @matchid ,
                        CNPIC = @cnpic ,
                        ENPIC = @enpic ,
                        CNTITLE = @cntitle ,
                        ENTITLE = @entitle ,
                        CNCONTENT = @cncontent ,
                        ENCONTENT = @encontent
				--更新缓存对象表
				exec pr_up_cache_object 8
            END

    END
GO