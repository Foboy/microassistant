/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50520
Source Host           : localhost:3306
Source Database       : microassistantdb

Target Server Type    : MYSQL
Target Server Version : 50520
File Encoding         : 65001

Date: 2013-08-31 14:07:59
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `pro_production`
-- ----------------------------
DROP TABLE IF EXISTS `pro_production`;
CREATE TABLE `pro_production` (
  `p_id` int(11) NOT NULL AUTO_INCREMENT,
  `p_name` varchar(45) NOT NULL COMMENT '产品名称',
  `p_info` varchar(200) DEFAULT NULL COMMENT '产品描述',
  `unit` varchar(45) DEFAULT NULL,
  `p_type_id` int(11) DEFAULT NULL COMMENT '产品分类',
  `lowest_price` decimal(10,2) NOT NULL DEFAULT '0.00',
  `market_price` decimal(10,2) DEFAULT '0.00',
  `user_id` int(11) NOT NULL,
  `ent_id` int(11) NOT NULL,
  `stock_count` int(11) NOT NULL DEFAULT '0' COMMENT '库存数量',
  PRIMARY KEY (`p_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='产品';

-- ----------------------------
-- Records of pro_production
-- ----------------------------
