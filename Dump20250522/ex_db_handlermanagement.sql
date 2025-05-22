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
-- Table structure for table `handlermanagement`
--

DROP TABLE IF EXISTS `handlermanagement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `handlermanagement` (
  `Handler_ID` int NOT NULL AUTO_INCREMENT,
  `Handler_Name` varchar(45) NOT NULL,
  `Handler_Role` varchar(45) NOT NULL,
  `Handler_Salary` varchar(45) NOT NULL,
  PRIMARY KEY (`Handler_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `handlermanagement`
--

LOCK TABLES `handlermanagement` WRITE;
/*!40000 ALTER TABLE `handlermanagement` DISABLE KEYS */;
INSERT INTO `handlermanagement` VALUES (6,'Robert Yie','Waterer','1000000'),(7,'Hanzel Fill','Gaffer','15000.00'),(8,'Leo Jazareno','Conditioning','10000.00'),(9,'Jose Marichan','Waterer','90000.00'),(10,'Tito Vic','Gaffer','89000.00'),(11,'Kuya Will','Feederer','10000.00'),(12,'Francis Bato','Hatcher','12000.00'),(13,'Yuni Koop','Waterer','67000.00'),(14,'Jose Gilbert Lopez','Feederer','89000.00'),(15,'Patrick Manalo','Gaffer','10000'),(17,'Pedro Jose','Waterer','100000.00'),(18,'Leonard Jazareno','Conditioning','10000.00');
/*!40000 ALTER TABLE `handlermanagement` ENABLE KEYS */;
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
