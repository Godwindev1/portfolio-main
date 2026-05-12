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