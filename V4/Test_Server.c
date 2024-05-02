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
int numconectados;
bool LogOut = false;

int i;
int sockets[100];	//vector de sockets

//Estructura de dades d'acc�s "excluyente"
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
	bool NotificateConnected = false;
	
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
			
			if(resp == 0)
			{
				sprintf(respuesta, "1#correcto,");
				//anyadir el usuario a la lista de conectados
				if(strcmp(conectados, "") == 0)	//si la lista esta vacia
				{
					pthread_mutex_lock (&mutex);	//No interrumpas ahorav
					sprintf(conectados, "%s", nombre);
					pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				}
				else
				{
					pthread_mutex_lock (&mutex);	//No interrumpas ahorav
					sprintf(conectados, "%s %s", conectados, nombre);
					pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				}
				numconectados ++;
				sprintf(USER, nombre);
				NotificateConnected = true;
			}
			else if (resp == 1)
			{
				sprintf(respuesta, "1#Incorrect password,");
			}
			else if (resp == -1)
			{
				sprintf(respuesta, "1#User doesn't exist. Register first, please,");
			}
			else if (resp == -2)
			{
				sprintf(respuesta, "1#User already connected. Try another user, please,");
			}
			   
			printf ("LOGIN: %s\n", respuesta);
			// Y lo enviamos
			write (socket_conn,respuesta, strlen(respuesta));
		}
		else if (codigo == 2)	//REGISTER
		{
			p= strtok (NULL,"/");
			strcpy(nombre,p);		//nombre
			p= strtok (NULL,"/");
			strcpy(contra,p);	//contrasenya
			
			resp=registro(nombre,contra,conn);

			if(resp == 0)
			{
				sprintf(respuesta, "2#correcto,");
				//anyadir el usuario a la lista de conectados
				if(strcmp(conectados, "") == 0)	//si la lista esta vacia
				{
					pthread_mutex_lock (&mutex);	//No interrumpas ahorav
					sprintf(conectados, "%s", nombre);
					pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				}
				else
				{
					pthread_mutex_lock (&mutex);	//No interrumpas ahorav
					sprintf(conectados, "%s %s", conectados, nombre);
					pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
				}
				numconectados ++;
				sprintf(USER, nombre);
				NotificateConnected = true;
			}
			else
			{
			   sprintf(respuesta, "2#incorrecto,");
			}
			
			printf ("REGISTER: %s\n", respuesta);
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
		
		else if (codigo == 0)	//DESCONECTAR O DESLOGUEAR USUARIO
		{
			terminar = 1;
			
			char newconectados [300] = "";
			char nom[60];
			
			//op = 0 - desconectar | op = 1 - desloguear
			int operacion;
			p = strtok (NULL,"/");
			operacion = atoi(p);
			
			//guardamos el nombre del usuario a desconectar
			char usuario[60];
			p = strtok (NULL,"/");

			if(p != NULL) //si hay usuario conectado
			{
				strcpy(usuario,p);
				printf ("USER: %s\n", usuario);
				strcpy(USER,usuario);
				
				
				if(operacion == 1)	//Logout
				{
					sprintf(respuesta, "0#1,");
				}
				else				//Salir del Juego
				{
					sprintf(respuesta, "0#0,");
				}
				
				NotificateConnected = true;
				
				//printf ("Operation: %d\n", operacion);

				borrarnombre(usuario, newconectados, operacion, conn);
				//strcat(newconectados,"|");
				printf ("New List of Connected: %s\n", newconectados);
				//guardamos la nueva lista sin el usuario en la lista de conectados
				pthread_mutex_lock (&mutex);	//No interrumpas ahora
				strcpy(conectados, newconectados);
				pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
			}
			else	//han cerrado el juego sin nadie logueado
			{
				sprintf(respuesta, "0#0,");
			}
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		else if (codigo == -1)
		{
			sprintf("%d", p);
		}
		else if (codigo == 100)	//SHOW DATABASE
		{
			respuesta[600] = "";
			printf("in\n");
			ShowDataBase(result, conn);
			
			sprintf(respuesta, "100#%s", result);
			printf("SHOWDATABASE: %s\n",respuesta);

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
			
			printf ("CREATEGAME: %s\n", respuesta);
			// Y lo enviamos
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 97)	//INVITATION
		{
			//msg del tipo 97/NForm/USER/Invited
			char INVITED[30];
			char INVITING[30];
			char game_info[60];
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			strcpy(INVITING,p);		//usuario que invita
			
			p= strtok (NULL,"/");
			strcpy(INVITED,p);		//jugador al que se invita
			
			p= strtok (NULL,"/");
			strcpy(game_info,p);		//info de la partida
			
			//retorna el numero de socket o -1 si hay un error o no existe
			resp = BuscarSocket(INVITED, conn);
			
			if(resp == -1)
			{
				sprintf(respuesta, "97#0#%d#incorrecto,", NForm);
			}
			else
			{
			    sprintf(respuesta, "97#0#%d#correcto#%s,", NForm, INVITED);
			}
			//una respuesta distinta para invitado y invitando
			
			printf ("INVITING: %s\n", respuesta);
			//LO DEVOLVEMOS AL SOCKET QUE HA HECHO LA PETICION
			write (socket_conn,respuesta, strlen(respuesta));
			
			// Y AL SOCKET DEL INVITADO SI ESTA BIEN (SINO NO TIENE QUE RECIBIR NADA)
			//tambien le enviamos la info de la partida creada para que se pueda unir
			if(resp != -1)
			{
				sprintf(respuesta, "97#1#%s#%s,", INVITING, game_info);
				printf ("INVITED: %s\n", respuesta);
				write (sockets[resp],respuesta, strlen(respuesta));
			}
		}
		
		else if (codigo == 96)	//JOIN GAME
		{
			char invited[30];
			char inviting[30];
			char game_info[60];
			char otherplayers[100] = "";
			int othersockets[4];
			
			p= strtok (NULL,"/");
			sprintf(invited, p);
			
			p= strtok (NULL,"/");
			sprintf(inviting, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			//retorna -1 si esta llena la partida
			//retorna -2 si no existe o ya ha terminado
			//retorna 0 si todo OK
			resp = JoinGame(invited, inviting, game_info, otherplayers, othersockets, conn);
			
			if(resp == 0)
			{
				sprintf(respuesta, "96#0#%s#%s,", game_info, otherplayers);
				printf ("JOINGAME: %s\n", respuesta);
			}
			else if (resp == -1)
			{
				sprintf(respuesta, "96#-1,");
			}
			else if (resp == -2)
			{
				sprintf(respuesta, "96#-2,");
			}
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		if(NotificateConnected)	//Si hay un USER a modificar
		{
			//if ((codigo == 0) || (codigo == 1) || (codigo == 2))
			char notificacion[300];
			/*
			if((codigo == 1) || (codigo == 2))
			{
				sprintf(notificacion, "99#0#New User Online: %s\n%s,", USER, conectados);
			}
			else if (codigo == 0)
			{
				sprintf(notificacion, "99#1#User Disconnected: %s\n%s,", USER, conectados);
			}
			*/
			sprintf(notificacion, "99#%s,", conectados);
			printf ("NOTIFICATION: %s\n", notificacion);
			int j;
			printf ("NUMBER OF SOCKETS NOTIFICATED: %d\n", i);
			for(j=0;j<i;j++)
			{
				//printf ("%s\n", notificacion);
				write (sockets[j], notificacion, strlen(notificacion));
			}
			if(codigo == 0)
			{
				i = numconectados;
				printf ("NUMBER OF SOCKETS NOW: %d\n", i);
			}

			printf ("NUMBER OF PLAYERS NOW: %d\n", numconectados);
			//numconectados = i;
			sprintf(USER, "");	//reset del valor de USER
			NotificateConnected = false;
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
	serv_adr.sin_port = htons(9065);
	if (bind(socket_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(socket_listen, 3) < 0)
		printf("Error en el Listen\n");
	
	contador_servicios = 0;
	conectados[200] = "";
	
	numconectados = 0;

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
	
	int res;
	
	//PRIMERO MIRAMOS SI EL USUARIO YA ESTA LOGEADO
	bool estalogeado = false;
	
	if(numconectados != 0)
	{
		sprintf (consulta, "SELECT * FROM Connected;");
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		while(row != NULL)
		{
			if(strcmp(row[0], nombre) == 0)
			{
				estalogeado = true;
			}
			row = mysql_fetch_row (resultado);
		}
	}
	
	if(!estalogeado)	//SI NO ESTA LOGEADO, MIRAMOS SI PUEDE LOGEARSE
	{
		sprintf (consulta, "SELECT * FROM Player WHERE nombre = '%s';", nombre);
		printf("LOGIN: %s\n", consulta);
		err = mysql_query(conn, consulta);
		if(err != 0)
		{
			res = -1;
		}
		else
		{
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if(row == NULL)
			{
				res = -1;
			}
			else
			{
				while (row!=NULL)
				{
					//En este caso el id sera row[0], el nombre sera row[1], y la contrasena sera row[2]
					if(strcmp(row[2], password) == 0)
					{
						sprintf (consulta,"INSERT INTO Connected VALUES ('%s');",nombre);
						err = mysql_query(conn, consulta);
						res = 0; //el usuario existe y la contrasena coincide
					}
					else
					{
						res =  1; //contrasenya incorrecta
					}
					row = mysql_fetch_row (resultado);
				}
			}
		}
		
	}
	else	//SI YA ESTA LOGEADO
	{
		res = -2;
	}
	return res;
}

int registro(char nombre[60], char password[60], MYSQL *conn) 
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[512];

	int filas;
	int new_id;
	int res;

	sprintf (consulta, "SELECT * FROM Player;");
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);

	//En nuestro caso hay tres columnas: dni(row[0]), nombre(row[1]) y edad (row[2]).
	while (row !=NULL) 
	{
		filas= atoi(row[0]);
		row = mysql_fetch_row (resultado);
	}
	new_id = filas + 1;

	sprintf (consulta, "SELECT * FROM Player WHERE nombre ='%s';", nombre);
	err = mysql_query(conn, consulta);
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if(row == NULL)		//Si no hay nadie logeado con nuestro perfil
	{
		sprintf (consulta,"INSERT INTO Player (id,nombre,contrasenya) VALUES (%d,'%s','%s');", new_id, nombre, password);
		err = mysql_query(conn, consulta);
		sprintf (consulta,"INSERT INTO Connected VALUES ('%s');",nombre);
		err = mysql_query(conn, consulta);
		res = 0;
	}
	else
	{
		res = -1;
	}
	
	return res;
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
			//row[0] id  jugador 1, row[1] id jugador 2, row[2] id jugador 3, row[3] id jugador 4, row[4] id jugador 5, row[5] id server, row[6] fecha, row[7] minijuego, row[8] ended
			sprintf(partidas, "%s-%s-%s-%s-%s-%s-%s-%s-%s", row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
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

void borrarnombre(char nombre[60], char newconectados[300], int operacion, MYSQL *conn) 
{
	//Buscamos host_id del serrver donde ha jugado esta persona
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	
	strcpy(consulta, "\0");
	sprintf (consulta, "DELETE FROM Connected WHERE conectado = '%s';",nombre);

	printf("consulta: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	if(err!=0)
	{
		printf("CLOSECONNECTION: No has podido delogear\n");
	}
	else
	{
		printf("CLOSECONNECTION: Deslogeo exitoso\n");
	}
	
	sprintf (consulta, "SELECT * FROM Connected;");
	
	err = mysql_query(conn, consulta);
	if(err!=0)
	{
		printf("Error\n");
	}
	else
	{
		printf("CLOSECONNECTION: OK\n");
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	pthread_mutex_lock (&mutex);	//No interrumpas ahora
	numconectados--;
	pthread_mutex_unlock(&mutex);	//Ya puedes interrumpir
	
	printf("CLOSECONNECTION: numconnected: %d\n", numconectados);
	if(numconectados != 0)
	{
		sprintf (newconectados, "%s",row[0]);	//primer valor de la llista
		row = mysql_fetch_row(resultado);
		
		while(row != NULL)
		{
			sprintf (newconectados, "%s %s", newconectados, row[0]);
			row = mysql_fetch_row(resultado);
		}
	}
	else
	{
		printf("Table Connected Empty\n");
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
	printf("CREATEGAME DATE: %s\n", time);
	
	//ID_J1
	sprintf (consulta, "SELECT id FROM Player WHERE nombre = '%s';", nombre);
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
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	//printf("SERVER id %d: %s\n", row[0], row[1]); 
	sprintf(server, row[0]);	//row[1] es el host_id (nombre server)
	printf("SERVER id %d: %s\n", id_s, server); 
	
	//INSERTAMOS EN GAME	(1 ES NEUTRAL - HOST DEVELOPPER)	| 0 indica que el juego aun exite (ended = 0)
	sprintf (consulta, "INSERT INTO Game VALUES (%d,1,1,1,1,%d,'%s','NULL', 0);", id_j1, id_s, date);
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, minijuego)
	printf("CREATEGAME: %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	if(err!=0)
	{
		printf("ADDED NOT OK\n");
		sprintf(partida, "ERROR");
		return -1;
	}
	else
	{
		printf("ADDED OK\n");
		sprintf(partida, "%s|%d|%s", server, id_j1, date);
		return 0;
	}	
}

int BuscarSocket(char invitado[30], MYSQL *conn)
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int j = 0;
	bool found = false;
	//Devolvera toda la lista de conectados. buscamos el index del invitado
	//EL INDEX DEL INVITADO COINCIDE CON SU INDEX DE SOCKET
	/*
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
	//printf("NUMBER OF CONNECTED: %d\n", j);
	//printf("NUMBER OF SOCKETS: %d\n", i);
	*/
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

int JoinGame(char invited[30], char inviting[30], char partida[200], char otherplayers[100], int othersockets[4], MYSQL *conn)
{
	printf("JOINGAME inviting: %s\n", inviting); 
	printf("JOINGAME invited: %s\n", invited); 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int contador = 0;
	char server[20];
	char date[30];
	int idplayerhost;
	int idserver;
	char index_id_to_ad[5];
	int id_to_ad;
	int idplayers[4] = {0};	//maximo otros 4 jugadores
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	
	sprintf(partida, "%s|", partida);
	
	//cogemos los valores de la partida
	char *h = strtok(partida, "|");
	sprintf(server, h);
	h = strtok(NULL, "|");
	idplayerhost = atoi(h);
	h = strtok(NULL, "|");
	sprintf(date, h);
	
	//buscamos el id del server
	
	sprintf (consulta, "SELECT id FROM Server WHERE host_id = '%s';", server);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	//no hace falta pasar por filtros porque si ha llegado hasta aqui es que todo existe i es correcto
	
	idserver = atoi(row[0]);
	
	//ahora buscamos la partida a la que nos han invitado para luego unirnos
	
	sprintf (consulta, "SELECT * FROM Game WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, minijuego)
	int j = 1;
	int id_x;
	bool found  = false;
	
	if(atoi(row[8]) == 1)
	{
		//el juego ya ha terminado o ya no esta disponible
		return -2;
	}
	//buscamos donde anyadir el jugador que se esta uniendo. SI id=1 ES QUE ESTA VACIO (id=1 es por defecto del developer)
	while((j < 5) && (!found))
	{
		id_x = atoi(row[j]);
		if(id_x == 1)
		{
			found = true;
		}
		else
		{
			//guardamos los valores de id de los otros jugadores
			//si sobra algun hueco, el valor es de 0 en ese hueco
			idplayers[j - 1] = id_x;
			j++;
		}
	}
	
	if(found)
	{
		printf("JOINGAME : able to join game\n");
	}
	else	//j=5, es decir idplayers esta lleno y no hay espacio para unirnos (la tabla de Game ya esta llena con 5 jugadores)
	{	
		printf("JOINGAME : game already full\n");
		return -1;
	}
	
	//j + 1 ya que la row[j] para j = 1 es el id_2 (j + 1)
	sprintf(index_id_to_ad, "id_%d", j + 1);
	
	//como nos podemos unir a la partida, buscamos el id del jugador a anyadir
	sprintf (consulta, "SELECT id FROM Player WHERE nombre = '%s';", invited);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);

	id_to_ad = atoi(row[0]);
	
	//anyadimos el jugador a la partida updateandola
	sprintf (consulta, "UPDATE Game SET %s = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", index_id_to_ad, id_to_ad, idplayerhost, idserver, date);
	err = mysql_query(conn, consulta);
	
	printf("JOINGAME : successfully joined game - position %s\n", index_id_to_ad);
	
	//ahora devolveremos la lista de gente que esta en la partida:
	

	//anyadimos el jugador que nos invita a la lista de jugadores de la partida ya que seguro que esta
	sprintf(otherplayers, "%s ", inviting);
	
	if(idplayers[0] != 0)	//si hay otros jugadores en la partida (maximo otros 3 a parte del host y el invitado)
	{
		sprintf (consulta, "SELECT (id, nombre) FROM Player;");
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		//row[0] = id | row[1] = nombre
		
		j = 0;
		found = false;
		int id_fromtable;
		
		//saltamos el id = 1 de developer
		row = mysql_fetch_row(resultado);
		
		while(idplayers[j] != 0)
		{
			//comparamos el id de idplayers[4] con TODOS los de la tabla
			while((row != NULL) && (!found))
			{
				id_fromtable = atoi(row[0]);
				
				if(id_fromtable == idplayers[j])
				{
					found = true;
					sprintf(otherplayers, "%s %s", otherplayers, row[1]);
				}
				else
				{
					row = mysql_fetch_row(resultado);
				}
			}
			j++;;
			found = false;
		}
	}
	
	printf("JOINGAME otherplayers: %s\n", otherplayers);
	
	//buscamos los sockets donde informar a los otros jugadores
	
	int res;
	//minimo informaremos al que nos ha invitado
	res = BuscarSocket(inviting, conn);
	
	if(res != -1)
	{
		othersockets[0] = res;
	}
	else
	{
		return -2;
	}
	
	j = 0;
	int k = 1;
	char *l = strtok(otherplayers, " ");
	while(idplayers[j] != 0)
	{
		othersockets[k] = BuscarSocket( l, conn);
		k++;
		j++;
		l = strtok(NULL, " ");
	}
	
	printf("JOINGAME OK: \n");
	printf("JOINGAME othersockets: %d\n", othersockets);
	
	return 1;
}











