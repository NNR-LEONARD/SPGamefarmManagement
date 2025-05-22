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
-- Table structure for table `fightrecords`
--

DROP TABLE IF EXISTS `fightrecords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fightrecords` (
  `Fight_ID` int NOT NULL,
  `GameFowl_ID` int NOT NULL,
  `OpponentBreed` varchar(45) NOT NULL,
  `Fight_Date` date NOT NULL,
  `Location` varchar(45) NOT NULL,
  `Result` varchar(45) NOT NULL,
  `Injured` tinyint NOT NULL,
  PRIMARY KEY (`Fight_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fightrecords`
--

LOCK TABLES `fightrecords` WRITE;
/*!40000 ALTER TABLE `fightrecords` DISABLE KEYS */;
INSERT INTO `fightrecords` VALUES (1,100,'Kelso','2025-01-05','Arena1','Win',0),(2,101,'Sweater','2025-01-05','Arena1','Loss',1),(3,102,'Sweater','2025-01-05','Arena1','Win',0),(4,103,'Claret','2025-01-05','Arena1','Win',1),(5,104,'Kelso','2025-01-05','Arena1','Win',0),(6,105,'Sweater','2025-01-10','Arena2','Loss',1),(7,106,'Kelso','2025-01-10','Arena2','Win',0),(8,107,'Sweater','2025-01-10','Arena2','Win',0),(9,108,'Claret','2025-01-10','Arena2','Win',0),(10,109,'Sweater','2025-01-10','Arena2','Win',0),(11,201,'Kelso','2024-02-01','Arena1','Win',0);
/*!40000 ALTER TABLE `fightrecords` ENABLE KEYS */;
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
