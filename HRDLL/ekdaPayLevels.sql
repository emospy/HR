CREATE DATABASE  IF NOT EXISTS `hrdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `hrdb`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: hrdb
-- ------------------------------------------------------
-- Server version	5.5.28

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
-- Table structure for table `ekdapaylevels`
--

DROP TABLE IF EXISTS `ekdapaylevels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ekdapaylevels` (
  `id_ekdaPayLevels` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) DEFAULT NULL,
  `EKDALevel` int(11) DEFAULT NULL,
  `LevelName` varchar(45) DEFAULT NULL,
  `S1Min` int(11) DEFAULT NULL,
  `S1Max` int(11) DEFAULT NULL,
  `S2Min` int(11) DEFAULT NULL,
  `S2Max` int(11) DEFAULT NULL,
  `S3Min` int(11) DEFAULT NULL,
  `S3Max` int(11) DEFAULT NULL,
  `S4Min` int(11) DEFAULT NULL,
  `S4Max` int(11) DEFAULT NULL,
  `S5Min` int(11) DEFAULT NULL,
  `S5Max` int(11) DEFAULT NULL,
  `S6Min` int(11) DEFAULT NULL,
  `S6Max` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_ekdaPayLevels`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ekdapaylevels`
--

LOCK TABLES `ekdapaylevels` WRITE;
/*!40000 ALTER TABLE `ekdapaylevels` DISABLE KEYS */;
INSERT INTO `ekdapaylevels` VALUES (1,1,1,'Ръководно ниво 1',1000,2500,1200,2800,1400,3100,1500,3400,1600,3700,1800,4000),(2,2,2,'Ръководно ниво 2',900,2400,1000,2700,1300,3000,1400,3300,1500,3600,1600,3900),(3,3,3,'Ръководно ниво 3А',700,2300,800,2600,1100,2900,1200,3200,1300,3500,1400,3800),(4,4,3,'Ръководно ниво 3Б',670,2200,770,2500,1000,2800,1100,3100,1200,3400,1300,3700),(5,5,3,'Ръководно ниво 3В',630,2150,730,2400,900,2700,1000,3000,1100,3300,1200,3600),(6,6,4,'Ръководно ниво 4А',600,2100,700,2300,800,2600,900,2900,1000,3200,1100,3500),(7,7,4,'Ръководно ниво 4Б',550,2000,650,2200,770,2500,850,2800,950,3100,1050,3400),(8,8,5,'Ръководно ниво 5А',500,1900,600,2100,750,2400,800,2700,900,3000,1000,3300),(9,9,5,'Ръководно ниво 5Б',470,1800,580,1950,740,2300,780,2600,880,2900,980,3200),(10,10,5,'Експертно ниво 1А',450,1700,570,1900,730,2200,770,2500,870,2800,970,3100),(11,11,5,'Експертно ниво 1Б',440,1650,560,1850,720,2150,760,2450,860,2750,960,3050),(12,12,6,'Ръководно ниво 6А',430,1600,550,1800,700,2100,750,2400,850,2700,950,3000),(13,13,6,'Ръководно ниво 6Б',420,1550,530,1750,680,2050,730,2350,830,2650,930,2950),(14,14,6,'Експертно ниво 2',410,1500,510,1700,660,2000,710,2300,810,2600,910,2900),(15,15,7,'Ръководно ниво 7А',400,1450,500,1650,650,1950,700,2250,800,2550,900,2850),(16,16,7,'Ръководно ниво 7Б',390,1400,480,1600,630,1900,680,2200,780,2500,880,2800),(17,17,7,'Експертно ниво 3',380,1350,460,1550,610,1850,660,2150,760,2450,860,2750),(18,18,8,'Ръководно ниво 8',370,1300,450,1500,600,1800,650,2100,750,2400,850,2700),(19,19,8,'Експертно ниво 4',360,1250,420,1450,570,1750,620,2050,720,2350,820,2650),(20,20,9,'Експертно ниво 5',350,1200,400,1400,550,1700,600,2000,700,2300,800,2600),(21,21,10,'Експертно ниво 6',340,1150,350,1300,500,1600,550,1900,600,2200,700,2500),(22,22,11,'Експертно ниво 7',335,1100,340,1200,450,1500,500,1800,550,2100,650,2400),(23,23,11,'Ниво специалист 1',315,700,330,850,420,1050,470,1200,520,1400,620,1600),(24,24,12,'Ниво стажант',310,650,320,800,400,1000,450,1150,500,1300,600,1500),(25,25,12,'Ниво специалист 2',310,600,310,750,370,950,420,1100,470,1250,570,1400),(26,26,13,'Ниво специалист 3',310,580,310,700,350,900,400,1050,450,1200,550,1300),(27,27,13,'Ниво сътрудник',310,550,310,650,310,800,350,950,400,1100,500,1200),(28,28,14,'Ниво изпълнител',310,500,310,600,310,750,310,900,350,1000,450,1100);
/*!40000 ALTER TABLE `ekdapaylevels` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-04-15  1:04:36
