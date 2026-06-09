-- MySQL dump 10.13  Distrib 8.0.46, for Win64 (x86_64)
--
-- Host: mediatekformation-server.mysql.database.azure.com    Database: mediatek86
-- ------------------------------------------------------
-- Server version	8.0.44-azure

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
-- Table structure for table `abonnement`
--

DROP TABLE IF EXISTS `abonnement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `abonnement` (
  `id` varchar(5) NOT NULL,
  `dateFinAbonnement` date DEFAULT NULL,
  `idRevue` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idRevue` (`idRevue`),
  CONSTRAINT `abonnement_ibfk_1` FOREIGN KEY (`id`) REFERENCES `commande` (`id`) ON DELETE CASCADE,
  CONSTRAINT `abonnement_ibfk_2` FOREIGN KEY (`idRevue`) REFERENCES `revue` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `abonnement`
--

LOCK TABLES `abonnement` WRITE;
/*!40000 ALTER TABLE `abonnement` DISABLE KEYS */;
INSERT INTO `abonnement` VALUES ('99006','2022-12-31','10002'),('99007','2024-12-31','10001'),('99008','2026-06-17','10002');
/*!40000 ALTER TABLE `abonnement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `abonnementexpiration`
--

DROP TABLE IF EXISTS `abonnementexpiration`;
/*!50001 DROP VIEW IF EXISTS `abonnementexpiration`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `abonnementexpiration` AS SELECT 
 1 AS `titre`,
 1 AS `dateFinAbonnement`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `commande`
--

DROP TABLE IF EXISTS `commande`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commande` (
  `id` varchar(5) NOT NULL,
  `dateCommande` date DEFAULT NULL,
  `montant` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commande`
--

LOCK TABLES `commande` WRITE;
/*!40000 ALTER TABLE `commande` DISABLE KEYS */;
INSERT INTO `commande` VALUES ('00001','2026-06-08',50),('00003','2026-05-30',0),('10000','2026-06-09',25),('99001','2024-03-01',45.9),('99002','2024-01-15',89.5),('99003','2023-11-10',120),('99005','2023-09-05',35),('99006','2022-01-01',180),('99007','2024-01-01',199.9),('99008','2025-06-17',215),('99009','2026-06-09',67),('99010','2026-06-09',9),('99011','2026-06-09',10),('99012','2026-06-09',10);
/*!40000 ALTER TABLE `commande` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandedocument`
--

DROP TABLE IF EXISTS `commandedocument`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandedocument` (
  `id` varchar(5) NOT NULL,
  `nbExemplaire` int DEFAULT NULL,
  `idLivreDvd` varchar(10) NOT NULL,
  `idSuivi` varchar(5) NOT NULL DEFAULT '00001',
  PRIMARY KEY (`id`),
  KEY `idLivreDvd` (`idLivreDvd`),
  KEY `commandedocument_ibfk_3` (`idSuivi`),
  CONSTRAINT `commandedocument_ibfk_1` FOREIGN KEY (`id`) REFERENCES `commande` (`id`),
  CONSTRAINT `commandedocument_ibfk_2` FOREIGN KEY (`idLivreDvd`) REFERENCES `livres_dvd` (`id`),
  CONSTRAINT `commandedocument_ibfk_3` FOREIGN KEY (`idSuivi`) REFERENCES `suivi` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandedocument`
--

LOCK TABLES `commandedocument` WRITE;
/*!40000 ALTER TABLE `commandedocument` DISABLE KEYS */;
INSERT INTO `commandedocument` VALUES ('00001',1,'20003','00004'),('00003',1,'20003','00004'),('99001',2,'00001','00001'),('99002',3,'00002','00003'),('99003',1,'00001','00004'),('99005',2,'20002','00004'),('99009',1,'20004','00003'),('99010',1,'20004','00004'),('99011',1,'00003','00001'),('99012',1,'00017','00004');
/*!40000 ALTER TABLE `commandedocument` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `document`
--

DROP TABLE IF EXISTS `document`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `document` (
  `id` varchar(10) NOT NULL,
  `titre` varchar(60) DEFAULT NULL,
  `image` varchar(500) DEFAULT NULL,
  `idRayon` varchar(5) NOT NULL,
  `idPublic` varchar(5) NOT NULL,
  `idGenre` varchar(5) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idRayon` (`idRayon`),
  KEY `idPublic` (`idPublic`),
  KEY `idGenre` (`idGenre`),
  CONSTRAINT `document_ibfk_1` FOREIGN KEY (`idRayon`) REFERENCES `rayon` (`id`),
  CONSTRAINT `document_ibfk_2` FOREIGN KEY (`idPublic`) REFERENCES `public` (`id`),
  CONSTRAINT `document_ibfk_3` FOREIGN KEY (`idGenre`) REFERENCES `genre` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `document`
--

LOCK TABLES `document` WRITE;
/*!40000 ALTER TABLE `document` DISABLE KEYS */;
INSERT INTO `document` VALUES ('00001','Quand sort la recluse','','LV003','00002','10014'),('00002','Un pays à l\'aube','','LV001','00002','10004'),('00003','Et je danse aussi','','LV002','00003','10013'),('00004','L\'armée furieuse','','LV003','00002','10014'),('00005','Les anonymes','','LV001','00002','10014'),('00006','La marque jaune','','BD001','00003','10001'),('00007','Dans les coulisses du musée','','LV001','00003','10006'),('00008','Histoire du juif errant','','LV002','00002','10006'),('00009','Pars vite et reviens tard','','LV003','00002','10014'),('00010','Le vestibule des causes perdues','','LV001','00002','10006'),('00011','L\'île des oubliés','','LV002','00003','10006'),('00012','La souris bleue','','LV002','00003','10006'),('00013','Sacré Pêre Noël','','JN001','00001','10001'),('00014','Mauvaise étoile','','LV003','00003','10014'),('00015','La confrérie des téméraires','','JN002','00004','10014'),('00016','Le butin du requin','','JN002','00004','10014'),('00017','Catastrophes au Brésil','','JN002','00004','10014'),('00018','Le Routard - Maroc','','DV005','00003','10011'),('00019','Guide Vert - Iles Canaries','','DV005','00003','10011'),('00020','Guide Vert - Irlande','','DV005','00003','10011'),('00021','Les déferlantes','','LV002','00002','10006'),('00022','Une part de Ciel','','LV002','00002','10006'),('00023','Le secret du janissaire','','BD001','00002','10001'),('00024','Pavillon noir','','BD001','00002','10001'),('00025','L\'archipel du danger','','BD001','00002','10001'),('00026','La planète des singes','','LV002','00003','10002'),('10001','Arts Magazine','','PR002','00002','10016'),('10002','Alternatives Economiques','','PR002','00002','10015'),('10003','Challenges','','PR002','00002','10015'),('10004','Rock and Folk','','PR002','00002','10016'),('10005','Les Echos','','PR001','00002','10015'),('10006','Le Monde','','PR001','00002','10018'),('10007','Telerama','','PR002','00002','10016'),('10008','L\'Obs','','PR002','00002','10018'),('10009','L\'Equipe','','PR001','00002','10017'),('10010','L\'Equipe Magazine','','PR002','00002','10017'),('10011','Geo','','PR002','00003','10016'),('20001','Star Wars 5 L\'empire contre attaque','','DF001','00003','10002'),('20002','Le seigneur des anneaux : la communauté de l\'anneau','','DF001','00003','10019'),('20003','Jurassic Park','','DF001','00003','10002'),('20004','Matrix','','DF001','00003','10002');
/*!40000 ALTER TABLE `document` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dvd`
--

DROP TABLE IF EXISTS `dvd`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dvd` (
  `id` varchar(10) NOT NULL,
  `synopsis` text,
  `realisateur` varchar(20) DEFAULT NULL,
  `duree` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `dvd_ibfk_1` FOREIGN KEY (`id`) REFERENCES `livres_dvd` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dvd`
--

LOCK TABLES `dvd` WRITE;
/*!40000 ALTER TABLE `dvd` DISABLE KEYS */;
INSERT INTO `dvd` VALUES ('20001','Luc est entraîné par Yoda pendant que Han et Leia tentent de se cacher dans la cité des nuages.','George Lucas',124),('20002','L\'anneau unique, forgé par Sauron, est porté par Fraudon qui l\'amène à Foncombe. De là, des représentants de peuples différents vont s\'unir pour aider Fraudon à amener l\'anneau à la montagne du Destin.','Peter Jackson',228),('20003','Un milliardaire et des généticiens créent des dinosaures à partir de clonage.','Steven Spielberg',128),('20004','Un informaticien réalise que le monde dans lequel il vit est une simulation gérée par des machines.','Les Wachowski',136);
/*!40000 ALTER TABLE `dvd` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `etat`
--

DROP TABLE IF EXISTS `etat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `etat` (
  `id` char(5) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `etat`
--

LOCK TABLES `etat` WRITE;
/*!40000 ALTER TABLE `etat` DISABLE KEYS */;
INSERT INTO `etat` VALUES ('00001','neuf'),('00002','usagé'),('00003','détérioré'),('00004','inutilisable');
/*!40000 ALTER TABLE `etat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exemplaire`
--

DROP TABLE IF EXISTS `exemplaire`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exemplaire` (
  `id` varchar(10) NOT NULL,
  `numero` int NOT NULL,
  `dateAchat` date DEFAULT NULL,
  `photo` varchar(500) NOT NULL,
  `idEtat` char(5) NOT NULL,
  PRIMARY KEY (`id`,`numero`),
  KEY `idEtat` (`idEtat`),
  CONSTRAINT `exemplaire_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`),
  CONSTRAINT `exemplaire_ibfk_2` FOREIGN KEY (`idEtat`) REFERENCES `etat` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exemplaire`
--

LOCK TABLES `exemplaire` WRITE;
/*!40000 ALTER TABLE `exemplaire` DISABLE KEYS */;
INSERT INTO `exemplaire` VALUES ('00017',1,'2026-06-09','','00001'),('10001',9999,'2024-06-15','','00001'),('10002',418,'2021-12-01','','00001'),('10007',3237,'2021-11-23','','00001'),('10007',3238,'2021-11-30','','00001'),('10007',3239,'2021-12-07','','00001'),('10007',3240,'2021-12-21','','00001'),('10007',3241,'2026-05-30','','00001'),('10011',505,'2022-10-16','','00001'),('10011',506,'2021-04-01','','00001'),('10011',507,'2021-05-03','','00001'),('10011',508,'2021-06-05','','00001'),('10011',509,'2021-07-01','','00001'),('10011',510,'2021-08-04','','00001'),('10011',511,'2021-09-01','','00001'),('10011',512,'2021-10-06','','00001'),('10011',513,'2021-11-01','','00001'),('10011',514,'2021-12-01','','00001'),('20003',1,'2026-05-30','','00001'),('20003',2,'2026-06-08','','00001'),('20004',1,'2026-06-09','','00001'),('20004',2,'2026-06-09','','00001');
/*!40000 ALTER TABLE `exemplaire` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
INSERT INTO `genre` VALUES ('10000','Humour'),('10001','Bande dessinée'),('10002','Science Fiction'),('10003','Biographie'),('10004','Historique'),('10006','Roman'),('10007','Aventures'),('10008','Essai'),('10009','Documentaire'),('10010','Technique'),('10011','Voyages'),('10012','Drame'),('10013','Comédie'),('10014','Policier'),('10015','Presse Economique'),('10016','Presse Culturelle'),('10017','Presse sportive'),('10018','Actualités'),('10019','Fantazy');
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livre`
--

DROP TABLE IF EXISTS `livre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livre` (
  `id` varchar(10) NOT NULL,
  `ISBN` varchar(13) DEFAULT NULL,
  `auteur` varchar(20) DEFAULT NULL,
  `collection` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `livre_ibfk_1` FOREIGN KEY (`id`) REFERENCES `livres_dvd` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livre`
--

LOCK TABLES `livre` WRITE;
/*!40000 ALTER TABLE `livre` DISABLE KEYS */;
INSERT INTO `livre` VALUES ('00001','1234569877896','Fred Vargas','Commissaire Adamsberg'),('00002','1236547896541','Dennis Lehanne',''),('00003','6541236987410','Anne-Laure Bondoux',''),('00004','3214569874123','Fred Vargas','Commissaire Adamsberg'),('00005','3214563214563','RJ Ellory',''),('00006','3213213211232','Edgar P. Jacobs','Blake et Mortimer'),('00007','6541236987541','Kate Atkinson',''),('00008','1236987456321','Jean d\'Ormesson',''),('00009','','Fred Vargas','Commissaire Adamsberg'),('00010','','Manon Moreau',''),('00011','','Victoria Hislop',''),('00012','','Kate Atkinson',''),('00013','','Raymond Briggs',''),('00014','','RJ Ellory',''),('00015','','Floriane Turmeau',''),('00016','','Julian Press',''),('00017','','Philippe Masson',''),('00018','','','Guide du Routard'),('00019','','','Guide Vert'),('00020','','','Guide Vert'),('00021','','Claudie Gallay',''),('00022','','Claudie Gallay',''),('00023','','Ayrolles - Masbou','De cape et de crocs'),('00024','','Ayrolles - Masbou','De cape et de crocs'),('00025','','Ayrolles - Masbou','De cape et de crocs'),('00026','','Pierre Boulle','Julliard');
/*!40000 ALTER TABLE `livre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livres_dvd`
--

DROP TABLE IF EXISTS `livres_dvd`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livres_dvd` (
  `id` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `livres_dvd_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livres_dvd`
--

LOCK TABLES `livres_dvd` WRITE;
/*!40000 ALTER TABLE `livres_dvd` DISABLE KEYS */;
INSERT INTO `livres_dvd` VALUES ('00001'),('00002'),('00003'),('00004'),('00005'),('00006'),('00007'),('00008'),('00009'),('00010'),('00011'),('00012'),('00013'),('00014'),('00015'),('00016'),('00017'),('00018'),('00019'),('00020'),('00021'),('00022'),('00023'),('00024'),('00025'),('00026'),('20001'),('20002'),('20003'),('20004');
/*!40000 ALTER TABLE `livres_dvd` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `nextcommandeid`
--

DROP TABLE IF EXISTS `nextcommandeid`;
/*!50001 DROP VIEW IF EXISTS `nextcommandeid`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `nextcommandeid` AS SELECT 
 1 AS `nextId`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `public`
--

DROP TABLE IF EXISTS `public`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `public` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `public`
--

LOCK TABLES `public` WRITE;
/*!40000 ALTER TABLE `public` DISABLE KEYS */;
INSERT INTO `public` VALUES ('00001','Jeunesse'),('00002','Adultes'),('00003','Tous publics'),('00004','Ados');
/*!40000 ALTER TABLE `public` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rayon`
--

DROP TABLE IF EXISTS `rayon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rayon` (
  `id` char(5) NOT NULL,
  `libelle` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rayon`
--

LOCK TABLES `rayon` WRITE;
/*!40000 ALTER TABLE `rayon` DISABLE KEYS */;
INSERT INTO `rayon` VALUES ('BD001','BD Adultes'),('BL001','Beaux Livres'),('DF001','DVD films'),('DV001','Sciences'),('DV002','Maison'),('DV003','Santé'),('DV004','Littérature classique'),('DV005','Voyages'),('JN001','Jeunesse BD'),('JN002','Jeunesse romans'),('LV001','Littérature étrangère'),('LV002','Littérature française'),('LV003','Policiers français étrangers'),('PR001','Presse quotidienne'),('PR002','Magazines');
/*!40000 ALTER TABLE `rayon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `revue`
--

DROP TABLE IF EXISTS `revue`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `revue` (
  `id` varchar(10) NOT NULL,
  `periodicite` varchar(2) DEFAULT NULL,
  `delaiMiseADispo` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `revue_ibfk_1` FOREIGN KEY (`id`) REFERENCES `document` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `revue`
--

LOCK TABLES `revue` WRITE;
/*!40000 ALTER TABLE `revue` DISABLE KEYS */;
INSERT INTO `revue` VALUES ('10001','MS',52),('10002','MS',52),('10003','HB',15),('10004','HB',15),('10005','QT',5),('10006','QT',5),('10007','HB',26),('10008','HB',26),('10009','QT',5),('10010','HB',12),('10011','MS',52);
/*!40000 ALTER TABLE `revue` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `service` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service`
--

LOCK TABLES `service` WRITE;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` VALUES ('00001','Diffusion'),('00002','Prêt'),('00003','Culture');
/*!40000 ALTER TABLE `service` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suivi`
--

DROP TABLE IF EXISTS `suivi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suivi` (
  `id` varchar(5) NOT NULL,
  `libelle` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suivi`
--

LOCK TABLES `suivi` WRITE;
/*!40000 ALTER TABLE `suivi` DISABLE KEYS */;
INSERT INTO `suivi` VALUES ('00001','En cours'),('00002','Relancee'),('00003','Livree'),('00004','Reglee');
/*!40000 ALTER TABLE `suivi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `tousabonnements`
--

DROP TABLE IF EXISTS `tousabonnements`;
/*!50001 DROP VIEW IF EXISTS `tousabonnements`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tousabonnements` AS SELECT 
 1 AS `id`,
 1 AS `dateCommande`,
 1 AS `montant`,
 1 AS `dateFinAbonnement`,
 1 AS `idRevue`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `toutescommandesdocuments`
--

DROP TABLE IF EXISTS `toutescommandesdocuments`;
/*!50001 DROP VIEW IF EXISTS `toutescommandesdocuments`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `toutescommandesdocuments` AS SELECT 
 1 AS `id`,
 1 AS `dateCommande`,
 1 AS `montant`,
 1 AS `nbExemplaire`,
 1 AS `idLivreDvd`,
 1 AS `idSuivi`,
 1 AS `libelleSuivi`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utilisateur` (
  `login` varchar(50) NOT NULL,
  `pwd` varchar(50) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) NOT NULL,
  `idService` varchar(5) NOT NULL,
  PRIMARY KEY (`login`),
  KEY `fk_utilisateur_service` (`idService`),
  CONSTRAINT `fk_utilisateur_service` FOREIGN KEY (`idService`) REFERENCES `service` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utilisateur`
--

LOCK TABLES `utilisateur` WRITE;
/*!40000 ALTER TABLE `utilisateur` DISABLE KEYS */;
INSERT INTO `utilisateur` VALUES ('admin','adminpwd','Admin','Super','00001'),('dupont','dupont','Dupont','Marie','00002'),('martin','martin','Martin','Jean','00003');
/*!40000 ALTER TABLE `utilisateur` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `abonnementexpiration`
--

/*!50001 DROP VIEW IF EXISTS `abonnementexpiration`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kwtthxwsiu`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `abonnementexpiration` AS select `d`.`titre` AS `titre`,`a`.`dateFinAbonnement` AS `dateFinAbonnement` from (`abonnement` `a` join `document` `d` on((`a`.`idRevue` = `d`.`id`))) where (`a`.`dateFinAbonnement` between curdate() and (curdate() + interval 30 day)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `nextcommandeid`
--

/*!50001 DROP VIEW IF EXISTS `nextcommandeid`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kwtthxwsiu`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `nextcommandeid` AS select lpad(cast((max(cast(`commande`.`id` as unsigned)) + 1) as char charset utf8mb4),5,'0') AS `nextId` from `commande` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tousabonnements`
--

/*!50001 DROP VIEW IF EXISTS `tousabonnements`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kwtthxwsiu`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tousabonnements` AS select `a`.`id` AS `id`,`c`.`dateCommande` AS `dateCommande`,`c`.`montant` AS `montant`,`a`.`dateFinAbonnement` AS `dateFinAbonnement`,`a`.`idRevue` AS `idRevue` from (`abonnement` `a` join `commande` `c` on((`a`.`id` = `c`.`id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `toutescommandesdocuments`
--

/*!50001 DROP VIEW IF EXISTS `toutescommandesdocuments`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kwtthxwsiu`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `toutescommandesdocuments` AS select `cd`.`id` AS `id`,`c`.`dateCommande` AS `dateCommande`,`c`.`montant` AS `montant`,`cd`.`nbExemplaire` AS `nbExemplaire`,`cd`.`idLivreDvd` AS `idLivreDvd`,`cd`.`idSuivi` AS `idSuivi`,`s`.`libelle` AS `libelleSuivi` from ((`commandedocument` `cd` join `commande` `c` on((`cd`.`id` = `c`.`id`))) join `suivi` `s` on((`cd`.`idSuivi` = `s`.`id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-09 12:45:28
