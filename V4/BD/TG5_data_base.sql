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
id_j3 INTEGER NOT NULL,
id_j4 INTEGER NOT NULL,
id_j5 INTEGER NOT NULL,
id_s  INTEGER NOT NULL,
fecha VARCHAR(30),
ronda INTEGER,
ended INTEGER,
started INTEGER,
form_j1 INTEGER,
form_j2 INTEGER,
form_j3 INTEGER,
form_j4 INTEGER,
form_j5 INTEGER,
FOREIGN KEY (id_j1) REFERENCES Player(id),
FOREIGN KEY (id_j2) REFERENCES Player(id),
FOREIGN KEY (id_j3) REFERENCES Player(id),
FOREIGN KEY (id_j4) REFERENCES Player(id),
FOREIGN KEY (id_j5) REFERENCES Player(id),
FOREIGN KEY (id_s) REFERENCES Server(id)
)ENGINE=innoDB;

CREATE TABLE Connected(
conectado VARCHAR(20)
)ENGINE=innoDB;

CREATE TABLE Time(
month VARCHAR(4),
referencenumber INTEGER,
weekday VARCHAR(4)
)ENGINE=innoDB;

INSERT INTO Player VALUES (1, 'HOST','DEV');
INSERT INTO Player VALUES (2, 'Maria','M12aria34');
INSERT INTO Player VALUES (3, 'Lucas','L012345');
INSERT INTO Player VALUES (4, 'Maya','Pineda1415.');
INSERT INTO Player VALUES (5, 'Juan','Juanito14?');


INSERT INTO Server VALUES (1, 'Barcelona');
INSERT INTO Server VALUES (2, 'Madrid');
INSERT INTO Server VALUES (3, 'Sevilla');
INSERT INTO Server VALUES (4, 'Tarragona');


INSERT INTO Game  VALUES (1,1,1,1,1,1, 'Tue May 11 23:50:00 2021',100,1,1,-1,-1,-1,-1,-1);

INSERT INTO Time  VALUES ('-'  , 00, 'Sun');
INSERT INTO Time  VALUES ('Jan', 01, 'Mon');
INSERT INTO Time  VALUES ('Feb', 02, 'Tue');
INSERT INTO Time  VALUES ('Mar', 03, 'Wed');
INSERT INTO Time  VALUES ('Apr', 04, 'Thu');
INSERT INTO Time  VALUES ('May', 05, 'Fri');
INSERT INTO Time  VALUES ('Jun', 06, 'Sat');
INSERT INTO Time  VALUES ('Jul', 07, '-');
INSERT INTO Time  VALUES ('Aug', 08, '-');
INSERT INTO Time  VALUES ('Sep', 09, '-');
INSERT INTO Time  VALUES ('Oct', 10, '-');
INSERT INTO Time  VALUES ('Nov', 11, '-');
INSERT INTO Time  VALUES ('Dec', 12, '-');





