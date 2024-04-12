#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

int contador_servicios;

//Estructura de dades d'accés "excluyente"
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//codigo del thread
void *AtenderCliente (void *socket)
{
	int socket_conn;
	int *s;
	s = (int *) socket;
	socket_conn = *s;
	
	int ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	char request[512];
	int err;
	
	MYSQL *conn;
	// Estructura especial para almacenar resultados de consultas
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BD",0, NULL, 0);
	//MYSQL *mysql, char *host, char *user, char *passwd, char *db, int port, char *unix_socket, long client_flag
	
	if (conn==NULL) 
	{
		printf ("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	printf("Base de datos conectada\n");
	
	int terminar=0;
	//bucle per atendre totes les peticions d'aquest client fins que es desconnecti
	while (terminar ==0)
	{
		// Ahora recibimos su nombre, que dejamos en buff
		ret=read(socket_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		//Escribimos el nombre en la consola
		
		printf ("Se ha conectado: %s\n",peticion);
		
		int codigo;
		char nom[30];
		char contra[20];
		int resp;
		
		char *p = strtok(peticion, "/");
		codigo =  atoi (p);	//codigo
		
		
		if (codigo == 1)	//LOGIN
		{
			p= strtok (NULL,"/");
			strcpy(nom,p);		//nombre
			p= strtok (NULL,"/");
			strcpy(contra,p);	//contrasenya
			printf("%d %s %s\n",codigo, nom,contra);
			
			resp=login(nom, contra,conn );
			
			printf("%d\n",resp);
			if(resp==0){
				
				sprintf(respuesta, "correcto,");
			}
			else
			   sprintf(respuesta,"incorrecto,");
			printf ("%s\n", respuesta);
			// Y lo enviamos
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		if (codigo == 2)	//REGISTER
		{
			p= strtok (NULL,"/");
			strcpy(nom,p);		//nombre
			p= strtok (NULL,"/");
			strcpy(contra,p);	//contrasenya
			printf("%d %s %s\n",codigo, nom,contra);
			
			resp=registro(nom,contra,conn);
			printf("%d\n",resp);
			if(resp==0){
				
				sprintf(respuesta, "correcto,");
			}
			else
			   sprintf(respuesta, "incorrecto,");
			printf ("%s\n", respuesta);
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		if (codigo == 100)	//SHOW DATABASE
		{
			char basedatos[500];
			char respuesta[500] = "";
			printf("in\n");
			ShowDataBase(respuesta, conn);
			printf("%s\n",respuesta);
			/*
			//COMPROBACION QUE LA BASE DE DATOS NO TIENE TABLAS VACIAS
			char *comp = strtok(basedatos, "/");
			
			if (strcmp(comp, "NO HAY JUGADORES,") == 0)
			{
				str
			}
			*/
			write (socket_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(socket_conn); 
}

//establecer connexion con el servidor + thread segun el socket (varios usuarios)
int main(int argc, char *argv[])
{
	int socket_conn, socket_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((socket_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9050
	serv_adr.sin_port = htons(9080);
	if (bind(socket_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(socket_listen, 3) < 0)
		printf("Error en el Listen\n");
	
	contador_servicios = 0;
	int i;
	int sockets[100];	//vector de sockets
	pthread_t thread;	//estructura de thread
	i = 0;
	// Bucle per atendre peticions
	
	for(;;)
	{
		printf ("Escuchando\n");
		
		socket_conn = accept(socket_listen, NULL, NULL);
		printf ("He recibido conexi?n\n");
		
		sockets[i] = socket_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		//crear thread i dir-li el que s'ha de fer
		
		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);
		i++;
	}
	
	/* //bucle nomes util si el bucle de peticions no és infinit
	for (i = 0; i<5; i++)
	{
		//espera a que acabin tots els threads en marxa
		pthread_join (thread[i], NULL);
	}
	*/
}

int login(char nombre[60], char password[60], MYSQL *conn) {
	//SELECT id_jugador FROM datos_personales WHERE nombre = name;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	strcpy(consulta, "\0");
	
	strcat (consulta, "SELECT * FROM jugador WHERE nombre = '");
	strcat (consulta, nombre);
	strcat (consulta, "'");
	printf("consulta: %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		return -1;
		exit(1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	if(row == NULL)
		return -2; //no existe el usuario al que se intenta iniciar sesion
	else{
		int respuesta;
		//en caso de que haya mas de un usuario con el mismo nombre, entonces comparamos cada uno sus nombres con la contrasena enviada
		while (row!=NULL){
			//en este caso el id sera row[0], el nombre sera row[1], y la contrasena sera row[2]
			if(strcmp(row[2], password) == 0){
				respuesta =0; //el usuario existe y la contrasena coincide
			}else{
				respuesta =  -1; //contrasena incorrecta
			}
			row = mysql_fetch_row (resultado);
		}
		return respuesta; //retornamos la respuesta, entre un -1 o un 0
	}
}

int registro(char nombre[60], char password[60], MYSQL *conn) 
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[512];
	char id2[20];
	int filas;
	int id;
	int new_id;
	//pasamos 'seconds' a un string para insertarlo a la base de datos
	
	// si existe usuario
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
	{
		while (row !=NULL) {
			// la columna 2 contiene una palabra que es la edad
			// la convertimos a entero 
			id = atoi (row[0]);
			
			// las columnas 0 y 1 contienen DNI y nombre
			printf ("ID:%s Nombre:%s Contrasenya:%s\n",row[0],row[1],row[2]);
			filas= atoi(row[0]);
			// obtenemos la siguiente fil
			row = mysql_fetch_row (resultado);
		}
	}	
	
	new_id = filas+1;
	sprintf(id2,"%d",new_id);
	
	sprintf (consulta, "SELECT * FROM jugador WHERE nombre ='%s';", nombre);
	printf("consulta: %s\n", consulta);
	
	
	//aqui error
	err = mysql_query(conn, consulta);
	
	if(err!=0)
	{	
		return -1;
		exit(1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if(row == NULL)
	{
		//INSERT INTO datos_personales(nombre, contrasenya) VALUES ('name', 'password');
		strcpy (consulta,"INSERT INTO jugador (id,nombre,contrasenya) VALUES (");
		
		strcat (consulta, id2);
		strcat (consulta, ",'");
		strcat (consulta, nombre);
		strcat (consulta, "','");
		strcat (consulta, password);
		strcat (consulta,"');");
		printf("%s\n", consulta);
		err = mysql_query(conn, consulta);
		if(err!=0){
			return -1;
			exit(1);
		}else
		   return 0;
		//retorna (-1) si ha habido un problema y (0) si la consulta ha salido bien
	}
	else
	{
		return -2;
	}
}


void ShowDataBase(char result[500], MYSQL *conn)
{

	char consulta[60];
	char jugadores[100] = "";
	char servers[50] = "";
	char partidas[200] = "";
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	strcpy(consulta, "\0");
	
	//BUSCAMOS PRIMERO TODOS LOS JUGADORES DE LA LISTA
	sprintf(consulta, "SELECT * FROM jugador;");
	printf("%s\n", consulta);
	err = mysql_query(conn, consulta);
	if(err!=0){
		sprintf(result, "NO HAY JUGADORES,");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		
		if(row == NULL)
			strcpy(result, "NULL,");
		else
		{
			//concatenamos todos los jugadorees de la BD uno seguido del otro
			//row[0] id, row[1] nombre, row[2] contrasenya
			sprintf(jugadores, "%s-%s-%s", row[0], row[1], row[2]);
			row = mysql_fetch_row (resultado);
			while (row!=NULL)
			{
				sprintf(jugadores, "%s_%s-%s-%s",jugadores, row[0], row[1], row[2]);
				row = mysql_fetch_row (resultado);
			}
			
			strcat(jugadores, "_|");	//al terminar con los jugadores, ponemos /
		}
		sprintf(result, jugadores);
	}

	//BUSCAMOS AHORA LA ISTA DE SERVIDORES
	sprintf(consulta, "SELECT * FROM servidor;");
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		strcat(result, "NO HAY SERVIDORES,");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		if(row == NULL)
			strcpy(result, "NULL,");
		else
		{
			//concatenamos todos los servidores de la BD uno seguido del otro
			//row[0] id, row[1] host (ciudad)
			sprintf(servers, "%s-%s", row[0], row[1]);
			row = mysql_fetch_row (resultado);
			while (row!=NULL)
			{
				sprintf(servers, "%s_%s-%s", servers, row[0], row[1]);
				row = mysql_fetch_row (resultado);
			}
			strcat(servers, "_|");	//al terminar con los servidores, ponemos /
		}
		strcat(result, servers);
	}

	//BUSCAMOS AHORA LA ISTA DE PARTIDAS
	sprintf(consulta, "SELECT * FROM partida;");
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		strcat(result, "NO HAY PARTIDAS,");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		if(row == NULL)
			strcpy(result, "NULL,");
		else
		{
			//concatenamos todos los servidores de la BD uno seguido del otro
			//row[0] id  jugador 1, row[1] id jugador 2, row[2] id server, row[3] fecha, row[4] ganador
			sprintf(partidas, "%s-%s-%s-%s-%s", row[0], row[1], row[2], row[3], row[4]);
			row = mysql_fetch_row (resultado);
			while (row!=NULL)
			{
				sprintf(partidas, "%s_%s-%s-%s-%s-%s", partidas, row[0], row[1], row[2], row[3], row[4]);
				row = mysql_fetch_row (resultado);
			}
			strcat(partidas, "_,");	//al terminar con toda la base de datos, ponemos ,
		}
		strcat(result, partidas);
	}
	printf("%s\n", result);
}
