using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * De Pablito Vera Peiró para vosotros.
 * 
* Sistema para crear e invocar eventos. Hay que definir el evento y sus parámetros
* y crear una función para invocarlo. Los objetos que se quieran suscribir a este
* evento, simplemente añadirán la FuncionCallback que quierán que responda al evento
* en el OnEnable() y desuscribirla en el OnDisable(). Para invocar el evnto, se debe
* llamar a EventBus.LanzarEventoX(parametros) para invocarlo.
*/
public static class EventBus 
{
    /*Para suscribirse y dessuscribirse de los eventos:
     * 
     * private void OnEnable()
        {
            EventBus.NombreEvento += FuncionCallback;
        }

        private void OnDisable()
        {
            EventBus.Evento -= FuncionCallback;
        }
    
        En el cuerpo del objeto a suscribir
     */

    //--------Declaración de eventos--------//

    //Con parámetros o sin (puede ser cualquier otra cosa que GameObject)
    //public static event Action OnEvento;
    //public static event Action<parametro> OnEventoParametro;
    public static event Action OnEmpezarPartida, OnRondaFinalizada, OnGameOver;
    public static event Action<int> OnNextRound, OnEnemigoMuerto;

    //----------Funciones para invocar los eventos----------//
    //Se llama a estas funciones, no a los eventos directamente,
    //pero las subscripciones si que se hacen al propio evento 

    //Llamadas a eventos con parámetros o sin (puede ser cualquier otra cosa que GameObject)
    //public static void LlamarEvento() => OnEvento?.Invoke();
    //public static void LlamarEventoParametro(Parametro param) => OnEvento?.Invoke();
    public static void EmpezarPartida() => OnEmpezarPartida?.Invoke();
    public static void FinalizarRonda() => OnRondaFinalizada?.Invoke();
    public static void EnemigoMuerto(int score) => OnEnemigoMuerto?.Invoke(score);
    public static void GameOver() => OnGameOver?.Invoke();
    public static void NextRound(int ronda) => OnNextRound?.Invoke(ronda);

}