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
  `Action` int(11) NOT NULL DEFAULT '0',
  `Deleted` bit(3) NOT NULL DEFAULT b'0',
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `InfoId` (`InfoId`),
  KEY `ToUserId` (`ToUserId`),
  KEY `FromUserId` (`FromUserId`),
  KEY `Type` (`Type`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.feed 的数据：~20 rows (大约)
DELETE FROM `feed`;
/*!40000 ALTER TABLE `feed` DISABLE KEYS */;
INSERT INTO `feed` (`ID`, `FormId`, `InfoId`, `Type`, `FromUserId`, `ToUserId`, `Action`, `Deleted`, `CreateTime`) VALUES
	(1, 1, 2, 0, 5, 0, 0, b'000', '2017-03-22 19:41:58'),
	(2, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 19:54:59'),
	(3, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:06:09'),
	(4, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:06:19'),
	(5, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:06:31'),
	(6, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:07:21'),
	(7, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:34:42'),
	(8, 1, 2, 0, 5, 0, 1, b'000', '2017-03-22 20:37:04'),
	(9, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 07:54:37'),
	(10, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 07:55:45'),
	(11, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 09:32:27'),
	(12, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 09:52:39'),
	(13, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 10:03:29'),
	(14, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 10:03:34'),
	(15, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 10:03:53'),
	(16, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 10:06:30'),
	(17, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 10:06:36'),
	(18, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 18:29:58'),
	(19, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 18:30:08'),
	(20, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 21:08:49');
/*!40000 ALTER TABLE `feed` ENABLE KEYS */;

-- 导出  表 oa.file 结构
CREATE TABLE IF NOT EXISTS `file` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FileName` text NOT NULL,
  `SavePath` text NOT NULL,
  `Size` bigint(20) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `InfoId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `InfoTID` (`InfoId`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.file 的数据：~15 rows (大约)
DELETE FROM `file`;
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
INSERT INTO `file` (`ID`, `FileName`, `SavePath`, `Size`, `CreateTime`, `UpdateTime`, `InfoId`) VALUES
	(1, '8f19f87d7d0dd045a114471980a7551d.csv', 'upload_files/8f19f87d7d0dd045a114471980a7551d.csv', 133808, '2017-03-23 09:29:20', NULL, 2),
	(2, '8f19f87d7d0dd045a114471980a7551d.csv', 'upload_files/8f19f87d7d0dd045a114471980a7551d.csv', 133808, '2017-03-23 09:29:48', NULL, 2),
	(3, 'App_Pai_Photo.xml', 'upload_files/e5d7139c965260eb0344385536b1076d.xml', 365634, '2017-03-23 09:52:37', '2017-03-23 09:52:37', 2),
	(4, '1b855ba8b49acf5627d5994ca1ba5af8.sql', 'upload_files/1b855ba8b49acf5627d5994ca1ba5af8.sql', 1216, '2017-03-23 09:35:06', NULL, 2),
	(5, 'App_Pai_Photo.csv', 'upload_files/8f19f87d7d0dd045a114471980a7551d.csv', 133808, '2017-03-23 18:21:46', NULL, 0),
	(6, 'App_Pai_Photo.csv', 'upload_files/8f19f87d7d0dd045a114471980a7551d.csv', 133808, '2017-03-23 18:25:49', NULL, 0),
	(7, 'Admin.sql', 'upload_files/1b855ba8b49acf5627d5994ca1ba5af8.sql', 1216, '2017-03-23 18:26:44', NULL, 0),
	(8, 'Admin.sql', 'upload_files/1b855ba8b49acf5627d5994ca1ba5af8.sql', 1216, '2017-03-23 18:28:12', NULL, 2),
	(10, 'App_Pai_Photo.csv', 'upload_files/8f19f87d7d0dd045a114471980a7551d.csv', 133808, '2017-03-23 18:34:50', NULL, 2),
	(11, 'mangxuedu_mysql.sql', 'upload_files/701bfa9f78ff459a21dadc3140a1a2f5.sql', 38764, '2017-03-23 18:35:17', NULL, 2),
	(12, 'App_Pai_Photo.sql', 'upload_files/8af9d855127475d24d1ab3d5ca93a951.sql', 183292, '2017-03-23 18:36:22', NULL, 2),
	(13, 'semester_date.txt', 'upload_files/98c68cbd8c76513ef48a075dd14ea775.txt', 2845, '2017-03-23 18:37:49', NULL, 2),
	(14, 'semester_date.txt', 'upload_files/98c68cbd8c76513ef48a075dd14ea775.txt', 2845, '2017-03-23 18:40:11', NULL, 2),
	(16, 'Admin.sql', 'upload_files/1b855ba8b49acf5627d5994ca1ba5af8.sql', 1216, '2017-03-23 21:01:03', NULL, 2),
	(17, 'App_Pai_Photo.sql', 'upload_files/8af9d855127475d24d1ab3d5ca93a951.sql', 183292, '2017-03-23 21:08:39', NULL, 2);
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
  `Step` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowId` (`FlowId`),
  KEY `UserId_GroupID_DepartmentId` (`UserId`,`GroupID`,`DepartmentId`),
  KEY `Step` (`Step`)
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
  `Step` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowDataId` (`FlowDataId`),
  KEY `ParentId` (`ParentId`),
  KEY `UserId` (`UserId`),
  KEY `DepartmentId` (`DepartmentId`),
  KEY `FlowNodeId` (`FlowNodeId`)
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
  `DataType` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowID` (`FlowID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.form 的数据：~1 rows (大约)
DELETE FROM `form`;
/*!40000 ALTER TABLE `form` DISABLE KEYS */;
INSERT INTO `form` (`ID`, `Name`, `FlowID`, `DataType`) VALUES
	(1, '公文', 0, 'Missive');
/*!40000 ALTER TABLE `form` ENABLE KEYS */;

-- 导出  表 oa.form_info 结构
CREATE TABLE IF NOT EXISTS `form_info` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FormId` int(11) NOT NULL,
  `Title` varchar(128) DEFAULT NULL,
  `Keywords` varchar(128) DEFAULT NULL,
  `CategoryId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Deleted` bit(1) NOT NULL,
  `PostUserId` int(11) NOT NULL,
  `FlowDataId` int(11) NOT NULL,
  `Data` text,
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `CreateTime` (`CreateTime`),
  KEY `PostUserId` (`PostUserId`),
  KEY `Title` (`Title`,`Keywords`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.form_info 的数据：~2 rows (大约)
DELETE FROM `form_info`;
/*!40000 ALTER TABLE `form_info` DISABLE KEYS */;
INSERT INTO `form_info` (`ID`, `FormId`, `Title`, `Keywords`, `CategoryId`, `CreateTime`, `UpdateTime`, `Deleted`, `PostUserId`, `FlowDataId`, `Data`) VALUES
	(1, 1, 'asdf', 'asdfasdf,', 0, '2017-03-21 19:45:24', '2017-03-21 19:45:27', b'0', 0, 0, NULL),
	(2, 1, '1111111111111111111111111111111111111', '111,1111', 0, '2017-03-22 19:41:55', '2017-03-23 21:08:49', b'0', 5, 0, '{"ZRR": "aaasd", "Word": {"ID": 3, "$id": "1", "Size": 365634, "InfoId": 2, "FileName": "App_Pai_Photo.xml", "SavePath": "upload_files/e5d7139c965260eb0344385536b1076d.xml", "CreateTime": "2017-03-23T09:52:37.3913505+08:00", "UpdateTime": "2017-03-23T09:52:37.3913505+08:00", "DisplaySize": "357K"}, "ZWGK": 2, "CS_JG": "222222", "FW_RQ": "2017-03-22T19:38:59+08:00", "GW_MJ": 3, "GW_WH": "111", "QX_RQ": "2017-03-16", "WJ_BT": "1111111111111111111111111111111111111", "ZS_JG": "111211", "Excels": [{"ID": 9, "$id": "1", "Size": 38764, "InfoId": 2, "FileName": "mangxuedu_mysql.sql", "SavePath": "upload_files/701bfa9f78ff459a21dadc3140a1a2f5.sql", "CreateTime": "2017-03-23T18:30:02.2926987+08:00", "UpdateTime": null, "DisplaySize": "37K"}], "GW_ZTC": "1111", "SF_FB_WWW": true}');
/*!40000 ALTER TABLE `form_info` ENABLE KEYS */;

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

-- 导出  表 oa.holiday 结构
CREATE TABLE IF NOT EXISTS `holiday` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.holiday 的数据：~0 rows (大约)
DELETE FROM `holiday`;
/*!40000 ALTER TABLE `holiday` DISABLE KEYS */;
/*!40000 ALTER TABLE `holiday` ENABLE KEYS */;

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

-- 导出  表 oa.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(1023) NOT NULL,
  `Role` bit(3) NOT NULL DEFAULT b'0',
  `DepartmentId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `DepartmentId` (`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user 的数据：~6 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `Username`, `Password`, `Name`, `Role`, `DepartmentId`) VALUES
	(1, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011', 1),
	(2, '汪建龙', 'e10adc3949ba59abbe56e057f20f883e', 'wjl', b'000', 3),
	(3, '唐尧', 'e10adc3949ba59abbe56e057f20f883e', 'ty', b'000', 1),
	(4, '赵斯思', 'e10adc3949ba59abbe56e057f20f883e', 'zss', b'000', 1),
	(5, '郑良军', 'e10adc3949ba59abbe56e057f20f883e', 'zlj', b'000', 2),
	(6, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  表 oa.user_form_info 结构
CREATE TABLE IF NOT EXISTS `user_form_info` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL DEFAULT '0',
  `FormID` int(11) NOT NULL DEFAULT '0',
  `InfoID` int(11) NOT NULL DEFAULT '0',
  `Status` int(11) NOT NULL DEFAULT '0',
  `FlowNodeDataID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `FormID` (`FormID`),
  KEY `InfoID` (`InfoID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user_form_info 的数据：~2 rows (大约)
DELETE FROM `user_form_info`;
/*!40000 ALTER TABLE `user_form_info` DISABLE KEYS */;
INSERT INTO `user_form_info` (`ID`, `UserID`, `FormID`, `InfoID`, `Status`, `FlowNodeDataID`) VALUES
	(1, 5, 1, 1, 0, 0),
	(3, 5, 0, 2, 0, 0);
/*!40000 ALTER TABLE `user_form_info` ENABLE KEYS */;

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

-- 导出  表 oa.user_right 结构
CREATE TABLE IF NOT EXISTS `user_right` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `GroupId` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user_right 的数据：~0 rows (大约)
DELETE FROM `user_right`;
/*!40000 ALTER TABLE `user_right` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_right` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
