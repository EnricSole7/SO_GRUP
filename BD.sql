DROP DATABASE IF EXISTS Campeonato;
CREATE DATABASE Campeonato;

USE Campeonato;

CREATE TABLE jugador(
id INTEGER PRIMARY KEY NOT NULL,
nombre VARCHAR(20)
)ENGINE=innoDB;

CREATE TABLE servidor(
id INTEGER PRIMARY KEY NOT NULL,
host_id VARCHAR(20)
)ENGINE=innoDB;

CREATE TABLE partida(
id_j1 INTEGER NOT NULL,
id_j2 INTEGER NOT NULL,
id_s  INTEGER NOT NULL,
fecha VARCHAR(20),
ganador INTEGER NOT NULL,
FOREIGN KEY (id_j1) REFERENCES jugador(id),
FOREIGN KEY (id_j2) REFERENCES jugador(id),
FOREIGN KEY (id_s) REFERENCES servidor(id),
FOREIGN KEY (ganador) REFERENCES jugador(id)
)ENGINE=innoDB;

INSERT INTO jugador VALUES (1, 'Juan');
INSERT INTO jugador VALUES (2, 'Maria');
INSERT INTO jugador VALUES (3, 'Lucas');
INSERT INTO jugador VALUES (4, 'Maya');


INSERT INTO servidor VALUES (1, 'Barcelona');
INSERT INTO servidor VALUES (2, 'Madrid');
INSERT INTO servidor VALUES (3, 'Sevilla');
INSERT INTO servidor VALUES (4, 'Tarragona');


INSERT INTO partida  VALUES (1,3,1, '10/11/2023',3);
INSERT INTO partida  VALUES (1,2,2, '04/12/2023',1);
INSERT INTO partida  VALUES (1,4,4, '12/01/2024',4);
INSERT INTO partida  VALUES (2,3,4, '29/02/2024',2);
INSERT INTO partida  VALUES (2,4,2, '07/06/2023',2);
INSERT INTO partida  VALUES (4,3,3, '02/03/2024',4);
INSERT INTO partida  VALUES (3,2,1, '30/01/2024',3);