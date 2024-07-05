IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_ADDAPic]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_get_ADDAPic]
    GO
CREATE PROCEDURE pr_get_ADDAPic
    (
      @op_flag INT ,
      @id INT ,
      @address varbinary(max) ,
      @address_en varbinary(max)
    )
AS 
    BEGIN
        IF ( @op_flag = 0 ) 
            BEGIN
                SELECT  *
                FROM    TB_AD_PIC ;
            END
        IF ( @op_flag = 1 ) 
            BEGIN
			 DELETE  FROM TB_AD_PIC;
                INSERT  INTO TB_AD_PIC
                        ( PIC_ADDRESS, PIC_ADDRESS_EN )
                VALUES  ( @address, @address_en ) ;
				--更新缓存对象表
				exec pr_up_cache_object 6
            END
        IF ( @op_flag = 2 ) 
            BEGIN
                DELETE  FROM TB_AD_PIC
                WHERE   AD_PIC_ID = @id ;
				--更新缓存对象表
				exec pr_up_cache_object 6
            END
        IF ( @op_flag = 3 ) 
            BEGIN
                UPDATE  TB_AD_PIC
                SET     PIC_ADDRESS = @address ,
                        PIC_ADDRESS_EN = @address_en
                WHERE   AD_PIC_ID = @id ;
				--更新缓存对象表
				exec pr_up_cache_object 6
            END

    END
GO