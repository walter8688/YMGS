DELETE from TB_SYSTEM_FUNC;

-- ************************** 1000 ϵͳ���� **************************
-- 1000 ϵͳ����ģ�飩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1000,null,'ϵͳ����',1,1,1,'');

-- 1001 ��Ա�ʺŹ���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1001,1000,'��Ա�ʺŹ���',1,2,1,'');
-- 1002 ����������ʺţ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1002,1001,'����������ʺ�',1,3,1,'');
-- 1003 �޸Ľ�ɫ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1003,1001,'�޸Ľ�ɫ',1,3,2,'');
-- 1004 �޸��ʽ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1004,1001,'�޸��ʽ�',1,3,3,'');

-- 1051 ��ɫ��Ȩ�޹���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1051,1000,'��ɫ��Ȩ�޹���',1,2,2,'');
-- 1052 ������ɫ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1052,1051,'������ɫ',1,3,1,'');
-- 1053 �޸Ľ�ɫ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1053,1051,'�޸Ľ�ɫ',1,3,2,'');
-- 1054 ɾ����ɫ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1054,1051,'ɾ����ɫ',1,3,3,'');

-- 1101 �������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1101,1000,'�������',1,2,3,'');
-- 1102 �������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1102,1101,'�������',1,3,1,'');

-- 1151 V��������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1151,1000,'V��������',1,2,4,'');
-- 1152 ����V������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1152,1151,'����V����',1,3,1,'');

-- 1201 �ʽ��˻�����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1201,1000,'�ʽ��˻�����',1,2,5,'');
-- 1202 �������п���Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1202,1201,'�������п�',1,3,1,'');

-- 1251 ��Ա���ֹ���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1251,1000,'��Ա���ֹ���',1,2,6,'');
-- 1252 �������֣�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1252,1251,'��������',1,3,1,'');
-- 1253 ת����ɣ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1253,1251,'ת�����',1,3,2,'');
-- 1254 �ܾ����֣�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1254,1251,'�ܾ�����',1,3,3,'');

--1301 �����������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1301,1000,'�����������',1,2,7,'');
--1302 ��������������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1302,1301,'������������',1,3,1,'');
--1303 ������׼������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1303,1301,'������������',1,3,1,'');
--1304 ����ܾ�������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1304,1301,'������������',1,3,1,'');


-- ************************** 2000 ���¹��� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2000,null,'���¹���',1,1,2,'');

-- 2001 �����������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2001,2000,'�����������',1,2,1,'');
-- 2002 ������������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2002,2001,'������������',1,3,1,'');
-- 2003 �޸���������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2003,2001,'�޸���������',1,3,2,'');
-- 2004 ɾ����������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2004,2001,'ɾ����������',1,3,3,'');

-- 2051 ���¹���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2051,2000,'���¹���',1,2,2,'');
-- 2052 �������£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2052,2051,'��������',1,3,1,'');
-- 2053 �޸����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2053,2051,'�޸�����',1,3,2,'');
-- 2054 ɾ�����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2054,2051,'ɾ������',1,3,3,'');
-- 2055 ������棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2055,2051,'�������',1,3,4,'');
-- 2056 ��������ͣ���£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2056,2051,'��������ͣ����',1,3,5,'');
-- 2057 ��ֹ���£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2057,2051,'��ֹ����',1,3,6,'');

-- 2101 ������Ա����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2101,2000,'������Ա����',1,2,3,'');
-- 2102 ����������Ա��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2102,2101,'����������Ա',1,3,1,'');
-- 2103 �޸Ĳ�����Ա��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2103,2101,'�޸Ĳ�����Ա',1,3,2,'');
-- 2104 ɾ��������Ա��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2104,2101,'ɾ��������Ա',1,3,3,'');
-- 2105 ��������ò�����Ա��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2105,2101,'��������ò�����Ա',1,3,4,'');


-- ************************** 3000 �����г����� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3000,null,'�����г�����',1,1,3,'');

-- 3001 �������͹���ҳ�棩
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(3001,3000,'�������͹���',1,2,1,'');
-- 3002 �����������ã�Ȩ�ޣ�
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(3002,3001,'������������',1,3,1,'');

-- 3051 �г�ģ�����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3051,3000,'�г�ģ�����',1,2,2,'');
-- 3052 �����г�ģ�壨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3052,3051,'�����г�ģ��',1,3,1,'');
-- 3053 �޸��г�ģ�壨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3053,3051,'�޸��г�ģ��',1,3,2,'');
-- 3054 ɾ���г�ģ�壨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3054,3051,'ɾ���г�ģ��',1,3,3,'');

-- 3101 ��������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3101,3000,'��������',1,2,3,'');
-- 3102 ����������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3102,3101,'��������',1,3,1,'');
-- 3103 �޸ı�����Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3103,3101,'�޸ı���',1,3,2,'');
-- 3104 ɾ��������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3104,3101,'ɾ������',1,3,3,'');
-- 3105 ��������ͣ������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3105,3101,'��������ͣ����',1,3,4,'');
-- 3106 ��ֹ������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3106,3101,'��ֹ����',1,3,5,'');
-- 3107 ������棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3107,3101,'�������',1,3,6,'');
-- 3108 �Ƽ�������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3108,3101,'�Ƽ�����',1,3,7,'');

-- 3151 �ھ����׹���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3151,3000,'�ھ����׹���',1,2,4,'');
-- 3152 �����ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3152,3151,'�����ھ�����',1,3,1,'');
-- 3153 �޸Ĺھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3153,3151,'�޸Ĺھ�����',1,3,2,'');
-- 3154 ɾ���ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3154,3151,'ɾ���ھ�����',1,3,3,'');
-- 3155 �ھ�������棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3155,3151,'�ھ��������',1,3,4,'');
-- 3156 ��������ͣ�ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3156,3151,'��������ͣ�ھ�����',1,3,5,'');
-- 3157 ��ֹ�ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3157,3151,'��ֹ�ھ�����',1,3,6,'');
-- 3158 �����ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3158,3151,'�����ھ�����',1,3,6,'');
-- 3159 ¼��ھ����½����Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3159,3151,'¼��ھ����½��',1,3,6,'');


-- ************************** 4000 ���п��� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4000,null,'���п���',1,1,4,'');

-- 4001 �������ƣ�ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4001,4000,'��������',1,2,1,'');
-- 4002 �������̣�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4002,4001,'��������',1,3,1,'');
-- 4003 ¼��ȷ֣�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4003,4001,'¼��ȷ�',1,3,2,'');
-- 4004 ����������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4004,4001,'��������',1,3,3,'');
-- 4005 ���������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4005,4001,'�������',1,3,4,'');
-- 4006 �볡������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4006,4001,'�볡����',1,3,5,'');
-- 4007 �����г���Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4007,4001,'�����г�',1,3,6,'');
-- 4008 �޸ı�����ʼʱ�䣨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4008,4001,'�޸ı�����ʼʱ��',1,3,7,'');
-- 4009 ��ʼ������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4009,4001,'��ʼ����',1,3,8,'');
-- 4010 ��ʼ������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4010,4001,'��ʼ�°볡����',1,3,9,'');


-- ************************** 5000 ������� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5000,null,'�������',1,1,5,'');

-- 5001 �������㣨ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5001,5000,'��������',1,2,1,'');
-- 5002 ���������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5002,5001,'�������',1,3,1,'');

-- 5010 ����ھ����£�ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5010,5000,'�ھ����½���',1,2,1,'');
-- 5011 ����ھ����£�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5011,5010,'����ھ�����',1,3,1,'');

-- 5051 Ӷ�����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5051,5000,'Ӷ�����',1,2,2,'');
-- 5052 ����Ӷ���ʣ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5052,5051,'����Ӷ����',1,3,1,'');


-- ************************** 6000 �������� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6000,null,'��������',1,1,6,'');

-- 6001 �������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6001,6000,'�������',1,2,1,'');
-- 6002 ��������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6002,6001,'��������',1,3,1,'');
-- 6003 �޸�����Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6003,6001,'�޸�����',1,3,2,'');
-- 6004 ɾ������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6004,6001,'ɾ������',1,3,3,'');

-- 6051 ���ʶԱȹ���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6051,6000,'���ʶԱȹ���',1,2,2,'');
-- 6052 �������ʶԱȣ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6052,6001,'�������ʶԱ�',1,3,1,'');
-- 6053 �޸����ʶԱȣ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6053,6001,'�޸����ʶԱ�',1,3,2,'');
-- 6054 ɾ�����ʶԱȣ�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6054,6001,'ɾ�����ʶԱ�',1,3,3,'');

-- 6101 ���λ��Ϣ����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6101,6000,'���λ��Ϣ����',1,2,3,'');
-- 6102 ���ù��λ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6102,6101,'���ù��λ',1,3,1,'');

-- 6151 ��վ�������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6151,6000,'��վ�������',1,2,4,'');
-- 6152 �������棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6152,6151,'��������',1,3,1,'');
-- 6153 �޸Ĺ��棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6153,6151,'�޸Ĺ���',1,3,2,'');
-- 6154 ɾ�����棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6154,6151,'ɾ������',1,3,3,'');
-- 6155 ��������ͣ���棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6155,6151,'��������ͣ����',1,3,4,'');

-- 6201 ��������ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6201,6000,'��������',1,2,5,'');
-- 6202 �������ã�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6202,6201,'��������',1,3,1,'');

-- 6251 ����Ŀ¼����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6251,6000,'����Ŀ¼����',1,2,6,'');
-- 6252 �������棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6252,6251,'����Ŀ¼',1,3,1,'');
-- 6253 �޸Ĺ��棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6253,6251,'�޸�Ŀ¼',1,3,2,'');
-- 6254 ɾ�����棨Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6254,6251,'ɾ��Ŀ¼',1,3,3,'');


-- ************************** 7000 �������� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7000,null,'��������',1,1,7,'');

-- 7001 ��ע����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7001,7000,'��ע����',1,2,1,'');

-- 7051 ���ױ���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7051,7000,'���ױ���',1,2,2,'');

-- 7101 �ʽ𱨱�ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7101,7000,'�ʽ𱨱�',1,2,3,'');

-- 7151 Ӷ�𱨱�ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7151,7000,'Ӷ�𱨱�',1,2,4,'');


-- ************************** 8000 ����ƽ̨ **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(8000,null,'����ƽ̨',2,1,1,'');

-- 8001 ע���¼��ҳ�棩
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8001,8000,'ע���¼',2,2,1,'');
-- 8002 ע���Ա��Ȩ�ޣ�
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8002,8001,'ע���Ա',2,3,1,'');
-- 8003 �������루Ȩ�ޣ�
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8003,8001,'��������',2,3,2,'');

-- 8051 �г�������ҳ�棩
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8051,8000,'�г�����',2,2,2,'');

-- 8101 ��ע���ף�ҳ�棩
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8101,8000,'��ע����',2,2,3,'');
-- 8102 ��ע��Ȩ�ޣ�
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8102,8101,'��ע',2,3,1,'');


-- ************************** 9000 ��Ա���� **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9000,null,'��Ա����',2,1,2,'');

-- 9001 ��Ա��Ϣ����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9001,9000,'��Ա��Ϣ����',2,2,1,'');
-- 9002 �޸��û���Ϣ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9002,9001,'�޸��û���Ϣ',2,3,1,'');

-- 9051 �ʽ��˻�����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9051,9000,'�ʽ��˻�����',2,2,2,'');
-- 9052 �������п���Ϣ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9052,9051,'�������п���Ϣ',2,3,1,'');
-- 9053 ���߳�ֵ��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9053,9051,'���߳�ֵ',2,3,2,'');
-- 9054 �������루Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9054,9051,'��������',2,3,3,'');

-- 9101 �����Ա����ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9101,9000,'�����Ա����',2,2,3,'');
-- 9102 ��չ��Ա��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9102,9101,'��չ��Ա',2,3,1,'');
-- 9103 ָ����������Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9103,9101,'ָ����������',2,3,2,'');
-- 9104 �������ã�Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9104,9101,'��������',2,3,3,'');

-- 9151 �ҵĽ��׹���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9151,9000,'�ҵĽ��׹���',2,2,4,'');
-- 9152 �޸���ע��Ȩ�ޣ�
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(9152,9151,'�޸���ע',2,3,1,'');
-- 9153 ������ע��Ȩ�ޣ�
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9153,9151,'������ע',2,3,2,'');

-- 9201 �ҵĻ��ֲ�ѯ��ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9201,9000,'�ҵĻ��ֲ�ѯ',2,2,5,'');

-- 9301 �����ܴ���ҳ�棩
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9301,9000,'�����ܴ���',2,2,6,'');

go
--ϵͳĬ�Ͻ�ɫ
if(not exists(select * from tb_system_role where role_name='ϵͳ����Ա'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('ϵͳ����Ա','ϵͳ����Ա','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='һ���ܴ���'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('1���ܴ���','1���ܴ���','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='����'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('����','����','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='��Ա'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('��Ա','��Ա','1',getdate(),1,getdate());
END
if(not exists(select * from tb_system_role where role_name='2���ܴ���'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('2���ܴ���','2���ܴ���','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='3���ܴ���'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('3���ܴ���','3���ܴ���','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='4���ܴ���'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('4���ܴ���','4���ܴ���','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='5���ܴ���'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('5���ܴ���','5���ܴ���','1',getdate(),1,getdate());
END


--ϵͳĬ�Ϲ���Աadmin
if(not exists(select * from tb_system_account where user_name='admin'))
begin
	insert into tb_system_account(user_name,born_year,born_month,born_day,email_address,
	role_id,account_status,login_name,password)
	values('admin','1970','12','01','2287547821@qq.com',1,1,'admin','AB83EFD73451D066A061622FDFF60999');
end
GO

--ϵͳ����ԱĬ���ʽ��˻�
if(not exists(select * from TB_USER_FUND where USER_ID=1))
begin
INSERT INTO TB_USER_FUND( USER_ID ,BANK_NAME ,OPEN_BANK_NAME ,CARD_NO ,ACCOUNT_HOLDER ,
          CUR_FUND ,FREEZED_FUND ,CUR_INTEGRAL ,STATUS ,LAST_UPDATE_TIME)
VALUES  ( 1 ,N'' ,N'' , N'' , N'' , 0.00 , 0.00 , 0 , 0 , GETDATE())
end
GO

--��ϵͳ����ԱĬ��ȫ����ϵͳȨ��
declare @role_id int
select @role_id=role_id from tb_system_role where role_name='ϵͳ����Ա'
delete from tb_role_func_map where role_id=@role_id
insert into tb_role_func_map
select @role_id,func_id
from tb_system_func b
go

DELETE FROM TB_BET_TYPE
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(1,'��׼��','Match Odds',1,1,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(2,'����','Correct Score',1,0,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(3,'��С��','Over/Under Goals',1,1,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(4,'������','Asian Handicap',1,1,1,getdate(),1,getdate())
GO

--������������Ĭ������
DELETE FROM TB_EVENT_TYPE
INSERT  INTO TB_EVENT_TYPE
        ( EventType_ID, EventType_Name, EventType_Name_En )
VALUES  ( 1, '������', 'Sports' )
INSERT  INTO TB_EVENT_TYPE
        ( EventType_ID, EventType_Name, EventType_Name_En )
VALUES  ( 2, '������', 'Entertainment' )
GO

--��������Ŀ���Ĭ������
DELETE FROM TB_EVENT_ITEM
INSERT INTO TB_EVENT_ITEM( EventItem_ID,EventType_ID, EventItem_Name, EventItem_Name_En )
VALUES  ( 1, 1, N'����', N'Football')
GO 

--�����������Ӹ�Ŀ¼�����硯
DELETE FROM TB_PARAM_ZONE
INSERT INTO TB_PARAM_ZONE ( PARENT_ZONE_ID ,ZONE_NAME ,ZONE_ORDER)
VALUES  ( 0 , N'����' , 1)
GO 

--��ϵͳ�����������Ĭ������
DELETE FROM tb_param_type
/*
insert into tb_param_type (param_type_id,param_type_name)
values (1,'����')
*/
insert into tb_param_type (param_type_id,param_type_name)
values (2,'������Ա����(����/ְҵ)')

insert into tb_param_type (param_type_id,param_type_name)
values (3,'������Ա����(����/Ů��)')

/*insert into tb_param_type (param_type_id,param_type_name)
values (4,'������')*/
/*
insert into tb_param_type (param_type_id,param_type_name)
values (5,'Country')
*/
insert into tb_param_type (param_type_id,param_type_name)
values (6,'V������ֵ')

insert into tb_param_type (param_type_id,param_type_name)
values (7,'������ֵ')

--V������ֵ
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 6 , N'100' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 6 , N'200' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 6 , N'500' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 6 , N'1000' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
--������ֵ
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 7 , N'100' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 7 , N'200' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 7 , N'500' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )
INSERT INTO dbo.TB_PARAM_PARAM
        ( PARAM_TYPE ,PARAM_NAME ,PARAM_ORDER ,IS_USE ,CREATE_USER ,CREATE_TIME ,LAST_UPDATE_USER ,LAST_UPDATE_TIME)
VALUES  ( 7 , N'1000' , 0 , 1 , 1 , GETDATE() , 1 , GETDATE()  )

--�г�ģ��
delete from tb_market_template;
--��׼��
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ����׼��','Match Odds',1,1,null,null,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡��׼��','Half Time',1,0,null,null,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('��/ȫ����׼��','Half Time/Full Time',1,2,null,null,null,null,null,1,getdate(),1,getdate())
--�������볡��
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 - 0','(H)0 - 0',2,0,0,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 - 1','(H)0 - 1',2,0,0,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 - 2','(H)0 - 2',2,0,0,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 - 3','(H)0 - 3',2,0,0,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡1 - 0','(H)1 - 0',2,0,1,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡1 - 1','(H)1 - 1',2,0,1,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡1 - 2','(H)1 - 2',2,0,1,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡1 - 3','(H)1 - 3',2,0,1,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡2 - 0','(H)2 - 0',2,0,2,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡2 - 1','(H)2 - 1',2,0,2,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡2 - 2','(H)2 - 2',2,0,2,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡2 - 3','(H)2 - 3',2,0,2,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡3 - 0','(H)3 - 0',2,0,3,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡3 - 1','(H)3 - 1',2,0,3,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡3 - 2','(H)3 - 2',2,0,3,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡3 - 3','(H)3 - 3',2,0,3,3,null,null,null,1,getdate(),1,getdate())
--������ȫ����
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 0','(F)0 - 0',2,1,0,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 1','(F)0 - 1',2,1,0,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 2','(F)0 - 2',2,1,0,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 3','(F)0 - 3',2,1,0,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 4','(F)0 - 4',2,1,0,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 - 5','(F)0 - 5',2,1,0,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 0','(F)1 - 0',2,1,1,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 1','(F)1 - 1',2,1,1,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 2','(F)1 - 2',2,1,1,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 3','(F)1 - 3',2,1,1,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 4','(F)1 - 4',2,1,1,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1 - 5','(F)1 - 5',2,1,1,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 0','(F)2 - 0',2,1,2,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 1','(F)2 - 1',2,1,2,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 2','(F)2 - 2',2,1,2,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 3','(F)2 - 3',2,1,2,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 4','(F)2 - 4',2,1,2,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2 - 5','(F)2 - 5',2,1,2,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 0','(F)3 - 0',2,1,3,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 1','(F)3 - 1',2,1,3,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 2','(F)3 - 2',2,1,3,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 3','(F)3 - 3',2,1,3,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 4','(F)3 - 4',2,1,3,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3 - 5','(F)3 - 5',2,1,3,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 0','(F)4 - 0',2,1,4,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 1','(F)4 - 1',2,1,4,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 2','(F)4 - 2',2,1,4,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 3','(F)4 - 3',2,1,4,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 4','(F)4 - 4',2,1,4,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4 - 5','(F)4 - 5',2,1,4,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 0','(F)5 - 0',2,1,5,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 1','(F)5 - 1',2,1,5,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 2','(F)5 - 2',2,1,5,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 3','(F)5 - 3',2,1,5,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 4','(F)5 - 4',2,1,5,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5 - 5','(F)5 - 5',2,1,5,5,null,null,null,1,getdate(),1,getdate())
--��С�򣨰볡��
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0.5��','First Half Goals 0.5',3,0,null,null,0.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡1.5��','First Half Goals 1.5',3,0,null,null,1.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡2.5��','First Half Goals 2.5',3,0,null,null,2.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡3.5��','First Half Goals 3.5',3,0,null,null,3.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡4.5��','First Half Goals 4.5',3,0,null,null,4.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡5.5��','First Half Goals 5.5',3,0,null,null,5.5,null,null,1,getdate(),1,getdate())
--��С��ȫ����
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0.5��','Over/Under 0.5 Goals',3,1,null,null,0.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��1.5��','Over/Under 1.5 Goals',3,1,null,null,1.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��2.5��','Over/Under 2.5 Goals',3,1,null,null,2.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��3.5��','Over/Under 3.5 Goals',3,1,null,null,3.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��4.5��','Over/Under 4.5 Goals',3,1,null,null,4.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��5.5��','Over/Under 5.5 Goals',3,1,null,null,5.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��6.5��','Over/Under 6.5 Goals',3,1,null,null,6.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��7.5��','Over/Under 7.5 Goals',3,1,null,null,7.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��8.5��','Over/Under 8.5 Goals',3,1,null,null,8.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��9.5��','Over/Under 9.5 Goals',3,1,null,null,9.5,null,null,1,getdate(),1,getdate())
--�÷��̣��볡��
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-2.0','(H)-2.0',4,0,null,null,null,null,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-1.5 & -2.0','(H)-1.5 & -2.0',4,0,null,null,null,-1.5,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-1.5','(H)-1.5',4,0,null,null,null,null,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-1.0 & -1.5','(H)-1.0 & -1.5',4,0,null,null,null,-1,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-1.0','(H)-1.0',4,0,null,null,null,null,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-0.5 & -1.0','(H)-0.5 & -1.0',4,0,null,null,null,-0.5,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡-0.5','(H)-0.5',4,0,null,null,null,null,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 & -0.5','(H)0 & -0.5',4,0,null,null,null,0,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0','(H)0',4,0,null,null,null,null,0,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡0 & +0.5','(H)0 & +0.5',4,0,null,null,null,0,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+0.5','(H)+0.5',4,0,null,null,null,null,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+0.5 & +1.0','(H)+0.5 & +1.0',4,0,null,null,null,0.5,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+1.0','(H)+1.0',4,0,null,null,null,null,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+1.0 & +1.5','(H)+1.0 & +1.5',4,0,null,null,null,1,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+1.5','(H)+1.5',4,0,null,null,null,null,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+1.5 & +2.0','(H)+1.5 & +2.0',4,0,null,null,null,1.5,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('�볡+2.0','(H)+2.0',4,0,null,null,null,null,2,1,getdate(),1,getdate())
--�÷��̣�ȫ����
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-4.0','(F)-4.0',4,1,null,null,null,null,-4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-3.5 & -4.0','(F)-3.5 & -4.0',4,1,null,null,null,-3.5,-4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-3.5','(F)-3.5',4,1,null,null,null,null,-3.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-3.0','(F)-3.0',4,1,null,null,null,null,-3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-2.5 & -3.0','(F)-2.5 & -3.0',4,1,null,null,null,-2.5,-3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-2.5','(F)-2.5',4,1,null,null,null,null,-2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-2.0 & -2.5','(F)-2.0 & -2.5',4,1,null,null,null,-2,-2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-2.0','(F)-2.0',4,1,null,null,null,null,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-1.5 & -2.0','(F)-1.5 & -2.0',4,1,null,null,null,-1.5,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-1.5','(F)-1.5',4,1,null,null,null,null,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-1.0 & -1.5','(F)-1.0 & -1.5',4,1,null,null,null,-1,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-1.0','(F)-1.0',4,1,null,null,null,null,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-0.5 & -1.0','(F)-0.5 & -1.0',4,1,null,null,null,-0.5,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��-0.5','(F)-0.5',4,1,null,null,null,null,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 & -0.5','(F)0 & -0.5',4,1,null,null,null,0,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0','(F)0',4,1,null,null,null,null,0,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��0 & +0.5','(F)0 & +0.5',4,1,null,null,null,0,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+0.5','(F)+0.5',4,1,null,null,null,null,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+0.5 & +1.0','(F)+0.5 & +1.0',4,1,null,null,null,0.5,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+1.0','(F)+1.0',4,1,null,null,null,null,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+1.0 & +1.5','(F)+1.0 & +1.5',4,1,null,null,null,1,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+1.5','(F)+1.5',4,1,null,null,null,null,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+1.5 & +2.0','(F)+1.5 & +2.0',4,1,null,null,null,1.5,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+2.0','(F)+2.0',4,1,null,null,null,null,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+2.0 & +2.5','(F)+2.0 & +2.5',4,1,null,null,null,2,2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+2.5','(F)+2.5',4,1,null,null,null,null,2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+2.5 & +3.0','(F)+2.5 & +3.0',4,1,null,null,null,2.5,3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+3.0','(F)+3.0',4,1,null,null,null,null,3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+3.5','(F)+3.5',4,1,null,null,null,null,3.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+3.5 & +4.0','(F)+3.5 & +4.0',4,1,null,null,null,3.5,4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('ȫ��+4.0','(F)+4.0',4,1,null,null,null,null,4,1,getdate(),1,getdate())
--ģ������ 0 �볡 1 ȫ�� 2 �볡/ȫ��
--bet_type_id ��select * from tb_bet_type��ȡ


--Ӷ���ʺͻ��ֶ�Ӧ��
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  (  0.0500 ,1 ,0 , 49999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  (  0.0485 ,1 ,50000 , 199999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0470 ,1 ,200000 , 374999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0455 ,1 ,375000 , 574999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0440 ,1 ,575000 , 849999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0425 ,1 ,850000 , 1199999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0410 ,1 ,1200000 , 1699999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0395 ,1 ,1700000 , 2499999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0380 ,1 ,2500000 , 3499999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0365 ,1 ,3500000 , 5249999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0350 ,1 ,5250000 , 7349999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0335 ,1 ,7350000 , 10749999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0320 ,1 ,10750000 , 15049999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0305 ,1 ,15050000 , 19569999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0290 ,1 ,19570000 , 25449999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0275 ,1 ,25450000 , 30499999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0260 ,1 ,30500000 , 36599999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0245 ,1 ,36600000 , 43899999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0230 ,1 ,43900000 , 52649999, 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0215 ,1 ,52650000 , 63749999 , 1 , GETDATE() , 1 , GETDATE() )
INSERT INTO dbo.TB_BROKERAGE_INTEGRAL_MAP( Brokerage_Rate ,[Status] ,Min_Integral ,Max_Integral ,Create_User ,Create_Time , Last_Update_User ,Last_Update_Time)
VALUES  ( 0.0200 ,1 ,63750000 , 99999999 , 1 , GETDATE() , 1 , GETDATE() )


--���ݻ�������
delete from TB_CACHE_OBJECT
insert into TB_CACHE_OBJECT values(0,'������Ŀ',getdate())
insert into TB_CACHE_OBJECT values(1,'��������',getdate())
insert into TB_CACHE_OBJECT values(2,'����',getdate())
insert into TB_CACHE_OBJECT values(3,'����', getdate())
insert into TB_CACHE_OBJECT values(4,'�ھ�', getdate())
insert into TB_CACHE_OBJECT values(5,'���ֹ��', getdate())
insert into TB_CACHE_OBJECT values(6,'ͼƬ���', getdate())
insert into TB_CACHE_OBJECT values(7,'����', getdate())
insert into TB_CACHE_OBJECT values(8,'�ö�����', getdate())
insert into TB_CACHE_OBJECT values(9,'�r�ʌ���', getdate())
insert into TB_CACHE_OBJECT values(10,'�����б�',GETDATE())
insert into TB_CACHE_OBJECT values(11,'Ͷע��Ϣ',GETDATE())
insert into TB_CACHE_OBJECT values(12,'��ע��Ϣ',GETDATE())
--ϵͳĬ���ʽ��˺�����
delete from tb_system_main_fund
insert into tb_system_main_fund values(1,0,'','',getdate())


/*Country*/
DELETE FROM TB_COUNTRY
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Afghanistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','Albania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','Algeria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������Ħ��','American Samoa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Andorra')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Angola')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Anguilla')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ϼ���','Antarctica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ϺͰͲ���','Antigua and Barbuda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����͢','Argentina')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Armenia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��³��','Aruba')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ĵ�����','Australia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�µ���','Austria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����ݽ�','Azerbaijan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Bahrain')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ϼ�����','Bangladesh')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ͰͶ�˹','Barbados')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�׶���˹','Belarus')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ʱ','Belgium')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Belize')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Benin')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Ľ��','Bermuda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Bhutan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ά��','Bolivia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Bosnia and Herzegovina')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Botswana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Brazil')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ӣ��ά����Ⱥ��','British Virgin Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Brunei Darussalam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Bulgaria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����ɷ���','Burkina Faso')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���','Burma')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��¡��','Burundi')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����կ','Cambodia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����¡','Cameroon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ô�','Canada')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��ý�','Cape Verde')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����Ⱥ��','Cayman Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�з�','Central African Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('է��','Chad')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Chile')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�й�','China')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ����','Christmas Island')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ƿ�˹�����֣�Ⱥ��','Cocos (Keeling) Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ױ���','Colombia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Ħ��','Comoros')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�չ�����','Congo, Democratic Republic of the')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�չ�������','Congo, Republic of the')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���Ⱥ��','Cook Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��˹�����','Costa Rica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ص���','Cote d''Ivoire')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���޵���','Croatia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ű�','Cuba')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����·˹','Cyprus')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ݿ�','Czech Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Denmark')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Djibouti')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Dominica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Dominican Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��϶��','Ecuador')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Egypt')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����߶�','El Salvador')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���������','Equatorial Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','Eritrea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��ɳ����','Estonia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���������','Ethiopia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������Ⱥ�������ά��˹��','Falkland Islands (Islas Malvinas)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����Ⱥ��','Faroe Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('쳼�','Fiji')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Finland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','France')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','French Guiana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������������','French Polynesia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Gabon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��³����','Georgia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�¹�','Germany')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Ghana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ֱ������','Gibraltar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ϣ��','Greece')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Greenland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����ɴ�','Grenada')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ϵ�����','Guadeloupe')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ص�','Guam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Σ������','Guatemala')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Guernsey')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����Ǳ���','Guinea-Bissau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Guyana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Haiti')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��ٸ�','Holy See (Vatican City)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�鶼��˹','Honduras')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�й����','Hong Kong (SAR)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Hungary')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Iceland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ӡ��','India')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ӡ��������','Indonesia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Iran')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Iraq')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Ireland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��ɫ��','Israel')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����','Italy')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����','Jamaica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ձ�','Japan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Լ��','Jordan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������˹̹','Kazakhstan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Kenya')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����˹','Kiribati')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Korea, North')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Korea, South')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Kuwait')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������˹˹̹','Kyrgyzstan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Laos')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ά��','Latvia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����','Lebanon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Lesotho')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Liberia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Libya')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��֧��ʿ��','Liechtenstein')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Lithuania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('¬ɭ��','Luxembourg')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Macao')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ǰ�������','Macedonia, The Former Yugoslav Republic of')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����˹��','Madagascar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ά','Malawi')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Malaysia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Maldives')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Mali')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����','Malta')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ܶ�Ⱥ��','Marshall Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Martinique')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ë��������','Mauritania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ë����˹','Mauritius')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Լ��','Mayotte')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ī����','Mexico')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ܿ���������','Micronesia, Federated States of')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ħ������','Moldova')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ħ�ɸ�','Monaco')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ɹ�','Mongolia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','Montserrat')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ħ���','Morocco')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Īɣ�ȿ�','Mozambique')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Namibia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�³','Nauru')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ჴ��','Nepal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Netherlands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������˹','Netherlands Antilles')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�¿��������','New Caledonia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','New Zealand')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Nicaragua')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ն�','Niger')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Nigeria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ŧ��','Niue')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ŵ���˵�','Norfolk Island')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������','Northern Mariana Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ų��','Norway')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Oman')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ͻ�˹̹','Pakistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Palau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Panama')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ͳ����¼�����','Papua New Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Paraguay')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��³','Peru')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ɱ�','Philippines')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Poland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Portugal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������','Puerto Rico')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Qatar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Reunion')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��������','Romania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����˹','Russia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('¬����','Rwanda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ������','Saint Helena')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ���ĺ���ά˹','Saint Kitts and Nevis')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ¬����','Saint Lucia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥƤ�������ܿ�¡','Saint Pierre and Miquelon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ��ɭ�غ͸����ɶ�˹','Saint Vincent and the Grenadines')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Ħ��','Samoa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ����ŵ','San Marino')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ʥ��������������','Sao Tome and Principe')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ɳ�ذ�����','Saudi Arabia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ڼӶ�','Senegal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ά�Ǻͺ�ɽ','Serbia and Montenegro')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�����','Seychelles')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Sierra Leone')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�¼���','Singapore')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('˹�工��','Slovakia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('˹��������','Slovenia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������Ⱥ��','Solomon Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Somalia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ϸ�','South Africa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Spain')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('˹������','Sri Lanka')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�յ�','Sudan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Suriname')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('˹�߶��͵��������ӵ�','Svalbard')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('˹��ʿ��','Swaziland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���','Sweden')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��ʿ','Switzerland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Syria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�й�̨��','China Taiwan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������˹̹','Tajikistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('̹ɣ����','Tanzania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('̩��','Thailand')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�͹���','The Bahamas')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�Ա���','The Gambia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���','Togo')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�п���','Tokelau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','Tonga')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�������Ͷ�͸�','Trinidad and Tobago')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ͻ��˹','Tunisia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Turkey')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������˹̹','Turkmenistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ؿ�˹�Ϳ���˹Ⱥ��','Turks and Caicos Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ͼ��¬','Tuvalu')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ڸɴ�','Uganda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ڿ���','Ukraine')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����������������','United Arab Emirates')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ӣ��','United Kingdom')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����','United States')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('������','Uruguay')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('���ȱ��˹̹','Uzbekistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Ŭ��ͼ','Vanuatu')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('ί������','Venezuela')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Խ��','Vietnam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����ά����Ⱥ��','Virgin Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('����˹�͸�ͼ��','Wallis and Futuna')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('Ҳ��','Yemen')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��˹����','Yugoslavia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('�ޱ���','Zambia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('��Ͳ�Τ','Zimbabwe')

--��ʼ��ʱ��
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('UK')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('ET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('PT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('CET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('CT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('MT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-12')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-11')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-10')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-9')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-8')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-7')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-6')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-4')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-3')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-2')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT-1')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+1')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+2')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+3')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+4')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+6')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+7')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+8')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+9')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+9.5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+10')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+10.5')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+11')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+12')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('GMT+13')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('EET')
INSERT INTO TB_TIMEZONE ( TIMEZONE_NAME ) VALUES ('POR')

--��ʼ������
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '�ı�', 'Australian Dollar (AUD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '����', 'US Dollar (USD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '�����', 'China Yuan (CNY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( 'ŷԪ', 'Euro (EUR)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( 'Ӣ��', 'Great Britain Pound (GBP)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '��Ԫ', 'Japan Yen (JPY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '�±�', 'Singapore Dollar (SGD)' )