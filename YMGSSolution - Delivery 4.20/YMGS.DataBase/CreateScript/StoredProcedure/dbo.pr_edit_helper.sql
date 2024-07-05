IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_edit_helper]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
    DROP PROCEDURE [dbo].[pr_edit_helper]
    GO
CREATE PROCEDURE pr_edit_helper
    (
      @op_flag INT ,
      @ITEMID INT ,
      @PITEMID INT ,
      @CNITEMNAME VARCHAR(50) ,
      @ENITEMNAME VARCHAR(50) ,
      @WEBLINK VARCHAR(200) ,
      @ENWEBLINK VARCHAR(200),
      @OrderNO INT,
      @RulesID NVARCHAR(50),
	  @LevelNO INT
    )
AS 
    BEGIN
       DECLARE @hcount INT ;
        IF ( @op_flag = 0 ) 
            BEGIN
                SELECT * FROM TB_HELPER th WITH(NOLOCK)
				ORDER BY th.PITEMID ASC,th.OrderNO ASC
            END
        IF ( @op_flag = 1 ) 
            BEGIN
                INSERT  INTO TB_HELPER
                        ( PITEMID ,
                          CNITEMNAME ,
                          ENITEMNAME ,
                          WEBLINK ,
                          ENWEBLINK,
                          OrderNO,
                          RulesID,
						  LevelNO
                        )
                VALUES  ( @PITEMID ,
                          @CNITEMNAME ,
                          @ENITEMNAME ,
                          @WEBLINK ,
                          @ENWEBLINK,
						  @OrderNO,
						  @RulesID,
						  @LevelNO
                        ) ;
						exec pr_up_cache_object 10
            END
        IF ( @op_flag = 2 ) 
            BEGIN
                SELECT  @hcount = COUNT(1)
                FROM    TB_HELPER
                WHERE   PITEMID = @ITEMID ;
                IF ( @hcount > 0 ) 
                    BEGIN
                        RAISERROR (N'不能删除，该结点下还有子结点!' , 16, 1) WITH NOWAIT
                        RETURN
                    END
                DELETE  FROM TB_HELPER
                WHERE   ITEMID = @ITEMID ;
					exec pr_up_cache_object 10
            END
        IF ( @op_flag = 3 ) 
            BEGIN
				IF EXISTS(SELECT * FROM TB_HELPER WHERE RulesID=@RulesID)
					UPDATE TB_HELPER SET RulesID = '' WHERE RulesID = @RulesID
                
				UPDATE  TB_HELPER
                SET     PITEMID = @PITEMID ,
                        CNITEMNAME = @CNITEMNAME ,
                        ENITEMNAME = @ENITEMNAME ,
                        WEBLINK = @WEBLINK ,
                        ENWEBLINK = @ENWEBLINK,
                        OrderNO = @OrderNO,
                        RulesID = @RulesID
                WHERE   ITEMID = @ITEMID ;
					exec pr_up_cache_object 10
            END
    END
GO