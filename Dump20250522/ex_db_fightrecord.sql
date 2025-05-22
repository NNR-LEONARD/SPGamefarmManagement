CREATE DATABASE  IF NOT EXISTS `ex_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ex_db`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: ex_db
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `fightrecord`
--

DROP TABLE IF EXISTS `fightrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fightrecord` (
  `Fight_ID` int NOT NULL AUTO_INCREMENT,
  `GameFowl_ID` int NOT NULL,
  `OpponentBreed` varchar(100) NOT NULL,
  `Fight_Date` date NOT NULL,
  `Location` varchar(100) NOT NULL,
  `Result` enum('Win','Loss') NOT NULL,
  `Injured` tinyint(1) NOT NULL,
  PRIMARY KEY (`Fight_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fightrecord`
--

LOCK TABLES `fightrecord` WRITE;
/*!40000 ALTER TABLE `fightrecord` DISABLE KEYS */;
INSERT INTO `fightrecord` VALUES (1,8899,'AA GF','2025-05-18','Arena1','Win',0),(2,2,'MINE GF','2025-05-20','Arena 3','Loss',0),(3,2,'MINE GF','2025-05-20','Arena 3','Loss',0),(4,1,'SDG GF','2025-05-21','Arena 7','Loss',1),(5,7,'MINE GF','2025-05-21','Arena 5','Win',0),(6,8,'SSS GF','2025-05-21','Arena 11','Win',0),(7,3,'FRG GF','2025-05-21','Arena 3','Win',0);
/*!40000 ALTER TABLE `fightrecord` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-22 10:23:56
