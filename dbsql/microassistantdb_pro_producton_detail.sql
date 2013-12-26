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
-- Table structure for table `pro_producton_detail`
--

DROP TABLE IF EXISTS `pro_producton_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_producton_detail` (
  `p_d_id` int(11) NOT NULL AUTO_INCREMENT,
  `price` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '采购单价',
  `p_num` int(11) NOT NULL COMMENT '采购数量',
  `p_code` varchar(45) NOT NULL COMMENT '采购批次号',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `user_id` int(11) DEFAULT NULL,
  `p_id` int(11) NOT NULL,
  `ent_id` int(11) NOT NULL,
  `is_pay` int(11) NOT NULL DEFAULT '1' COMMENT '1:未付款 2:已付款',
  `pay_time` datetime DEFAULT NULL,
  PRIMARY KEY (`p_d_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='产品入库单表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pro_producton_detail`
--

LOCK TABLES `pro_producton_detail` WRITE;
/*!40000 ALTER TABLE `pro_producton_detail` DISABLE KEYS */;
INSERT INTO `pro_producton_detail` VALUES (1,333.00,444,'20131709011746362','2013-11-09 05:17:46',14,5,14,1,NULL),(2,555.00,555,'20135111115151874','2013-11-11 15:51:52',14,5,14,1,NULL),(3,4.00,4,'20135314035333325','2013-12-14 07:53:33',35,7,35,1,NULL),(4,444.00,44,'20133514043527064','2013-12-14 08:35:27',33,6,33,1,NULL);
/*!40000 ALTER TABLE `pro_producton_detail` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-12-21 18:08:09
