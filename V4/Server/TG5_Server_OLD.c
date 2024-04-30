#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <stdbool.h>
#include <pthread.h>
#include <mysql.h>
#include <time.h>

int contador_servicios;
char conectados[200];

int i;
int sockets[100];	//vector de sockets

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
	char respuesta[600];
	char result[600];
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
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "TG5_data_base",0, NULL, 0);
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
		
		printf ("Se ha conectado. Peticion: %s\n",peticion);
		
		int codigo;
		int NForm;
		char nombre[30];
		char contra[20];
		char USER[30] = "";
		int resp;
		
		char *p = strtok(peticion, "/");
		codigo =  atoi (p);	//codigo
		printf("CODE: %d\n",codigo);
		
		if (codigo == 1)	//LOGIN
		{
			p= strtok (NULL,"/");
			strcpy(nombre,p);		//nombre
			p= strtok (NULL,"/");
			strcpy(contra,p);	//contrasenya
			printf("%d %s %s\n",codigo, nombre,contra);
			
			resp=login(nombre, contra,conn );
			
			printf("%d\n",resp);
			if(resp==0)
			{
				sprintf(respuesta, "1#correcto,");
				//anyadir el usuario a la lista de conectados
				pthread_mutex_lock (&mutex);	//No interrumpas ahorav
				sprintf(conectados, "%s|%s|", conectados, nombre);
				pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				
				sprintf(USER, nombre);
			}
			else
			   sprintf(respuesta, "1#incorrecto,");
			printf ("%s\n", respuesta);
			// Y lo enviamos
			write (socket_conn,respuesta, strlen(respuesta));
		}
		else if (codigo == 2)	//REGISTER
		{
			p= strtok (NULL,"/");
			strcpy(nombre,p);		//nombre
			p= strtok (NULL,"/");
			strcpy(contra,p);	//contrasenya
			printf("%d %s %s\n",codigo, nombre,contra);
			
			resp=registro(nombre,contra,conn);
			printf("%d\n",resp);
			if(resp==0)
			{
				sprintf(respuesta, "2#correcto,");
				//anyadir el usuario a la lista de conectados
				pthread_mutex_lock (&mutex);	//No interrumpas ahora
				sprintf(conectados, "%s|%s|", conectados, nombre);
				pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				
				sprintf(USER, nombre);
			}
			else
			   sprintf(respuesta, "2#incorrecto,");
			printf ("%s\n", respuesta);
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo==3)	//CONSULTA 1
		{
			p = strtok (NULL,"/");
			strcpy(nombre,p);
			
			localizacion(nombre, result, conn);
			sprintf(respuesta, "3#%s,", result);
			printf ("%s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo==4)	//CONSULTA 2
		{
			char fecha[200];
			p= strtok (NULL,"*");
			strcpy(fecha,p);
			
			sacarnombre(fecha, result, conn);
			sprintf(respuesta, "4#%s,", result);
			printf ("%s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo==5)	//COOOONSULTA 3
		{
			char servidor[200];
			p = strtok (NULL,"/");
			strcpy(servidor,p);
			
			nombreserv(servidor, result, conn);
			sprintf(respuesta, "5#%s,", result);
			printf ("%s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 0)	//DESCONECTAR USUARIO
		{
			terminar = 1;
			
			char newconectados [100] = "";
			char nom[60];
			
			//guardamos el nombre del usuario a desconectar
			char usuario[60];
			p = strtok (NULL,"/");

			if(p != NULL) //si hay usuario conectado
			{
				strcpy(usuario,p);
				printf ("%s\n", usuario);
				strcpy(USER,usuario);
	
				char *d = strtok(conectados, "|");
				strcpy(nom, d);
				printf ("%s\n", nom);
				//	|Enric||Juan||Maria|
				if(strcmp(usuario, nom) == 0)	//si el usuario a desconectar es el primero de la lista
				{
					printf ("El usuario era el primero de la lista\n");
					d = strtok(NULL, "|");
					
					while(d != NULL)
					{
						strcpy(nom, d);
						sprintf(newconectados, "%s|%s|", newconectados, nom);
						d = strtok(NULL, "|");
					}
				}
				else	//si el usuario a desconectar no es el primero dee la lista
				{
					printf ("El usuario no era el primero de la lista\n");
					//recorremos la lista de conectados hasta encontrar nuestro usuario
					while(strcmp(usuario, nom) != 0)
					{
						//mientras no lo encontremos, construimos nuestra nueva lista sin el usuario
						sprintf(newconectados, "%s|%s|", newconectados, nom);
						d = strtok(NULL, "|");
						strcpy(nom, d);
					}
					printf ("%s\n", newconectados);
					//cuando lo encontremos, lo saltamos en la lista y seguimos con los siguientes usuarios
					d = strtok(NULL, "|");
					
					while(d != NULL)
					{
						strcpy(nom, d);
						sprintf(newconectados, "%s|%s|", newconectados, nom);
						d = strtok(NULL, "|");
					}
				}
				borrarnombre(nom,conn);
				//strcat(newconectados,"|");
				printf ("%s\n", newconectados);
				//guardamos la nueva lista sin el usuario en la lista de conectados
				pthread_mutex_lock (&mutex);	//No interrumpas ahora
				strcpy(conectados, newconectados);
				pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
			}
			
			sprintf(respuesta, "0#User Disconnected,");
			printf ("%s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		else if (codigo == 100)	//SHOW DATABASE
		{
			respuesta[600] = "";
			printf("in\n");
			ShowDataBase(result, conn);
			
			sprintf(respuesta, "100#%s", result);
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
		
		//CREATE GAME ES UN THREAD DE PER SI JA QUE HAS DE PODER JUGAR VARIES PARTIDES A LA VEGADA
		else if (codigo == 98)	//CREATE GAME
		{
			char host[30];
			p= strtok (NULL,"/");
			strcpy(host,p);		//nombre
			
			char partida[200];
			
			resp = CreateGame(host, partida, conn);
			
			if(resp==0)
			{
				sprintf(respuesta, "98#correcto#%s,", partida);
			}
			else
			{
			    sprintf(respuesta, "98#incorrecto,");
			}
			
			printf ("%s\n", respuesta);
			// Y lo enviamos
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 97)	//INVITATION
		{
			//msg del tipo 97/NForm/USER/Invited
			char INVITED[30];
			char INVITING[30];
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			strcpy(INVITING,p);		//usuario que invita
			
			p= strtok (NULL,"/");
			strcpy(INVITED,p);		//jugador al que se invita

			resp = BuscarInvitado(INVITED, conn);
			
			if(resp == -1)
			{
				sprintf(respuesta, "97#0#%d#incorrecto,", NForm);
			}
			else
			{
			    sprintf(respuesta, "97#0#%d#correcto#%s,", NForm, INVITED);
			}
			
			printf ("INVITING: %s\n", respuesta);
			//LO DEVOLVEMOS AL SOCKET QUE HA HECHO LA PETICION
			write (socket_conn,respuesta, strlen(respuesta));
			
			// Y AL SOCKET DEL INVITADO SI ESTA BIEN (SINO NO TIENE QUE RECIBIR NADA)
			if(resp != -1)
			{
				sprintf(respuesta, "97#1#%s,", INVITING);
				printf ("INVITED: %s\n", respuesta);
				write (sockets[resp],respuesta, strlen(respuesta));
			}
		}
		/*
		if (codigo == 99)	//SHOW LISTA CONECTADOS
		{
			printf("99/%s\n", conectados);
			sprintf(respuesta, "%s,", conectados);
			write (socket_conn, respuesta, strlen(respuesta));
		}
		*/
		if(strcmp(USER, "") != 0)	//Si hay un USER a modificar
		{
			//if ((codigo == 0) || (codigo == 1) || (codigo == 2))
			char notificacion[300];
			if((codigo == 1) || (codigo == 2))
			{
				sprintf(notificacion, "99#0#New User Online: %s\n%s,", USER, conectados);
			}
			else if (codigo == 0)
			{
				sprintf(notificacion, "99#1#User Disconnected: %s\n%s,", USER, conectados);
			}
			int j;
			printf ("%s\n", notificacion);
			printf ("NUMBER OF SOCKETS: %d\n", i);
			for(j=0;j<i;j++)
			{
				printf ("%s\n", notificacion);
				write (sockets[j], notificacion, strlen(notificacion));
			}
			sprintf(USER, "");	//reset del valor de USER
		}
		printf ("DONE\n");
	}
	// Se acabo el servicio para este cliente
	close(socket_conn); 
	printf ("Cliente desconectado\n");
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
	int port = 50080;
	serv_adr.sin_port = htons(9049);
	if (bind(socket_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(socket_listen, 3) < 0)
		printf("Error en el Listen\n");
	
	contador_servicios = 0;
	conectados[200] = "";

	pthread_t thread;	//estructura de thread
	i = 0;
	// Bucle per atendre peticions
	
	for(;;)
	{
		printf ("Escuchando\n");
		
		socket_conn = accept(socket_listen, NULL, NULL);
		printf ("He recibido conexi?n\n");
		
		//LISTA DE CONNECTADOS Y SOCKETS TIENEN EL MISMO ORDEN DE USUARIOS
		sockets[i] = socket_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		//crear thread i dir-li el que s'ha de fer
		
		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);
		i++;
	}
	
}

int login(char nombre[60], char password[60], MYSQL *conn) {
	//SELECT id_jugador FROM datos_personales WHERE nombre = name;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	strcpy(consulta, "\0");
	
	strcat (consulta, "SELECT * FROM Player WHERE nombre = '");
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
				sprintf (consulta,"INSERT INTO Connected VALUES ('%s');",nombre);
				err = mysql_query(conn, consulta);
				printf(consulta);
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
	err=mysql_query (conn, "SELECT * FROM Player;");
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
	
	sprintf (consulta, "SELECT * FROM Player WHERE nombre ='%s';", nombre);
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
		strcpy (consulta,"INSERT INTO Player (id,nombre,contrasenya) VALUES (");
		
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
		{
			sprintf (consulta,"INSERT INTO Connected VALUES ('%s');",nombre);
			err = mysql_query(conn, consulta);
			printf(consulta);
		   return 0;
		}
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
	sprintf(consulta, "SELECT * FROM Player;");
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		sprintf(result, "NO PLAYERS,");
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
	sprintf(consulta, "SELECT * FROM Server;");
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		strcat(result, "NO SERVERS,");
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
	sprintf(consulta, "SELECT * FROM Game;");
	
	err = mysql_query(conn, consulta);
	if(err!=0){
		strcat(result, "NO GAMES,");
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
			//row[0] id  jugador 1, row[1] id jugador 2, row[2] id jugador 3, row[3] id jugador 4, row[4] id jugador 5, row[2] id server, row[3] fecha, row[4] minijuego
			sprintf(partidas, "%s-%s-%s-%s-%s-%s-%s-%s", row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
			row = mysql_fetch_row (resultado);
			while (row!=NULL)
			{
				sprintf(partidas, "%s_%s-%s-%s-%s-%s-%s-%s-%s", partidas, row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
				row = mysql_fetch_row (resultado);
			}
			strcat(partidas, "_,");	//al terminar con toda la base de datos, ponemos ,
		}
		strcat(result, partidas);
	}
	printf("%s\n", result);
}

void localizacion(char nombre[60], char respuesta[100], MYSQL *conn) 
{
	//Buscamos host_id del serrver donde ha jugado esta persona
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	strcpy(consulta, "\0");
	
	sprintf (consulta, "SELECT Server.host_id FROM Player,Game,Server WHERE Player.nombre= '%s' AND (Player.id=Game.id_j1 OR Player.id=Game.id_j2) AND Game.id_s=Server.id;",nombre);
	
	printf("consulta: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	
	if(err!=0)
	{
		sprintf(respuesta,"No existe o no ha ganado el jugador que nos dices");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		
		if(row == NULL)		//La consulta esta vacia por lo que puede no existir o no haber ganado nunca
		{
			sprintf(respuesta,"No existe o no ha ganado el jugador que nos dices");
		}
		else
		{
			char ciudades[200];
			
			sprintf(ciudades, "%s has played at: %s",nombre, row[0]);
			row = mysql_fetch_row (resultado);
			
			while(row !=NULL)
			{
				printf("hola\n");
				sprintf(ciudades, "%s %s",ciudades,row[0]);
			
				row = mysql_fetch_row (resultado);
			}
			sprintf(respuesta, "%s\n",ciudades);
		}
		//retornamos la respuesta con las ciudades (host id server))
	}
	
}

void sacarnombre(char fecha[60],char respuesta[100], MYSQL *conn) 
{
	//Buscamos el nombre de la persona que ha ganado en esta fecha
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	strcpy(consulta, "\0");
	
	sprintf (consulta," SELECT DISTINCT Game.minijuego FROM Game WHERE Game.fecha='%s';",fecha);
	
	printf("consulta: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	
	if(err!=0){
		sprintf(respuesta,"No se ha jugado en la fecha que dices");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		
		if(row == NULL)		//La consulta esta vacia por lo que puede no existir o no haber ganado nunca
		{
			sprintf(respuesta,"No se ha jugado en la fecha que dices");
		}
		else
		{
			char minigame[200];
			
			sprintf(minigame, "%s",row[0]);
			row = mysql_fetch_row (resultado);
			while(row !=NULL)
			{
				sprintf(minigame, "%s %s",minigame,row[0]);
			
				row = mysql_fetch_row (resultado);
			}
			sprintf(respuesta, "%s was/were played on the selected date %s\n",minigame, fecha);
		}
		//retornamos la respuesta con el nombre dee la persona
	}
}


void nombreserv(char servidor[60], char respuesta[100], MYSQL *conn) 
{
	//Buscamos los nombres de las personas que han jugado en este servidor
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	strcpy(consulta, "\0");
	
	sprintf (consulta, "SELECT DISTINCT Player.nombre FROM Player,Game,Server WHERE Server.host_id='%s' AND Server.id=Game.id_s AND (Game.id_j1=Player.id OR Game.id_j2=Player.id);",servidor);
	
	printf("consulta: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	
	if(err!=0)
	{
		sprintf(respuesta,"No existe o no ha jugado nadie en este servidor");
	}
	else
	{
		resultado = mysql_store_result (conn);
		
		row = mysql_fetch_row (resultado);
		
		if(row == NULL)	//La consulta esta vacia por lo que puede no existir o no haber ganado nunca
		{
			sprintf(respuesta,"No existe o no ha jugado nadie en este servidor");
		}
		else
		{
			char aux[200];
			char nombres[200];
			
			sprintf(nombres, "The players who have played at %s are: %s", servidor, row[0]);
			sprintf(respuesta,"%s",nombres);
			row = mysql_fetch_row (resultado);
			while(row !=NULL)
			{
				sprintf(aux,"%s",row[0]);
				if( strcmp (nombres,aux)!=0)
				{
					sprintf(nombres, "%s",row[0]);
					sprintf(respuesta, "%s %s",respuesta,nombres);
				}
			
				row = mysql_fetch_row (resultado);
			}
		}
		//retornamos la respuesta con los nombres
	}
}

void borrarnombre(char nombre[60],  MYSQL *conn) 
{
	//Buscamos host_id del serrver donde ha jugado esta persona
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	strcpy(consulta, "\0");
	sprintf (consulta, "DELETE FROM Connected WHERE conectado='%s';",nombre);

	printf("consulta: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	if(err!=0)
	{
		printf("No has podido delogear");
	}
	else
	{
		printf("Deslogeo exitoso");
	}
}

int CreateGame(char nombre[60], char partida[200], MYSQL *conn)
{
	printf("J1: %s\n", nombre); 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int contador = 0;
	char server[20];
	
	//DATE
	time_t t;
	time(&t);
	char date[30];
	
	sprintf(date, "%s", ctime(&t));
	//CTIME(&T) acaba amb un \n per defecte que s'ha de treure
	char *time = strtok(date, "\n");
	printf("DATE: %s\n", time);
	
	//ID_J1
	sprintf (consulta, "SELECT id FROM Player WHERE nombre = '%s';", nombre);
	printf("consulta: %s\n", consulta);
	//El jugador existe, sino no estariamos aqui
	err = mysql_query(conn, consulta);
	if(err!=0)
	{
		printf("ID_J1 NOT OK\n");
	}
	else
	{
		printf("ID_J1 OK\n");
	}	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	//printf("ID %s: %s\n",nombre, row[0]); 
	int id_j1 = atoi(row[0]);
	printf("ID_J1: %d\n", id_j1); 
	
	//ID_S
	sprintf (consulta, "SELECT id FROM Server;");
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	while(row != NULL)
	{
		contador++;
		row = mysql_fetch_row (resultado);
	}
	printf("Num servers: %d\n", contador); 
	
	int id_s = rand() % contador + 1; //SE COGE UN SERVER RANDOM DE LA LISTA
	sprintf (consulta, "SELECT host_id FROM Server WHERE id ='%d';", id_s);
	printf("consulta: %s\n", consulta);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	sprintf(server, row[0]);
	printf("SERVER id %d: %s\n", id_s, server); 
	
	sprintf(partida, "%s|%d|%s", server, id_j1, date);
	//INSERTAMOS EN GAME	(1 ES NEUTRAL - HOST DEVELOPPER)
	sprintf (consulta, "INSERT INTO Game VALUES (%d,1,1,1,1,%d,'%s','NULL');", id_j1, id_s, date);
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, minijuego)
	printf("consulta: %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	if(err!=0)
	{
		printf("ADDED NOT OK\n");
		return -1;
	}
	else
	{
		printf("ADDED OK\n");
		return 0;
	}	
}

int BuscarInvitado(char invitado[30], MYSQL *conn)
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int j = 0;
	bool found = false;
	//Devolvera toda la lista de  buscamos el index del invitado
	//EL INDEX DEL INVITADO COINCIDE CON conectadosSU INDEX DE SOCKET
	sprintf (consulta, "SELECT * FROM Connected");
	printf("consulta: %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	while(row != NULL)
	{
		row = mysql_fetch_row (resultado);
		j++;
	}
	printf("NUMBER OF CONNECTED: %d\n", j);
	printf("NUMBER OF SOCKETS: %d\n", i);
	
	j = 0;
	sprintf (consulta, "SELECT * FROM Connected");
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	while((row != NULL) && (!found))
	{
		if(strcmp(row[0], invitado) == 0)
		{
			found = true;
		}
		else
		{
			row = mysql_fetch_row (resultado);
			j++;
		}
	}
	
	if(found == false)
	{
		printf("ERROR");
		return -1;
	}
	else
	{
		printf("INVITED NUMBER: %d\n", j);
		return j;
	}
	
}












