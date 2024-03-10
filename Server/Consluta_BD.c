
SELECT jugador.nombre FROM partida,jugador WHERE partida.fecha =  '29/02/2024' AND partida.ganador=jugador.id;

//Retorna el nom del jugador que va guanyar la partida del 29/02/2024