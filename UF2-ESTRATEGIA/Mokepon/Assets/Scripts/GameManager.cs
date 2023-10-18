using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool m_turnoJugador1;
    bool m_comienzoturno;

    public Action<String> OnComunicateUI;

    void Start()
    {
        bool m_turnoJugador1=true;
        m_comienzoturno = true;
        StartCoroutine("CambiodeTurno");

    }

    void Update()
    {
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
