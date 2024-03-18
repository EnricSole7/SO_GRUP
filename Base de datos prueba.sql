DROP DATABASE IF EXISTS Base;
CREATE DATABASE Base;

USE Base;

CREATE TABLE jugador(
id INTEGER PRIMARY KEY NOT NULL,
nombre VARCHAR(20),
contraseña VARCHAR(20)

)ENGINE=innoDB;

INSERT INTO jugador VALUES (1, 'Juan','Juanito14?');
INSERT INTO jugador VALUES (2, 'Maria','M12aria34');
INSERT INTO jugador VALUES (3, 'Lucas','L012345');
INSERT INTO jugador VALUES (4, 'Maya','Pineda1415.');