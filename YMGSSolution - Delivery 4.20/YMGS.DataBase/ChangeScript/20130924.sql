-- =============================================
-- Script Template
-- =============================================
---文章管理
ALTER TABLE dbo.TB_HELPER
ADD RulesID NVARCHAR(50) NULL,
	LevelNO INT NULL

IF EXISTS ( SELECT * FROM sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[pr_get_helperParData]')
                    AND OBJECTPROPERTY(id, N'Isprocedure') = 1 ) 
DROP PROCEDURE [dbo].[pr_get_helperParData]
GO