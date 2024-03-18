int main(int argc, char **argv)
{
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char nombre[20];
	
	int  id;
	char host[20];
	char consulta [80];
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi￯﾿ﾃ￯ﾾﾳn: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Base",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexi￯﾿ﾃ￯ﾾﾳn: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	// consulta SQL para obtener una tabla con todos los datos
	// de la base de datos
	err=mysql_query (conn, "SELECT * FROM jugador;");
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//recogemos el resultado de la consulta. El resultado de la
	//consulta se devuelve en una variable del tipo puntero a
	//MYSQL_RES tal y como hemos declarado anteriormente.
	//Se trata de una tabla virtual en memoria que es la copia
	//de la tabla real en disco.
	resultado = mysql_store_result (conn);
	// El resultado es una estructura matricial en memoria
	// en la que cada fila contiene los datos de una persona.
	// Ahora obtenemos la primera fila que se almacena en una
	// variable de tipo MYSQL_ROW
	row = mysql_fetch_row (resultado);
	// En una fila hay tantas columnas como datos tiene una
	// persona. En nuestro caso hay tres columnas: dni(row[0]),
	// nombre(row[1]) y edad (row[2]).
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		while (row !=NULL) {
			// la columna 2 contiene una palabra que es la edad
			// la convertimos a entero 
			id = atoi (row[0]);
			
			// las columnas 0 y 1 contienen DNI y nombre
			printf ("ID:%s Nombre:%s Contrase￱a:%s\n",row[0],row[1],row[2]);
			// obtenemos la siguiente fil
			row = mysql_fetch_row (resultado);
	}
		// Ahora vamos a buscar el nombre de la persona cuyo DNI es uno
		// dado por el usuario
		printf ("Dame el nombre del jugador que ha ganado en la ciudad que quieres buscar\n"); 
		scanf ("%s",row[0] );
		// construimos la consulta SQL
		strcpy (consulta,""); 
		strcat (consulta, row[0]);
		strcat (consulta,"*");
		// hacemos la consulta
		err=mysql_query (conn, consulta); 
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}