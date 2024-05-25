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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG5_data_base",0, NULL, 0);
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
		
		else if (codigo == 3)	//CONSULTA 1 (Listado de jugadores con los que he echado alguna partida)
		{
			p = strtok (NULL,"/");
			strcpy(nombre,p);
			
			char Lista [500];
			
			resp = GetListaJugadores(nombre, Lista, conn);
			
			if (resp == 0)
			{
				sprintf(respuesta, "3#%s,", Lista);
			}
			else if (resp == -1)
			{
				sprintf(respuesta, "3#error|No other players found,");
			}
			printf ("GETLISTAJUGADORES : %s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 4)	//CONSULTA 2 (Resultados de las partidas que jugue con un jugador (o jugadores) determinado)
		{
			char nombres[200];
			
			p = strtok (NULL,"/");
			strcpy(nombre,p);
			
			p = strtok (NULL,"/");
			strcpy(nombres,p);
			
			char Lista [500];
			printf ("GETLISTARESULTADOS : OK\n");
			resp = GetListaResultados(nombre, nombres, Lista, conn);
			
			if (resp == 0)
			{
				sprintf(respuesta, "4#%s,", Lista);
			}
			else if (resp == -1)
			{
				sprintf(respuesta, "4#error|error,");
			}
			else if (resp == -2)
			{
				sprintf(respuesta, "4#error|No results found matching your request,");
			}
			else if (resp == -3)
			{
				sprintf(respuesta, "4#error|A player introduced is not registered,");
			}
			printf ("GETLISTARESULTADOS : %s\n", respuesta);
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 5)	//CONSULTA 3 (Lista de partidas jugadas en un periodo de tiempo dado)
		{
			char date1[20];
			char date2[20];
			
			p = strtok (NULL,"/");
			strcpy(date1,p);
			
			p = strtok (NULL,"/");
			strcpy(date2,p);
			
			char Lista[500];
			
			printf ("GETLISTARGAMES: OK\n");
			
			resp = GetListaPartidas(date1, date2, Lista, conn);
			
			if (resp == 0)
			{
				sprintf(respuesta, "5#%s,", Lista);
			}
			else if (resp == -1)
			{
				sprintf(respuesta, "5#error|No games found matching this interval,");
			}
			else if (resp == -2)
			{
				sprintf(respuesta, "5#error|Input is in the wrong format\nCheck the information icon next to the button,");
			}
			else if (resp == -5)
			{
				sprintf(respuesta, "5#error|Input is in the wrong format\nCheck the information icon next to the button,");
			}
			printf ("GETLISTAPARTIDAS : %s\n", respuesta);
			
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
				
				if (operacion == 2)	//unregister
				{
					sprintf(respuesta, "0#2,");
					
					unregister(usuario, conn);
				}
				else if(operacion == 1)	//Logout
				{
					sprintf(respuesta, "0#1,");
				}
				else if (operacion == 0)	//Salir del Juego
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
			char hostcon[30];
			sprintf (hostcon, "TRUNCATE TABLE Connected;");
			printf("consulta: %s\n",hostcon);
			err = mysql_query(conn, hostcon);
			sprintf("%d", p);
		}
		/*
		else if (codigo == 100)	//SHOW DATABASE
		{
			respuesta[600] = "";
			printf("in\n");
			ShowDataBase(result, conn);
			
			sprintf(respuesta, "100#%s", result);
			printf("SHOWDATABASE: %s\n",respuesta);

			write (socket_conn,respuesta, strlen(respuesta));
		}
		*/
		//CREATE GAME ES UN THREAD DE PER SI JA QUE HAS DE PODER JUGAR VARIES PARTIDES A LA VEGADA
		else if (codigo == 98)	//CREATE GAME
		{
			char host[30];
			char partida[200];
			
			p= strtok (NULL,"/");
			strcpy(host,p);		
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			resp = CreateGame(host, partida, NForm, conn);
			
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
			//y el numero de Form del que lo invita para que cuando la acepte, el que invita tambien reciva la informacion
			if(resp != -1)
			{
				sprintf(respuesta, "97#1#%s#%s#%d,", INVITING, game_info, NForm);
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
			int otherforms[4];
			
			p= strtok (NULL,"/");
			sprintf(invited, p);
			
			p= strtok (NULL,"/");
			sprintf(inviting, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			//retorna -1 si esta llena la partida
			//retorna -2 si no existe o ya ha terminado
			//retorna sino el numero de jugadores de la partida
			resp = JoinGame(invited, inviting, game_info, otherplayers, othersockets, otherforms, NForm, conn);
			
			if (resp == -1)	//el juego esta lleno
			{
				sprintf(respuesta, "96#-1#,");
			}
			else if (resp == -2)	//el juego ya no existe o ha terminado
			{
				sprintf(respuesta, "96#-2#,");
			}
			else if (resp == -3)	//el juego ya ha empezado
			{
				sprintf(respuesta, "96#-3#,");
			}
			else
			{
				int k = 0;
				int numForm;
				while (k < resp)	//resp es el numero de sockets a los que informar
				{	//othersockets i otherforms tienen el mismo index
					numForm = otherforms[k];
					sprintf(respuesta, "96#1#%s#%d#%d,", invited , resp, numForm);
					printf ("JOINGAME otherplayer socket %d: %s\n", othersockets[k], respuesta);
					write (sockets[othersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "96#0#%s#%s#%d,", game_info, otherplayers, NForm);
				printf ("JOINGAME invited: %s\n", respuesta);
			}
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 95)	//LEAVE GAME
		{
			int creator;
			char disconnecting[30];
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerforms[4];
			int playerscount;
			int position_leaving;
			
			//"95/" + "1" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + position_leaving;
			
			p= strtok (NULL,"/");
			creator = atoi(p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(disconnecting, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			p= strtok (NULL,"/");
			position_leaving = atoi(p);
			
			resp = LeaveGame(creator, disconnecting, game_info, players, playerscount, playersockets, playerforms, position_leaving, conn);
			
			int k = 0;
			int numForm;
			
			if (resp == 1) //el host cierra partida
			{
				while (k < playerscount - 1)	//informamos al numero de jugadores de la partida (menos al host -> -1)
				{
					numForm = playerforms[k];
					sprintf(respuesta, "95#1#%s#%d,", disconnecting, numForm);
					printf ("LEAVEGAME player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "95#2#%s#%d,", disconnecting, NForm);
				printf ("LEAVEGAME player HOST: %s\n", respuesta);
			}
			else if (resp == 0)	//un jugador abandona la partida
			{
				while (k < playerscount - 1)	//informamos al numero de jugadores de la partida (menos al usuario -> -1)
				{
					numForm = playerforms[k];
					sprintf(respuesta, "95#0#%s#%d,", disconnecting, numForm);
					printf ("LEAVEGAME player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "95#2#%s#%d,", disconnecting, NForm);
				printf ("LEAVEGAME player LEAVING: %s\n", respuesta);
			}
			else
			{
				sprintf(respuesta, "95#-1#%s#%d,", disconnecting, NForm);
				printf ("LEAVEGAME error\n", respuesta);
			}
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 94)	//START GAME
		{
			char host[30];
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerforms[4];
			int playerscount;
			
			// "94/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount;
		
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(host, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			char vectorimagenes_host[15];
			char vectorposiciones[15];
			char vectorimagenes_others[100];
			
			resp = StartGame(host, game_info, players, playerscount, playersockets, vectorimagenes_host, vectorposiciones, vectorimagenes_others, playerforms, conn);
			
			int k = 0;
			
			if (resp == 0) //SYMBOLS
			{
				int numForm;
				while (k < playerscount - 1)	//informamos a cada jugador de la partida
				{
					numForm = playerforms[k];
					sprintf(respuesta, "94#0#1#%s#%s#%s#%d,", vectorimagenes_host, vectorimagenes_others, vectorposiciones, numForm);
					printf ("STARTGAME Symbols player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "94#0#0#%s#%d,", vectorimagenes_host, NForm);
				printf ("STARTGAME Symbols player HOST: %s\n", respuesta);
			}
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 50)	//CHAT MESSAGE
		{
			char sender[30];
			char message[80];
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerforms[4];
			int playerscount;
			int position_sender;
			
			// "50/" + messageTxt.Text + Nform + "/" + USER + "/" + listplayers + "/" + playerscount + "/" + partida + "/" + position_sender;
			
			p= strtok (NULL,"/");
			sprintf(message, p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(sender, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
		
			p= strtok (NULL,"/");
			position_sender = atoi(p);
			
			printf("CHATMESSAGE %s message: %s\n", sender, message);
			
			//BUSCAMOS LOS FORMS:
			BuscarForms(game_info, playerforms, position_sender, conn);
			
			BuscarSockets(players, playerscount, playersockets, conn);
			
			int k = 0;
			int numForm;
			
			while (k < playerscount - 1)	//informamos a cada jugador de la partida
			{
				numForm = playerforms[k];
				sprintf(respuesta, "50#%s: %s#%d,", sender, message, numForm);
				printf ("CHATMESSAGE player socket %d: %s\n", playersockets[k], respuesta);
				write (sockets[playersockets[k]],respuesta, strlen(respuesta));
				k++;
			}
			sprintf(respuesta, "50#%s: %s#%d,", sender, message, NForm);
			printf ("CHATMESSAGE player SENDER: %s\n", respuesta);
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 51)	//CLICK A UN PICTUREBOX (RETORNA SI ESTA BE O MAL)
		{
			char sender[30];
			char expresion[80];
			int numimagen;
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerscount;
			int playerforms[4];
			int position_sender;
			
			// "51/" + expresion + "/" + vectorimatges_host[x] + "/" + Nform + "/" + USER + "/" + listplayers + "/" + playerscount + "/" + partida + "/" + position_sender;
			
			p= strtok (NULL,"/");
			sprintf(expresion, p);
			
			p= strtok (NULL,"/");
			numimagen = atoi(p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(sender, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			position_sender = atoi(p);
			
			printf("PICTUREMESSAGE %s expresion: %s\n", sender, expresion);
			
			BuscarForms(game_info, playerforms, position_sender, conn);
			
			BuscarSockets(players, playerscount, playersockets, conn);
			
			int k = 0;
			int numForm;
			while (k < playerscount - 1)	//informamos a cada jugador de la partida
			{
				numForm = playerforms[k];
				sprintf(respuesta, "51#%s#%s#%d#%d,", expresion, sender, numimagen, numForm);
				printf ("PICTUREMESSAGE player socket %d: %s\n", playersockets[k], respuesta);
				write (sockets[playersockets[k]],respuesta, strlen(respuesta));
				k++;
			}
			sprintf(respuesta, "51#%s#%s#%d#%d,", expresion, sender, numimagen, NForm);
			printf ("PICTUREMESSAGE player SENDER: %s\n", respuesta);
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 49)	//SHUFFLE
		{
			char player[30];
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerforms[4];
			int playerscount;
			int position_sender;
			int operation;
			int ronda;
			
			// "49/" + "0/" + Nform + "/" + USER + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + ronda + "/" + position_sender;
			
			p= strtok (NULL,"/");
			operation = atoi(p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(player, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			p= strtok (NULL,"/");
			ronda = atoi(p);
			
			p= strtok (NULL,"/");
			position_sender = atoi(p);
			
			char vectorimagenes_host[15] = "";
			char vectorposiciones[15] = "";
			char vectorimagenes_others[100] = "";
			
			resp = Shuffle(player, operation, ronda, game_info, players, playerscount, playersockets, vectorimagenes_host, vectorposiciones, vectorimagenes_others, conn);
			
			BuscarForms(game_info, playerforms, position_sender, conn);
			
			int k = 0;
			int numForm;
			if (resp == 0)	//SHUFFLE next ronda
			{
				printf ("SHUFFLE Symbols NEXT ROUND\n");
				while (k < playerscount - 1)	//informamos a cada jugador de la partida
				{
					numForm = playerforms[k];
					sprintf(respuesta, "49#0#1#%s#%s#%s#%s#%d#%d,", vectorimagenes_host, vectorimagenes_others, vectorposiciones, player, ronda, numForm);
					printf ("SHUFFLE Symbols player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "49#0#0#%s#%s#%d#%d,", vectorimagenes_host, player, ronda, NForm);
				printf ("SHUFFLE Symbols player WON: %s\n", respuesta);
			}
			else if (resp == 2)	//SHUFFLE alguien se ha ido de la partida
			{
				printf ("SHUFFLE Symbols PLAYER LEFT\n");
				while (k < playerscount - 1)	//informamos a cada jugador de la partida
				{
					numForm = playerforms[k];
					sprintf(respuesta, "49#2#1#%s#%s#%s#%s#%d#%d,", vectorimagenes_host, vectorimagenes_others, vectorposiciones, player, ronda, numForm);
					printf ("SHUFFLE Symbols player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "49#2#0#%s#%s#%d#%d,", vectorimagenes_host, player, ronda, NForm);
				printf ("SHUFFLE Symbols player LEFT: %s\n", respuesta);
			}
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 48)	//END GAME
		{
			char player[30];
			char game_info[60];
			char players[100];
			int playersockets[4];
			int playerforms[4];
			int playerscount;
			int position_sender;
			int operation;
			int ronda;
			
			// "48/" + "0/" + Nform + "/" + guest + "/" + datos_partida + "/" + listplayers + "/" + playerscount + "/" + ronda  + "/" + position_sender;
			
			p= strtok (NULL,"/");
			operation = atoi(p);
			
			p= strtok (NULL,"/");
			NForm = atoi(p);
			
			p= strtok (NULL,"/");
			sprintf(player, p);
			
			p= strtok (NULL,"/");
			sprintf(game_info, p);
			
			p= strtok (NULL,"/");
			sprintf(players, p);
			
			p= strtok (NULL,"/");
			playerscount = atoi(p);
			
			p= strtok (NULL,"/");
			ronda = atoi(p);
			
			p= strtok (NULL,"/");
			position_sender = atoi(p);
			
			resp = EndGame(player, operation, ronda, game_info, players, playerscount, playersockets, conn);
			
			BuscarForms(game_info, playerforms, position_sender, conn);
			
			int k = 0;
			int numForm;
			if (resp == 0)	//button clicked 
			{
				//player corresponde al host de la partida (el unico que puede clicar el boton)
				while (k < playerscount - 1)	//informamos a cada jugador de la partida
				{
					numForm = playerforms[k];
					sprintf(respuesta, "48#0#%s#%d#%d,", player, ronda, numForm);
					printf ("ENDGAME Symbols player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "48#0#%s#%d#%d,", player, ronda, NForm);
				printf ("ENDGAME Symbols player HOST: %s\n", respuesta);
			}
			else if (resp == 1)	//only host remaining
			{
				//player es el host
				sprintf(respuesta, "48#1#%s#%d#%d,", player, ronda, NForm);
				printf ("ENDGAME Symbols player HOST: %s\n", respuesta);
			}
			else if (resp == 2)	//ronda perdida
			{
				//player es el jugador que ha cometido el error
				while (k < playerscount - 1)	//informamos a cada jugador de la partida
				{
					numForm = playerforms[k];
					sprintf(respuesta, "48#2#%s#%d#%d,", player, ronda, numForm);
					printf ("ENDGAME Symbols player socket %d: %s\n", playersockets[k], respuesta);
					write (sockets[playersockets[k]],respuesta, strlen(respuesta));
					k++;
				}
				sprintf(respuesta, "48#2#%s#%d#%d,", player, ronda, NForm);
				printf ("ENDGAME Symbols player LOST: %s\n", respuesta);
			}
			
			write (socket_conn,respuesta, strlen(respuesta));
		}
	 
		if(NotificateConnected)	//Si hay un USER a modificar
		{
			//if ((codigo == 0) || (codigo == 1) || (codigo == 2))
			char notificacion[300];
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
		
		printf ("DONE\n\n///////////////////////////////////////////////////////////////// \n\n");
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
	int port = 50081;
	serv_adr.sin_port = htons(port);
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

int GetListaJugadores(char nombre[60], char Lista[500], MYSQL *conn) 
{
	//Buscamos host_id del serrver donde ha jugado esta persona
	int err;
	MYSQL_RES *resultado;
	MYSQL_RES *resultado2;
	MYSQL_ROW row;
	MYSQL_ROW row2;
	char consulta[500];
	
	int idplayer;
	int j = 0;
	int k = 0;
	int ids[4];
	char name[30];
	char Lista_cpy[500];
	bool first = true;

	sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';",nombre);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	idplayer = atoi(row[0]);
	
	printf("GETLISTAJUGADORES iduser : %d\n", idplayer);
	
	sprintf (consulta, "SELECT DISTINCT id_j1, id_j2, id_j3, id_j4, id_j5 FROM Game WHERE id_j1 = %d OR id_j2 = %d OR id_j3 = %d OR id_j4 = %d OR id_j5 = %d;", idplayer, idplayer, idplayer, idplayer, idplayer);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);

	if(row == NULL)	//no ha jugado ninguna partida
	{
		return -1;
	}
	else
	{
		//filtrem
		while (row != NULL)
		{
			while (j < 5)	//guardamos los ids de los jugadores de cada row obtenida (distintos al del usuario solicitante)
			{
				if (atoi(row[j]) != idplayer)
				{
					ids[k] = atoi(row[j]);
					k++;
				}
				j++;
			}
			k = 0;
			while (k < 4)
			{
				if (ids[k] != 1)
				{
					sprintf (consulta," SELECT nombre FROM Player WHERE id = %d;", ids[k]);
					err = mysql_query(conn, consulta);
					resultado2 = mysql_store_result (conn);
					row2 = mysql_fetch_row (resultado2);
					
					sprintf(name, row2[0]);
					
					if (first)
					{
						sprintf(Lista_cpy, "%s", name);
						first = false;
					}
					else
					{
						sprintf(Lista_cpy, "%s %s", Lista_cpy, name);
					}
				}
				k++;
			}
			row = mysql_fetch_row (resultado);
			j = 0;
			k = 0;
		}
		if (first)	//si no se ha modificado el valor de la Lista (no hay resultados que coincidan con la petiticon)
		{
			printf("GETLISTARESULTADOS return -2\n");
			return -2;
		}
		
		strcpy(Lista, Lista_cpy);
		return 0;
	}
	
}

int GetListaResultados(char nombre[30], char nombres[200], char Lista[500], MYSQL *conn) 
{
	//Buscamos el nombre de la persona que ha ganado en esta fecha
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char nombre1[30];
	char nombre2[30];
	char nombre3[30];
	char nombre4[30];
	int iduser;
	int ids[4] = {-1,-1,-1,-1};
	int idspartidas[4];
	int numplayers;
	char Lista_cpy[500];
	bool first = true;

	sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';", nombre);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	iduser = atoi(row[0]);
	
	//puede haber varios jugadores, otro strtok
	
	char *h = strtok(nombres, " ");

	
	if (h != NULL)
	{
		sprintf(nombre1, "%s", h);
		numplayers = 1;
		
		sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';",nombre1);
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		if (row == NULL)	//el jugador no existe
		{
			return -3;
		}
		ids[0] = atoi(row[0]);
		printf("GETLISTARESULTADOS %s : %d\n", nombre1, ids[0]);
		
		h = strtok(NULL, " ");
		
		if (h != NULL)
		{
			sprintf(nombre2, "%s", h);
			numplayers = 2;
			
			sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';",nombre2);
			err = mysql_query(conn, consulta);
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row == NULL)	//el jugador no existe
			{
				return -3;
			}
			ids[1] = atoi(row[0]);
			printf("GETLISTARESULTADOS %s : %d\n", nombre2, ids[1]);
			
			h = strtok(NULL, " ");
			if (h != NULL)
			{
				sprintf(nombre3, "%s", h);
				numplayers = 3;
				
				sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';",nombre3);
				err = mysql_query(conn, consulta);
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				if (row == NULL)	//el jugador no existe
				{
					return -3;
				}
				ids[2] = atoi(row[0]);
				printf("GETLISTARESULTADOS %s : %d\n", nombre3, ids[2]);
				
				h = strtok(NULL, " ");
				if (h != NULL)
				{
					sprintf(nombre4, "%s", h);
					numplayers = 4;
					
					sprintf (consulta," SELECT id FROM Player WHERE nombre = '%s';",nombre4);
					err = mysql_query(conn, consulta);
					resultado = mysql_store_result (conn);
					row = mysql_fetch_row (resultado);
					if (row == NULL)	//el jugador no existe
					{
						return -3;
					}
					ids[3] = atoi(row[0]);
					printf("GETLISTARESULTADOS %s : %d\n", nombre4, ids[3]);
				}
			}
		}
	}
	printf("GETLISTARESULTADOS player count : %d\n", numplayers);
	printf("GETLISTARESULTADOS ids: %d %d %d %d \n", ids[0], ids[1], ids[2], ids[3]);
	
	sprintf (consulta, "SELECT id_j1, id_j2, id_j3, id_j4, id_j5, ronda FROM Game WHERE id_j1 = %d OR id_j2 = %d OR id_j3 = %d OR id_j4 = %d OR id_j5 = %d;", ids[0], ids[0], ids[0], ids[0], ids[0]);
	printf("GETLISTARESULTADOS: %s\n", consulta);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	int j = 0;
	int k = 0;
	int cont = 0;
	bool Iaminthegame = false;
	
	if(row == NULL)	//no ha jugado ninguna partida
	{
		return -1;
	}
	else
	{
		while (row != NULL)
		{
			printf("GETLISTARESULTADOS ROW\n");
			while (j < 5)	//guardamos los ids de los jugadores de cada row obtenida (distintos al del usuario solicitante)
			{
				if (atoi(row[j]) != iduser)
				{
					idspartidas[k] = atoi(row[j]);
					k++;
				}
				else if (atoi(row[j]) == iduser)
				{
					Iaminthegame = true;
				}
				j++;
			}
			if (!Iaminthegame)
			{
				return -2;
			}
			printf("GETLISTARESULTADOS idspartidas: %d %d %d %d \n", idspartidas[0], idspartidas[1], idspartidas[2], idspartidas[3]);
			k = 0;
			j = 0;
			while (ids[k] != -1)	//recorremos ids[k]
			{
				while ((j != 1) && (j < 4))	//recorremos idspartidas[j]
				{
					if((idspartidas[j] == ids[k]) || (idspartidas[j] == iduser))
					{
						cont++;
					}
					j++;
				}
				k++;
			}
			printf("GETLISTARESULTADOS cont %d\n", cont);
			if (cont == numplayers)	//si en la partida hemos encontrado a todos los jugadores
			{
				printf("GETLISTARESULTADOS ronda %s\n", row[5]);
				if (first)
				{
					sprintf(Lista_cpy, "%s", row[5]);	//guardamos la ronda
					first = false;
				}
				else
				{
					sprintf(Lista_cpy, "%s %s", Lista_cpy, row[5]);
				}
			}
			row = mysql_fetch_row (resultado);
			cont = 0;
			j = 0;
			k = 0;
		}
		if (first)	//si no se ha modificado el valor de la Lista (no hay resultados que coincidan con la petiticon)
		{
			printf("GETLISTARESULTADOS return -2\n");
			return -2;
		}
		strcpy(Lista, Lista_cpy);
		printf("GETLISTARESULTADOS return 0\n");
		return 0;
		
	}
}


int GetListaPartidas(char date1[20], char date2[20], char Lista[500], MYSQL *conn) 
{
	//CODI:
	
		//https://stackoverflow.com/questions/5283120/date-comparison-to-find-which-is-bigger-in-c
		
		//https://stackoverflow.com/questions/11213326/how-to-convert-a-string-variable-containing-time-to-time-t-type-in-c
		
		//https://linux.die.net/man/3/strptime
		
		//https://es.wikipedia.org/wiki/Time.h
	
	
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	MYSQL_RES *resultado2;
	MYSQL_ROW row2;
	char consulta[500];
	
	bool found = false;
	char Lista_cpy[100];
	
	char date1_cpy[20];
	char date2_cpy[20];
	
	sprintf(date1_cpy, "%s", date1);
	sprintf(date2_cpy, "%s", date2);
	
	char full_numdate1[10];
	char *d1 = strtok(date1_cpy, " ");
	sprintf(full_numdate1, "%s", d1);
	
	int num_weekday = get_weekday(full_numdate1);
	
	char weekday_date1[4];
	
	if (num_weekday == -1)
	{
		printf("GETLISTARESULTADOS : return -5\n");
		return -5;
	}
	else
	{
		sprintf (consulta, "SELECT weekday FROM Time WHERE referencenumber = %d;", num_weekday);
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		sprintf(weekday_date1, "%s", row[0]);
		printf("GETLISTARESULTADOS %d : %s\n", num_weekday, weekday_date1);
	}
	
	char full_numdate2[10];
	char *d2 = strtok(date2_cpy, " ");
	sprintf(full_numdate2, "%s", d2);
	
	num_weekday = get_weekday(full_numdate2);
	
	char weekday_date2[4];
	
	if (num_weekday == -1)
	{
		printf("GETLISTARESULTADOS : return -5\n");
		return -5;
	}
	else
	{
		sprintf (consulta, "SELECT weekday FROM Time WHERE referencenumber = %d;", num_weekday);
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		sprintf(weekday_date2, "%s", row[0]);
		printf("GETLISTARESULTADOS %d : %s\n", num_weekday, weekday_date2);
	}
	
	int day_cons;
	int month_cons;
	int year_cons;
	int hour_cons;
	int minutes_cons;
	
	sprintf(date1_cpy, "%s", date1);
	sprintf(date2_cpy, "%s", date2);
	
	char *h1 = strtok(date1_cpy, "-");
	if (h1 == NULL)
	{ return -2; }
	int day1 = atoi(h1);
	h1 = strtok(NULL, "-");
	if (h1 == NULL)
	{ return -2; }
	int month1 = atoi(h1);
	h1 = strtok(NULL, " ");
	if (h1 == NULL)
	{ return -2; }
	int year1 = atoi(h1);
	h1 = strtok(NULL, ":");
	if (h1 == NULL)
	{ return -2; }
	int hour1 = atoi(h1);
	h1 = strtok(NULL, " ");
	if (h1 == NULL)
	{ return -2; }
	int minutes1 = atoi(h1);
	
	char *h2 = strtok(date2_cpy, "-");
	if (h2 == NULL)
	{ return -2; }
	int day2 = atoi(h2);
	h2 = strtok(NULL, "-");
	if (h2 == NULL)
	{ return -2; }
	int month2 = atoi(h2);
	h2 = strtok(NULL, " ");
	if (h2 == NULL)
	{ return -2; }
	int year2 = atoi(h2);
	h2 = strtok(NULL, ":");
	if (h2 == NULL)
	{ return -2; }
	int hour2 = atoi(h2);
	h2 = strtok(NULL, " ");
	if (h2 == NULL)
	{ return -2; }
	int minutes2 = atoi(h2);
	
	char month_date1[4];
	char month_date2[4];
	
	sprintf (consulta, "SELECT month FROM Time WHERE referencenumber = %d;", month1);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	sprintf(month_date1, "%s", row[0]);
	
	sprintf (consulta, "SELECT month FROM Time WHERE referencenumber = %d;", month2);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	sprintf(month_date2, "%s", row[0]);
	
	//CONSTRUIMOS LA FECHA COMPLETA DE AMBAS FECHAS (del tipo "Thu May 16 02:06:26 2024")
	
	char fulldate1[30];
	char fulldate2[30];
	
	sprintf(fulldate1, "%s %s %d %d:%d:00 %d\0", weekday_date1, month_date1, day1, hour1, minutes1, year1);
	sprintf(fulldate2, "%s %s %d %d:%d:00 %d\0", weekday_date2, month_date2, day2, hour2, minutes2, year2);
	
	time_t t1;
	time_t t2;
	
	struct tm tm1;
	struct tm tm2;
	
	strptime(fulldate1, "%A %B %d %H:%M:%S %Y", &tm1);
	t1 = mktime(&tm1);  // t1 is now your desired time_t
	
	strptime(fulldate2, "%A %B %d %H:%M:%S %Y", &tm2);
	t2 = mktime(&tm2);  // t2 is now your desired time_t
	
	
	sprintf (consulta, "SELECT fecha, id_j1, id_s, ronda FROM Game;");
	printf("GETLISTARESULTADOS: %s\n", consulta);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	char *h;
	printf("GETLISTARESULTADOS OK\n");
	
	double difference;
	
	time_t t_table;
	struct tm tm_table;
	
	//filtrem per trobar la primera partida a guardar
	while ((row != NULL) && (!found))
	{
		strptime(row[0], "%A %B %d %H:%M:%S %Y", &tm_table);
		t_table = mktime(&tm_table); 
		
		difference = difftime(t1, t_table);
		
		if (difference > 0)	//t1 > t_table
		{
			printf("GETLISTARESULTADOS ROW\n");
			row = mysql_fetch_row (resultado);
		}
		else
		{
			printf("GETLISTARESULTADOS FOUND\n");
			found = true;
		}
	}
	
	printf("GETLISTARESULTADOS OK\n");
	
	if (!found)	//no se han encontrado resultados entre estas fechas
	{
		printf("GETLISTARESULTADOS : return -1\n");
		return -1;
	}
	
	found = false;
	bool first = true;
	char game[50];
	char server[20];
	char name[30];
	
	while ((row != NULL) && (!found))
	{
		
		strptime(row[0], "%A %B %d %H:%M:%S %Y", &tm_table);
		t_table = mktime(&tm_table); 
		
		difference = difftime(t_table, t2);
		
		if (difference > 0)	//t_table > t2
		{
			found = true;
		}
		else
		{
			sprintf (consulta," SELECT host_id FROM Server WHERE id = %d;", atoi(row[2]));
			err = mysql_query(conn, consulta);
			resultado2 = mysql_store_result (conn);
			row2 = mysql_fetch_row (resultado2);
			
			sprintf(server, "%s", row2[0]);
			
			sprintf (consulta," SELECT nombre FROM Player WHERE id = %d;", atoi(row[1]));
			err = mysql_query(conn, consulta);
			resultado2 = mysql_store_result (conn);
			row2 = mysql_fetch_row (resultado2);
			
			sprintf(name, "%s", row2[0]);
			
			if (first)
			{
				sprintf(game, "Host: %s ; %s ; Round: %s", name, server, row[3]);
				sprintf(Lista_cpy, "%s", game);
				first = false;
			}
			else
			{
				sprintf(game, "Host: %s ; %s ; Round: %s", name, server, row[3]);
				sprintf(Lista_cpy, "%s|%s", Lista_cpy, game);
			}
			
			row = mysql_fetch_row (resultado);
		}
		
	}
	
	strcpy(Lista, Lista_cpy);
	return 0;

}

int get_weekday(char * str) 
{
	struct tm tm;
	memset((void *) &tm, 0, sizeof(tm));
	if (strptime(str, "%d-%m-%Y", &tm) != NULL) {
		time_t t = mktime(&tm);
		if (t >= 0) {
			return localtime(&t)->tm_wday; // Sunday=0, Monday=1, etc.
		}
	}
	return -1;
}

void borrarnombre(char nombre[60], char newconectados[300], int operacion, MYSQL *conn) 
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	
	strcpy(consulta, "\0");
	sprintf (consulta, "DELETE FROM Connected WHERE conectado = '%s';",nombre);

	printf("CLOSECONNECTION: %s\n",consulta);
	
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
		printf("CLOSECONNECTION: Error\n");
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
	
	printf("CLOSECONNECTION numconnected : %d\n", numconectados);
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

void unregister(char nombre[60], MYSQL *conn) 
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	
	
	strcpy(consulta, "\0");
	sprintf (consulta, "DELETE FROM Player WHERE nombre = '%s';",nombre);
	
	printf("UNREGISTER: %s\n",consulta);
	
	err = mysql_query(conn, consulta);
	if(err!=0)
	{
		printf("UNREGISTER: error\n");
	}
	else
	{
		printf("UNREGISTER: ok\n");
	}
}

int BuscarSocket(char namesocket[30], MYSQL *conn)
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int j = 0;
	bool found = false;
	//Devolvera toda la lista de conectados. buscamos el index del invitado
	//EL INDEX DEL INVITADO COINCIDE CON SU INDEX DE SOCKET

	j = 0;
	sprintf (consulta, "SELECT * FROM Connected");
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	while((row != NULL) && (!found))
	{
		if(strcmp(row[0], namesocket) == 0)
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
		printf("SOCKET : ERROR\n");
		return -1;
	}
	else
	{
		printf("SOCKET number: %d\n", j);
		return j;
	}
	
}

void BuscarSockets(char players[100], int playerscount, int playersockets[4], MYSQL *conn)
{ 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char server[20];
	char partida_cpy[60];
	char players_cpy[100];
	char date[30];
	int idplayerhost;
	int idserver;
	
	//GUARDAMOS TODOS LOS SOCKETS A LOS QUE HAY QUE AVISAR (A TODOS LOS INTEGRANTES DE LA PARTIDA EXCEPTO EL QUE SE DESCONECTA) - seguimos la misma metodologia que en JoinGame
	char namesocket[30];
	int j = 0;
	printf("BUSCARSOCKETS players : %s\n", players);
	if (strcmp(players, "-") != 0)	//players = - cuando se desconecta el host y estaba el solo en la partida
	{
		sprintf(players_cpy, players);
		
		char *l = strtok(players_cpy, " ");
		
		while(j < playerscount - 1) //no incluimos al host del cual ya sabemos el socket (socket_conn)
		{
			sprintf(namesocket, l);
			playersockets[j] = BuscarSocket(namesocket, conn);
			printf("BUSCARSOCKETS socket %s: %d\n", namesocket, playersockets[j]);
			j++;
			l = strtok(NULL, " ");
		}
	}
	
	printf("BUSCARSOCKETS sockets: %d %d %d %d \n", playersockets[0], playersockets[1], playersockets[2], playersockets[3]);
}

void BuscarForms(char partida[60], int playerforms[4], int position_sender, MYSQL *conn)
{
	char server[20];
	char partida_cpy[60];
	char date[30];
	int idplayerhost;
	int idserver;
	
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];

	sprintf(partida_cpy, "%s|", partida);
	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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
	
	idserver = atoi(row[0]);
	
	sprintf (consulta, "SELECT * FROM Game WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
	printf("LEAVEGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, ronda, ended, started, form_j1, form_j2, form_j3, form_j4, form_j5)		
	int k = 10;
	int t = 0;
	int position = position_sender + 10;
	while (k < 15)
	{
		if (atoi(row[k]) != -1)
		{
			if (k != position)
			{
				playerforms[t] = atoi(row[k]);	//el que retornaremos a la funcion
				t++;
			}
		}
		k++;
	}
	printf("BUSCARFORMS forms: %d %d %d %d \n", playerforms[0], playerforms[1], playerforms[2], playerforms[3]);
}

int CreateGame(char nombre[60], char partida[200], int numForm, MYSQL *conn)
{
	printf("J1: %s\n", nombre); 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	int contador = 0;
	char server[20];
	char date_format[30];
	
	int seed;
	FILE *f = fopen("/dev/urandom", "r");
	fread(&seed, sizeof(int), 1, f);
	srand(seed);
	fclose(f);
	
	//DATE
	time_t t;
	time(&t);
	char date[30];
	sprintf(date_format, "%s", ctime(&t));
	//CTIME(&T) acaba amb un \n per defecte que s'ha de treure
	char *time = strtok(date_format, "\n");
	/*
	//adaptem el format de la data per a que sigui mes comode i atractiu (de "Thu May 16 02:06:26 2024" a "16/5/2024 02:06")
	char *h = strtok(date, " ");
	h = strtok(NULL, " ");
	char month_letters[3];
	sprintf(month_letters, "%s", h);
	char day[3];
	h = strtok(NULL, " ");
	sprintf(day, "%s", h);
	char clock_time[10];
	h = strtok(NULL, " ");
	sprintf(clock_time, "%s", h);
	char year[5];
	h = strtok(NULL, " ");
	sprintf(year, "%s", h);
	
	//busquem l'equivalent del mes en numero a la taula creada de Months
	sprintf (consulta, "SELECT referencenumber FROM Months WHERE name = '%s';", month_letters);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	char month[3];
	sprintf(month, "%s", row[0]);
	
	char *r = strtok(clock_time, ":");
	char hour[3];
	sprintf(hour, "%s", r);
	char minutes[3];
	r = strtok(NULL, ":");
	sprintf(minutes, "%s", r);
	
	char time_game[6];
	sprintf(time_game, "%s:%s", hour, minutes);
	
	char date_format[30];
	sprintf(date_format, "%s-%s-%s %s:%s", day, month, year, hour, minutes);
	*/
	printf("CREATEGAME DATE: %s\n", date_format);
	
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
	sprintf (consulta, "INSERT INTO Game VALUES (%d,1,1,1,1,%d,'%s',0,0,0,%d,-1,-1,-1,-1);", id_j1, id_s, date_format, numForm);
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, ronda, ended, started, form_j1, form_j2, form_j3, form_j4, form_j5)
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
		sprintf(partida, "%s|%d|%s", server, id_j1, date_format);
		return 0;
	}	
}

int JoinGame(char invited[30], char inviting[30], char partida[60], char otherplayers[100], int othersockets[4], int otherforms[4], int numForm, MYSQL *conn)
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
	char partida_cpy[60];
	char otherplayers_cpy[100];
	int idplayerhost;
	int idserver;
	char index_id_to_ad[5];
	int id_to_ad;
	char index_form_to_ad[10];
	int idplayers[4] = {0};	//maximo otros 4 jugadores
	int id_fromtable;
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	printf("JOINGAME : %s\n", partida);
	sprintf(partida_cpy, "%s|", partida);

	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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
	//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, ronda, ended, started, form_j1, form_j2, form_j3, form_j4, form_j5)
	int j = 1;
	int t = 10;
	int id_x;
	int form_x;
	bool found  = false;
	
	if(atoi(row[8]) == 1)
	{
		//el juego ya ha terminado o ya no esta disponible
		return -2;
	}
	if(atoi(row[9]) == 1)
	{
		//el juego ya ha empezado
		return -3;
	}
	//buscamos donde anyadir el jugador que se esta uniendo. SI id=1 ES QUE ESTA VACIO (id=1 es por defecto del developer)
	while((j < 5) && (!found))
	{
		id_x = atoi(row[j]);
		form_x = atoi(row[t]);
		if(id_x == 1)
		{
			found = true;
		}
		else
		{
			//guardamos los valores de id de los otros jugadores y tambien de los forms
			//si sobra algun hueco, el valor es de 0 en ese hueco
			idplayers[j - 1] = id_x;
			otherforms[j - 1] = form_x;
			t++;
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
	
	
	//si solo esta el host, no se habra guardado el form en otherforms. lo anyadimos
	if (t == 10)
	{
		otherforms[0] = form_x;
	}
	
	//como nos podemos unir a la partida, buscamos el id del jugador a anyadir
	sprintf (consulta, "SELECT id FROM Player WHERE nombre = '%s';", invited);
	err = mysql_query(conn, consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);

	id_to_ad = atoi(row[0]);
	
	//j + 1 ya que la row[j] para j = 1 es el id_2 (j + 1)
	sprintf(index_id_to_ad, "id_j%d", j + 1);
	
	//anyadimos el id del jugador a la partida updateandola
	sprintf (consulta, "UPDATE Game SET %s = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", index_id_to_ad, id_to_ad, idplayerhost, idserver, date);
	printf("JOINGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);
	
	sprintf(index_form_to_ad, "form_j%d", j + 1);
	
	//anyadimos el form del jugador a la partida updateandola
	sprintf (consulta, "UPDATE Game SET %s = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", index_form_to_ad, numForm, idplayerhost, idserver, date);
	printf("JOINGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);

	
	if(err != 0)
	{
		printf("JOINGAME : could not update table \n");
	}
	
	printf("JOINGAME : successfully joined game - position %s\n", index_id_to_ad);
	
	
	//ahora devolveremos la lista de gente que esta en la partida:
	
	//anyadimos el jugador que nos invita a la lista de jugadores de la partida ya que seguro que esta
	sprintf(otherplayers, "%s", inviting);
	
	printf ("JOINGAME ids : %d %d %d %d\n", idplayers[0], idplayers[1], idplayers[2], idplayers[3]);
	
	if(idplayers[0] != 0)	//si hay otros jugadores en la partida (maximo otros 3 a parte del host y el invitado)
	{

		sprintf (consulta, "SELECT * FROM Player;");
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		//row[0] = id | row[1] = nombre
		j = 0;
		found = false;
		
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
					printf ("JOINGAME player found : id = %d , name = %s\n", id_fromtable, row[1]);
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
	
	//buscamos los sockets donde informar a los otros jugadores
	
	int res;
	//minimo informaremos al que nos ha invitado
	res = BuscarSocket(inviting, conn);
	
	if(res != -1)
	{
		othersockets[0] = res;
		printf("JOINGAME hostsocket %s: %d\n", inviting, res);
	}
	else
	{
		return -2;
	}
	
	char namesocket[30];
	int k = 1;
	int numplayers = 1;	//el host minimo
	j = 0;
	sprintf(otherplayers_cpy, otherplayers);
	
	char *l = strtok(otherplayers_cpy, " ");
	l = strtok(NULL, " ");	//saltamos el valor del host que ya lo tenemos
	sprintf(namesocket, l);

	while(idplayers[j] != 0)
	{
		othersockets[k] = BuscarSocket(namesocket, conn);
		printf("JOINGAME socket %s: %d\n", namesocket, othersockets[k]);
		k++;
		j++;
		numplayers ++;
		l = strtok(NULL, " ");
		sprintf(namesocket, l);
	}
	
	printf("JOINGAME OK: \n");
	printf("JOINGAME otherplayers: %s\n", otherplayers);
	printf("JOINGAME othersockets: %d %d %d %d \n", othersockets[0], othersockets[1], othersockets[2], othersockets[3]);
	printf("JOINGAME otherforms: %d %d %d %d \n", otherforms[0], otherforms[1], otherforms[2], otherforms[3]);
	
	return numplayers;
}


int LeaveGame(int creator, char disconnecting[30], char partida[60], char players[100], int playerscount, int playersockets[4], int playerforms[4], int position_leaving, MYSQL *conn)
{
	printf("LEAVEGAME leaving: %s\n", disconnecting); 
	printf("LEAVEGAME game: %s\n", partida); 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char server[20];
	char partida_cpy[60];
	char players_cpy[100];
	char date[30];
	int idplayerhost;
	int idserver;
	int id_discon;
	int idplayers[5];	//maximo otros 4 jugadores
	int formplayers[5];
	int id_fromtable;
	bool found = false;
	
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	sprintf(partida_cpy, "%s|", partida);
	
	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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
	
	//buscamos los forms de los jugadores en la tabla de Game
	BuscarForms(partida, playerforms, position_leaving, conn);
	//y modificamos a partir de los forms obtenidos para introducirlos en la tabla
	int k = 0;
	while (k < playerscount)
	{
		formplayers[k] = playerforms[k];	//formplayers es el que introduciremos en la tabla
		k++;
	}

	while (k < 5)	//llenamos el resto de posiciones con -1 para la tabla
	{
		formplayers[k] = -1;
		k++;
	}
	
	//buscamos los sockets a los que informar
	BuscarSockets(players, playerscount, playersockets, conn);
	
	if (creator == 1)	//esta cerrando partida el host
	{
		//actualizamos la partida en la base de datos con parametro ended = 1 (ha terminado)
		sprintf (consulta, "UPDATE Game SET ended = 1 WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
		//printf("JOINGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		
		//ahora actualizamos a todos los players de la partida que esta ha terminado
		
		printf("LEAVEGAME OK : leaving HOST\n");
		printf("LEAVEGAME players now: %s\n", players);
		printf("LEAVEGAME playersockets: %d %d %d %d \n", playersockets[0], playersockets[1], playersockets[2], playersockets[3]);
		printf("LEAVEGAME playerforms: %d %d %d %d \n", playerforms[0], playerforms[1], playerforms[2], playerforms[3]);
		
		return 1;
	}
	else if (creator == 0)	//creator = 0 --- se va un usuario que no es el host de la partida (no se cierra)
	{
		//s haura de fer el update de la taula Game de la partida
		//borrar el jugador que marxa -> actualitzar la taula movent els ids dels jugadors a la nova posicio -> actualitzar llista de players
		
		//guardamos todos los ids de las personas de la partida
		sprintf (consulta, "SELECT * FROM Game WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
		printf("LEAVEGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		//(id_j1, id_j2, id_j3, id_j4, id_j5, id_s, fecha, ronda, ended, started, form_j1, form_j2, form_j3, form_j4, form_j5)		
		int j = 0;
		while (j < playerscount)
		{
			idplayers[j] = atoi(row[j]);
			j++;
		}
		
		printf ("LEAVEGAME ids : %d %d %d %d %d\n", idplayers[0], idplayers[1], idplayers[2], idplayers[3], idplayers[4]);
		printf("LEAVEGAME playerforms: %d %d %d %d \n", playerforms[0], playerforms[1], playerforms[2], playerforms[3]);
		//buscamos el id del jugador a desconectar
		sprintf (consulta, "SELECT id FROM Player WHERE nombre = '%s';", disconnecting);
		err = mysql_query(conn, consulta);
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		id_discon = atoi(row[0]);
		//lo buscamos en la lista de ids de la partida
		j = 0;
		k = 0;
		while (!found)
		{
			if (idplayers[j] == id_discon)
			{
				found = true;
			}
			else
			{
				j++;
			}
		}
		//lo sustituimos en la lista para luego insertarlo updated en la partida
		while (j + 1 < playerscount)
		{
			idplayers[j] = idplayers[j + 1];
			j++;
		}
		//ponemos el resto de ids a 1 (default developer)
		while (j < 5)
		{
			idplayers[j] = 1;
			j++;
		}
		printf("LEAVEGAME : OK \n");
		//sustituimos el jugador que se ha ido en la tabla
		//idplayers[0] es el host (este no lo actualizamos ya que en este caso (creator = 0) este no se esta desconectando
		sprintf (consulta, "UPDATE Game SET id_j2 = %d, id_j3 = %d, id_j4 = %d, id_j5 = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayers[1], idplayers[2], idplayers[3], idplayers[4], idplayerhost, idserver, date);
		printf("LEAVEGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		
		sprintf (consulta, "UPDATE Game SET form_j2 = %d, form_j3 = %d, form_j4 = %d, form_j5 = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", formplayers[1], formplayers[2], formplayers[3], formplayers[4], idplayerhost, idserver, date);
		printf("LEAVEGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		
		return 0;
	}
	else
	{
		return -1;
	}
}


int StartGame(char host[30], char partida[30],char players[100], int playerscount, int playersockets[4], char vectorimagenes_host[15], char vectorposiciones[15], char vectorimagenes_others[100], int playerforms[4], MYSQL *conn)
{
	//vectorimagenes_host[playerscount];
	//vectorimagenes_others[playerscount * 5];
	
	printf("STARTGAME host: %s\n", host); 
	printf("STARTGAME game: %s\n", partida); 
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char server[20];
	char partida_cpy[60];
	char players_cpy[100];
	char date[30];
	int idplayerhost;
	int idserver;
	
	int random_escogidas[5] = {-1};
	int random_posiciones[5] = {-1};
	int vector_final[20] = {-1};
	
	BuscarForms(partida, playerforms, 0, conn);	//posicion del sender es 0 (posicion del host)

	BuscarSockets(players, playerscount, playersockets, conn);
	
	SymbolsRandomGeneration(random_escogidas, random_posiciones, vector_final, playerscount);
	
	sprintf(vectorimagenes_host, "%d", random_escogidas[0]);
	sprintf(vectorposiciones, "%d", random_posiciones[0]);
	sprintf(vectorimagenes_others, "%d", vector_final[0]);
	
	int j = 1;
	while (j < playerscount)
	{
		sprintf(vectorimagenes_host, "%s %d", vectorimagenes_host, random_escogidas[j]);
		j++;
	}
	j = 1;
	while (j < 5)
	{
		sprintf(vectorposiciones, "%s %d", vectorposiciones, random_posiciones[j]);
		j++;
	}
	j = 1;
	int imagecount = 5 * (playerscount - 1);
	while (j < imagecount)
	{
		sprintf(vectorimagenes_others, "%s %d", vectorimagenes_others, vector_final[j]);
		j++;
	}
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	sprintf(partida_cpy, "%s|", partida);
	
	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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

	sprintf (consulta, "UPDATE Game SET started = 1 WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
	//printf("JOINGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);
	printf("STARTGAME : Symbols started corrected \n");
	return 0;
	
}


void SymbolsRandomGeneration(int random_escogidas[5], int random_posiciones[5], int vector_final[20], int numplayers)
{
	int j = 0;
	int k = 0;

	int num = numplayers - 1;
	
	int seed;
	FILE *f = fopen("/dev/urandom", "r");
	fread(&seed, sizeof(int), 1, f);
	srand(seed);
	fclose(f);
	
	int random;
	bool equal = false;
	
	//generamos las 5 imagenes que se tendrasn que descifrar
	while (j < numplayers)
	{
		random = rand() % 60;
		
		while ((!equal) && (k < numplayers))
		{
			if (random == random_escogidas[k])
			{
				equal = true;
			}
			else
			{
				k++;
			}
		}
		if (!equal)
		{
			random_escogidas[j] = random;
			j++;
		}
		equal = false;
		k = 0;
	}
	 
	printf("RANDOMGENERATION vector escogido : %d %d %d %d %d \n", random_escogidas[0], random_escogidas[1], random_escogidas[2], random_escogidas[3], random_escogidas[4]);
	
	j = 0;
	k = 0;
	//srand(getpid());
	//generamos las 5 posiciones donde iran las imagenes dentro del vector de las 20 totales
	while (j < numplayers)
	{
		random = rand() % (num * 5);
		
		while ((!equal) && (k < numplayers))
		{
			if (random == random_posiciones[k])
			{
				equal = true;
			}
			else
			{
				k++;
			}
		}
		if (!equal)
		{
			random_posiciones[j] = random;
			j++;
		}
		equal = false;
		k = 0;
	}
	printf("RANDOMGENERATION vector posicion : %d %d %d %d %d \n", random_posiciones[0], random_posiciones[1], random_posiciones[2], random_posiciones[3], random_posiciones[4]);
	
	
	j = 0;
	k = 0;
	int h = 0;
	int l = 0;
	bool posicion_taken = false;
	bool image_taken = false;
	
	while (j < (num * 5))
	{
		while ((l < numplayers) && (!posicion_taken))
		{
			if(random_posiciones[l] == j)
			{
				posicion_taken = true;
				//si laa posicion yaa estta eescogida por el vectoor randomm_posiciones, añadimos la que hemos cogido en random_escogidas
				vector_final[j] = random_escogidas[l];
				printf("RANDOMGENERATION final vector %d : %d\n", j, random_escogidas[l]);
			}
			else
			{
				l++;
			}
		}
		if(posicion_taken)
		{
			j++;
			posicion_taken = false;
		}
		else
		{
			random = rand() % 40;
			while ((!image_taken) && (k < (num * 5)))
			{
				if (random == vector_final[k])
				{
					image_taken = true;
				}
				else
				{
					k++;
				}
			}
			if (!image_taken)
			{
				while ((!image_taken) && (h < numplayers))
				{
					if (random == random_escogidas[h])
					{
						image_taken = true;
					}
					else
					{
						h++;
					}
				}
				if (!image_taken)
				{
					vector_final[j] = random;
					printf("RANDOMGENERATION final vector %d : %d\n", j, random);
					j++;
				}
				equal = false;
				h = 0;;
			}
			image_taken = false;
			k = 0;
		}
		
		l = 0;
	}
}

int Shuffle(char player[30], int operation, int ronda, char partida[30],char players[100], int playerscount, int playersockets[4], char vectorimagenes_host[15], char vectorposiciones[15], char vectorimagenes_others[100], MYSQL *conn)
{
	printf("SHUFFLE operation: %d\n", operation);
	printf("SHUFFLE user: %s\n", player);
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char server[20];
	char partida_cpy[60];
	char players_cpy[100];
	char date[30];
	int idplayerhost;
	int idserver;
	
	int random_escogidas[5] = {-1};
	int random_posiciones[5] = {-1};
	int vector_final[20] = {-1};
	
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	sprintf(partida_cpy, "%s|", partida);
	
	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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
	
	BuscarSockets(players, playerscount, playersockets, conn);
	
	
	if(operation == 0)	//SHUFFLE NEXT RONDA
	{
		SymbolsRandomGeneration(random_escogidas, random_posiciones, vector_final, playerscount);
		
		sprintf(vectorimagenes_host, "%d", random_escogidas[0]);
		sprintf(vectorposiciones, "%d", random_posiciones[0]);
		sprintf(vectorimagenes_others, "%d", vector_final[0]);
		
		int j = 1;
		while (j < playerscount)
		{
			sprintf(vectorimagenes_host, "%s %d", vectorimagenes_host, random_escogidas[j]);
			j++;
		}
		j = 1;
		while (j < 5)
		{
			sprintf(vectorposiciones, "%s %d", vectorposiciones, random_posiciones[j]);
			j++;
		}
		j = 1;
		int imagecount = 5 * (playerscount - 1);
		while (j < imagecount)
		{
			sprintf(vectorimagenes_others, "%s %d", vectorimagenes_others, vector_final[j]);
			j++;
		}
		//el valor de ronda ya se ha sumado en el C#
		sprintf (consulta, "UPDATE Game SET ronda = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", ronda, idplayerhost, idserver, date);
		//printf("JOINGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		printf("SHUFFLE : new round %d \n", ronda);
		return 0;
	}
	else if (operation == 2)	//SHUFFLE SIN SUBIR DE RONDA (UN JUGADOR SE HA IDO)
	{
		SymbolsRandomGeneration(random_escogidas, random_posiciones, vector_final, playerscount);
		
		sprintf(vectorimagenes_host, "%d", random_escogidas[0]);
		sprintf(vectorposiciones, "%d", random_posiciones[0]);
		sprintf(vectorimagenes_others, "%d", vector_final[0]);
		
		int j = 1;
		while (j < playerscount)
		{
			sprintf(vectorimagenes_host, "%s %d", vectorimagenes_host, random_escogidas[j]);
			j++;
		}
		j = 1;
		while (j < 5)
		{
			sprintf(vectorposiciones, "%s %d", vectorposiciones, random_posiciones[j]);
			j++;
		}
		j = 1;
		int imagecount = 5 * (playerscount - 1);
		while (j < imagecount)
		{
			sprintf(vectorimagenes_others, "%s %d", vectorimagenes_others, vector_final[j]);
			j++;
		}
		
		sprintf (consulta, "UPDATE Game SET ronda = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", ronda, idplayerhost, idserver, date);
		//printf("JOINGAME : %s \n", consulta);
		err = mysql_query(conn, consulta);
		printf("SHUFFLE : new round %d \n", ronda);
		return 2;
	}
}

int EndGame(char player[30], int operation, int ronda, char partida[30], char players[100], int playerscount, int playersockets[4], MYSQL *conn)
{
	printf("EndGame operation: %d\n", operation);
	printf("EndGame user: %s\n", player);
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	char server[20];
	char partida_cpy[60];
	char players_cpy[100];
	char date[30];
	int idplayerhost;
	int idserver;
	
	
	//partida del estilo : ("%s|%d|%s", server, id_j1, date)
	sprintf(partida_cpy, "%s|", partida);
	
	//cogemos los valores de la partida
	char *h = strtok(partida_cpy, "|");
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
	
	sprintf (consulta, "UPDATE Game SET ronda = %d WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", ronda, idplayerhost, idserver, date);
	//printf("JOINGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);
	printf("ENDGAME : round %d \n", ronda);
	
	//seteamos que la aprtida ya no esta en curso y que se puede unir gente por lo tanto (si hay espacio)
	sprintf (consulta, "UPDATE Game SET started = 0 WHERE id_j1 = %d AND id_s = %d AND fecha = '%s';", idplayerhost, idserver, date);
	//printf("JOINGAME : %s \n", consulta);
	err = mysql_query(conn, consulta);
	printf("ENDGAME : started to 0 \n");

	if (operation == 1)	//Un jugador ha abandonado la partida y solo queda el host (no hace falta informar a otros sockets)
	{
		printf("ENDGAME : only host remaining\n");
		return 1;
	}
	
	BuscarSockets(players, playerscount, playersockets, conn);

	if (operation == 0)	//Se clica el boton de finalizar partida
	{
		printf("ENDGAME : button clicked\n");
		return 0;
	}
	else if (operation == 2)	//RONDA PERDIDA
	{
		printf("ENDGAME : round lost\n");
		return 2;
	}
}













