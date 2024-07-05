if EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_DSADWords]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_DSADWords]
    GO
CREATE PROCEDURE pr_get_DSADWords
    (
      @op_flag INT ,
      @id int,
      @title NVARCHAR(30),
	  @title_en NVARCHAR(30),
	  @DESC NVARCHAR(100),
	  @DESC_en NVARCHAR(100),
      @WEBLINK NVARCHAR(100)
    )
AS 
    BEGIN
	/*0:查 1 增 2删除 3改*/
        if(@op_flag=0)
		begin
		SELECT * FROM TB_AD_WORDS;
		end
		  if(@op_flag=1)
		begin
		insert into TB_AD_WORDS(TITLE,TITLE_EN,[DESC],DESC_EN,WEBLINK)
		VALUES(@title,@title_en,@DESC,@DESC_en,@WEBLINK);
		--更新缓存对象表
				exec pr_up_cache_object 5
		end
		  if(@op_flag=2)
		begin
		DELETE FROM TB_AD_WORDS WHERE AD_WORDS_ID=@id;
		--更新缓存对象表
				exec pr_up_cache_object 5
		end
		  if(@op_flag=3)
		begin
		UPDATE TB_AD_WORDS SET TITLE=@title,TITLE_EN=@title_en,[DESC]=@DESC,DESC_EN=@DESC_en,WEBLINK=@WEBLINK
		WHERE  AD_WORDS_ID=@id;
		--更新缓存对象表
				exec pr_up_cache_object 5
		end
    END
GO