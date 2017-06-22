#2017-06-22
ALTER TABLE `missive`
	ADD COLUMN `RedTitleId` INT(11) NOT NULL AFTER `ID`;

ALTER TABLE `missive`
	CHANGE COLUMN `WordId` `ContentId` INT(11) NULL DEFAULT NULL AFTER `RedTitleId`;


#2017-06-21 已经执行
#把已经结束流程的 更新到归档箱
--update user_form_info
--set status=3
--where status!=3 and infoid in (
--select i.ID 
--from form_info i
--left join flow_data f
--on i.ID = f.InfoId
--where completed =1
--)

--#更新已读的
--update user_form_info uf
--join freeflow_nodedata ffnd
--on uf.UserID = ffnd.UserId
--join flow_node_data fnd
--on ffnd.freeflowdataid = fnd.ID
--join flow_data fd
--on fnd.FlowDataId = fd.ID
-- set status=2
--where ffnd.updatetime is not null and uf.InfoID =fd.InfoId

--#删除不该存在的user_form_info

-- CREATE TEMPORARY TABLE IF NOT EXISTS Temp_UserFlowInfo(
-- 	ID INT(11)
-- );
 
-- insert into Temp_UserFlowInfo (id) (select uf.id from user_form_info uf
--	join
--	(select fd.infoid as infoid,ffnd.userid from freeflow_nodedata ffnd
--		join flow_node_data fnd
--		on ffnd.freeflowdataid = fnd.ID
--		join flow_data fd
--		on fnd.FlowDataId = fd.ID
--		where ffnd.updatetime is not null
--	) ufr
--	on uf.InfoID = ufr.infoid and uf.userid = ufr.userid);
	
--	 insert into Temp_UserFlowInfo (id) (select uf.id from user_form_info uf
--	join
--	(select fnd.UserId ,fd.InfoId from flow_node_data fnd
--		join flow_node fn
--		on fnd.flownodeid = fn.id
--		join flow_data fd
--		on fnd.FlowDataId = fd.ID
--	) ufr
--	on uf.InfoID = ufr.infoid and uf.userid = ufr.userid);
	
--	delete from user_form_info where id not in(select * from Temp_UserFlowInfo);
--	DROP TEMPORARY TABLE IF EXISTS Temp_UserFlowInfo

#2017-06-20
RENAME TABLE `organization` TO `department`;

ALTER TABLE `user`
	ADD COLUMN `Sort` INT NOT NULL AFTER `Password`;


CREATE TABLE `missive_redtitle` (
	`ID` INT(11) NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50) NOT NULL,
	`FormId` INT(11) NOT NULL DEFAULT '0',
	`TemplateId` INT(11) NOT NULL,
	PRIMARY KEY (`ID`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

#2017-06-15

CREATE TABLE `meetingroom` (
	`ID` INT(11) NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50) NOT NULL,
	`Number` VARCHAR(50) NOT NULL,
	`UpdateTime` DATETIME NULL DEFAULT NULL,
	`Type` INT(11) NOT NULL,
	`Status` INT(11) NOT NULL,
	`Deleted` BIT(1) NOT NULL,
	`PhotoId` INT(11) NOT NULL,
	PRIMARY KEY (`ID`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=8
;
CREATE TABLE `seal` (
	`ID` INT(11) NOT NULL AUTO_INCREMENT,
	`Deleted` BIT(1) NOT NULL DEFAULT b'0',
	`Name` VARCHAR(50) NOT NULL,
	`Status` INT(11) NOT NULL,
	PRIMARY KEY (`ID`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `task` (
	`ID` INT(11) NOT NULL,
	`LY_LX` INT(11) NOT NULL,
	`ZRR` INT(11) NOT NULL,
	`MC` VARCHAR(50) NOT NULL,
	`LY` VARCHAR(50) NOT NULL,
	`ZB_DW` VARCHAR(50) NOT NULL,
	`XB_DW` VARCHAR(50) NOT NULL,
	`GZ_MB` VARCHAR(128) NOT NULL,
	`JH_SJ` DATETIME NULL DEFAULT NULL,
	`BZ` VARCHAR(255) NULL DEFAULT NULL,
	PRIMARY KEY (`ID`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


CREATE TABLE `task_progress` (
	`ID` INT(11) NOT NULL AUTO_INCREMENT,
	`TaskId` INT(11) NOT NULL DEFAULT '0',
	`UserId` INT(11) NOT NULL DEFAULT '0',
	`CreateTime` DATETIME NOT NULL,
	`Content` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`ID`),
	INDEX `UserId` (`UserId`),
	INDEX `TaskId` (`TaskId`)
)
ENGINE=InnoDB
;



#2017-06-14
ALTER TABLE `car_apply`
	ALTER `CarId` DROP DEFAULT;
ALTER TABLE `car_apply`
	CHANGE COLUMN `CarId` `InfoId` INT(11) NOT NULL AFTER `ID`;
RENAME TABLE `car_apply` TO `form_info_extend1`;

#2017-06-09
ALTER TABLE `car_apply`
	ADD COLUMN `ApprovalUserId` INT NOT NULL AFTER `CreateTime`,
	ADD INDEX `ApprovalUserId` (`ApprovalUserId`);


#2017-06-06
CREATE TABLE `car_apply` (
	`ID` INT(11) NOT NULL,
	`CarId` INT(11) NOT NULL,
	`UserId` INT(11) NOT NULL,
	`CreateTime` DATETIME NOT NULL,
	`ScheduleBeginTime` DATETIME NOT NULL,
	`ScheduleEndTime` DATETIME NULL DEFAULT NULL,
	`RealEndTime` DATETIME NULL DEFAULT NULL,
	`Reason` VARCHAR(255) NULL DEFAULT NULL,
	`Result` BIT(1) NULL DEFAULT NULL,
	`UpdateTime` DATETIME NULL DEFAULT NULL,
	PRIMARY KEY (`ID`),
	INDEX `CarId` (`CarId`),
	INDEX `UserId` (`UserId`),
	INDEX `CreateTime` (`CreateTime`)
)
ENGINE=InnoDB
;

#2017-05-26 zlj

ALTER TABLE `missive`
	ALTER `MJ` DROP DEFAULT;
ALTER TABLE `missive`
	CHANGE COLUMN `WH` `WJ_ZH` VARCHAR(50) NULL DEFAULT NULL AFTER `WJ_BT`,
	CHANGE COLUMN `MJ` `WJ_MJ` INT(11) NOT NULL AFTER `WJ_ZH`,
	ADD COLUMN `JJ_DJ` INT(11) NOT NULL AFTER `WJ_MJ`,
	ADD COLUMN `DJR` VARCHAR(50) NULL DEFAULT NULL AFTER `ZRR`,
	ADD COLUMN `JB_RQ` DATETIME NULL DEFAULT NULL AFTER `FW_RQ`,
	CHANGE COLUMN `LY` `WJ_LY` VARCHAR(255) NULL DEFAULT NULL AFTER `QX_RQ`;

	ALTER TABLE `missive`
	ALTER `ZWGK` DROP DEFAULT,
	ALTER `GKFB` DROP DEFAULT;
ALTER TABLE `missive`
	CHANGE COLUMN `WordId` `WordId` INT(11) NULL DEFAULT NULL AFTER `ID`,
	CHANGE COLUMN `QX_RQ` `QX_RQ` DATETIME NULL DEFAULT NULL AFTER `JB_RQ`,
	CHANGE COLUMN `ZWGK` `ZW_GK` INT(11) NOT NULL AFTER `WJ_LY`,
	CHANGE COLUMN `GKFB` `GK_FB` BIT(1) NOT NULL AFTER `ZW_GK`,
	DROP COLUMN `SX_SJ`,
	DROP COLUMN `HLW_FB`;


ALTER TABLE `flow_node`
	ALTER `Name` DROP DEFAULT,
	ALTER `JobTitleId` DROP DEFAULT;
ALTER TABLE `flow_node`
	CHANGE COLUMN `Name` `Name` VARCHAR(50) NOT NULL AFTER `FlowId`,
	CHANGE COLUMN `UserId` `UserIds` VARCHAR(50) NULL DEFAULT '0' AFTER `Name`,
	CHANGE COLUMN `JobTitleId` `JobTitleIds` VARCHAR(50) NULL AFTER `DepartmentIds`,
	DROP COLUMN `GroupID`;
