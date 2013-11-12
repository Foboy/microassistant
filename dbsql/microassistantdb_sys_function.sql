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
-- Table structure for table `sys_function`
--

DROP TABLE IF EXISTS `sys_function`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_function` (
  `idsys_function` int(11) NOT NULL AUTO_INCREMENT,
  `function_name` varchar(45) NOT NULL,
  `father_id` int(11) NOT NULL,
  `mark` varchar(200) DEFAULT NULL,
  `function_url` varchar(100) DEFAULT NULL,
  `function_code` varchar(45) DEFAULT NULL COMMENT '后期用,第一级就是模块 用两位编码 例子：10 ，第二级是子模块 4位 例子：1001， 第三级是页面 6位 例子 100101 第四级是按钮 8位 例子 10010101 所有的模块页面按钮必须有编码，系统设置默认角色赋予默认权限\n',
  `level` int(11) NOT NULL,
  PRIMARY KEY (`idsys_function`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_function`
--

LOCK TABLES `sys_function` WRITE;
/*!40000 ALTER TABLE `sys_function` DISABLE KEYS */;
INSERT INTO `sys_function` VALUES (1,'产品管理',0,NULL,NULL,'20',1),(2,'添加产品',1,NULL,NULL,'2010',2),(3,'查看入库清单',1,NULL,NULL,'2011',2),(4,'产品详细页',1,NULL,NULL,'2012',2),(5,'产品列表',1,NULL,NULL,'2013',2),(6,'客户信息',0,NULL,NULL,'10',1),(7,'企业客户列表',6,NULL,NULL,'1001',2),(8,'个人客户列表',6,NULL,NULL,'1002',2),(9,'添加普通客户',6,NULL,NULL,'1003',2),(10,'添加企业客户',6,NULL,NULL,'1004',2),(11,'财务管理',0,NULL,NULL,'11',1),(12,'应收款列表',11,NULL,NULL,'1101',2),(13,'应付款列表',11,NULL,NULL,'1102',2),(14,'应收款详情',11,NULL,NULL,'1103',2),(15,'确认付款',11,NULL,NULL,'1104',2),(16,'确认收款',11,NULL,NULL,'1105',2),(17,'销售管理',0,NULL,NULL,'12',1),(18,'销售机会列表',17,NULL,NULL,'1201',2),(19,'添加销售机会',17,NULL,NULL,'1202',2),(20,'拜访列表',17,NULL,NULL,'1203',2),(21,'拜访',17,NULL,NULL,'1204',2),(22,'销售机会详情',17,NULL,NULL,'1205',2),(23,'合同列表',17,NULL,NULL,'1206',2),(24,'添加合同',17,NULL,NULL,'1207',2),(25,'企业管理',0,NULL,NULL,'13',1),(26,'员工管理',25,NULL,NULL,'1301',2),(27,'老板模式',0,NULL,NULL,'18',1),(28,'统计',27,NULL,NULL,'1801',2);
/*!40000 ALTER TABLE `sys_function` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-12 17:46:32
