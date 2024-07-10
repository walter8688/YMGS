部署步骤：
一.修改配置和数据还原
1.还原备份数据库（bestabet）, 创建操作数据库的账号密码，设置数据库启用ENABLE_BROKER；
2.更新YMGS.Service\YMGS.Service.exe.config 中数据库连接方式；
3.更新YMGS.Manage.Web\web.config 中数据库连接方式；
4.更新YMGS.Trade.Web\web.config 中数据库连接方式；

二.服务部署
1.YMGS.Service 为Windows系统服务，使用管理员方式启动CMD,
   InstallUtil.exe YMGS.Service.exe, 注册成功，在服务管理中可以看到GameControlService服务，启动服务；
2.配置IIS，新建两个网站，物理路径，指向YMGS.Manage.Web/YMGS.trade.Web 根路径，端口根据需要配置；
