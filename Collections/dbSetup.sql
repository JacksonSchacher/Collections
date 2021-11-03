CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'keep Id',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) COMMENT 'Creator Id',
  name varchar(255) NOT NULL COMMENT 'Keep Name',
  description TEXT COMMENT 'Keep Description',
  img TEXT NOT NULL COMMENT 'Keep Img',
  views int COMMENT 'Keep Views',
  shares int COMMENT 'Keep Shares',
  keeps int COMMENT 'Keeps in Vault',
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO keeps(name, description, img)
VALUES("Test Keep Name", "Test Keep Description", "https://placehoder.com");

SELECT * FROM vaults WHERE id = 5;

CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'vault Id',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name varchar(255) NOT NULL COMMENT 'vault Name',
  description TEXT COMMENT 'vault Description',
  isPrivate TINYINT DEFAULT 0 COMMENT 'is vault private?',
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'vaultKeep Id',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(vaultId) REFERENCES vaults(id),
  FOREIGN KEY(keepId) REFERENCES keeps(id) 
) default charset utf8 COMMENT '';

SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a on a.id = v.creatorId
      WHERE v.id = 39;

SELECT * FROM vaults;

ALTER TABLE vaults
ADD img TEXT COMMENT 'vault image';

SET FOREIGN_KEY_CHECKS = 0; 
TRUNCATE table vaults; 
TRUNCATE TABLE keeps;
TRUNCATE TABLE vaultKeeps;
SET FOREIGN_KEY_CHECKS = 1;