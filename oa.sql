-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.17-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping structure for table oa.category
DROP TABLE IF EXISTS `category`;
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

-- Dumping data for table oa.category: ~2 rows (approximately)
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`ID`, `Name`, `FormID`, `Sort`, `Deleted`, `ParentID`, `Type`) VALUES
	(1, '种类1', 1, 0, b'00', 0, 1),
	(2, '种类2', 1, 0, b'00', 0, 1);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;

-- Dumping structure for table oa.diary
DROP TABLE IF EXISTS `diary`;
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

-- Dumping data for table oa.diary: ~0 rows (approximately)
/*!40000 ALTER TABLE `diary` DISABLE KEYS */;
/*!40000 ALTER TABLE `diary` ENABLE KEYS */;

-- Dumping structure for table oa.feed
DROP TABLE IF EXISTS `feed`;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.feed: ~0 rows (approximately)
/*!40000 ALTER TABLE `feed` DISABLE KEYS */;
/*!40000 ALTER TABLE `feed` ENABLE KEYS */;

-- Dumping structure for table oa.file
DROP TABLE IF EXISTS `file`;
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

-- Dumping data for table oa.file: ~0 rows (approximately)
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
/*!40000 ALTER TABLE `file` ENABLE KEYS */;

-- Dumping structure for table oa.flow
DROP TABLE IF EXISTS `flow`;
CREATE TABLE IF NOT EXISTS `flow` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow: ~3 rows (approximately)
/*!40000 ALTER TABLE `flow` DISABLE KEYS */;
INSERT INTO `flow` (`ID`, `Name`) VALUES
	(1, '模板1'),
	(2, '模板2'),
	(3, '模板3');
/*!40000 ALTER TABLE `flow` ENABLE KEYS */;

-- Dumping structure for table oa.flow_data
DROP TABLE IF EXISTS `flow_data`;
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

-- Dumping data for table oa.flow_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_data` ENABLE KEYS */;

-- Dumping structure for table oa.flow_node
DROP TABLE IF EXISTS `flow_node`;
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

-- Dumping data for table oa.flow_node: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_node` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_node` ENABLE KEYS */;

-- Dumping structure for table oa.flow_node_data
DROP TABLE IF EXISTS `flow_node_data`;
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

-- Dumping data for table oa.flow_node_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_node_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `flow_node_data` ENABLE KEYS */;

-- Dumping structure for table oa.form
DROP TABLE IF EXISTS `form`;
CREATE TABLE IF NOT EXISTS `form` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `FlowID` int(11) NOT NULL DEFAULT '0',
  `DataType` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowID` (`FlowID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form: ~1 rows (approximately)
/*!40000 ALTER TABLE `form` DISABLE KEYS */;
INSERT INTO `form` (`ID`, `Name`, `FlowID`, `DataType`) VALUES
	(1, '公文', 0, 'Missive');
/*!40000 ALTER TABLE `form` ENABLE KEYS */;

-- Dumping structure for table oa.form_info
DROP TABLE IF EXISTS `form_info`;
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
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `CreateTime` (`CreateTime`),
  KEY `PostUserId` (`PostUserId`),
  KEY `Title` (`Title`,`Keywords`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form_info: ~1 rows (approximately)
/*!40000 ALTER TABLE `form_info` DISABLE KEYS */;
INSERT INTO `form_info` (`ID`, `FormId`, `Title`, `Keywords`, `CategoryId`, `CreateTime`, `UpdateTime`, `Deleted`, `PostUserId`, `FlowDataId`) VALUES
	(1, 1, 'asdf', 'asdfasdf,', 0, '2017-03-21 19:45:24', '2017-03-21 19:45:27', b'0', 0, 0);
/*!40000 ALTER TABLE `form_info` ENABLE KEYS */;

-- Dumping structure for table oa.form_info_data
DROP TABLE IF EXISTS `form_info_data`;
CREATE TABLE IF NOT EXISTS `form_info_data` (
  `InfoID` int(11) NOT NULL,
  `Json` text NOT NULL,
  PRIMARY KEY (`InfoID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form_info_data: ~1 rows (approximately)
/*!40000 ALTER TABLE `form_info_data` DISABLE KEYS */;
INSERT INTO `form_info_data` (`InfoID`, `Json`) VALUES
	(1, '{"FormId":"1","GW_WH":"asdfasdf","FW_RQ":"2017-03-21T19:21:39+08:00","WJ_BT":"asdf","GW_ZTC":"asdf","ZWGK":"2","ZS_JG":"asdf","CS_JG":"asdf","SF_FB_WWW":"true","GW_MJ":"1","ZRR":"asdfasdf","QX_RQ":"2017-03-21"}');
/*!40000 ALTER TABLE `form_info_data` ENABLE KEYS */;

-- Dumping structure for table oa.group
DROP TABLE IF EXISTS `group`;
CREATE TABLE IF NOT EXISTS `group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Type` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.group: ~3 rows (approximately)
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` (`ID`, `Name`, `Type`) VALUES
	(1, '组1', b'01'),
	(2, '组2', b'01'),
	(3, '组3', b'00');
/*!40000 ALTER TABLE `group` ENABLE KEYS */;

-- Dumping structure for table oa.organization
DROP TABLE IF EXISTS `organization`;
CREATE TABLE IF NOT EXISTS `organization` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(255) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.organization: ~3 rows (approximately)
/*!40000 ALTER TABLE `organization` DISABLE KEYS */;
INSERT INTO `organization` (`ID`, `ParentID`, `Name`, `Sort`) VALUES
	(1, 0, '办公室', 0),
	(2, 0, '规划耕保科', 0),
	(3, 0, '利用科', 0);
/*!40000 ALTER TABLE `organization` ENABLE KEYS */;

-- Dumping structure for table oa.subscription
DROP TABLE IF EXISTS `subscription`;
CREATE TABLE IF NOT EXISTS `subscription` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.subscription: ~3 rows (approximately)
/*!40000 ALTER TABLE `subscription` DISABLE KEYS */;
INSERT INTO `subscription` (`ID`, `Name`, `Deleted`) VALUES
	(1, '订阅1', b'00'),
	(2, '订阅2', b'00'),
	(3, '订阅3', b'00');
/*!40000 ALTER TABLE `subscription` ENABLE KEYS */;

-- Dumping structure for table oa.user
DROP TABLE IF EXISTS `user`;
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

-- Dumping data for table oa.user: ~6 rows (approximately)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `Username`, `Password`, `Name`, `Role`, `DepartmentId`) VALUES
	(1, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011', 1),
	(2, '汪建龙', 'e10adc3949ba59abbe56e057f20f883e', 'wjl', b'000', 3),
	(3, '唐尧', 'e10adc3949ba59abbe56e057f20f883e', 'ty', b'000', 1),
	(4, '赵斯思', 'e10adc3949ba59abbe56e057f20f883e', 'zss', b'000', 1),
	(5, '郑良军', 'e10adc3949ba59abbe56e057f20f883e', 'zlj', b'000', 2),
	(6, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Dumping structure for table oa.user_form_info
DROP TABLE IF EXISTS `user_form_info`;
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_form_info: ~1 rows (approximately)
/*!40000 ALTER TABLE `user_form_info` DISABLE KEYS */;
INSERT INTO `user_form_info` (`ID`, `UserID`, `FormID`, `InfoID`, `Status`, `FlowNodeDataID`) VALUES
	(1, 5, 1, 1, 0, 0);
/*!40000 ALTER TABLE `user_form_info` ENABLE KEYS */;

-- Dumping structure for table oa.user_group
DROP TABLE IF EXISTS `user_group`;
CREATE TABLE IF NOT EXISTS `user_group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `GroupID` (`GroupID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_group: ~0 rows (approximately)
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
INSERT INTO `user_group` (`ID`, `UserID`, `GroupID`) VALUES
	(1, 1, 1);
/*!40000 ALTER TABLE `user_group` ENABLE KEYS */;

-- Dumping structure for table oa.user_right
DROP TABLE IF EXISTS `user_right`;
CREATE TABLE IF NOT EXISTS `user_right` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `GroupId` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_right: ~0 rows (approximately)
/*!40000 ALTER TABLE `user_right` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_right` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
