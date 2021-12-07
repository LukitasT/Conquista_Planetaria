
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		
		public String Consulta1( ArbolGeneral<Planeta> arbol) //Calcula la distancia entre el bot y la raiz
		{
			
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> ArbolAux;
			
			int nivelBot=0;
			
			c.encolar(arbol);
			c.encolar(null);
			
			while(!c.esVacia()){
				ArbolAux = c.desencolar();
				
				if(ArbolAux==null){
					nivelBot++;
					if(!c.esVacia()){
						c.encolar(null);
					}
				}
				else{
					foreach(var hijos in ArbolAux.getHijos()){
						c.encolar(hijos);
					}

					if(ArbolAux.getDatoRaiz().EsPlanetaDeLaIA()){
						return "El bot esta en "+nivelBot;
					}
				}
			}
			return "El bot no esta???";
		}

//---------------------------------------------------------------------------------------------------------------------------

		public String Consulta2( ArbolGeneral<Planeta> arbol) 
		{
			//Retorna un texto con el listado de los planetas ubicados en todos los descendientes del nodo que contiene al planeta del Bot.
			
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>(); //esta cola se va a usar para poder procesar el arbol
            Cola<ArbolGeneral<Planeta>> cr = new Cola<ArbolGeneral<Planeta>>(); //esta cola es la que tiene el resultado "Cola Return"
            ArbolGeneral<Planeta> ArbolAux;

            c.encolar(arbol);

            while(!c.esVacia()){
                ArbolAux = c.desencolar();

                if(ArbolAux.getDatoRaiz().EsPlanetaDeLaIA()){//este if se usa para buscar el bot
                    while(!c.esVacia()){ //cuando encuentro al bot, vacio la cola para poder usarla para procesar
                        c.desencolar();
                    }

                    //ahora tengo que encolar los hijos de esos hijos, y repetirlo hasta que sean hojas
                    while(!ArbolAux.esHoja()){//se ejecuta hasta que no tenga descendientes

                        foreach(var hijos in ArbolAux.getHijos()){
                            cr.encolar(hijos);
                            c.encolar(hijos);
                        }
                        ArbolAux = c.desencolar();
                    }
                }
                else{
                    foreach(var hijos in ArbolAux.getHijos())
                        c.encolar(hijos);
                }
            }

            string texto = "Los planetas descendientes del bot son: \n";
            while(!cr.esVacia()){
                ArbolAux= cr.desencolar();
                texto = texto+ArbolAux.getDatoRaiz().Poblacion().ToString()+"-";
            }
            texto = texto + "\n";
            return texto;
		}

//---------------------------------------------------------------------------------------------------------------------------
		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			//
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> ArbolAux;
			
			int nivelBot=0;
			int[] poblacionXNivel = {0,0,0,0};
			int[] promedioXNivel = {0,0,0,0};
		
			int cantPlanetasXNivel=0;
            c.encolar(arbol);
            c.encolar(null);


            while(!c.esVacia()){
                ArbolAux = c.desencolar();

                if(ArbolAux==null){
                    nivelBot++;
                    cantPlanetasXNivel=0;
                    if(!c.esVacia()){
                        c.encolar(null);
                    }
                }
                else{
                    foreach(var hijos in ArbolAux.getHijos()){
                        c.encolar(hijos);
                    }
                    cantPlanetasXNivel++;
                    poblacionXNivel[nivelBot]=poblacionXNivel[nivelBot]+ArbolAux.getDatoRaiz().Poblacion();
                    promedioXNivel[nivelBot]=poblacionXNivel[nivelBot]/cantPlanetasXNivel;
                }
            }
            int poblacionTotal=0;
            int niv=0;
            while(niv<=poblacionXNivel.Length-1){//hardco
                poblacionTotal+=poblacionXNivel[niv];  
                niv++;
            }
			
            return "\n\nLa poblacion total es: "+poblacionTotal+"\nEl promedio del primer nivel es: "+promedioXNivel[1]+" total: "+poblacionXNivel[1]+"\nEl promedio del segundo nivel es: "+promedioXNivel[2]+" total: "+poblacionXNivel[2]+"\nEl promedio del tercer nivel es: "+promedioXNivel[3]+" total: "+poblacionXNivel[3];
		}
		
		
//--------------------------------------------------------------------------------------------------------------------------------	

	
	
public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
{
	List<ArbolGeneral<Planeta>> bot = new List<ArbolGeneral<Planeta>>();//lista de planetas del bot
	List<ArbolGeneral<Planeta>> player = new List<ArbolGeneral<Planeta>>();//lista de planetas del jugador
	
	__caminoBot(arbol,arbol,bot); // Declaraciones del bot
	__caminoJugador(arbol,arbol,player); // Declaraciones del jugador
	
	if(!bot[0].getDatoRaiz().EsPlanetaDeLaIA()){ //Si desde su pocision es siguiente paneta es de la IA
		
		Movimiento movimiento = new Movimiento(bot[bot.Count-1].getDatoRaiz(),bot[bot.Count-2].getDatoRaiz());
		//Se mueve desde el origen hasta llegar a la raiz
		
		return movimiento;
	}
	int contador = 0;
	if(player[0].getDatoRaiz().EsPlanetaDeLaIA()){//si la raiz es de la IA
		while(contador<player.Count){
			if(player[contador].getDatoRaiz().EsPlanetaDeLaIA() && !player[contador+1].getDatoRaiz().EsPlanetaDeLaIA()){
				//si el planeta del contador es de la IA y el siguiente no es de la IA
				
				Movimiento movimiento = new Movimiento(player[contador].getDatoRaiz(),player[contador+1].getDatoRaiz());
				//Se mueve hasta llegar el jugador
				
				return movimiento;
			}
			else{
				contador++;
			}
		}
	}
	return null;
}


	
	private bool __caminoBot(ArbolGeneral<Planeta> arbol, ArbolGeneral<Planeta> origen, List<ArbolGeneral<Planeta>> camino){
	//Funcion recursiva para el bot. Si el planeta (que es arbol auxiliar), no es del bot, sigue revisando por el hijo.
		bool caminoHallado = false;
		
		camino.Add(origen);


		if(origen.getDatoRaiz().EsPlanetaDeLaIA()){
			caminoHallado=true;
		}
		
		else{
			foreach(var hijo in origen.getHijos()){
				 caminoHallado = __caminoBot(arbol,hijo,camino);
				
				if(caminoHallado){
					break;
				}
				
				camino.RemoveAt(camino.Count - 1);
			}
		}
		return caminoHallado;
	}
		
	private bool __caminoJugador(ArbolGeneral<Planeta> arbol, ArbolGeneral<Planeta> origen, List<ArbolGeneral<Planeta>> camino){
	//funcion recursiva para el jugador.Si el planeta (que es arbol auxiliar), no es del jugador, sigue revisando por el hijo.
		bool caminoHallado = false;
		
		camino.Add(origen);


		if(origen.getDatoRaiz().EsPlanetaDelJugador()){
			caminoHallado=true;
		}
		
		else{
			foreach(var hijo in origen.getHijos()){
				caminoHallado = __caminoJugador(arbol,hijo,camino);
				
				if(caminoHallado){
					break;
				}
				
				camino.RemoveAt(camino.Count - 1);
			}
		}
		return caminoHallado;
	}
	
	
	}

}
