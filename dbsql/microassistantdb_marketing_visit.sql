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
-- Table structure for table `marketing_visit`
--

DROP TABLE IF EXISTS `marketing_visit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `marketing_visit` (
  `idmarketing_visit` int(11) NOT NULL AUTO_INCREMENT,
  `visit_type` int(11) NOT NULL DEFAULT '1' COMMENT '1:电话 2:面谈',
  `amount` decimal(10,2) DEFAULT NULL,
  `address` varchar(100) DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  `visit_time` datetime DEFAULT NULL COMMENT '拜访时间',
  `chance_id` int(11) NOT NULL,
  `ent_id` int(11) NOT NULL,
  PRIMARY KEY (`idmarketing_visit`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COMMENT='拜访记录表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marketing_visit`
--

LOCK TABLES `marketing_visit` WRITE;
/*!40000 ALTER TABLE `marketing_visit` DISABLE KEYS */;
INSERT INTO `marketing_visit` VALUES (6,1,0.00,'','555','2013-11-16 18:38:04',8,14),(7,1,0.00,'','344324','2013-11-19 22:02:29',6,14),(8,2,0.00,'','33','2013-11-21 23:10:47',11,33),(9,2,0.00,'成都市，青羊区武都路11-2号  ( 30.684455, 104.076291)','444','2013-11-22 17:58:50',12,33),(10,1,0.00,'','dsfsadf','2013-11-23 17:55:47',8,14),(11,1,0.00,'','sdfasdfa','2013-11-23 17:55:51',8,14),(12,1,0.00,'','sdfdsf','2013-11-23 17:55:56',8,14),(13,1,0.00,'','sdfadsf','2013-11-23 17:56:01',8,14);
/*!40000 ALTER TABLE `marketing_visit` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-12-21 18:08:08
