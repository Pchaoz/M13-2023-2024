using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Camera m_CameraWorld;
    [SerializeField]
    Camera m_CameraBattle;
    [SerializeField]
    JugadorController m_jugador1;
    [SerializeField]
    JugadorController m_jugador2;

    bool m_enCombate;
    bool m_turnoJugador1;
    bool m_comienzoturno;

    public Action<String> OnComunicateUI;

    void Start()
    {
        //Assert.IsNotNull(m_jugador1);
        //Assert.IsNotNull(m_jugador2);
        m_enCombate = false;
        Debug.Log("Me suscribo a los jugadores");
        m_jugador1.OnCombatStart += StartCombat;
        m_jugador2.OnCombatStart += StartCombat;
        Debug.Log("Me he suscrito a los jugadores");

    }

    void Update()
    {
        if (m_enCombate) { 
        if (m_comienzoturno == true)
        {
            if (m_turnoJugador1 == true)
            {
                ComunicateUI("Turno jugador 1");
                m_turnoJugador1 = false;
            }
            else
            {
                ComunicateUI("Turno jugador 2");
                m_turnoJugador1 = true;
            }
        }
        m_comienzoturno = false;
        }
    }

    public void StartCombat(bool b)
    {
        m_CameraBattle.GetComponent<Camera>().enabled = true;
        m_enCombate = true;
        bool m_turnoJugador1 = true;
        m_comienzoturno = true;
        StartCoroutine("CambiodeTurno");

    }

    public void ComunicateUI(String n)
    {
        OnComunicateUI?.Invoke(n);
    }

    IEnumerator CambiodeTurno()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            m_comienzoturno = true;
        }
    }
}
