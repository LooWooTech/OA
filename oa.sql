-- --------------------------------------------------------
-- 主机:                           10.22.102.32
-- 服务器版本:                        5.1.73-community - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 oa 的数据库结构
CREATE DATABASE IF NOT EXISTS `oa` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `oa`;

-- 导出  表 oa.category 结构
CREATE TABLE IF NOT EXISTS `category` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(1023) NOT NULL,
  `FormID` int(11) NOT NULL DEFAULT '0',
  `Sort` int(11) NOT NULL DEFAULT '0',
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `Type` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FormID` (`FormID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.category 的数据：~2 rows (大约)
DELETE FROM `category`;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`ID`, `Name`, `FormID`, `Sort`, `Deleted`, `ParentID`, `Type`) VALUES
	(1, '种类1', 1, 0, b'00', 0, 1),
	(2, '种类2', 1, 0, b'00', 0, 1);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;

-- 导出  表 oa.diary 结构
CREATE TABLE IF NOT EXISTS `diary` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Time` datetime NOT NULL,
  `Equipment` int(10) NOT NULL DEFAULT '0',
  `Project` varchar(1023) NOT NULL,
  `Content` text NOT NULL,
  `UID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `UID` (`UID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.diary 的数据：~0 rows (大约)
DELETE FROM `diary`;
/*!40000 ALTER TABLE `diary` DISABLE KEYS */;
/*!40000 ALTER TABLE `diary` ENABLE KEYS */;

-- 导出  表 oa.feed 结构
CREATE TABLE IF NOT EXISTS `feed` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FormId` int(11) NOT NULL DEFAULT '0',
  `InfoId` int(11) NOT NULL DEFAULT '0',
  `Type` int(11) NOT NULL DEFAULT '0',
  `FromUserId` int(11) NOT NULL DEFAULT '0',
  `ToUserId` int(11) NOT NULL DEFAULT '0',
  `Deleted` bit(3) NOT NULL DEFAULT b'0',
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `InfoId` (`InfoId`),
  KEY `ToUserId` (`ToUserId`),
  KEY `FromUserId` (`FromUserId`),
  KEY `Type` (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.feed 的数据：~0 rows (大约)
DELETE FROM `feed`;
/*!40000 ALTER TABLE `feed` DISABLE KEYS */;
/*!40000 ALTER TABLE `feed` ENABLE KEYS */;

-- 导出  表 oa.file 结构
CREATE TABLE IF NOT EXISTS `file` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `FileName` text NOT NULL,
  `SavePath` text NOT NULL,
  `Size` bigint(20) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime NOT NULL,
  `Type` bit(3) NOT NULL DEFAULT b'0',
  `InfoTID` int(11) DEFAULT NULL,
  `FormID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `ParentID` (`ParentID`),
  KEY `InfoTID` (`InfoTID`),
  KEY `FormID` (`FormID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.file 的数据：~0 rows (大约)
DELETE FROM `file`;
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
/*!40000 ALTER TABLE `file` ENABLE KEYS */;

-- 导出  表 oa.flow 结构
CREATE TABLE IF NOT EXISTS `flow` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flow 的数据：~3 rows (大约)
DELETE FROM `flow`;
/*!40000 ALTER TABLE `flow` DISABLE KEYS */;
INSERT INTO `flow` (`ID`, `Name`) VALUES
	(1, '模板1'),
	(2, '模板2'),
	(3, '模板3');
/*!40000 ALTER TABLE `flow` ENABLE KEYS */;

-- 导出  表 oa.flow_data 结构
CREATE TABLE IF NOT EXISTS `flow_data` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `InfoId` int(11) NOT NULL DEFAULT '0',
  `FormId` int(11) NOT NULL DEFAULT '0',
  `FlowId` int(11) NOT NULL DEFAULT '0',
  `Completed` bit(5) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `InfoId` (`InfoId`),
  KEY `FormId` (`FormId`),
  KEY `FlowId` (`FlowId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flow_data 的数据：~0 rows (大约)
DELETE FROM `flow_data`;
/*!40000 ALTER TABLE `flow_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_data` ENABLE KEYS */;

-- 导出  表 oa.flow_node 结构
CREATE TABLE IF NOT EXISTS `flow_node` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FlowId` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(1023) NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  `GroupID` int(11) NOT NULL DEFAULT '0',
  `DepartmentId` int(11) NOT NULL DEFAULT '0',
  `BackNodeID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowId` (`FlowId`),
  KEY `UserId` (`UserId`),
  KEY `DepartmentId` (`DepartmentId`),
  KEY `BackNodeID` (`BackNodeID`),
  KEY `GroupID` (`GroupID`),
  CONSTRAINT `FK_flow_node_flow` FOREIGN KEY (`FlowId`) REFERENCES `flow` (`ID`),
  CONSTRAINT `FK_flow_node_organization` FOREIGN KEY (`DepartmentId`) REFERENCES `organization` (`ID`),
  CONSTRAINT `FK_flow_node_user` FOREIGN KEY (`UserId`) REFERENCES `user` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flow_node 的数据：~0 rows (大约)
DELETE FROM `flow_node`;
/*!40000 ALTER TABLE `flow_node` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_node` ENABLE KEYS */;

-- 导出  表 oa.flow_node_data 结构
CREATE TABLE IF NOT EXISTS `flow_node_data` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FlowDataId` int(11) NOT NULL DEFAULT '0',
  `ParentId` int(11) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  `DepartmentId` int(11) NOT NULL DEFAULT '0',
  `UpdateTime` datetime DEFAULT NULL,
  `Result` bit(2) DEFAULT NULL,
  `Content` text,
  `FlowNodeId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowDataId` (`FlowDataId`),
  KEY `ParentId` (`ParentId`),
  KEY `UserId` (`UserId`),
  KEY `DepartmentId` (`DepartmentId`),
  KEY `FlowNodeId` (`FlowNodeId`),
  CONSTRAINT `FK_flow_node_data_flow_node` FOREIGN KEY (`FlowNodeId`) REFERENCES `flow_node` (`ID`),
  CONSTRAINT `FK_flow_node_data_flow_data` FOREIGN KEY (`FlowDataId`) REFERENCES `flow_data` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flow_node_data 的数据：~0 rows (大约)
DELETE FROM `flow_node_data`;
/*!40000 ALTER TABLE `flow_node_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_node_data` ENABLE KEYS */;

-- 导出  表 oa.form 结构
CREATE TABLE IF NOT EXISTS `form` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `FlowID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowID` (`FlowID`),
  CONSTRAINT `FK_form_flow` FOREIGN KEY (`FlowID`) REFERENCES `flow` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.form 的数据：~24 rows (大约)
DELETE FROM `form`;
/*!40000 ALTER TABLE `form` DISABLE KEYS */;
INSERT INTO `form` (`ID`, `Name`, `FlowID`) VALUES
	(1, '公文', 1),
	(2, '车辆', 1),
	(3, '会议', 1),
	(4, '公文', 1),
	(5, '车辆', 1),
	(6, '会议', 1),
	(7, '公文', 1),
	(8, '车辆', 1),
	(9, '会议', 1),
	(10, '公文', 1),
	(11, '车辆', 1),
	(12, '会议', 1),
	(13, '公文', 1),
	(14, '车辆', 1),
	(15, '会议', 1),
	(16, '公文', 1),
	(17, '车辆', 1),
	(18, '会议', 1),
	(19, '公文', 1),
	(20, '车辆', 1),
	(21, '会议', 1),
	(22, '公文', 1),
	(23, '车辆', 1),
	(24, '会议', 1);
/*!40000 ALTER TABLE `form` ENABLE KEYS */;

-- 导出  表 oa.group 结构
CREATE TABLE IF NOT EXISTS `group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Type` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.group 的数据：~3 rows (大约)
DELETE FROM `group`;
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` (`ID`, `Name`, `Type`) VALUES
	(1, '组1', b'01'),
	(2, '组2', b'01'),
	(3, '组3', b'00');
/*!40000 ALTER TABLE `group` ENABLE KEYS */;

-- 导出  表 oa.missive 结构
CREATE TABLE IF NOT EXISTS `missive` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` varchar(255) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  `Confidential` bit(3) NOT NULL DEFAULT b'0',
  `UserID` int(11) NOT NULL DEFAULT '0',
  `CategoryID` int(11) NOT NULL DEFAULT '0',
  `EmergencyEnum` int(3) NOT NULL DEFAULT '0',
  `PrintTime` datetime DEFAULT NULL,
  `EffectTime` datetime DEFAULT NULL,
  `KeyWords` varchar(1023) DEFAULT NULL,
  `BornOrganID` int(11) NOT NULL DEFAULT '0',
  `ToOrganID` int(11) NOT NULL DEFAULT '0',
  `NodeID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `CategoryID` (`CategoryID`),
  KEY `BornOrganID` (`BornOrganID`),
  KEY `ToOrganID` (`ToOrganID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `FK_missive_category` FOREIGN KEY (`CategoryID`) REFERENCES `category` (`ID`),
  CONSTRAINT `FK_missive_organization` FOREIGN KEY (`BornOrganID`) REFERENCES `organization` (`ID`),
  CONSTRAINT `FK_missive_organization_2` FOREIGN KEY (`ToOrganID`) REFERENCES `organization` (`ID`),
  CONSTRAINT `FK_missive_user` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.missive 的数据：~0 rows (大约)
DELETE FROM `missive`;
/*!40000 ALTER TABLE `missive` DISABLE KEYS */;
/*!40000 ALTER TABLE `missive` ENABLE KEYS */;

-- 导出  表 oa.organization 结构
CREATE TABLE IF NOT EXISTS `organization` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(255) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.organization 的数据：~3 rows (大约)
DELETE FROM `organization`;
/*!40000 ALTER TABLE `organization` DISABLE KEYS */;
INSERT INTO `organization` (`ID`, `ParentID`, `Name`, `Sort`) VALUES
	(1, 0, '办公室', 0),
	(2, 0, '规划耕保科', 0),
	(3, 0, '利用科', 0);
/*!40000 ALTER TABLE `organization` ENABLE KEYS */;

-- 导出  表 oa.subscription 结构
CREATE TABLE IF NOT EXISTS `subscription` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.subscription 的数据：~3 rows (大约)
DELETE FROM `subscription`;
/*!40000 ALTER TABLE `subscription` DISABLE KEYS */;
INSERT INTO `subscription` (`ID`, `Name`, `Deleted`) VALUES
	(1, '订阅1', b'00'),
	(2, '订阅2', b'00'),
	(3, '订阅3', b'00');
/*!40000 ALTER TABLE `subscription` ENABLE KEYS */;

-- 导出  表 oa.task 结构
CREATE TABLE IF NOT EXISTS `task` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(1023) NOT NULL,
  `Content` varchar(1023) NOT NULL,
  `ScheduledTime` datetime DEFAULT NULL,
  `CompletedTime` datetime DEFAULT NULL,
  `CreatorID` int(11) NOT NULL DEFAULT '0',
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `CreatorID` (`CreatorID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.task 的数据：~0 rows (大约)
DELETE FROM `task`;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
/*!40000 ALTER TABLE `task` ENABLE KEYS */;

-- 导出  表 oa.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(1023) NOT NULL,
  `Role` bit(3) NOT NULL DEFAULT b'0',
  `DepartmentId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `DepartmentId` (`DepartmentId`),
  CONSTRAINT `FK_user_organization` FOREIGN KEY (`DepartmentId`) REFERENCES `organization` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user 的数据：~5 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `Username`, `Password`, `Name`, `Role`, `DepartmentId`) VALUES
	(1, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011', 1),
	(2, '汪建龙', 'e10adc3949ba59abbe56e057f20f883e', 'wjl', b'000', NULL),
	(3, '唐尧', 'e10adc3949ba59abbe56e057f20f883e', 'ty', b'000', NULL),
	(4, '赵斯思', 'e10adc3949ba59abbe56e057f20f883e', 'zss', b'000', NULL),
	(5, '郑良军', 'e10adc3949ba59abbe56e057f20f883e', 'zlj', b'000', NULL);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  表 oa.user_form 结构
CREATE TABLE IF NOT EXISTS `user_form` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL DEFAULT '0',
  `FormID` int(11) NOT NULL DEFAULT '0',
  `InfoID` int(11) NOT NULL DEFAULT '0',
  `State` bit(5) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `FormID` (`FormID`),
  KEY `InfoID` (`InfoID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user_form 的数据：~0 rows (大约)
DELETE FROM `user_form`;
/*!40000 ALTER TABLE `user_form` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_form` ENABLE KEYS */;

-- 导出  表 oa.user_group 结构
CREATE TABLE IF NOT EXISTS `user_group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `GroupID` (`GroupID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user_group 的数据：~1 rows (大约)
DELETE FROM `user_group`;
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
INSERT INTO `user_group` (`ID`, `UserID`, `GroupID`) VALUES
	(1, 1, 1);
/*!40000 ALTER TABLE `user_group` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
