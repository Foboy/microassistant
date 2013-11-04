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
-- Table structure for table `sys_role_user`
--

DROP TABLE IF EXISTS `sys_role_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_role_user` (
  `sys_role_user_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `ent_id` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`sys_role_user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_role_user`
--

LOCK TABLES `sys_role_user` WRITE;
/*!40000 ALTER TABLE `sys_role_user` DISABLE KEYS */;
INSERT INTO `sys_role_user` VALUES (1,1,1,1),(2,1,3,1),(3,2,2,1);
/*!40000 ALTER TABLE `sys_role_user` ENABLE KEYS */;
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
