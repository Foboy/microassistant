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
-- Table structure for table `contract_info`
--

DROP TABLE IF EXISTS `contract_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contract_info` (
  `contract_info_id` int(11) NOT NULL AUTO_INCREMENT,
  `contract_no` varchar(45) DEFAULT NULL,
  `c_name` varchar(45) DEFAULT NULL COMMENT '合同名称',
  `customer_name` varchar(45) DEFAULT NULL COMMENT '客户名称',
  `start_time` datetime DEFAULT NULL,
  `end_time` datetime DEFAULT NULL,
  `owner_id` int(11) NOT NULL COMMENT '操作人ID',
  `contract_time` datetime NOT NULL COMMENT '合同签订时间',
  `amount` decimal(10,2) DEFAULT NULL,
  `howtopay` int(11) NOT NULL DEFAULT '0' COMMENT '0: 全额 1：分期',
  `ent_id` int(11) NOT NULL,
  `chance_id` int(11) NOT NULL DEFAULT '0' COMMENT '机会ID',
  PRIMARY KEY (`contract_info_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='合同表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contract_info`
--

LOCK TABLES `contract_info` WRITE;
/*!40000 ALTER TABLE `contract_info` DISABLE KEYS */;
INSERT INTO `contract_info` VALUES (1,'20131119224615','sfgfdg','fdsg','2013-11-19 00:00:00','2013-11-30 00:00:00',14,'2013-11-19 00:00:00',444444.00,1,14,8),(2,'20131122175959','43','34343','1899-12-06 00:00:00','2013-11-01 00:00:00',33,'1899-12-21 00:00:00',7777.00,1,33,12);
/*!40000 ALTER TABLE `contract_info` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-01-04 15:50:17
