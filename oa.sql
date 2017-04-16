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
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.feed: ~53 rows (approximately)
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
	(20, 1, 2, 0, 5, 0, 1, b'000', '2017-03-23 21:08:49'),
	(21, 1, 2, 0, 5, 0, 1, b'000', '2017-03-24 17:05:28'),
	(22, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 11:40:22'),
	(23, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 11:48:45'),
	(24, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 11:52:48'),
	(25, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 11:58:07'),
	(26, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 11:58:25'),
	(27, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:03:12'),
	(28, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:11:10'),
	(29, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:17:39'),
	(30, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:25:19'),
	(31, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:30:01'),
	(32, 1, 2, 0, 5, 0, 1, b'000', '2017-04-11 12:30:20'),
	(33, 1, 3, 0, 5, 0, 0, b'000', '2017-04-12 14:33:44'),
	(34, 1, 3, 0, 5, 0, 1, b'000', '2017-04-12 14:33:52'),
	(35, 1, 3, 0, 5, 0, 1, b'000', '2017-04-12 14:34:07'),
	(36, 1, 3, 0, 5, 0, 1, b'000', '2017-04-12 14:34:28'),
	(37, 1, 4, 0, 5, 0, 0, b'000', '2017-04-12 16:25:25'),
	(38, 1, 4, 0, 5, 0, 1, b'000', '2017-04-12 16:26:03'),
	(39, 1, 4, 0, 5, 0, 1, b'000', '2017-04-12 16:26:10'),
	(40, 1, 4, 0, 5, 0, 1, b'000', '2017-04-13 14:30:17'),
	(41, 1, 5, 0, 5, 0, 0, b'000', '2017-04-13 16:43:53'),
	(42, 1, 5, 0, 5, 0, 1, b'000', '2017-04-13 16:44:48'),
	(43, 1, 5, 0, 5, 0, 1, b'000', '2017-04-13 16:45:42'),
	(44, 1, 5, 0, 5, 0, 1, b'000', '2017-04-13 18:47:34'),
	(45, 1, 6, 0, 5, 0, 0, b'000', '2017-04-14 18:24:23'),
	(46, 1, 7, 0, 5, 0, 0, b'000', '2017-04-14 18:33:13'),
	(47, 1, 8, 0, 5, 0, 0, b'000', '2017-04-15 20:16:05'),
	(48, 1, 8, 0, 5, 0, 1, b'000', '2017-04-15 20:16:56'),
	(49, 1, 7, 0, 5, 0, 1, b'000', '2017-04-16 14:31:13'),
	(50, 1, 7, 0, 5, 0, 1, b'000', '2017-04-16 14:32:43'),
	(51, 1, 7, 0, 5, 0, 1, b'000', '2017-04-16 14:34:32'),
	(52, 1, 7, 0, 5, 0, 1, b'000', '2017-04-16 16:00:26'),
	(53, 1, 7, 0, 5, 0, 1, b'000', '2017-04-16 16:00:44'),
	(54, 1, 1, 0, 5, 0, 0, b'000', '2017-04-16 16:17:03');
/*!40000 ALTER TABLE `feed` ENABLE KEYS */;

-- Dumping structure for table oa.file
DROP TABLE IF EXISTS `file`;
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.file: ~0 rows (approximately)
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
INSERT INTO `file` (`ID`, `FileName`, `SavePath`, `Size`, `CreateTime`, `UpdateTime`, `InfoId`) VALUES
	(1, '舟山市国土资源局定海分局办公自动化系统.docx', 'b46d8ec718b3fcbea53f5076d7b51f8e.docx', 2150132, '2017-04-16 16:16:39', NULL, 0),
	(2, '舟山市国土资源局定海分局办公自动化系统.docx.pdf', 'b46d8ec718b3fcbea53f5076d7b51f8e.docx.pdf', 2150132, '2017-04-16 16:16:39', NULL, 0),
	(3, '舟山市国土资源局定海分局发文拟稿纸.xlsx', 'ea52aa2166c37874a0fba77ec189b298.xlsx', 9680, '2017-04-16 16:16:59', NULL, 0);
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
	(1, '公文流程'),
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
  `Completed` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `InfoId` (`InfoId`),
  KEY `FormId` (`FormId`),
  KEY `FlowId` (`FlowId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_data` DISABLE KEYS */;
INSERT INTO `flow_data` (`ID`, `InfoId`, `FormId`, `FlowId`, `Completed`) VALUES
	(1, 1, 1, 1, b'1');
/*!40000 ALTER TABLE `flow_data` ENABLE KEYS */;

-- Dumping structure for table oa.flow_node
DROP TABLE IF EXISTS `flow_node`;
CREATE TABLE IF NOT EXISTS `flow_node` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FlowId` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(1023) NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  `GroupID` int(11) NOT NULL DEFAULT '0',
  `PrevId` int(11) NOT NULL DEFAULT '0',
  `DepartmentId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowId` (`FlowId`),
  KEY `UserId_GroupID_DepartmentId` (`UserId`,`GroupID`,`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_node: ~5 rows (approximately)
/*!40000 ALTER TABLE `flow_node` DISABLE KEYS */;
INSERT INTO `flow_node` (`ID`, `FlowId`, `Name`, `UserId`, `GroupID`, `PrevId`, `DepartmentId`) VALUES
	(1, 1, '拟稿人', 0, 1, 0, 2),
	(2, 1, '科室负责人审核', 0, 2, 1, 2),
	(3, 1, '办公室审核', 0, 3, 2, 1),
	(4, 1, '分管领导审核', 0, 4, 3, 0),
	(5, 1, '签发', 4, 0, 4, 0);
/*!40000 ALTER TABLE `flow_node` ENABLE KEYS */;

-- Dumping structure for table oa.flow_node_data
DROP TABLE IF EXISTS `flow_node_data`;
CREATE TABLE IF NOT EXISTS `flow_node_data` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FlowDataId` int(11) NOT NULL DEFAULT '0',
  `ParentId` int(11) NOT NULL DEFAULT '0',
  `FlowNodeName` varchar(50) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  `Signature` varchar(50) DEFAULT '0',
  `Department` varchar(50) DEFAULT '0',
  `UpdateTime` datetime DEFAULT NULL,
  `Result` bit(1) DEFAULT NULL,
  `Content` text,
  `FlowNodeId` int(11) NOT NULL DEFAULT '0',
  `Step` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowDataId` (`FlowDataId`),
  KEY `ParentId` (`ParentId`),
  KEY `UserId` (`UserId`),
  KEY `FlowNodeId` (`FlowNodeId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_node_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_node_data` DISABLE KEYS */;
INSERT INTO `flow_node_data` (`ID`, `FlowDataId`, `ParentId`, `FlowNodeName`, `CreateTime`, `UserId`, `Signature`, `Department`, `UpdateTime`, `Result`, `Content`, `FlowNodeId`, `Step`) VALUES
	(1, 1, 0, '拟稿人', '2017-04-16 16:17:46', 5, '郑良军', '规划耕保科', '2017-04-16 16:17:46', b'1', '拟稿人意见', 1, 0),
	(2, 1, 0, '科室负责人审核', '2017-04-16 16:17:46', 3, '唐尧', '规划耕保科', '2017-04-16 16:28:45', b'0', 'word文档内容不完整，请重新填写！', 2, 0),
	(4, 1, 0, '拟稿人', '2017-04-16 16:28:45', 5, '郑良军', '规划耕保科', '2017-04-16 16:29:29', b'1', '已修改，请审核。', 1, 0),
	(5, 1, 0, '科室负责人审核', '2017-04-16 16:29:29', 3, '唐尧', '规划耕保科', '2017-04-16 16:30:07', b'1', '内容完整，通过审核', 2, 0),
	(6, 1, 0, '办公室审核', '2017-04-16 16:30:07', 4, '赵斯思', '办公室', '2017-04-16 16:31:55', b'1', '已阅', 3, 0),
	(7, 1, 0, '分管领导审核', '2017-04-16 16:31:55', 7, '分管领导', '办公室', '2017-04-16 16:32:17', b'1', '已阅!', 4, 0),
	(8, 1, 0, '签发', '2017-04-16 16:32:17', 4, '赵斯思', '办公室', '2017-04-16 16:32:37', b'1', 'OVER', 5, 0);
/*!40000 ALTER TABLE `flow_node_data` ENABLE KEYS */;

-- Dumping structure for table oa.form
DROP TABLE IF EXISTS `form`;
CREATE TABLE IF NOT EXISTS `form` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `FlowID` int(11) NOT NULL DEFAULT '0',
  `DataType` varchar(50) DEFAULT NULL,
  `FormType` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowID` (`FlowID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form: ~1 rows (approximately)
/*!40000 ALTER TABLE `form` DISABLE KEYS */;
INSERT INTO `form` (`ID`, `Name`, `FlowID`, `DataType`, `FormType`) VALUES
	(1, '公文', 1, 'Missive', 1);
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
  `FlowStep` varchar(50) DEFAULT NULL,
  `Data` json DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `CreateTime` (`CreateTime`),
  KEY `PostUserId` (`PostUserId`),
  KEY `Title` (`Title`,`Keywords`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form_info: ~0 rows (approximately)
/*!40000 ALTER TABLE `form_info` DISABLE KEYS */;
INSERT INTO `form_info` (`ID`, `FormId`, `Title`, `Keywords`, `CategoryId`, `CreateTime`, `UpdateTime`, `Deleted`, `PostUserId`, `FlowDataId`, `FlowStep`, `Data`) VALUES
	(1, 1, '测试一片测试专用的公文拟稿的标题', '20170416,这里是主题词的内容', 0, '2017-04-16 16:17:03', '2017-04-16 16:32:37', b'0', 5, 1, '完结', '{"Pdf": {"ID": 2, "$id": "1", "Size": 2150132, "InfoId": 0, "FileName": "舟山市国土资源局定海分局办公自动化系统.docx.pdf", "SavePath": "b46d8ec718b3fcbea53f5076d7b51f8e.docx.pdf", "CreateTime": "2017-04-16T16:16:39.4904364+08:00", "UpdateTime": null, "DisplaySize": "2M"}, "ZRR": "郑良军", "Word": {"ID": 1, "$id": "1", "Size": 2150132, "InfoId": 0, "FileName": "舟山市国土资源局定海分局办公自动化系统.docx", "SavePath": "b46d8ec718b3fcbea53f5076d7b51f8e.docx", "CreateTime": "2017-04-16T16:16:39.1281803+08:00", "UpdateTime": null, "DisplaySize": "2M"}, "ZWGK": 1, "CS_JG": "测试抄送机关", "FW_RQ": "2017-04-16T16:16:58+08:00", "GW_MJ": 2, "GW_WH": "20170416", "QX_RQ": "2017-04-30", "WJ_BT": "测试一片测试专用的公文拟稿的标题", "ZS_JG": "测试主送机关", "Excels": [{"ID": 3, "$id": "1", "Size": 9680, "InfoId": 0, "FileName": "舟山市国土资源局定海分局发文拟稿纸.xlsx", "SavePath": "ea52aa2166c37874a0fba77ec189b298.xlsx", "CreateTime": "2017-04-16T16:16:58.7657138+08:00", "UpdateTime": null, "DisplaySize": "9K"}], "GW_ZTC": "这里是主题词的内容", "HLW_FB": true}');
/*!40000 ALTER TABLE `form_info` ENABLE KEYS */;

-- Dumping structure for table oa.group
DROP TABLE IF EXISTS `group`;
CREATE TABLE IF NOT EXISTS `group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Type` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.group: ~3 rows (approximately)
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` (`ID`, `Name`, `Type`) VALUES
	(1, '拟稿人', b'00'),
	(2, '负责人', b'00'),
	(3, '审批人', b'00'),
	(4, '分管领导', b'00');
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
  `UserName` varchar(50) NOT NULL,
  `RealName` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Role` int(11) NOT NULL DEFAULT '0',
  `DepartmentId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `DepartmentId` (`DepartmentId`),
  KEY `UserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user: ~6 rows (approximately)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `UserName`, `RealName`, `Password`, `Role`, `DepartmentId`) VALUES
	(1, 'Admin', '管理员', '202cb962ac59075b964b07152d234b70', 3, 1),
	(2, 'wjl', '汪建龙', '202cb962ac59075b964b07152d234b70', 0, 3),
	(3, 'ty', '唐尧', '202cb962ac59075b964b07152d234b70', 0, 2),
	(4, 'zss', '赵斯思', '202cb962ac59075b964b07152d234b70', 0, 1),
	(5, 'zlj', '郑良军', '202cb962ac59075b964b07152d234b70', 0, 2),
	(7, 'fgld', '分管领导', '202cb962ac59075b964b07152d234b70', 0, 1);
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
  KEY `InfoID` (`InfoID`),
  KEY `FlowNodeDataID` (`FlowNodeDataID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_form_info: ~0 rows (approximately)
/*!40000 ALTER TABLE `user_form_info` DISABLE KEYS */;
INSERT INTO `user_form_info` (`ID`, `UserID`, `FormID`, `InfoID`, `Status`, `FlowNodeDataID`) VALUES
	(2, 3, 1, 1, 3, 5),
	(3, 5, 1, 1, 3, 4),
	(4, 4, 1, 1, 3, 8),
	(5, 7, 1, 1, 3, 7);
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_group: ~4 rows (approximately)
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
INSERT INTO `user_group` (`ID`, `UserID`, `GroupID`) VALUES
	(1, 1, 1),
	(4, 5, 1),
	(8, 4, 3),
	(9, 3, 2),
	(10, 7, 4);
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
