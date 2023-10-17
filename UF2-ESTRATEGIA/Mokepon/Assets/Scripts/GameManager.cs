using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool m_turnoJugador1;
    bool m_turnoJugador2;
    bool m_comienzoturno;

    public Action<String> OnComunicateUI;


    void Start()
    {
        bool m_turnoJugador1=true;
        StartCoroutine("CambiodeTurno");

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ComunicateUI(String n)
    {
        OnComunicateUI?.Invoke(n);
    }

    IEnumerator CambiodeTurno()
    {
        while (true)
        {
            ComunicateUI("Hola");


        }
    }
}
