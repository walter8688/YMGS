/***
Create Date:2013/01/10
Description:ά��������������
***/
IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_handler_param_type]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 ) 
    DROP PROCEDURE pr_handler_param_type
    GO
CREATE PROCEDURE [dbo].[pr_handler_param_type]
    (
      @Handler_Type INT ,--0:��ȡ����;1:��������;2:��������;3:ɾ������
      @Param_Type_ID INT ,
      @Param_Type_Name NVARCHAR(50)
    )
AS 
    DECLARE @Count INT
    BEGIN
		--0:��ȡ����
        IF @Handler_Type = 0 
            BEGIN
                SELECT  *
                FROM    dbo.TB_PARAM_TYPE
            END
		--1:��������
        ELSE 
            IF @Handler_Type = 1 
                BEGIN
					--�ж������Ƿ����
                    SELECT  @count = COUNT(*)
                    FROM    dbo.TB_PARAM_TYPE
                    WHERE   PARAM_TYPE_NAME = @Param_Type_Name
                    IF ( @Count < 1 ) 
                        BEGIN
                            INSERT  INTO dbo.TB_PARAM_TYPE
                                    ( PARAM_TYPE_NAME )
                            VALUES  ( @Param_Type_Name  -- PARAM_TYPE_NAME - nvarchar(50)
                                      )
                        END
                    ELSE 
                        BEGIN
                            RETURN -1
                        END
                    SET @Count = 0
                END
		--2:��������
            ELSE 
                IF @Handler_Type = 2 
                    BEGIN
						--�ж������Ƿ����
                        SELECT  @count = COUNT(*)
                        FROM    dbo.TB_PARAM_TYPE
                        WHERE   PARAM_TYPE_NAME = @Param_Type_Name
                        IF ( @Count < 1 ) 
                            BEGIN
                                UPDATE  dbo.TB_PARAM_TYPE
                                SET     PARAM_TYPE_NAME = @Param_Type_Name
                                WHERE   PARAM_TYPE_ID = @Param_Type_ID
                            END
                        ELSE 
                            BEGIN
                                RETURN -1
                            END
                        SET @Count = 0
                    END
		--3:ɾ������
                ELSE 
                    IF @Handler_Type = 3 
                        BEGIN
							--ɾ��TB_Param_Param���ڶ�����������
                            DELETE  FROM dbo.TB_PARAM_PARAM
                            WHERE   PARAM_TYPE = @Param_Type_ID
                            --ɾ��TB_Param_Type��Ӧ����
                            DELETE  FROM dbo.TB_PARAM_TYPE
                            WHERE   PARAM_TYPE_ID = @Param_Type_ID
                        END
    END
GO