--总代理分为5级。1级总代理；2级总代理；3级总代理；4级总代理；5级总代理
-------------------------------------------------------------------------------------
UPDATE tb_system_role SET role_name = '1级总代理',role_desc = '1级总代理' WHERE role_name = '总代理'
-------------------------------------------------------------------------------------
if(not exists(select * from tb_system_role where role_name='2级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('2级总代理','2级总代理','1',getdate(),1,getdate());
END
-------------------------------------------------------------------------------------
if(not exists(select * from tb_system_role where role_name='3级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('3级总代理','3级总代理','1',getdate(),1,getdate());
END
-------------------------------------------------------------------------------------
if(not exists(select * from tb_system_role where role_name='4级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('4级总代理','4级总代理','1',getdate(),1,getdate());
END
-------------------------------------------------------------------------------------
if(not exists(select * from tb_system_role where role_name='5级总代理'))
begin
	insert into tb_system_role(role_name,role_desc,create_user,create_time,last_update_user,last_update_time)
	values('5级总代理','5级总代理','1',getdate(),1,getdate());
END
