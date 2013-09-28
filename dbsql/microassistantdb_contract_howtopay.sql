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
-- Table structure for table `contract_howtopay`
--

DROP TABLE IF EXISTS `contract_howtopay`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contract_howtopay` (
  `howtopay_id` int(11) NOT NULL AUTO_INCREMENT,
  `instalments_no` int(11) NOT NULL COMMENT '分期序号',
  `amount` decimal(10,2) NOT NULL DEFAULT '0.00',
  `pay_time` datetime NOT NULL COMMENT '分期付款时间',
  `received_time` datetime DEFAULT NULL COMMENT '实际收款时间',
  `IsReceived` int(11) NOT NULL DEFAULT '0' COMMENT '0:没确认收款 1：已收款',
  `contract_info_id` int(11) NOT NULL,
  PRIMARY KEY (`howtopay_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='付款方式';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contract_howtopay`
--

LOCK TABLES `contract_howtopay` WRITE;
/*!40000 ALTER TABLE `contract_howtopay` DISABLE KEYS */;
/*!40000 ALTER TABLE `contract_howtopay` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-09-28 14:09:50
