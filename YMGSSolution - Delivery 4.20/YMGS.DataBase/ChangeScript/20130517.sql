﻿--比赛表增加是否自动封盘字段，默认0,0：未自动封盘；1：已自动封盘
ALTER TABLE TB_MATCH ADD IS_AUTO_FREEZED BIT DEFAULT 0