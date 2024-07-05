/***
CREATE DATE:2013-01-31
DESCRIPTION:佣金积分对应表
***/
IF NOT EXISTS ( SELECT  *
                FROM    sysobjects
                WHERE   xtype = 'U'
                        AND UPPER(name) = 'TB_BROKERAGE_INTEGRAL_MAP' ) 
    CREATE TABLE TB_BROKERAGE_INTEGRAL_MAP
        (
          Brokerage_Rate_ID INT PRIMARY KEY IDENTITY(1,1)
                                NOT NULL ,
          Brokerage_Rate DECIMAL(4, 4) ,
          [Status] INT ,
          Min_Integral INT ,
          Max_Integral INT ,
          Create_User INT ,
          Create_Time DATETIME ,
          Last_Update_User INT ,
          Last_Update_Time DATETIME
        )
        


