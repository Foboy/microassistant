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
-- Table structure for table `marketing_chance`
--

DROP TABLE IF EXISTS `marketing_chance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `marketing_chance` (
  `idmarketing_chance` int(11) NOT NULL AUTO_INCREMENT,
  `chance_type` int(11) DEFAULT '1' COMMENT '机会类型分为：1:新客户机会2:老客户机会；默认新客户机会',
  `customer_type` int(11) DEFAULT '1' COMMENT '客户类型分为：1:企业客户2:个人客户；默认为企业客户',
  `contact_name` varchar(45) DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  `add_time` datetime DEFAULT NULL,
  `qq` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `tel` varchar(45) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `rate` int(11) DEFAULT NULL COMMENT '盈率',
  `ent_id` int(11) DEFAULT NULL COMMENT '所属企业ID',
  `user_id` int(11) DEFAULT NULL COMMENT '销售人员ID',
  `IsVisit` int(11) NOT NULL DEFAULT '1' COMMENT '1:未拜访 2：已拜访',
  PRIMARY KEY (`idmarketing_chance`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='销售机会表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marketing_chance`
--

LOCK TABLES `marketing_chance` WRITE;
/*!40000 ALTER TABLE `marketing_chance` DISABLE KEYS */;
/*!40000 ALTER TABLE `marketing_chance` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-02 19:19:22
