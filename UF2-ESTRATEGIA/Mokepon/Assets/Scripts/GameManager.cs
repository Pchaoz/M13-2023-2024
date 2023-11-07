using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    //STATES
    private enum States { NONE, IDLE, PLAYER1, PLAYER2, IA, OPENWORLD };
    private States m_CurrentState;

    private void ChangeState(States newState)
    {
        //Debug.Log(newState);

        if (newState == m_CurrentState)
            return;

        ExitState();
        InitState(newState);
    }

    private void UpdateState()
    {


        switch (m_CurrentState)
        {
            case States.IDLE:
                break;

            case States.PLAYER1:
                break;

            case States.PLAYER2:
                break;

            case States.IA:
                break;

            case States.OPENWORLD:
                break;
        }
    }
    private void InitState(States currentState)
    {
        m_CurrentState = currentState;

        switch (m_CurrentState)
        {
            case States.IDLE:
                break;

            case States.PLAYER1:
                break;

            case States.PLAYER2:
                break;

            case States.IA:
                break;

            case States.OPENWORLD:
                break;
        }
    }
    private void ExitState()
    {
        switch (m_CurrentState)
        {
            case States.IDLE:
                break;

            case States.PLAYER1:
                break;

            case States.PLAYER2:
                break;

            case States.IA:
                break;

            case States.OPENWORLD:
                break;
        }
    }


    bool m_turnoJugador1;
    bool m_comienzoturno;

    public Action<String> OnComunicateUI;

    void Start()
    {
        InitState(States.OPENWORLD);

    }

    void Update()
    {
        UpdateState();
    }

    public void ComunicateUI(String n)
    {
        OnComunicateUI?.Invoke(n);
    }

    public void OnCambioTurno ()
    {

    }
}
