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

-- 导出  表 oa.flow 结构
CREATE TABLE IF NOT EXISTS `flow` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `InfoID` int(11) NOT NULL,
  `InfoType` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `InfoID` (`InfoID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flow 的数据：~1 rows (大约)
DELETE FROM `flow`;
/*!40000 ALTER TABLE `flow` DISABLE KEYS */;
INSERT INTO `flow` (`ID`, `Name`, `InfoID`, `InfoType`) VALUES
	(1, '测试公文', 1, 0);
/*!40000 ALTER TABLE `flow` ENABLE KEYS */;

-- 导出  表 oa.flowstep 结构
CREATE TABLE IF NOT EXISTS `flowstep` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `FlowID` int(11) NOT NULL,
  `Result` bit(2) DEFAULT NULL,
  `Content` varchar(1023) DEFAULT NULL,
  `Step` int(11) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FlowID` (`FlowID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.flowstep 的数据：~0 rows (大约)
DELETE FROM `flowstep`;
/*!40000 ALTER TABLE `flowstep` DISABLE KEYS */;
/*!40000 ALTER TABLE `flowstep` ENABLE KEYS */;

-- 导出  表 oa.group 结构
CREATE TABLE IF NOT EXISTS `group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Type` bit(2) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.group 的数据：~0 rows (大约)
DELETE FROM `group`;
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
/*!40000 ALTER TABLE `group` ENABLE KEYS */;

-- 导出  表 oa.receive_document 结构
CREATE TABLE IF NOT EXISTS `receive_document` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` varchar(255) NOT NULL,
  `Title` varchar(1023) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  `ConfidentialLevel` int(10) NOT NULL DEFAULT '0',
  `UID` int(11) NOT NULL,
  `Filing` int(11) DEFAULT NULL,
  `Category` varchar(255) NOT NULL,
  `Emergency` int(10) NOT NULL DEFAULT '0',
  `ReceiveWord` varchar(255) DEFAULT NULL,
  `Keywords` varchar(255) DEFAULT NULL,
  `SWOrgan` varchar(255) DEFAULT NULL,
  `FromOrgan` varchar(255) DEFAULT NULL,
  `PrintTime` datetime DEFAULT NULL,
  `EffectTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `UID` (`UID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.receive_document 的数据：~1 rows (大约)
DELETE FROM `receive_document`;
/*!40000 ALTER TABLE `receive_document` DISABLE KEYS */;
INSERT INTO `receive_document` (`ID`, `Number`, `Title`, `CreateTime`, `Deleted`, `ConfidentialLevel`, `UID`, `Filing`, `Category`, `Emergency`, `ReceiveWord`, `Keywords`, `SWOrgan`, `FromOrgan`, `PrintTime`, `EffectTime`) VALUES
	(1, '20170216111111111111', '测试公文', '2017-02-16 14:19:23', b'00', 0, 1, NULL, '测试种类1', 1, '20170301', '', NULL, NULL, NULL, NULL);
/*!40000 ALTER TABLE `receive_document` ENABLE KEYS */;

-- 导出  表 oa.send_document 结构
CREATE TABLE IF NOT EXISTS `send_document` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` varchar(255) NOT NULL,
  `Title` varchar(1023) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `Deleted` bit(2) NOT NULL DEFAULT b'0',
  `ConfidentialLevel` bit(2) NOT NULL DEFAULT b'0',
  `UID` datetime NOT NULL,
  `Filing` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `UID` (`UID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.send_document 的数据：~0 rows (大约)
DELETE FROM `send_document`;
/*!40000 ALTER TABLE `send_document` DISABLE KEYS */;
/*!40000 ALTER TABLE `send_document` ENABLE KEYS */;

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
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user 的数据：~0 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `Username`, `Password`, `Name`, `Role`) VALUES
	(1, '管理员', '21232f297a57a5a743894a0e4a801fc3', 'Admin', b'011'),
	(2, '汪建龙', 'e10adc3949ba59abbe56e057f20f883e', 'wjl', b'000'),
	(3, '唐尧', 'e10adc3949ba59abbe56e057f20f883e', 'ty', b'000'),
	(4, '赵斯思', 'e10adc3949ba59abbe56e057f20f883e', 'zss', b'000'),
	(5, '郑良军', 'e10adc3949ba59abbe56e057f20f883e', 'zlj', b'000');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  表 oa.user_group 结构
CREATE TABLE IF NOT EXISTS `user_group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `GroupID` (`GroupID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  oa.user_group 的数据：~0 rows (大约)
DELETE FROM `user_group`;
/*!40000 ALTER TABLE `user_group` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_group` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
