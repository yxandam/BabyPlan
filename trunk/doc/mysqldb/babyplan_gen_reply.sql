CREATE DATABASE  IF NOT EXISTS `babyplan` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `babyplan`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: babyplan
-- ------------------------------------------------------
-- Server version	5.5.25

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
-- Table structure for table `gen_reply`
--

DROP TABLE IF EXISTS `gen_reply`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gen_reply` (
  `rid` int(11) NOT NULL AUTO_INCREMENT,
  `obj_id` int(11) NOT NULL,
  `obj_type` int(11) NOT NULL DEFAULT '1' COMMENT '1:产品 2:其他',
  `content` varchar(512) NOT NULL,
  `create_id` int(11) DEFAULT NULL,
  `state` int(11) NOT NULL DEFAULT '1' COMMENT '1：启用 2：禁用',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ref_user_id` int(11) DEFAULT NULL,
  `ref_reply_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`rid`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gen_reply`
--

LOCK TABLES `gen_reply` WRITE;
/*!40000 ALTER TABLE `gen_reply` DISABLE KEYS */;
INSERT INTO `gen_reply` VALUES (1,2,1,'22222222222222222222222222222222222',2,1,'2012-07-27 20:06:49',2,NULL),(2,2,1,'fdsjfdsfdfsf',2,1,'2012-08-05 03:18:31',2,NULL),(3,2,1,' 上搭建发了狂的骄傲了空间发了的骄傲了看法',2,1,'2012-08-05 03:18:59',2,NULL),(4,2,1,'sjflasfdsafjdsalfj',2,1,'2012-08-05 03:21:56',2,NULL),(5,2,1,'jdkfljlskajfdsjafkjdsalfdsa',2,1,'2012-08-05 03:28:18',2,NULL),(6,2,1,'324324324234324324',2,1,'2012-08-05 07:06:27',2,0),(7,2,1,'ksfjdksj;fjd;sajf;das;jdlsjaf',2,1,'2012-08-05 07:06:38',2,0),(8,2,1,'jljljljljljl',2,1,'2012-08-05 07:25:44',2,2),(9,2,1,'jljljlkjljlkjlkjlkjljlk00000000000',2,1,'2012-08-05 07:26:09',2,2);
/*!40000 ALTER TABLE `gen_reply` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2012-08-20 22:02:12
