--------------------------------------------------------------------------------------------
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4010,4001,'开始下半场比赛',1,3,9,'');

--------------------------------------------------------------------------------------------
insert into TB_ROLE_FUNC_MAP values (1,4010)

--------------------------------------------------------------------------------------------
ALTER TABLE TB_MATCH ADD First_Half_End_Time DATETIME 
ALTER TABLE TB_MATCH ADD Sec_Half_Start_Time DATETIME