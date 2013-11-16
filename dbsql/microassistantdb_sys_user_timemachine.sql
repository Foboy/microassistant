CREATE DATABASE  IF NOT EXISTS `microassistantdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `microassistantdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: microassistantdb
-- ------------------------------------------------------
-- Server version	5.6.13-enterprise-commercial-advanced

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `sys_user_timemachine`
--

DROP TABLE IF EXISTS `sys_user_timemachine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_user_timemachine` (
  `idsys_user_timemachine` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `user_name` varchar(45) NOT NULL,
  `role_name` varchar(45) DEFAULT NULL,
  `ent_name` varchar(45) DEFAULT NULL,
  `ent_id` int(11) DEFAULT NULL,
  `start_time` datetime DEFAULT NULL,
  `end_time` datetime DEFAULT NULL,
  PRIMARY KEY (`idsys_user_timemachine`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COMMENT='用户时间轴表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user_timemachine`
--

LOCK TABLES `sys_user_timemachine` WRITE;
/*!40000 ALTER TABLE `sys_user_timemachine` DISABLE KEYS */;
INSERT INTO `sys_user_timemachine` VALUES (1,20,'qwe','销售部','rrr',14,'2013-11-16 17:43:29','0001-01-01 00:00:00'),(3,27,'zzz',NULL,NULL,0,'2013-11-16 17:52:42','0001-01-01 00:00:00'),(4,27,'zzz','未审核','aaa',21,'2013-11-16 17:52:51','0001-01-01 00:00:00'),(5,27,'zzz','管理员','aaa',21,'2013-11-16 17:54:15','0001-01-01 00:00:00'),(6,28,'fjdskajf','管理员','fjdskajf',28,'2013-11-16 17:59:25','0001-01-01 00:00:00'),(7,30,'a3','未审核','aaa',21,'2013-11-16 18:03:44','0001-01-01 00:00:00'),(8,27,'zzz','未审核','aaa',21,'2013-11-16 18:04:51','0001-01-01 00:00:00'),(9,27,'zzz','未审核','rrr',14,'2013-11-16 18:12:26','0001-01-01 00:00:00');
/*!40000 ALTER TABLE `sys_user_timemachine` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-16 18:24:53
