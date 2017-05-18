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

-- Dumping structure for table oa.car
DROP TABLE IF EXISTS `car`;
CREATE TABLE IF NOT EXISTS `car` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Number` varchar(50) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `Status` int(11) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `PhotoId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.car: ~4 rows (approximately)
/*!40000 ALTER TABLE `car` DISABLE KEYS */;
INSERT INTO `car` (`ID`, `Name`, `Number`, `UpdateTime`, `Type`, `Status`, `Deleted`, `PhotoId`) VALUES
	(2, '浙L50050', '浙L50050', NULL, 2, 0, b'0', 0),
	(3, '浙L31027', '浙L31027', NULL, 1, 0, b'0', 0),
	(4, '浙L20113', '浙L20113', NULL, 1, 0, b'0', 0),
	(5, '不动产', '浙L09756', NULL, 2, 0, b'0', 0);
/*!40000 ALTER TABLE `car` ENABLE KEYS */;

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
  `InfoId` int(11) NOT NULL,
  `FormId` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL,
  `Action` int(11) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Title` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `InfoId` (`InfoId`),
  KEY `ToUserId` (`ToUserId`),
  KEY `FromUserId` (`FromUserId`),
  KEY `Type` (`Type`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.feed: ~30 rows (approximately)
/*!40000 ALTER TABLE `feed` DISABLE KEYS */;
INSERT INTO `feed` (`ID`, `InfoId`, `FormId`, `Type`, `FromUserId`, `ToUserId`, `Action`, `Deleted`, `CreateTime`, `UpdateTime`, `Description`, `Title`) VALUES
	(1, 1, 1, 0, 5, 0, 0, b'0', '2017-05-17 21:35:05', NULL, NULL, '111111111111111'),
	(2, 1, 0, 0, 5, 3, 5, b'0', '2017-05-17 21:35:19', NULL, NULL, NULL),
	(3, 1, 0, 0, 5, 2, 5, b'0', '2017-05-17 21:35:19', NULL, NULL, NULL),
	(4, 2, 2, 0, 3, 0, 0, b'0', '2017-05-18 10:45:05', NULL, NULL, '11111111111111111111111111111111111'),
	(5, 2, 2, 0, 3, 14, 5, b'0', '2017-05-18 10:49:33', NULL, NULL, NULL),
	(6, 2, 0, 0, 14, 5, 5, b'0', '2017-05-18 10:55:56', NULL, NULL, NULL),
	(7, 2, 0, 0, 14, 3, 5, b'0', '2017-05-18 10:55:56', NULL, NULL, NULL),
	(8, 2, 0, 0, 14, 2, 5, b'0', '2017-05-18 10:55:56', NULL, NULL, NULL),
	(9, 1, 1, 0, 5, 0, 0, b'0', '2017-05-18 11:04:30', NULL, '这是发文拟稿，测试专用的', '这是发文拟稿，测试专用的'),
	(10, 1, 1, 0, 5, 0, 2, b'0', '2017-05-18 11:04:38', NULL, '这是发文拟稿，测试专用的', '这是发文拟稿，测试专用的'),
	(11, 2, 2, 0, 5, 0, 0, b'0', '2017-05-18 11:05:36', NULL, '水岸东方', '这是收文测试专用的第一季度数据册子定稿'),
	(12, 2, 2, 0, 5, 14, 5, b'0', '2017-05-18 11:05:50', NULL, NULL, NULL),
	(13, 1, 1, 0, 5, 3, 5, b'0', '2017-05-18 11:06:13', NULL, NULL, NULL),
	(14, 2, 0, 0, 14, 5, 5, b'0', '2017-05-18 11:06:36', NULL, NULL, NULL),
	(15, 2, 0, 0, 14, 3, 5, b'0', '2017-05-18 11:06:36', NULL, NULL, NULL),
	(16, 2, 0, 0, 14, 2, 5, b'0', '2017-05-18 11:06:36', NULL, NULL, NULL),
	(17, 1, 2, 0, 5, 0, 0, b'0', '2017-05-18 16:25:49', NULL, NULL, '11111111111111'),
	(18, 1, 2, 0, 5, 14, 5, b'0', '2017-05-18 16:26:33', NULL, NULL, NULL),
	(19, 1, 0, 0, 14, 5, 5, b'0', '2017-05-18 17:19:00', NULL, NULL, '11111111111111'),
	(20, 1, 0, 0, 14, 3, 5, b'0', '2017-05-18 17:19:00', NULL, NULL, '11111111111111'),
	(21, 1, 0, 0, 14, 2, 5, b'0', '2017-05-18 17:19:00', NULL, NULL, '11111111111111'),
	(22, 1, 0, 0, 14, 5, 5, b'0', '2017-05-18 17:31:50', NULL, NULL, '11111111111111'),
	(23, 1, 0, 0, 14, 3, 5, b'0', '2017-05-18 17:31:50', NULL, NULL, '11111111111111'),
	(24, 1, 0, 0, 14, 2, 5, b'0', '2017-05-18 17:31:50', NULL, NULL, '11111111111111'),
	(25, 1, 0, 0, 14, 5, 5, b'0', '2017-05-18 17:33:09', NULL, NULL, '11111111111111'),
	(26, 1, 0, 0, 14, 3, 5, b'0', '2017-05-18 17:33:09', NULL, NULL, '11111111111111'),
	(27, 1, 0, 0, 14, 2, 5, b'0', '2017-05-18 17:33:09', NULL, NULL, '11111111111111'),
	(28, 1, 0, 0, 14, 5, 5, b'0', '2017-05-18 17:40:09', NULL, NULL, '11111111111111'),
	(29, 1, 0, 0, 14, 3, 5, b'0', '2017-05-18 17:40:09', NULL, NULL, '11111111111111'),
	(30, 1, 0, 0, 14, 2, 5, b'0', '2017-05-18 17:40:09', NULL, NULL, '11111111111111'),
	(31, 2, 1, 0, 5, 0, 0, b'0', '2017-05-18 20:07:35', NULL, NULL, 'asdfasdfasdf'),
	(32, 2, 1, 0, 5, 0, 2, b'0', '2017-05-18 20:08:39', NULL, NULL, 'asdfasdfasdf');
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
  `InfoId` int(11) NOT NULL,
  `Inline` bit(1) NOT NULL,
  `ParentId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `InfoTID` (`InfoId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.file: ~1 rows (approximately)
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
INSERT INTO `file` (`ID`, `FileName`, `SavePath`, `Size`, `CreateTime`, `UpdateTime`, `InfoId`, `Inline`, `ParentId`) VALUES
	(1, 'OA系统流程设计.docx', '507f0e806bdc164d94864cc3ca2d4c5f.docx', 87165, '2017-05-18 16:25:40', NULL, 1, b'1', 0),
	(2, 'OA系统流程设计.docx.pdf', '507f0e806bdc164d94864cc3ca2d4c5f.docx.pdf', 87165, '2017-05-18 19:58:59', NULL, 1, b'0', 1),
	(3, 'OA系统流程设计.docx.pdf', '507f0e806bdc164d94864cc3ca2d4c5f.docx.pdf', 87165, '2017-05-18 19:59:20', NULL, 1, b'0', 1),
	(5, '2017第一季度数据册子定稿.doc', '7b27b8cc28ab14bdc03493f2f0c1aa61.doc', 240640, '2017-05-18 20:08:36', NULL, 2, b'1', 0),
	(7, '2017第一季度数据册子定稿.doc.pdf', '7b27b8cc28ab14bdc03493f2f0c1aa61.doc.pdf', 240640, '2017-05-18 20:09:27', NULL, 2, b'0', 5);
/*!40000 ALTER TABLE `file` ENABLE KEYS */;

-- Dumping structure for table oa.flow
DROP TABLE IF EXISTS `flow`;
CREATE TABLE IF NOT EXISTS `flow` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CanBack` bit(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow: ~4 rows (approximately)
/*!40000 ALTER TABLE `flow` DISABLE KEYS */;
INSERT INTO `flow` (`ID`, `Name`, `CanBack`) VALUES
	(1, '公文流程', b'1'),
	(2, '收文流程', b'0'),
	(3, '公车流程', b'0'),
	(4, '', b'0');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_data: ~1 rows (approximately)
/*!40000 ALTER TABLE `flow_data` DISABLE KEYS */;
INSERT INTO `flow_data` (`ID`, `InfoId`, `FormId`, `FlowId`, `Completed`) VALUES
	(1, 1, 2, 2, b'0'),
	(2, 2, 1, 1, b'0');
/*!40000 ALTER TABLE `flow_data` ENABLE KEYS */;

-- Dumping structure for table oa.flow_node
DROP TABLE IF EXISTS `flow_node`;
CREATE TABLE IF NOT EXISTS `flow_node` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FreeFlowId` int(11) NOT NULL DEFAULT '0',
  `FlowId` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(1023) NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  `GroupID` int(11) NOT NULL DEFAULT '0',
  `PrevId` int(11) NOT NULL DEFAULT '0',
  `DepartmentIds` varchar(50) DEFAULT '0',
  `JobTitleId` int(11) NOT NULL,
  `LimitMode` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FlowId` (`FlowId`),
  KEY `UserId_GroupID_DepartmentId` (`UserId`,`GroupID`,`DepartmentIds`),
  KEY `FreeFlowId` (`FreeFlowId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_node: ~7 rows (approximately)
/*!40000 ALTER TABLE `flow_node` DISABLE KEYS */;
INSERT INTO `flow_node` (`ID`, `FreeFlowId`, `FlowId`, `Name`, `UserId`, `GroupID`, `PrevId`, `DepartmentIds`, `JobTitleId`, `LimitMode`) VALUES
	(1, 1, 1, '拟稿人', 0, 1, 0, NULL, 4, 0),
	(2, 0, 1, '科室负责人审核', 0, 2, 1, '2', 0, 0),
	(3, 0, 1, '办公室审核', 0, 3, 2, '1', 0, 0),
	(4, 0, 1, '分管领导审核', 0, 4, 3, '0', 0, 0),
	(5, 0, 1, '签发', 4, 0, 4, '0', 0, 0),
	(6, 0, 2, '接收人', 0, 0, 0, '1', 0, 0),
	(7, 4, 2, '办公室', 0, 0, 6, '1', 3, 1);
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
  PRIMARY KEY (`ID`),
  KEY `FlowDataId` (`FlowDataId`),
  KEY `ParentId` (`ParentId`),
  KEY `UserId` (`UserId`),
  KEY `FlowNodeId` (`FlowNodeId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_node_data: ~2 rows (approximately)
/*!40000 ALTER TABLE `flow_node_data` DISABLE KEYS */;
INSERT INTO `flow_node_data` (`ID`, `FlowDataId`, `ParentId`, `FlowNodeName`, `CreateTime`, `UserId`, `Signature`, `Department`, `UpdateTime`, `Result`, `Content`, `FlowNodeId`) VALUES
	(1, 1, 0, '接收人', '2017-05-18 16:25:49', 5, '郑良军', '0', '2017-05-18 16:26:32', b'1', NULL, 6),
	(2, 1, 0, '办公室', '2017-05-18 16:26:32', 14, '陈敏', '0', NULL, NULL, NULL, 7),
	(3, 2, 0, '拟稿人', '2017-05-18 20:07:34', 5, '郑良军', '0', NULL, NULL, NULL, 1);
/*!40000 ALTER TABLE `flow_node_data` ENABLE KEYS */;

-- Dumping structure for table oa.form
DROP TABLE IF EXISTS `form`;
CREATE TABLE IF NOT EXISTS `form` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `FlowID` int(11) NOT NULL DEFAULT '0',
  `Ename` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form: ~4 rows (approximately)
/*!40000 ALTER TABLE `form` DISABLE KEYS */;
INSERT INTO `form` (`ID`, `Name`, `FlowID`, `Ename`) VALUES
	(1, '发文 ', 1, 'missive'),
	(2, '收文', 2, 'missive'),
	(3, '公车', 3, 'car'),
	(4, '任务', 4, 'task');
/*!40000 ALTER TABLE `form` ENABLE KEYS */;

-- Dumping structure for table oa.form_info
DROP TABLE IF EXISTS `form_info`;
CREATE TABLE IF NOT EXISTS `form_info` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FormId` int(11) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Deleted` bit(1) NOT NULL,
  `PostUserId` int(11) NOT NULL,
  `FlowDataId` int(11) NOT NULL,
  `FlowStep` varchar(50) DEFAULT NULL,
  `ExtendId` int(11) NOT NULL,
  `Title` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FormId` (`FormId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `CreateTime` (`CreateTime`),
  KEY `PostUserId` (`PostUserId`),
  KEY `ExtendId` (`ExtendId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form_info: ~1 rows (approximately)
/*!40000 ALTER TABLE `form_info` DISABLE KEYS */;
INSERT INTO `form_info` (`ID`, `FormId`, `CategoryId`, `CreateTime`, `UpdateTime`, `Deleted`, `PostUserId`, `FlowDataId`, `FlowStep`, `ExtendId`, `Title`) VALUES
	(1, 2, 0, '2017-05-18 16:25:49', '2017-05-18 16:25:49', b'0', 5, 1, '办公室', 0, '11111111111111'),
	(2, 1, 0, '2017-05-18 20:07:34', '2017-05-18 20:07:34', b'0', 5, 2, '拟稿人', 0, 'asdfasdfasdf');
/*!40000 ALTER TABLE `form_info` ENABLE KEYS */;

-- Dumping structure for table oa.freeflow
DROP TABLE IF EXISTS `freeflow`;
CREATE TABLE IF NOT EXISTS `freeflow` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LimitMode` int(11) NOT NULL,
  `DepartmentIds` varchar(50) DEFAULT NULL,
  `CrossDepartment` bit(1) NOT NULL,
  `CrossLevel` bit(1) NOT NULL,
  `CompleteUserDepartmentIds` varchar(50) DEFAULT NULL,
  `CompleteUserJobTitleIds` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.freeflow: ~4 rows (approximately)
/*!40000 ALTER TABLE `freeflow` DISABLE KEYS */;
INSERT INTO `freeflow` (`ID`, `LimitMode`, `DepartmentIds`, `CrossDepartment`, `CrossLevel`, `CompleteUserDepartmentIds`, `CompleteUserJobTitleIds`) VALUES
	(1, 1, '2,3', b'0', b'1', NULL, NULL),
	(2, 2, NULL, b'1', b'0', '3', '4'),
	(3, 2, NULL, b'1', b'0', '2', '4'),
	(4, 2, NULL, b'1', b'0', '2', NULL);
/*!40000 ALTER TABLE `freeflow` ENABLE KEYS */;

-- Dumping structure for table oa.freeflow_data
DROP TABLE IF EXISTS `freeflow_data`;
CREATE TABLE IF NOT EXISTS `freeflow_data` (
  `ID` int(11) NOT NULL,
  `Completed` bit(1) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `CompletedUserId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.freeflow_data: ~1 rows (approximately)
/*!40000 ALTER TABLE `freeflow_data` DISABLE KEYS */;
INSERT INTO `freeflow_data` (`ID`, `Completed`, `CreateTime`, `UpdateTime`, `CompletedUserId`) VALUES
	(2, b'1', '2017-05-18 17:40:09', '2017-05-18 18:21:20', 5);
/*!40000 ALTER TABLE `freeflow_data` ENABLE KEYS */;

-- Dumping structure for table oa.freeflow_nodedata
DROP TABLE IF EXISTS `freeflow_nodedata`;
CREATE TABLE IF NOT EXISTS `freeflow_nodedata` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FreeFlowDataId` int(11) NOT NULL,
  `ParentId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `DepartmentName` varchar(50) DEFAULT NULL,
  `Signature` varchar(50) DEFAULT NULL,
  `Content` varchar(512) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FreeFlowDataId` (`FreeFlowDataId`),
  KEY `UserId` (`UserId`),
  KEY `ParentId` (`ParentId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.freeflow_nodedata: ~3 rows (approximately)
/*!40000 ALTER TABLE `freeflow_nodedata` DISABLE KEYS */;
INSERT INTO `freeflow_nodedata` (`ID`, `FreeFlowDataId`, `ParentId`, `UserId`, `DepartmentName`, `Signature`, `Content`, `CreateTime`, `UpdateTime`) VALUES
	(1, 2, 0, 5, NULL, '郑良军', NULL, '2017-05-18 17:40:09', NULL),
	(2, 2, 0, 3, NULL, '唐尧', NULL, '2017-05-18 17:40:09', NULL),
	(3, 2, 0, 2, NULL, '汪建龙', NULL, '2017-05-18 17:40:09', NULL);
/*!40000 ALTER TABLE `freeflow_nodedata` ENABLE KEYS */;

-- Dumping structure for table oa.group
DROP TABLE IF EXISTS `group`;
CREATE TABLE IF NOT EXISTS `group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Type` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.group: ~5 rows (approximately)
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` (`ID`, `Name`, `Type`) VALUES
	(1, '经办人', 0),
	(2, '科室负责人', 0),
	(3, '办公室审批人', 0),
	(4, '分管领导', 0),
	(5, '局长', 0);
/*!40000 ALTER TABLE `group` ENABLE KEYS */;

-- Dumping structure for table oa.missive
DROP TABLE IF EXISTS `missive`;
CREATE TABLE IF NOT EXISTS `missive` (
  `ID` int(11) NOT NULL,
  `WJ_BT` varchar(50) DEFAULT NULL,
  `WH` varchar(50) DEFAULT NULL,
  `MJ` int(11) NOT NULL,
  `ZRR` varchar(50) DEFAULT NULL,
  `FW_RQ` datetime DEFAULT NULL,
  `SX_SJ` datetime DEFAULT NULL,
  `ZTC` varchar(50) DEFAULT NULL,
  `ZS_JG` varchar(50) DEFAULT NULL,
  `CS_JG` varchar(50) DEFAULT NULL,
  `HLW_FB` bit(1) NOT NULL,
  `QX_RQ` datetime DEFAULT NULL,
  `LY` varchar(255) DEFAULT NULL,
  `WordId` int(11) DEFAULT NULL,
  `ZWGK` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.missive: ~1 rows (approximately)
/*!40000 ALTER TABLE `missive` DISABLE KEYS */;
INSERT INTO `missive` (`ID`, `WJ_BT`, `WH`, `MJ`, `ZRR`, `FW_RQ`, `SX_SJ`, `ZTC`, `ZS_JG`, `CS_JG`, `HLW_FB`, `QX_RQ`, `LY`, `WordId`, `ZWGK`) VALUES
	(1, '11111111111111', '111111111111', 1, NULL, '2017-05-18 16:25:41', NULL, NULL, NULL, NULL, b'1', NULL, '111111111111111', 1, 1),
	(2, 'asdfasdfasdf', NULL, 0, NULL, '2017-05-18 20:07:29', NULL, NULL, NULL, NULL, b'0', NULL, NULL, 5, 1);
/*!40000 ALTER TABLE `missive` ENABLE KEYS */;

-- Dumping structure for table oa.organization
DROP TABLE IF EXISTS `organization`;
CREATE TABLE IF NOT EXISTS `organization` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(255) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.organization: ~12 rows (approximately)
/*!40000 ALTER TABLE `organization` DISABLE KEYS */;
INSERT INTO `organization` (`ID`, `ParentID`, `Name`, `Sort`) VALUES
	(1, 0, '办公室', 0),
	(2, 0, '规划耕保科', 0),
	(3, 0, '土地利用科', 0),
	(4, 0, '局领导', 0),
	(5, 0, '政策法规科', 0),
	(6, 0, '行政审批科', 0),
	(7, 0, '矿产管理科', 0),
	(8, 0, '征地管理所', 0),
	(9, 0, '监查大队', 0),
	(10, 0, '纪检监察室', 0),
	(11, 0, '驾驶班', 0),
	(12, 0, '中心所、资源所', 0);
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
  `JobTitleId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `DepartmentId` (`DepartmentId`),
  KEY `UserName` (`UserName`),
  KEY `JobTitleId` (`JobTitleId`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user: ~68 rows (approximately)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `UserName`, `RealName`, `Password`, `Role`, `DepartmentId`, `JobTitleId`) VALUES
	(1, 'Admin', '管理员', '202cb962ac59075b964b07152d234b70', 3, 1, 0),
	(2, 'wjl', '汪建龙', '202cb962ac59075b964b07152d234b70', 0, 3, 0),
	(3, 'ty', '唐尧', '202cb962ac59075b964b07152d234b70', 0, 2, 0),
	(4, 'zss', '赵斯思', '202cb962ac59075b964b07152d234b70', 0, 1, 0),
	(5, 'zlj', '郑良军', '202cb962ac59075b964b07152d234b70', 0, 2, 0),
	(7, 'fgld', '分管领导', '202cb962ac59075b964b07152d234b70', 0, 1, 0),
	(8, '陈彦伟', '陈彦玮', 'e10adc3949ba59abbe56e057f20f883e', 0, 0, 0),
	(9, '陈杰', '陈杰', 'e10adc3949ba59abbe56e057f20f883e', 0, 4, 1),
	(10, '王海光', '王海光', 'e10adc3949ba59abbe56e057f20f883e', 0, 0, 0),
	(11, '苗杰', '苗杰', 'e10adc3949ba59abbe56e057f20f883e', 0, 4, 2),
	(12, '胡锦江', '胡锦江', 'e10adc3949ba59abbe56e057f20f883e', 0, 4, 2),
	(13, '刘贤', '刘贤', 'e10adc3949ba59abbe56e057f20f883e', 0, 4, 2),
	(14, '陈敏', '陈敏', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 3),
	(15, '周波', '周波', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(16, '马莹', '马莹', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(17, '余伟', '余伟', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(18, '翁海萍', '翁海萍', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(19, '史琼', '史琼', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(20, '李云来', '李云来', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(21, '邬晓群', '邬晓群', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(22, '鲍如华', '鲍如华', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(23, '叶豪', '叶豪', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(24, '江建儿', '江建儿', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(25, '张娜儿', '张娜儿', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(26, '戴婕', '戴婕', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(27, '鲍建芬', '鲍建芬', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(28, '任红霞', '任红霞', 'e10adc3949ba59abbe56e057f20f883e', 0, 1, 4),
	(29, '张培珍', '张培珍', 'e10adc3949ba59abbe56e057f20f883e', 0, 5, 3),
	(30, '黄兴忠', '黄兴忠', 'e10adc3949ba59abbe56e057f20f883e', 0, 5, 3),
	(31, '曹智明', '曹智明', 'e10adc3949ba59abbe56e057f20f883e', 0, 5, 4),
	(32, '刘静艳', '刘静艳', 'e10adc3949ba59abbe56e057f20f883e', 0, 5, 4),
	(33, '陈宇', '陈宇', 'e10adc3949ba59abbe56e057f20f883e', 0, 2, 3),
	(34, '王晴晴', '王晴晴', 'e10adc3949ba59abbe56e057f20f883e', 0, 2, 3),
	(35, '陈家祖', '陈家祖', 'e10adc3949ba59abbe56e057f20f883e', 0, 2, 4),
	(36, '韩晨妤', '韩晨妤', 'e10adc3949ba59abbe56e057f20f883e', 0, 2, 4),
	(37, '李文杰', '李文杰', 'e10adc3949ba59abbe56e057f20f883e', 0, 2, 4),
	(38, '黄璐', '黄璐', 'e10adc3949ba59abbe56e057f20f883e', 0, 6, 4),
	(39, '鲍争争', '鲍争争', 'e10adc3949ba59abbe56e057f20f883e', 0, 6, 4),
	(40, '韩佩芬', '韩佩芬', 'e10adc3949ba59abbe56e057f20f883e', 0, 6, 4),
	(41, '马魏魏', '马魏魏', 'e10adc3949ba59abbe56e057f20f883e', 0, 6, 4),
	(42, '王婷婷', '王婷婷', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 3),
	(43, '章兰兰', '章兰兰', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 4),
	(44, '陈轩昂', '陈轩昂', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 4),
	(45, '江泽宏', '江泽宏', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 4),
	(46, '潘海芬', '潘海芬', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 4),
	(47, '王志锋', '王志锋', 'e10adc3949ba59abbe56e057f20f883e', 0, 3, 4),
	(48, '陆飞', '陆飞', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 3),
	(49, '李景忠', '李景忠', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 4),
	(50, '徐珊红', '徐珊红', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 4),
	(51, '谢龙常', '谢龙常', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 4),
	(52, '王岳成', '王岳成', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 4),
	(53, '袁芳舟', '袁芳舟', 'e10adc3949ba59abbe56e057f20f883e', 0, 7, 4),
	(54, '赵艳波', '赵艳波', 'e10adc3949ba59abbe56e057f20f883e', 0, 8, 3),
	(55, '郑明霞', '郑明霞', 'e10adc3949ba59abbe56e057f20f883e', 0, 8, 4),
	(56, '李金鱼', '李金鱼', 'e10adc3949ba59abbe56e057f20f883e', 0, 8, 4),
	(57, '潘胜利', '潘胜利', 'e10adc3949ba59abbe56e057f20f883e', 0, 8, 4),
	(58, '林晨', '林晨', 'e10adc3949ba59abbe56e057f20f883e', 0, 8, 4),
	(59, '乐友财', '乐友财', 'e10adc3949ba59abbe56e057f20f883e', 0, 9, 3),
	(60, '张立明', '张立明', 'e10adc3949ba59abbe56e057f20f883e', 0, 9, 4),
	(61, '王莹', '王莹', 'e10adc3949ba59abbe56e057f20f883e', 0, 9, 4),
	(62, '徐景敏', '徐景敏', 'e10adc3949ba59abbe56e057f20f883e', 0, 9, 4),
	(63, '周科毅', '周科毅', 'e10adc3949ba59abbe56e057f20f883e', 0, 9, 4),
	(64, '张剑', '张剑', 'e10adc3949ba59abbe56e057f20f883e', 0, 10, 4),
	(65, '阮佩军', '阮佩军', 'e10adc3949ba59abbe56e057f20f883e', 0, 11, 3),
	(66, '李晨', '李晨', 'e10adc3949ba59abbe56e057f20f883e', 0, 11, 0),
	(67, '王静军', '王静军', 'e10adc3949ba59abbe56e057f20f883e', 0, 11, 0),
	(68, '贺舟辉', '贺舟辉', 'e10adc3949ba59abbe56e057f20f883e', 0, 11, 0),
	(69, '乐友平', '乐友平', 'e10adc3949ba59abbe56e057f20f883e', 0, 12, 3);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Dumping structure for table oa.user_action
DROP TABLE IF EXISTS `user_action`;
CREATE TABLE IF NOT EXISTS `user_action` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `InfoId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL,
  `Extend` varchar(255) DEFAULT NULL,
  `Deleted` bit(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `InfoId` (`InfoId`),
  KEY `FromUserId` (`FromUserId`),
  KEY `ToUserId` (`ToUserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_action: ~0 rows (approximately)
/*!40000 ALTER TABLE `user_action` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_action` ENABLE KEYS */;

-- Dumping structure for table oa.user_department
DROP TABLE IF EXISTS `user_department`;
CREATE TABLE IF NOT EXISTS `user_department` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `DepartmentId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserId` (`UserId`),
  KEY `DepartmentId` (`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=136 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_department: ~68 rows (approximately)
/*!40000 ALTER TABLE `user_department` DISABLE KEYS */;
INSERT INTO `user_department` (`ID`, `UserId`, `DepartmentId`) VALUES
	(3, 1, 1),
	(4, 4, 1),
	(5, 7, 1),
	(6, 14, 1),
	(7, 15, 1),
	(8, 16, 1),
	(9, 17, 1),
	(10, 18, 1),
	(11, 19, 1),
	(12, 20, 1),
	(13, 21, 1),
	(14, 22, 1),
	(15, 23, 1),
	(16, 24, 1),
	(17, 25, 1),
	(18, 26, 1),
	(19, 27, 1),
	(20, 28, 1),
	(21, 3, 2),
	(22, 5, 2),
	(23, 33, 2),
	(24, 34, 2),
	(25, 35, 2),
	(26, 36, 2),
	(27, 37, 2),
	(29, 42, 3),
	(30, 43, 3),
	(31, 44, 3),
	(32, 45, 3),
	(33, 46, 3),
	(34, 47, 3),
	(35, 9, 4),
	(36, 11, 4),
	(37, 12, 4),
	(38, 13, 4),
	(39, 29, 5),
	(40, 30, 5),
	(41, 31, 5),
	(42, 32, 5),
	(43, 38, 6),
	(44, 39, 6),
	(45, 40, 6),
	(46, 41, 6),
	(47, 48, 7),
	(48, 49, 7),
	(49, 50, 7),
	(50, 51, 7),
	(51, 52, 7),
	(52, 53, 7),
	(53, 54, 8),
	(54, 55, 8),
	(55, 56, 8),
	(56, 57, 8),
	(57, 58, 8),
	(58, 59, 9),
	(59, 60, 9),
	(60, 61, 9),
	(61, 62, 9),
	(62, 63, 9),
	(63, 64, 10),
	(64, 65, 11),
	(65, 66, 11),
	(66, 67, 11),
	(67, 68, 11),
	(68, 69, 12),
	(133, 2, 2),
	(134, 8, 1),
	(135, 10, 1);
/*!40000 ALTER TABLE `user_department` ENABLE KEYS */;

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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_form_info: ~10 rows (approximately)
/*!40000 ALTER TABLE `user_form_info` DISABLE KEYS */;
INSERT INTO `user_form_info` (`ID`, `UserID`, `FormID`, `InfoID`, `Status`, `FlowNodeDataID`) VALUES
	(1, 5, 1, 1, 2, 1),
	(2, 5, 2, 2, 1, 2),
	(3, 14, 2, 2, 1, 3),
	(4, 3, 1, 1, 1, 4),
	(5, 3, 2, 2, 1, 3),
	(6, 2, 2, 2, 1, 3),
	(7, 5, 2, 1, 1, 1),
	(8, 14, 2, 1, 1, 2),
	(9, 3, 2, 1, 1, 2),
	(10, 2, 2, 1, 1, 2),
	(11, 5, 1, 2, 1, 3);
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
) ENGINE=InnoDB AUTO_INCREMENT=94 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_group: ~74 rows (approximately)
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
INSERT INTO `user_group` (`ID`, `UserID`, `GroupID`) VALUES
	(1, 1, 1),
	(4, 5, 1),
	(8, 4, 3),
	(9, 3, 2),
	(10, 7, 4),
	(14, 9, 5),
	(16, 11, 4),
	(17, 12, 4),
	(18, 13, 4),
	(20, 15, 1),
	(21, 16, 1),
	(22, 17, 1),
	(23, 18, 1),
	(24, 19, 1),
	(25, 20, 1),
	(26, 21, 1),
	(27, 22, 1),
	(28, 23, 1),
	(29, 24, 1),
	(30, 25, 1),
	(31, 26, 1),
	(32, 27, 1),
	(33, 28, 1),
	(34, 29, 1),
	(35, 30, 1),
	(36, 31, 1),
	(37, 32, 1),
	(38, 33, 1),
	(39, 34, 1),
	(40, 35, 1),
	(41, 36, 1),
	(42, 37, 1),
	(44, 38, 1),
	(45, 39, 1),
	(46, 40, 1),
	(47, 41, 1),
	(48, 42, 1),
	(49, 43, 1),
	(50, 44, 1),
	(51, 45, 1),
	(52, 46, 1),
	(53, 47, 1),
	(54, 48, 1),
	(55, 49, 1),
	(56, 50, 1),
	(57, 51, 1),
	(58, 52, 1),
	(59, 53, 1),
	(60, 54, 1),
	(61, 55, 1),
	(62, 56, 1),
	(63, 57, 1),
	(64, 58, 1),
	(65, 59, 1),
	(66, 60, 1),
	(67, 61, 1),
	(68, 62, 1),
	(69, 63, 1),
	(70, 64, 1),
	(71, 65, 1),
	(72, 66, 1),
	(73, 67, 1),
	(74, 68, 1),
	(75, 14, 1),
	(76, 14, 2),
	(77, 14, 3),
	(85, 69, 1),
	(86, 69, 2),
	(88, 8, 1),
	(89, 8, 2),
	(90, 8, 3),
	(91, 8, 4),
	(92, 8, 5),
	(93, 10, 1);
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

-- Dumping structure for table oa.user_title
DROP TABLE IF EXISTS `user_title`;
CREATE TABLE IF NOT EXISTS `user_title` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ParentID` (`ParentID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_title: ~4 rows (approximately)
/*!40000 ALTER TABLE `user_title` DISABLE KEYS */;
INSERT INTO `user_title` (`ID`, `ParentID`, `Name`) VALUES
	(1, 0, '局长'),
	(2, 1, '副局长'),
	(3, 2, '科长/主任'),
	(4, 3, '科员');
/*!40000 ALTER TABLE `user_title` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
