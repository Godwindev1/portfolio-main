CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `CaseStudies` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CoverImageUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Category` longtext CHARACTER SET utf8mb4 NOT NULL,
        `DisplayOrder` int NOT NULL,
        `IsFeatured` tinyint(1) NOT NULL,
        `Label` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Summary` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ProblemJson` json NOT NULL,
        `SolutionJson` json NOT NULL,
        CONSTRAINT `PK_CaseStudies` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `Certifications` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Icon` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Grade` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Year` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Provider` longtext CHARACTER SET utf8mb4 NOT NULL,
        `BadgeUrl` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Certifications` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `Experiences` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Period` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Role` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Company` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Tags` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Responsibilities` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Experiences` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `ProjectBriefs` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ProjectName` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ProjectScope` longtext CHARACTER SET utf8mb4 NOT NULL,
        `RoleNeeded` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Timeline` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Budget` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ProjectDescription` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Quote` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_ProjectBriefs` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `SkillDomains` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Number` int NOT NULL,
        `Icon` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_SkillDomains` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `Testimonials` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Quote` longtext CHARACTER SET utf8mb4 NOT NULL,
        `AuthorInitials` longtext CHARACTER SET utf8mb4 NOT NULL,
        `AuthorName` longtext CHARACTER SET utf8mb4 NOT NULL,
        `AuthorTitle` longtext CHARACTER SET utf8mb4 NOT NULL,
        `TestimonialLink` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Testimonials` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `ArchitectureComponents` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CaseStudyId` int NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Role` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Tech` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_ArchitectureComponents` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_ArchitectureComponents_CaseStudies_CaseStudyId` FOREIGN KEY (`CaseStudyId`) REFERENCES `CaseStudies` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `ArtifactLinks` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CaseStudyId` int NOT NULL,
        `Label` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Url` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Type` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_ArtifactLinks` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_ArtifactLinks_CaseStudies_CaseStudyId` FOREIGN KEY (`CaseStudyId`) REFERENCES `CaseStudies` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `CaseStudySkills` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CaseStudyId` int NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Category` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_CaseStudySkills` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_CaseStudySkills_CaseStudies_CaseStudyId` FOREIGN KEY (`CaseStudyId`) REFERENCES `CaseStudies` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `ImplementationSteps` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CaseStudyId` int NOT NULL,
        `Order` int NOT NULL,
        `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_ImplementationSteps` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_ImplementationSteps_CaseStudies_CaseStudyId` FOREIGN KEY (`CaseStudyId`) REFERENCES `CaseStudies` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `Metrics` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CaseStudyId` int NOT NULL,
        `Label` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Value` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_Metrics` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Metrics_CaseStudies_CaseStudyId` FOREIGN KEY (`CaseStudyId`) REFERENCES `CaseStudies` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE TABLE `SkillItem` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Level` longtext CHARACTER SET utf8mb4 NOT NULL,
        `SkillDomainId` int NOT NULL,
        CONSTRAINT `PK_SkillItem` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SkillItem_SkillDomains_SkillDomainId` FOREIGN KEY (`SkillDomainId`) REFERENCES `SkillDomains` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_ArchitectureComponents_CaseStudyId` ON `ArchitectureComponents` (`CaseStudyId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_ArtifactLinks_CaseStudyId` ON `ArtifactLinks` (`CaseStudyId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_CaseStudies_DisplayOrder` ON `CaseStudies` (`DisplayOrder`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_CaseStudySkills_CaseStudyId` ON `CaseStudySkills` (`CaseStudyId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_ImplementationSteps_CaseStudyId_Order` ON `ImplementationSteps` (`CaseStudyId`, `Order`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_Metrics_CaseStudyId` ON `Metrics` (`CaseStudyId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    CREATE INDEX `IX_SkillItem_SkillDomainId` ON `SkillItem` (`SkillDomainId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260511180919_Portfolio') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260511180919_Portfolio', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

