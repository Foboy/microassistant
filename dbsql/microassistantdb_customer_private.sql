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
-- Table structure for table `customer_private`
--

DROP TABLE IF EXISTS `customer_private`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer_private` (
  `customer_private_id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `sex` int(11) DEFAULT '0' COMMENT '0:man 1:woman',
  `birthday` datetime DEFAULT NULL,
  `industy` varchar(45) DEFAULT NULL,
  `mobile` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `qq` varchar(45) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `address` varchar(45) DEFAULT NULL,
  `detail` varchar(45) DEFAULT NULL,
  `ent_id` int(11) NOT NULL COMMENT '所属企业ID',
  `owner_id` int(11) NOT NULL,
  PRIMARY KEY (`customer_private_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='个人客户';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_private`
--

LOCK TABLES `customer_private` WRITE;
/*!40000 ALTER TABLE `customer_private` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_private` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-10-15 20:32:31
