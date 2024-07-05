IF NOT EXISTS ( SELECT  *
                FROM    SYSOBJECTS
                WHERE   XTYPE = 'U'
                        AND UPPER(NAME) = 'TB_ODDS_COMPARE' ) 
    CREATE TABLE TB_ODDS_COMPARE
        (
          [MATCHID] INT ,
		  [MATCHNAME] VARCHAR(100) ,
          [CN_CORP] VARCHAR(100) ,
          [EN_CORP] VARCHAR(100),
		  [PROFIT] DECIMAL(18,2)
        )
    GO