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
-- Table structure for table `sys_user`
--

DROP TABLE IF EXISTS `sys_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(45) NOT NULL COMMENT '用户名或者企业名',
  `user_account` varchar(45) NOT NULL COMMENT '用户账号()',
  `pwd` varchar(45) NOT NULL,
  `mobile` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `create_time` datetime DEFAULT NULL COMMENT '注册时间',
  `end_time` datetime DEFAULT NULL COMMENT '失效时间',
  `ent_admin_id` int(11) DEFAULT NULL COMMENT '所属企业管理员ID',
  `is_enable` int(11) NOT NULL DEFAULT '1' COMMENT '2:不可用 1：可用',
  `type` int(11) NOT NULL DEFAULT '0' COMMENT '1：普通用户 2：企业用户',
  `ent_id` int(11) DEFAULT NULL COMMENT '员工所属企业ID',
  `sex` int(11) NOT NULL DEFAULT '1' COMMENT '1:男 2：女',
  `birthday` datetime DEFAULT NULL,
  `pic_id` int(11) NOT NULL DEFAULT '0',
  `ent_code` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user`
--

LOCK TABLES `sys_user` WRITE;
/*!40000 ALTER TABLE `sys_user` DISABLE KEYS */;
INSERT INTO `sys_user` VALUES (1,'王胖子公司','www','099b3b060154898840f0ebdfb46ec78f',NULL,'www','2013-10-23 20:43:05','2014-01-21 20:43:05',0,1,2,1,1,NULL,0,NULL),(2,'qq','qq','099b3b060154898840f0ebdfb46ec78f',NULL,'qq','2013-11-02 11:00:12','2014-01-31 11:00:12',0,1,1,1,1,NULL,0,NULL),(7,'222','222','bcbe3365e6ac95ea2c0343a2395834dd',NULL,'222','2013-11-02 17:09:09','2014-01-31 17:09:09',0,1,1,1,1,NULL,0,NULL),(8,'333','333','310dcbbf4cce62f762a2aaa148d556bd',NULL,'333','2013-11-03 10:08:02','2014-02-01 10:08:02',0,1,1,1,0,'0001-01-01 00:00:00',0,NULL),(14,'rrr','rrr','44f437ced647ec3f40fa0841041871cd',NULL,'rrr','2013-11-06 20:49:59','2014-02-04 20:49:59',0,1,2,14,0,'0001-01-01 00:00:00',0,NULL),(15,'ttt','ttt','9990775155c3518a0d7917f7780b24aa',NULL,'ttt','2013-11-12 18:09:29','2014-02-10 18:09:29',0,1,2,15,0,'0001-01-01 00:00:00',0,'CX4IP4'),(16,'fff','fff','343d9040a671c45832ee5381860e2996',NULL,'fff','2013-11-12 18:14:54','2014-02-10 18:14:54',0,1,2,16,0,'0001-01-01 00:00:00',0,'AJXPK1'),(17,'ddd','ddd','77963b7a931377ad4ab5ad6a9cd718aa',NULL,'ddd','2013-11-12 18:15:42','2014-02-10 18:15:42',0,1,1,16,0,'0001-01-01 00:00:00',0,'AJXPK1'),(18,'hhh','hhh','a3aca2964e72000eea4c56cb341002a4',NULL,'hhh','2013-11-12 18:16:50','2014-02-10 18:16:50',0,1,1,0,0,'0001-01-01 00:00:00',0,NULL),(19,'qqq','qqq','b2ca678b4c936f905fb82f2733f5297f',NULL,'qqq','2013-11-12 21:24:51','2014-02-10 21:24:51',0,1,1,0,0,'0001-01-01 00:00:00',0,NULL);
/*!40000 ALTER TABLE `sys_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-14 16:11:00
