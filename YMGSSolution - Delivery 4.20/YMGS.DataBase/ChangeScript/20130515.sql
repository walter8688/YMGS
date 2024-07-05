--结算表增加投注/受注输赢标志字段 0:投注赢 1:受注赢  2:平局
ALTER TABLE tb_exchange_settle ADD EXCHANGE_WIN_FLAG int 
--投注时当前比赛的比分
alter table tb_exchange_back add HOME_TEAM_SCORE int;
alter table tb_exchange_back add GUEST_TEAM_SCORE int;
alter table tb_exchange_lay add HOME_TEAM_SCORE int;
alter table tb_exchange_lay add GUEST_TEAM_SCORE int;
--比赛表增加结算状态字段 0：未结算 1：半场已结算 2：全场已结算
ALTER TABLE dbo.TB_MATCH ADD SETTLE_STATUS INT DEFAULT 0