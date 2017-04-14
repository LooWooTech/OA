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
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.feed: ~44 rows (approximately)
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
	(46, 1, 7, 0, 5, 0, 0, b'000', '2017-04-14 18:33:13');
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
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.file: ~49 rows (approximately)
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
	(17, 'App_Pai_Photo.sql', 'upload_files/8af9d855127475d24d1ab3d5ca93a951.sql', 183292, '2017-03-23 21:08:39', NULL, 2),
	(18, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 11:40:16', NULL, 2),
	(19, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 11:40:29', NULL, 2),
	(20, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 11:43:49', NULL, 2),
	(21, 'IMG_20150618_155312.jpg', 'upload_files/e7d96072d013c2498b44c357240dd9b2.jpg', 1879774, '2017-04-11 11:45:05', NULL, 2),
	(22, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 11:48:41', NULL, 2),
	(23, 'IMG_20150618_155312.jpg', 'upload_files/e7d96072d013c2498b44c357240dd9b2.jpg', 1879774, '2017-04-11 11:48:52', NULL, 2),
	(24, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 11:48:55', NULL, 2),
	(25, 'IMG_20150618_155312.jpg', 'upload_files/e7d96072d013c2498b44c357240dd9b2.jpg', 1879774, '2017-04-11 11:58:23', NULL, 2),
	(26, 'IMG_20150618_155312.jpg', 'upload_files/e7d96072d013c2498b44c357240dd9b2.jpg', 1879774, '2017-04-11 12:02:37', NULL, 2),
	(27, 'IMG_20150618_180746.jpg', 'upload_files/714b9ba3741ec76a417764890782e320.jpg', 1515661, '2017-04-11 12:03:07', NULL, 2),
	(28, 'IMG_20150618_180746.jpg', 'upload_files/714b9ba3741ec76a417764890782e320.jpg', 1515661, '2017-04-11 12:11:06', NULL, 2),
	(29, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:11:08', NULL, 2),
	(30, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:14:49', NULL, 2),
	(31, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:14:52', NULL, 2),
	(32, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:16:37', NULL, 2),
	(33, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:16:39', NULL, 2),
	(34, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:17:35', NULL, 2),
	(35, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:17:37', NULL, 2),
	(36, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:25:15', NULL, 2),
	(37, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:25:17', NULL, 2),
	(38, 'IMG_20150618_180808.jpg', 'upload_files/f531da166508ec063b0df1cfe4be5f05.jpg', 1271036, '2017-04-11 12:27:16', NULL, 2),
	(39, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:27:18', NULL, 2),
	(42, 'IMG_20150618_180746.jpg', 'upload_files/714b9ba3741ec76a417764890782e320.jpg', 1515661, '2017-04-11 12:31:18', NULL, 2),
	(43, 'IMG_20150621_174736.jpg', 'upload_files/0f79d5bf0bd0878e69192adc8aec3874.jpg', 2615167, '2017-04-11 12:34:24', NULL, 2),
	(44, 'IMG_20150621_174736.jpg', 'upload_files/0f79d5bf0bd0878e69192adc8aec3874.jpg', 2615167, '2017-04-11 12:36:54', NULL, 2),
	(45, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:38:13', NULL, 2),
	(46, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 12:40:48', NULL, 2),
	(47, 'IMG_20150621_174736.jpg', 'upload_files/0f79d5bf0bd0878e69192adc8aec3874.jpg', 2615167, '2017-04-11 14:25:58', NULL, 2),
	(48, 'IMG_20150621_174651.jpg', 'upload_files/25071e5cb79e7178c386c90ad26fc608.jpg', 1744787, '2017-04-11 14:26:05', NULL, 2),
	(49, '表格导出问题.docx', 'upload_files/88b3655de03223e28a4a90981508dabf.docx', 11114, '2017-04-12 14:32:57', NULL, 0),
	(51, '表格导出问题.docx', 'upload_files/88b3655de03223e28a4a90981508dabf.docx', 11114, '2017-04-12 14:33:30', NULL, 0),
	(52, 'QQ图片20170412100850.jpg', 'upload_files/a6e454411daa5109db2f78f912580f39.jpg', 8437, '2017-04-12 16:22:34', NULL, 0),
	(53, 'xs1702.dxf', 'upload_files/949916d31b1a580f2ef39621aea47ed3.dxf', 8675, '2017-04-12 16:24:10', NULL, 0),
	(54, '表格导出问题.docx', 'upload_files/88b3655de03223e28a4a90981508dabf.docx', 11114, '2017-04-12 16:24:57', NULL, 0),
	(55, 'QQ图片20170412100850.jpg', 'upload_files/a6e454411daa5109db2f78f912580f39.jpg', 8437, '2017-04-12 16:26:08', '2017-04-12 16:26:08', 4),
	(56, 'xs1702.dxf', 'upload_files/949916d31b1a580f2ef39621aea47ed3.dxf', 8675, '2017-04-12 16:26:01', NULL, 4),
	(57, '表格导出问题.docx', 'upload_files/88b3655de03223e28a4a90981508dabf.docx', 11114, '2017-04-13 16:44:46', NULL, 5),
	(58, '表格导出问题.docx', 'upload_files/88b3655de03223e28a4a90981508dabf.docx', 11114, '2017-04-14 18:24:08', NULL, 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_data` DISABLE KEYS */;
INSERT INTO `flow_data` (`ID`, `InfoId`, `FormId`, `FlowId`, `Completed`) VALUES
	(1, 5, 1, 1, b'1'),
	(2, 6, 1, 1, b'0');
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

-- Dumping data for table oa.flow_node: ~3 rows (approximately)
/*!40000 ALTER TABLE `flow_node` DISABLE KEYS */;
INSERT INTO `flow_node` (`ID`, `FlowId`, `Name`, `UserId`, `GroupID`, `PrevId`, `DepartmentId`) VALUES
	(1, 1, '拟稿人', 0, 1, 0, 2),
	(2, 1, '科室负责人', 0, 2, 1, 2),
	(3, 1, '办公室审批', 0, 0, 2, 1),
	(4, 1, '分管领导审核', 0, 4, 3, 1),
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.flow_node_data: ~0 rows (approximately)
/*!40000 ALTER TABLE `flow_node_data` DISABLE KEYS */;
INSERT INTO `flow_node_data` (`ID`, `FlowDataId`, `ParentId`, `FlowNodeName`, `CreateTime`, `UserId`, `Signature`, `Department`, `UpdateTime`, `Result`, `Content`, `FlowNodeId`, `Step`) VALUES
	(1, 1, 0, '拟稿人', '2017-04-13 18:53:43', 5, '郑良军', '规划耕保科', '2017-04-13 18:54:31', b'1', NULL, 1, 0),
	(3, 1, 0, '科室负责人', '2017-04-13 18:54:31', 3, '唐尧', '规划耕保科', '2017-04-13 18:55:25', b'1', NULL, 2, 0),
	(5, 1, 0, '办公室审批', '2017-04-13 18:55:25', 4, '赵斯思', '办公室', '2017-04-13 18:55:57', b'0', NULL, 3, 0),
	(6, 1, 0, '拟稿人', '2017-04-13 18:55:57', 5, '郑良军', '规划耕保科', '2017-04-13 18:56:47', b'1', NULL, 1, 0),
	(8, 1, 0, '科室负责人', '2017-04-13 18:56:47', 3, '唐尧', '规划耕保科', '2017-04-13 18:57:02', b'1', NULL, 2, 0),
	(9, 1, 0, '办公室审批', '2017-04-13 18:57:02', 4, '赵斯思', '办公室', '2017-04-13 18:57:12', b'1', NULL, 3, 0),
	(10, 2, 0, '拟稿人', '2017-04-14 18:24:39', 5, '郑良军', '规划耕保科', '2017-04-14 18:24:39', b'1', '测试提交', 1, 0),
	(11, 2, 0, '科室负责人', '2017-04-14 18:24:39', 3, '唐尧', '规划耕保科', NULL, NULL, NULL, 2, 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.form_info: ~3 rows (approximately)
/*!40000 ALTER TABLE `form_info` DISABLE KEYS */;
INSERT INTO `form_info` (`ID`, `FormId`, `Title`, `Keywords`, `CategoryId`, `CreateTime`, `UpdateTime`, `Deleted`, `PostUserId`, `FlowDataId`, `FlowStep`, `Data`) VALUES
	(3, 1, '测试公文拟稿', '20170401,公文拟稿', 0, '2017-04-12 14:33:44', '2017-04-12 14:34:28', b'0', 5, 0, '负责人', '{"ZRR": "郑良军", "Word": {"ID": 51, "$id": "1", "Size": 11114, "InfoId": 0, "FileName": "表格导出问题.docx", "SavePath": "upload_files/88b3655de03223e28a4a90981508dabf.docx", "CreateTime": "2017-04-12T14:33:29.5243664+08:00", "UpdateTime": null, "DisplaySize": "10K"}, "ZWGK": 2, "CS_JG": "测试机关", "FW_RQ": "2017-04-01T14:32:28+08:00", "GW_MJ": 2, "GW_WH": "20170401", "QX_RQ": "2017-04-29", "WJ_BT": "测试公文拟稿", "ZS_JG": "本机关", "Excels": [{"ID": 50, "$id": "1", "Size": 8675, "InfoId": 0, "FileName": "xs1702.dxf", "SavePath": "upload_files/949916d31b1a580f2ef39621aea47ed3.dxf", "CreateTime": "2017-04-12T14:33:26.9988253+08:00", "UpdateTime": null, "DisplaySize": "8K"}], "GW_ZTC": "公文拟稿", "SF_FB_WWW": true}'),
	(4, 1, '测试公文2', '201704123,拟稿', 0, '2017-04-12 16:25:25', '2017-04-13 16:41:39', b'0', 5, 0, '完结', '{"Word": {"ID": 55, "$id": "1", "Size": 8437, "InfoId": 4, "FileName": "QQ图片20170412100850.jpg", "SavePath": "upload_files/a6e454411daa5109db2f78f912580f39.jpg", "CreateTime": "2017-04-12T16:26:08.0910961+08:00", "UpdateTime": "2017-04-12T16:26:08.0910961+08:00", "DisplaySize": "8K"}, "ZWGK": 1, "CS_JG": "测试机关2", "FW_RQ": "2017-04-12T16:25:22+08:00", "GW_MJ": 2, "GW_WH": "201704123", "QX_RQ": "", "WJ_BT": "测试公文2", "ZS_JG": "测试机关1", "Excels": [{"ID": 54, "$id": "1", "Size": 11114, "InfoId": 0, "FileName": "表格导出问题.docx", "SavePath": "upload_files/88b3655de03223e28a4a90981508dabf.docx", "CreateTime": "2017-04-12T16:24:57.3085575+08:00", "UpdateTime": null, "DisplaySize": "10K"}, {"ID": 56, "$id": "1", "Size": 8675, "InfoId": 4, "FileName": "xs1702.dxf", "SavePath": "upload_files/949916d31b1a580f2ef39621aea47ed3.dxf", "CreateTime": "2017-04-12T16:26:01.0930898+08:00", "UpdateTime": null, "DisplaySize": "8K"}], "GW_ZTC": "拟稿", "SF_FB_WWW": true}'),
	(5, 1, '测试公文流程2号文件', '2017-03-01,测试文件', 0, '2017-04-13 16:43:53', '2017-04-13 18:57:12', b'0', 5, 1, '完结', '{"ZRR": "郑良军", "Word": {}, "ZWGK": 1, "CS_JG": "测试抄送机关", "FW_RQ": "2017-03-01T16:43:40+08:00", "GW_MJ": 3, "GW_WH": "2017-03-01", "QX_RQ": "2017-04-13", "WJ_BT": "测试公文流程2号文件", "ZS_JG": "测试机关", "Excels": [{"ID": 57, "$id": "1", "Size": 11114, "InfoId": 5, "FileName": "表格导出问题.docx", "SavePath": "upload_files/88b3655de03223e28a4a90981508dabf.docx", "CreateTime": "2017-04-13T16:44:46.4078502+08:00", "UpdateTime": null, "DisplaySize": "10K"}], "GW_ZTC": "测试文件", "HLW_FB": true}'),
	(6, 1, '这是一条测试公文拟稿八号文件', '20170404,测试', 0, '2017-04-14 18:24:23', '2017-04-14 18:24:23', b'0', 5, 2, '科室负责人', '{"ZRR": "郑良军", "Word": {"ID": 58, "$id": "1", "Size": 11114, "InfoId": 0, "FileName": "表格导出问题.docx", "SavePath": "upload_files/88b3655de03223e28a4a90981508dabf.docx", "CreateTime": "2017-04-14T18:24:07.9319018+08:00", "UpdateTime": null, "DisplaySize": "10K"}, "ZWGK": 2, "CS_JG": "测试抄送机关名称", "FW_RQ": "2017-04-14T18:24:20+08:00", "GW_MJ": 2, "GW_WH": "20170404", "QX_RQ": "2017-04-14", "WJ_BT": "这是一条测试公文拟稿八号文件", "ZS_JG": "测试机关名称", "Excels": [], "GW_ZTC": "测试", "SF_FB_WWW": true}'),
	(7, 1, '测试', '2017050301,测试', 0, '2017-04-14 18:33:13', '2017-04-14 18:33:13', b'0', 5, 0, '未提交', '{"Word": {}, "ZWGK": 1, "FW_RQ": "2017-04-14T18:33:11+08:00", "GW_MJ": 2, "GW_WH": "2017050301", "QX_RQ": "", "WJ_BT": "测试", "Excels": [], "GW_ZTC": "测试"}');
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user: ~5 rows (approximately)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `UserName`, `RealName`, `Password`, `Role`, `DepartmentId`) VALUES
	(1, 'Admin', '管理员', '202cb962ac59075b964b07152d234b70', 3, 1),
	(2, 'wjl', '汪建龙', '202cb962ac59075b964b07152d234b70', 0, 3),
	(3, 'ty', '唐尧', '202cb962ac59075b964b07152d234b70', 0, 2),
	(4, 'zss', '赵斯思', '202cb962ac59075b964b07152d234b70', 0, 1),
	(5, 'zlj', '郑良军', '202cb962ac59075b964b07152d234b70', 0, 2);
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_form_info: ~1 rows (approximately)
/*!40000 ALTER TABLE `user_form_info` DISABLE KEYS */;
INSERT INTO `user_form_info` (`ID`, `UserID`, `FormID`, `InfoID`, `Status`, `FlowNodeDataID`) VALUES
	(1, 5, 1, 5, 3, 6),
	(5, 4, 1, 5, 3, 9),
	(6, 3, 1, 5, 3, 8),
	(7, 5, 1, 6, 2, 10),
	(8, 3, 1, 6, 1, 11),
	(9, 5, 1, 7, 0, 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Dumping data for table oa.user_group: ~3 rows (approximately)
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
INSERT INTO `user_group` (`ID`, `UserID`, `GroupID`) VALUES
	(1, 1, 1),
	(4, 5, 1),
	(8, 4, 3),
	(9, 3, 2);
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
