DELETE from TB_SYSTEM_FUNC;

-- ************************** 1000 系统管理 **************************
-- 1000 系统管理（模块）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1000,null,'系统管理',1,1,1,'');

-- 1001 会员帐号管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1001,1000,'会员帐号管理',1,2,1,'');
-- 1002 锁定或解锁帐号（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1002,1001,'锁定或解锁帐号',1,3,1,'');
-- 1003 修改角色（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1003,1001,'修改角色',1,3,2,'');
-- 1004 修改资金（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1004,1001,'修改资金',1,3,3,'');

-- 1051 角色和权限管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1051,1000,'角色和权限管理',1,2,2,'');
-- 1052 新增角色（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1052,1051,'新增角色',1,3,1,'');
-- 1053 修改角色（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1053,1051,'修改角色',1,3,2,'');
-- 1054 删除角色（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1054,1051,'删除角色',1,3,3,'');

-- 1101 代理管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1101,1000,'代理管理',1,2,3,'');
-- 1102 代理管理（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1102,1101,'代理管理',1,3,1,'');

-- 1151 V网卡管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1151,1000,'V网卡管理',1,2,4,'');
-- 1152 生成V网卡（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1152,1151,'生成V网卡',1,3,1,'');

-- 1201 资金账户管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1201,1000,'资金账户管理',1,2,5,'');
-- 1202 设置银行卡（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1202,1201,'设置银行卡',1,3,1,'');

-- 1251 会员提现管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1251,1000,'会员提现管理',1,2,6,'');
-- 1252 审批提现（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1252,1251,'审批提现',1,3,1,'');
-- 1253 转款完成（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1253,1251,'转款完成',1,3,2,'');
-- 1254 拒绝提现（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1254,1251,'拒绝提现',1,3,3,'');

--1301 代理申请管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1301,1000,'代理申请管理',1,2,7,'');
--1302 代理申请审批（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1302,1301,'代理申请审批',1,3,1,'');
--1303 代理批准审批（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1303,1301,'代理申请审批',1,3,1,'');
--1304 代理拒绝审批（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(1304,1301,'代理申请审批',1,3,1,'');


-- ************************** 2000 赛事管理 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2000,null,'赛事管理',1,1,2,'');

-- 2001 赛事区域管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2001,2000,'赛事区域管理',1,2,1,'');
-- 2002 新增赛事区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2002,2001,'新增赛事区域',1,3,1,'');
-- 2003 修改赛事区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2003,2001,'修改赛事区域',1,3,2,'');
-- 2004 删除赛事区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2004,2001,'删除赛事区域',1,3,3,'');

-- 2051 赛事管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2051,2000,'赛事管理',1,2,2,'');
-- 2052 新增赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2052,2051,'新增赛事',1,3,1,'');
-- 2053 修改赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2053,2051,'修改赛事',1,3,2,'');
-- 2054 删除赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2054,2051,'删除赛事',1,3,3,'');
-- 2055 赛事另存（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2055,2051,'赛事另存',1,3,4,'');
-- 2056 启动或暂停赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2056,2051,'启动或暂停比赛',1,3,5,'');
-- 2057 终止赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2057,2051,'终止赛事',1,3,6,'');

-- 2101 参赛成员管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2101,2000,'参赛成员管理',1,2,3,'');
-- 2102 新增参赛成员（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2102,2101,'新增参赛成员',1,3,1,'');
-- 2103 修改参赛成员（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2103,2101,'修改参赛成员',1,3,2,'');
-- 2104 删除参赛成员（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2104,2101,'删除参赛成员',1,3,3,'');
-- 2105 启动或禁用参赛成员（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(2105,2101,'启动或禁用参赛成员',1,3,4,'');


-- ************************** 3000 比赛市场管理 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3000,null,'比赛市场管理',1,1,3,'');

-- 3001 交易类型管理（页面）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(3001,3000,'交易类型管理',1,2,1,'');
-- 3002 交易类型设置（权限）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(3002,3001,'交易类型设置',1,3,1,'');

-- 3051 市场模板管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3051,3000,'市场模板管理',1,2,2,'');
-- 3052 新增市场模板（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3052,3051,'新增市场模板',1,3,1,'');
-- 3053 修改市场模板（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3053,3051,'修改市场模板',1,3,2,'');
-- 3054 删除市场模板（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3054,3051,'删除市场模板',1,3,3,'');

-- 3101 比赛管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3101,3000,'比赛管理',1,2,3,'');
-- 3102 新增比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3102,3101,'新增比赛',1,3,1,'');
-- 3103 修改比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3103,3101,'修改比赛',1,3,2,'');
-- 3104 删除比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3104,3101,'删除比赛',1,3,3,'');
-- 3105 启动或暂停比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3105,3101,'启动或暂停比赛',1,3,4,'');
-- 3106 终止比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3106,3101,'终止比赛',1,3,5,'');
-- 3107 比赛另存（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3107,3101,'比赛另存',1,3,6,'');
-- 3108 推荐比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3108,3101,'推荐比赛',1,3,7,'');

-- 3151 冠军交易管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3151,3000,'冠军交易管理',1,2,4,'');
-- 3152 新增冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3152,3151,'新增冠军赛事',1,3,1,'');
-- 3153 修改冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3153,3151,'修改冠军赛事',1,3,2,'');
-- 3154 删除冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3154,3151,'删除冠军赛事',1,3,3,'');
-- 3155 冠军赛事另存（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3155,3151,'冠军赛事另存',1,3,4,'');
-- 3156 启动或暂停冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3156,3151,'启动或暂停冠军赛事',1,3,5,'');
-- 3157 终止冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3157,3151,'终止冠军赛事',1,3,6,'');
-- 3158 结束冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3158,3151,'结束冠军赛事',1,3,6,'');
-- 3159 录入冠军赛事结果（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(3159,3151,'录入冠军赛事结果',1,3,6,'');


-- ************************** 4000 赛中控制 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4000,null,'赛中控制',1,1,4,'');

-- 4001 比赛控制（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4001,4000,'比赛控制',1,2,1,'');
-- 4002 比赛封盘（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4002,4001,'比赛封盘',1,3,1,'');
-- 4003 录入比分（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4003,4001,'录入比分',1,3,2,'');
-- 4004 结束比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4004,4001,'结束比赛',1,3,3,'');
-- 4005 激活比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4005,4001,'激活比赛',1,3,4,'');
-- 4006 半场结束（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4006,4001,'半场结束',1,3,5,'');
-- 4007 清理市场（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4007,4001,'清理市场',1,3,6,'');
-- 4008 修改比赛开始时间（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4008,4001,'修改比赛开始时间',1,3,7,'');
-- 4009 开始比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4009,4001,'开始比赛',1,3,8,'');
-- 4010 开始比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(4010,4001,'开始下半场比赛',1,3,9,'');


-- ************************** 5000 赛后结算 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5000,null,'赛后结算',1,1,5,'');

-- 5001 比赛结算（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5001,5000,'比赛结算',1,2,1,'');
-- 5002 结算比赛（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5002,5001,'结算比赛',1,3,1,'');

-- 5010 结算冠军赛事（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5010,5000,'冠军赛事结算',1,2,1,'');
-- 5011 结算冠军赛事（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5011,5010,'结算冠军赛事',1,3,1,'');

-- 5051 佣金管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5051,5000,'佣金管理',1,2,2,'');
-- 5052 设置佣金率（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(5052,5051,'设置佣金率',1,3,1,'');


-- ************************** 6000 辅助管理 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6000,null,'辅助管理',1,1,6,'');

-- 6001 区域管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6001,6000,'区域管理',1,2,1,'');
-- 6002 新增区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6002,6001,'新增区域',1,3,1,'');
-- 6003 修改区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6003,6001,'修改区域',1,3,2,'');
-- 6004 删除区域（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6004,6001,'删除区域',1,3,3,'');

-- 6051 赔率对比管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6051,6000,'赔率对比管理',1,2,2,'');
-- 6052 新增赔率对比（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6052,6001,'新增赔率对比',1,3,1,'');
-- 6053 修改赔率对比（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6053,6001,'修改赔率对比',1,3,2,'');
-- 6054 删除赔率对比（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6054,6001,'删除赔率对比',1,3,3,'');

-- 6101 广告位信息管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6101,6000,'广告位信息管理',1,2,3,'');
-- 6102 设置广告位（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6102,6101,'设置广告位',1,3,1,'');

-- 6151 网站公告管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6151,6000,'网站公告管理',1,2,4,'');
-- 6152 新增公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6152,6151,'新增公告',1,3,1,'');
-- 6153 修改公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6153,6151,'修改公告',1,3,2,'');
-- 6154 删除公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6154,6151,'删除公告',1,3,3,'');
-- 6155 启动或暂停公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6155,6151,'启动或暂停公告',1,3,4,'');

-- 6201 参数管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6201,6000,'参数管理',1,2,5,'');
-- 6202 参数设置（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6202,6201,'参数设置',1,3,1,'');

-- 6251 文章目录管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6251,6000,'文章目录管理',1,2,6,'');
-- 6252 新增公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6252,6251,'新增目录',1,3,1,'');
-- 6253 修改公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6253,6251,'修改目录',1,3,2,'');
-- 6254 删除公告（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(6254,6251,'删除目录',1,3,3,'');


-- ************************** 7000 报表中心 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7000,null,'报表中心',1,1,7,'');

-- 7001 下注报表（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7001,7000,'下注报表',1,2,1,'');

-- 7051 交易报表（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7051,7000,'交易报表',1,2,2,'');

-- 7101 资金报表（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7101,7000,'资金报表',1,2,3,'');

-- 7151 佣金报表（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(7151,7000,'佣金报表',1,2,4,'');


-- ************************** 8000 交易平台 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(8000,null,'交易平台',2,1,1,'');

-- 8001 注册登录（页面）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8001,8000,'注册登录',2,2,1,'');
-- 8002 注册会员（权限）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8002,8001,'注册会员',2,3,1,'');
-- 8003 忘记密码（权限）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8003,8001,'忘记密码',2,3,2,'');

-- 8051 市场检索（页面）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8051,8000,'市场检索',2,2,2,'');

-- 8101 下注交易（页面）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8101,8000,'下注交易',2,2,3,'');
-- 8102 下注（权限）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(8102,8101,'下注',2,3,1,'');


-- ************************** 9000 会员中心 **************************
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9000,null,'会员中心',2,1,2,'');

-- 9001 会员信息管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9001,9000,'会员信息管理',2,2,1,'');
-- 9002 修改用户信息（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9002,9001,'修改用户信息',2,3,1,'');

-- 9051 资金账户管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9051,9000,'资金账户管理',2,2,2,'');
-- 9052 设置银行卡信息（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9052,9051,'设置银行卡信息',2,3,1,'');
-- 9053 在线充值（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9053,9051,'在线充值',2,3,2,'');
-- 9054 提现申请（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9054,9051,'提现申请',2,3,3,'');

-- 9101 代理会员管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9101,9000,'代理会员管理',2,2,3,'');
-- 9102 发展会员（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9102,9101,'发展会员',2,3,1,'');
-- 9103 指定或撤销代理（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9103,9101,'指定或撤销代理',2,3,2,'');
-- 9104 代理设置（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9104,9101,'代理设置',2,3,3,'');

-- 9151 我的交易管理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9151,9000,'我的交易管理',2,2,4,'');
-- 9152 修改下注（权限）
--INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
--VALUES(9152,9151,'修改下注',2,3,1,'');
-- 9153 撤销下注（权限）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9153,9151,'撤销下注',2,3,2,'');

-- 9201 我的积分查询（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9201,9000,'我的积分查询',2,2,5,'');

-- 9301 申请总代理（页面）
INSERT INTO TB_SYSTEM_FUNC(FUNC_ID,PARENT_FUNC_ID,FUNC_NAME,FUNC_TYPE,LEVELNO,FUNC_ORDER,URL)
VALUES(9301,9000,'申请总代理',2,2,6,'');

go
--系统默认角色
if(not exists(select * from tb_system_role where role_name='系统管理员'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('系统管理员','系统管理员','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='一级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('1级总代理','1级总代理','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('代理','代理','1',getdate(),1,getdate());
end
if(not exists(select * from tb_system_role where role_name='会员'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('会员','会员','1',getdate(),1,getdate());
END
if(not exists(select * from tb_system_role where role_name='2级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('2级总代理','2级总代理','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='3级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('3级总代理','3级总代理','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='4级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('4级总代理','4级总代理','1',getdate(),1,getdate());
END

if(not exists(select * from tb_system_role where role_name='5级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('5级总代理','5级总代理','1',getdate(),1,getdate());
END


--系统默认管理员admin
if(not exists(select * from tb_system_account where user_name='admin'))
begin
	insert into tb_system_account(user_name,born_year,born_month,born_day,email_address,
	role_id,account_status,login_name,password)
	values('admin','1970','12','01','2287547821@qq.com',1,1,'admin','AB83EFD73451D066A061622FDFF60999');
end
GO

--系统管理员默认资金账户
if(not exists(select * from TB_USER_FUND where USER_ID=1))
begin
INSERT INTO TB_USER_FUND( USER_ID ,BANK_NAME ,OPEN_BANK_NAME ,CARD_NO ,ACCOUNT_HOLDER ,
          CUR_FUND ,FREEZED_FUND ,CUR_INTEGRAL ,STATUS ,LAST_UPDATE_TIME)
VALUES  ( 1 ,N'' ,N'' , N'' , N'' , 0.00 , 0.00 , 0 , 0 , GETDATE())
end
GO

--给系统管理员默认全部的系统权限
declare @role_id int
select @role_id=role_id from tb_system_role where role_name='系统管理员'
delete from tb_role_func_map where role_id=@role_id
insert into tb_role_func_map
select @role_id,func_id
from tb_system_func b
go

DELETE FROM TB_BET_TYPE
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(1,'标准盘','Match Odds',1,1,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(2,'波胆','Correct Score',1,0,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(3,'大小球','Over/Under Goals',1,1,1,getdate(),1,getdate())
INSERT INTO TB_BET_TYPE(BET_TYPE_ID,BET_TYPE_NAME,BET_TYPE_NAME_EN,BET_BEFORE_GAME,BET_GAMING,CREATE_USER,CREATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)
VALUES(4,'让球盘','Asian Handicap',1,1,1,getdate(),1,getdate())
GO

--给赛事类别添加默认数据
DELETE FROM TB_EVENT_TYPE
INSERT  INTO TB_EVENT_TYPE
        ( EventType_ID, EventType_Name, EventType_Name_En )
VALUES  ( 1, '体育类', 'Sports' )
INSERT  INTO TB_EVENT_TYPE
        ( EventType_ID, EventType_Name, EventType_Name_En )
VALUES  ( 2, '娱乐类', 'Entertainment' )
GO

--给赛事项目添加默认数据
DELETE FROM TB_EVENT_ITEM
INSERT INTO TB_EVENT_ITEM( EventItem_ID,EventType_ID, EventItem_Name, EventItem_Name_En )
VALUES  ( 1, 1, N'足球', N'Football')
GO 

--给区域管理添加根目录‘世界’
DELETE FROM TB_PARAM_ZONE
INSERT INTO TB_PARAM_ZONE ( PARENT_ZONE_ID ,ZONE_NAME ,ZONE_ORDER)
VALUES  ( 0 , N'世界' , 1)
GO 

--给系统参数类型添加默认数据
DELETE FROM tb_param_type
/*
insert into tb_param_type (param_type_id,param_type_name)
values (1,'国家')
*/
insert into tb_param_type (param_type_id,param_type_name)
values (2,'参赛成员类型(国家/职业)')

insert into tb_param_type (param_type_id,param_type_name)
values (3,'参赛成员类型(男子/女子)')

/*insert into tb_param_type (param_type_id,param_type_name)
values (4,'积分率')*/
/*
insert into tb_param_type (param_type_id,param_type_name)
values (5,'Country')
*/
insert into tb_param_type (param_type_id,param_type_name)
values (6,'V网卡面值')

insert into tb_param_type (param_type_id,param_type_name)
values (7,'购卡面值')

--V网卡面值
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
--购卡面值
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

--市场模板
delete from tb_market_template;
--标准盘
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场标准盘','Match Odds',1,1,null,null,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场标准盘','Half Time',1,0,null,null,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半/全场标准盘','Half Time/Full Time',1,2,null,null,null,null,null,1,getdate(),1,getdate())
--波胆（半场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 - 0','(H)0 - 0',2,0,0,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 - 1','(H)0 - 1',2,0,0,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 - 2','(H)0 - 2',2,0,0,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 - 3','(H)0 - 3',2,0,0,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场1 - 0','(H)1 - 0',2,0,1,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场1 - 1','(H)1 - 1',2,0,1,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场1 - 2','(H)1 - 2',2,0,1,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场1 - 3','(H)1 - 3',2,0,1,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场2 - 0','(H)2 - 0',2,0,2,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场2 - 1','(H)2 - 1',2,0,2,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场2 - 2','(H)2 - 2',2,0,2,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场2 - 3','(H)2 - 3',2,0,2,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场3 - 0','(H)3 - 0',2,0,3,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场3 - 1','(H)3 - 1',2,0,3,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场3 - 2','(H)3 - 2',2,0,3,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场3 - 3','(H)3 - 3',2,0,3,3,null,null,null,1,getdate(),1,getdate())
--波胆（全场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 0','(F)0 - 0',2,1,0,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 1','(F)0 - 1',2,1,0,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 2','(F)0 - 2',2,1,0,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 3','(F)0 - 3',2,1,0,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 4','(F)0 - 4',2,1,0,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 - 5','(F)0 - 5',2,1,0,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 0','(F)1 - 0',2,1,1,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 1','(F)1 - 1',2,1,1,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 2','(F)1 - 2',2,1,1,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 3','(F)1 - 3',2,1,1,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 4','(F)1 - 4',2,1,1,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1 - 5','(F)1 - 5',2,1,1,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 0','(F)2 - 0',2,1,2,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 1','(F)2 - 1',2,1,2,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 2','(F)2 - 2',2,1,2,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 3','(F)2 - 3',2,1,2,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 4','(F)2 - 4',2,1,2,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2 - 5','(F)2 - 5',2,1,2,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 0','(F)3 - 0',2,1,3,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 1','(F)3 - 1',2,1,3,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 2','(F)3 - 2',2,1,3,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 3','(F)3 - 3',2,1,3,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 4','(F)3 - 4',2,1,3,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3 - 5','(F)3 - 5',2,1,3,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 0','(F)4 - 0',2,1,4,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 1','(F)4 - 1',2,1,4,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 2','(F)4 - 2',2,1,4,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 3','(F)4 - 3',2,1,4,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 4','(F)4 - 4',2,1,4,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4 - 5','(F)4 - 5',2,1,4,5,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 0','(F)5 - 0',2,1,5,0,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 1','(F)5 - 1',2,1,5,1,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 2','(F)5 - 2',2,1,5,2,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 3','(F)5 - 3',2,1,5,3,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 4','(F)5 - 4',2,1,5,4,null,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5 - 5','(F)5 - 5',2,1,5,5,null,null,null,1,getdate(),1,getdate())
--大小球（半场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0.5球','First Half Goals 0.5',3,0,null,null,0.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场1.5球','First Half Goals 1.5',3,0,null,null,1.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场2.5球','First Half Goals 2.5',3,0,null,null,2.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场3.5球','First Half Goals 3.5',3,0,null,null,3.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场4.5球','First Half Goals 4.5',3,0,null,null,4.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场5.5球','First Half Goals 5.5',3,0,null,null,5.5,null,null,1,getdate(),1,getdate())
--大小球（全场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0.5球','Over/Under 0.5 Goals',3,1,null,null,0.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场1.5球','Over/Under 1.5 Goals',3,1,null,null,1.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场2.5球','Over/Under 2.5 Goals',3,1,null,null,2.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场3.5球','Over/Under 3.5 Goals',3,1,null,null,3.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场4.5球','Over/Under 4.5 Goals',3,1,null,null,4.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场5.5球','Over/Under 5.5 Goals',3,1,null,null,5.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场6.5球','Over/Under 6.5 Goals',3,1,null,null,6.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场7.5球','Over/Under 7.5 Goals',3,1,null,null,7.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场8.5球','Over/Under 8.5 Goals',3,1,null,null,8.5,null,null,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场9.5球','Over/Under 9.5 Goals',3,1,null,null,9.5,null,null,1,getdate(),1,getdate())
--让分盘（半场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-2.0','(H)-2.0',4,0,null,null,null,null,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-1.5 & -2.0','(H)-1.5 & -2.0',4,0,null,null,null,-1.5,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-1.5','(H)-1.5',4,0,null,null,null,null,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-1.0 & -1.5','(H)-1.0 & -1.5',4,0,null,null,null,-1,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-1.0','(H)-1.0',4,0,null,null,null,null,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-0.5 & -1.0','(H)-0.5 & -1.0',4,0,null,null,null,-0.5,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场-0.5','(H)-0.5',4,0,null,null,null,null,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 & -0.5','(H)0 & -0.5',4,0,null,null,null,0,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0','(H)0',4,0,null,null,null,null,0,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场0 & +0.5','(H)0 & +0.5',4,0,null,null,null,0,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+0.5','(H)+0.5',4,0,null,null,null,null,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+0.5 & +1.0','(H)+0.5 & +1.0',4,0,null,null,null,0.5,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+1.0','(H)+1.0',4,0,null,null,null,null,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+1.0 & +1.5','(H)+1.0 & +1.5',4,0,null,null,null,1,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+1.5','(H)+1.5',4,0,null,null,null,null,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+1.5 & +2.0','(H)+1.5 & +2.0',4,0,null,null,null,1.5,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('半场+2.0','(H)+2.0',4,0,null,null,null,null,2,1,getdate(),1,getdate())
--让分盘（全场）
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-4.0','(F)-4.0',4,1,null,null,null,null,-4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-3.5 & -4.0','(F)-3.5 & -4.0',4,1,null,null,null,-3.5,-4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-3.5','(F)-3.5',4,1,null,null,null,null,-3.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-3.0','(F)-3.0',4,1,null,null,null,null,-3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-2.5 & -3.0','(F)-2.5 & -3.0',4,1,null,null,null,-2.5,-3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-2.5','(F)-2.5',4,1,null,null,null,null,-2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-2.0 & -2.5','(F)-2.0 & -2.5',4,1,null,null,null,-2,-2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-2.0','(F)-2.0',4,1,null,null,null,null,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-1.5 & -2.0','(F)-1.5 & -2.0',4,1,null,null,null,-1.5,-2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-1.5','(F)-1.5',4,1,null,null,null,null,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-1.0 & -1.5','(F)-1.0 & -1.5',4,1,null,null,null,-1,-1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-1.0','(F)-1.0',4,1,null,null,null,null,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-0.5 & -1.0','(F)-0.5 & -1.0',4,1,null,null,null,-0.5,-1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场-0.5','(F)-0.5',4,1,null,null,null,null,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 & -0.5','(F)0 & -0.5',4,1,null,null,null,0,-0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0','(F)0',4,1,null,null,null,null,0,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场0 & +0.5','(F)0 & +0.5',4,1,null,null,null,0,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+0.5','(F)+0.5',4,1,null,null,null,null,0.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+0.5 & +1.0','(F)+0.5 & +1.0',4,1,null,null,null,0.5,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+1.0','(F)+1.0',4,1,null,null,null,null,1,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+1.0 & +1.5','(F)+1.0 & +1.5',4,1,null,null,null,1,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+1.5','(F)+1.5',4,1,null,null,null,null,1.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+1.5 & +2.0','(F)+1.5 & +2.0',4,1,null,null,null,1.5,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+2.0','(F)+2.0',4,1,null,null,null,null,2,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+2.0 & +2.5','(F)+2.0 & +2.5',4,1,null,null,null,2,2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+2.5','(F)+2.5',4,1,null,null,null,null,2.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+2.5 & +3.0','(F)+2.5 & +3.0',4,1,null,null,null,2.5,3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+3.0','(F)+3.0',4,1,null,null,null,null,3,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+3.5','(F)+3.5',4,1,null,null,null,null,3.5,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+3.5 & +4.0','(F)+3.5 & +4.0',4,1,null,null,null,3.5,4,1,getdate(),1,getdate())
insert into tb_market_template(market_tmp_name,market_tmp_name_en,bet_type_id,market_tmp_type,homescore,awayscore,
goals,scorea,scoreb,create_user,create_time,last_update_user,last_update_time)
values('全场+4.0','(F)+4.0',4,1,null,null,null,null,4,1,getdate(),1,getdate())
--模板类型 0 半场 1 全场 2 半场/全场
--bet_type_id 从select * from tb_bet_type获取


--佣金率和积分对应表
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


--数据缓存对象表
delete from TB_CACHE_OBJECT
insert into TB_CACHE_OBJECT values(0,'赛事项目',getdate())
insert into TB_CACHE_OBJECT values(1,'赛事区域',getdate())
insert into TB_CACHE_OBJECT values(2,'赛事',getdate())
insert into TB_CACHE_OBJECT values(3,'比赛', getdate())
insert into TB_CACHE_OBJECT values(4,'冠军', getdate())
insert into TB_CACHE_OBJECT values(5,'文字广告', getdate())
insert into TB_CACHE_OBJECT values(6,'图片广告', getdate())
insert into TB_CACHE_OBJECT values(7,'公告', getdate())
insert into TB_CACHE_OBJECT values(8,'置顶比赛', getdate())
insert into TB_CACHE_OBJECT values(9,'賠率對比', getdate())
insert into TB_CACHE_OBJECT values(10,'帮助列表',GETDATE())
insert into TB_CACHE_OBJECT values(11,'投注信息',GETDATE())
insert into TB_CACHE_OBJECT values(12,'受注信息',GETDATE())
--系统默认资金账号数据
delete from tb_system_main_fund
insert into tb_system_main_fund values(1,0,'','',getdate())


/*Country*/
DELETE FROM TB_COUNTRY
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿富汗','Afghanistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿尔巴尼亚','Albania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿尔及利亚','Algeria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('美属萨摩亚','American Samoa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('安道尔','Andorra')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('安哥拉','Angola')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('安圭拉','Anguilla')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('南极洲','Antarctica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('安提瓜和巴布达','Antigua and Barbuda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿根廷','Argentina')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('亚美尼亚','Armenia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿鲁巴','Aruba')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('澳大利亚','Australia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('奥地利','Austria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿塞拜疆','Azerbaijan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴林','Bahrain')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('孟加拉国','Bangladesh')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴巴多斯','Barbados')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('白俄罗斯','Belarus')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('比利时','Belgium')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('伯利兹','Belize')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('贝宁','Benin')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('百慕大','Bermuda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('不丹','Bhutan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('玻利维亚','Bolivia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('波黑','Bosnia and Herzegovina')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('博茨瓦纳','Botswana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴西','Brazil')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('英属维尔京群岛','British Virgin Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('文莱','Brunei Darussalam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('保加利亚','Bulgaria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('布基纳法索','Burkina Faso')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('缅甸','Burma')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('布隆迪','Burundi')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('柬埔寨','Cambodia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('喀麦隆','Cameroon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('加拿大','Canada')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('佛得角','Cape Verde')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('开曼群岛','Cayman Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('中非','Central African Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('乍得','Chad')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('智利','Chile')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('中国','China')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣诞岛','Christmas Island')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('科科斯（基林）群岛','Cocos (Keeling) Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('哥伦比亚','Colombia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('科摩罗','Comoros')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('刚果（金）','Congo, Democratic Republic of the')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('刚果（布）','Congo, Republic of the')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('库克群岛','Cook Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('哥斯达黎加','Costa Rica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('科特迪瓦','Cote d''Ivoire')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('克罗地亚','Croatia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('古巴','Cuba')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塞浦路斯','Cyprus')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('捷克','Czech Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('丹麦','Denmark')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('吉布提','Djibouti')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('多米尼克','Dominica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('多米尼加','Dominican Republic')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('厄瓜多尔','Ecuador')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('埃及','Egypt')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('萨尔瓦多','El Salvador')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('赤道几内亚','Equatorial Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('厄立特里亚','Eritrea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('爱沙尼亚','Estonia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('埃塞俄比亚','Ethiopia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('福克兰群岛（马尔维纳斯）','Falkland Islands (Islas Malvinas)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('法罗群岛','Faroe Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斐济','Fiji')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('芬兰','Finland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('法国','France')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('法属圭亚那','French Guiana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('法属波利尼西亚','French Polynesia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('加蓬','Gabon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('格鲁吉亚','Georgia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('德国','Germany')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('加纳','Ghana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('直布罗陀','Gibraltar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('希腊','Greece')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('格陵兰','Greenland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('格林纳达','Grenada')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瓜德罗普','Guadeloupe')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('关岛','Guam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('危地马拉','Guatemala')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('根西岛','Guernsey')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('几内亚','Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('几内亚比绍','Guinea-Bissau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圭亚那','Guyana')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('海地','Haiti')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('梵蒂冈','Holy See (Vatican City)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('洪都拉斯','Honduras')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('中国香港','Hong Kong (SAR)')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('匈牙利','Hungary')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('冰岛','Iceland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('印度','India')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('印度尼西亚','Indonesia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('伊朗','Iran')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('伊拉克','Iraq')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('爱尔兰','Ireland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('以色列','Israel')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('意大利','Italy')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('牙买加','Jamaica')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('日本','Japan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('约旦','Jordan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('哈萨克斯坦','Kazakhstan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('肯尼亚','Kenya')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('基里巴斯','Kiribati')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('朝鲜','Korea, North')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('韩国','Korea, South')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('科威特','Kuwait')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('吉尔吉斯斯坦','Kyrgyzstan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('老挝','Laos')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('拉脱维亚','Latvia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('黎巴嫩','Lebanon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('莱索托','Lesotho')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('利比里亚','Liberia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('利比亚','Libya')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('列支敦士登','Liechtenstein')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('立陶宛','Lithuania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('卢森堡','Luxembourg')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('澳门','Macao')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('前南马其顿','Macedonia, The Former Yugoslav Republic of')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马达加斯加','Madagascar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马拉维','Malawi')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马来西亚','Malaysia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马尔代夫','Maldives')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马里','Mali')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马耳他','Malta')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马绍尔群岛','Marshall Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马提尼克','Martinique')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('毛里塔尼亚','Mauritania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('毛里求斯','Mauritius')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('马约特','Mayotte')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('墨西哥','Mexico')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('密克罗尼西亚','Micronesia, Federated States of')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('摩尔多瓦','Moldova')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('摩纳哥','Monaco')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('蒙古','Mongolia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('蒙特塞拉特','Montserrat')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('摩洛哥','Morocco')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('莫桑比克','Mozambique')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('纳米尼亚','Namibia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瑙鲁','Nauru')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('尼泊尔','Nepal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('荷兰','Netherlands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('荷属安的列斯','Netherlands Antilles')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('新喀里多尼亚','New Caledonia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('新西兰','New Zealand')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('尼加拉瓜','Nicaragua')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('尼日尔','Niger')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('尼日利亚','Nigeria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('纽埃','Niue')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('诺福克岛','Norfolk Island')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('北马里亚纳','Northern Mariana Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('挪威','Norway')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿曼','Oman')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴基斯坦','Pakistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('帕劳','Palau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴拿马','Panama')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴布亚新几内亚','Papua New Guinea')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴拉圭','Paraguay')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('秘鲁','Peru')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('菲律宾','Philippines')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('波兰','Poland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('葡萄牙','Portugal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('波多黎各','Puerto Rico')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('卡塔尔','Qatar')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('留尼汪','Reunion')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('罗马尼亚','Romania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('俄罗斯','Russia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('卢旺达','Rwanda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣赫勒拿','Saint Helena')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣基茨和尼维斯','Saint Kitts and Nevis')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣卢西亚','Saint Lucia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣皮埃尔和密克隆','Saint Pierre and Miquelon')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣文森特和格林纳丁斯','Saint Vincent and the Grenadines')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('萨摩亚','Samoa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣马力诺','San Marino')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('圣多美和普林西比','Sao Tome and Principe')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('沙特阿拉伯','Saudi Arabia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塞内加尔','Senegal')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塞尔维亚和黑山','Serbia and Montenegro')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塞舌尔','Seychelles')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塞拉利','Sierra Leone')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('新加坡','Singapore')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斯洛伐克','Slovakia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斯洛文尼亚','Slovenia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('所罗门群岛','Solomon Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('索马里','Somalia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('南非','South Africa')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('西班牙','Spain')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斯里兰卡','Sri Lanka')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('苏丹','Sudan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('苏里南','Suriname')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斯瓦尔巴岛和扬马延岛','Svalbard')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('斯威士兰','Swaziland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瑞典','Sweden')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瑞士','Switzerland')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('叙利亚','Syria')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('中国台湾','China Taiwan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('塔吉克斯坦','Tajikistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('坦桑尼亚','Tanzania')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('泰国','Thailand')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('巴哈马','The Bahamas')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('冈比亚','The Gambia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('多哥','Togo')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('托克劳','Tokelau')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('汤加','Tonga')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('特立尼达和多巴哥','Trinidad and Tobago')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('突尼斯','Tunisia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('土耳其','Turkey')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('土库曼斯坦','Turkmenistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('特克斯和凯科斯群岛','Turks and Caicos Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('图瓦卢','Tuvalu')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('乌干达','Uganda')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('乌克兰','Ukraine')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('阿拉伯联合酋长国','United Arab Emirates')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('英国','United Kingdom')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('美国','United States')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('乌拉圭','Uruguay')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('乌兹别克斯坦','Uzbekistan')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瓦努阿图','Vanuatu')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('委内瑞拉','Venezuela')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('越南','Vietnam')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('美属维尔京群岛','Virgin Islands')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('瓦利斯和富图纳','Wallis and Futuna')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('也门','Yemen')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('南斯拉夫','Yugoslavia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('赞比亚','Zambia')
INSERT INTO TB_COUNTRY(COUNTRY_NAME_CN,COUNTRY_NAME_EN) VALUES('津巴布韦','Zimbabwe')

--初始化时区
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

--初始化货币
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '澳币', 'Australian Dollar (AUD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '美金', 'US Dollar (USD)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '人民币', 'China Yuan (CNY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '欧元', 'Euro (EUR)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '英镑', 'Great Britain Pound (GBP)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '日元', 'Japan Yen (JPY)' )
INSERT INTO TB_CURRENCY ( CURRENCY_NAME, CURRENCY_EN ) VALUES  ( '新币', 'Singapore Dollar (SGD)' )