--比赛市场表增加状态字段 1 激活状态 0 关闭状态
alter table tb_match_market add MARKET_STATUS int DEFAULT 1 not null;