--���������Ͷע/��ע��Ӯ��־�ֶ� 0:ͶעӮ 1:��עӮ  2:ƽ��
ALTER TABLE tb_exchange_settle ADD EXCHANGE_WIN_FLAG int 
--Ͷעʱ��ǰ�����ıȷ�
alter table tb_exchange_back add HOME_TEAM_SCORE int;
alter table tb_exchange_back add GUEST_TEAM_SCORE int;
alter table tb_exchange_lay add HOME_TEAM_SCORE int;
alter table tb_exchange_lay add GUEST_TEAM_SCORE int;
--���������ӽ���״̬�ֶ� 0��δ���� 1���볡�ѽ��� 2��ȫ���ѽ���
ALTER TABLE dbo.TB_MATCH ADD SETTLE_STATUS INT DEFAULT 0