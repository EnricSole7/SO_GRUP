DROP DATABASE IF EXISTS TG5_data_base;
CREATE DATABASE TG5_data_base;

USE TG5_data_base;

CREATE TABLE Player(
id INTEGER PRIMARY KEY NOT NULL,
nombre VARCHAR(20),
contrasenya VARCHAR(20)
)ENGINE=innoDB;

CREATE TABLE Server(
id INTEGER PRIMARY KEY NOT NULL,
host_id VARCHAR(20)
)ENGINE=innoDB;

CREATE TABLE Game(
id_j1 INTEGER NOT NULL,
id_j2 INTEGER NOT NULL,
id_s  INTEGER NOT NULL,
fecha VARCHAR(20),
minijuego VARCHAR(30),
FOREIGN KEY (id_j1) REFERENCES Player(id),
FOREIGN KEY (id_j2) REFERENCES Player(id),
FOREIGN KEY (id_s) REFERENCES Server(id)
)ENGINE=innoDB;

CREATE TABLE Connected(
conectado VARCHAR(20)
)ENGINE=innoDB;

INSERT INTO Player VALUES (1, 'Juan','Juanito14?');
INSERT INTO Player VALUES (2, 'Maria','M12aria34');
INSERT INTO Player VALUES (3, 'Lucas','L012345');
INSERT INTO Player VALUES (4, 'Maya','Pineda1415.');


INSERT INTO Server VALUES (1, 'Barcelona');
INSERT INTO Server VALUES (2, 'Madrid');
INSERT INTO Server VALUES (3, 'Sevilla');
INSERT INTO Server VALUES (4, 'Tarragona');


INSERT INTO Game  VALUES (1,3,1, '10/11/2023','TBD');
INSERT INTO Game  VALUES (1,2,2, '04/12/2023','SYMBOLS');
INSERT INTO Game  VALUES (1,4,4, '12/01/2024','SYMBOLS');
INSERT INTO Game  VALUES (2,3,4, '29/02/2024','MAZE');
INSERT INTO Game  VALUES (2,4,2, '07/06/2023','MAZE');
INSERT INTO Game  VALUES (4,3,3, '02/03/2024','TBD');
INSERT INTO Game  VALUES (3,2,1, '30/01/2024','TBD');
