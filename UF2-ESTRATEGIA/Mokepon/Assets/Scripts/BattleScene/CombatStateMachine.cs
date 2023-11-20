using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    //STATE MACHINE STUFF

    //STATES
    private enum States { NONE, IDLE, PLAYER1, PLAYER2, IA };
    [SerializeField]
    private States m_CurrentState;

    private void ChangeState(States newState)
    {
        //Debug.Log(newState);

        if (newState == m_CurrentState)
            return;

        ExitState();
        Debug.Log(newState);
        InitState(newState);
    }

    private void UpdateState()
    {
        switch (m_CurrentState)
        {
            case States.IDLE:
                break;
            case States.PLAYER1:

                if (m_ActionOption == -1)
                    return; //DOES NOTHING

                if (m_ActionOption == 0)
                {
                    //THEN SELECTS ATTACK 
                    if (m_MokeponJ1.States != MokeStates.SLEEP && m_MokeponJ1.States != MokeStates.DEFEATED)
                    {
                        //THEN HE CAN ATTACK 
                        Debug.Log("ATACA");
                        if (m_AttackOption == -1)
                            return;
                        if (m_AttackOption == 0 || m_AttackOption == 1)
                        {
                            m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(m_MokeponJ1.GetComponent<Mokepon>().m_AttacksList[m_ActionOption]);

                        }
                    }
                    m_ActionOption = -1; //RESET THE CHOISE
                    m_AttackOption = -1; //RESET THE CHOISE
                    ChangeState(States.PLAYER2);
                }

                if (m_ActionOption == 1)
                {
                    Debug.Log("DESCANSA"); //THEN REST
                    m_MokeponJ1.Rest();
                    m_ActionOption = -1; //RESET THE CHOISE
                    m_AttackOption = -1; //RESET THE CHOISE
                    ChangeState(States.PLAYER2);
                }
                break;
            case States.PLAYER2:

                if (m_ActionOption == -1)
                    return; //DOES NOTHING

                if (m_ActionOption == 0)
                {
                    //THEN SELECTS ATTACK 
                    if (m_MokeponJ2.States != MokeStates.SLEEP && m_MokeponJ2.States != MokeStates.DEFEATED)
                    {
                        //THEN HE CAN ATTACK 
                        Debug.Log("ATACA");
                        if (m_AttackOption == -1)
                            return;
                        if (m_AttackOption == 0 || m_AttackOption == 1)
                        {
                            m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(m_MokeponJ2.GetComponent<Mokepon>().m_AttacksList[m_ActionOption]);

                        }
                    }
                    m_ActionOption = -1; //RESET THE CHOISE
                    m_AttackOption = -1; //RESET THE CHOISE
                    ChangeState(States.IA);
                }

                if (m_ActionOption == 1)
                {
                    Debug.Log("DESCANSA"); //THEN REST
                    m_MokeponJ1.Rest();
                    m_ActionOption = -1; //RESET THE CHOISE
                    m_AttackOption = -1; //RESET THE CHOISE
                    ChangeState(States.IA);
                }
                break;
            case States.IA:
                if (m_MokeponIA.States == MokeStates.SLEEP || m_MokeponIA.States == MokeStates.DEFEATED)
                {
                    Debug.Log("The mokepon is: " + m_MokeponJ1.States + " he cant fight");
                    ChangeState(States.PLAYER1); //DOES NOTHING, THE POKEMON CANT FIGHT
                }

                Attack atkIA = m_MokeponIA.m_AttacksList[Random.Range(0, m_MokeponIA.m_AttacksList.Count)];
                if (atkIA.pp > 0)
                {
                    atkIA.pp--;

                    int PlayerToAtk = Random.Range(0, 2); //ATACKS RANDOMLY ONE OF THE PLAYERS

                    if (PlayerToAtk == 0)
                        m_MokeponJ1.GetComponent<Mokepon>().ReciveAttack(atkIA);
                    else
                        m_MokeponJ2.GetComponent<Mokepon>().ReciveAttack(atkIA);

                    ChangeState(States.PLAYER1);
                }
                Debug.Log("NO ATACO A ESTE ATAQUE NO LE QUEDAN PP (IA)");
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
        }
    }

    // ------------------------------------------------------------------------------------------ \\

    //LOGIC

    //COSAS
    [SerializeField]
    List<MokeponInfo> m_PossibleMoke;

    //JUGADORES
    [SerializeField]
    private Mokepon m_MokeponJ1;
    [SerializeField]
    private Mokepon m_MokeponJ2;

    //IA
    [SerializeField]
    private Mokepon m_MokeponIA;

    //OPTIONS
    private int m_ActionOption;
    private int m_AttackOption;

    private void Awake()
    {
        InitState(States.IDLE);
        m_ActionOption = -1;
        m_AttackOption = -1;
        SetUpBattle();
    }

    private void Update()
    {
        UpdateState();
    }

    public void SetUpBattle()
    {
        int Moke = Random.Range(0, m_PossibleMoke.Count); //ESCOJO UN MOKEPON ALEATORIO CONTRA EL QUE LUCHAR
        m_MokeponIA.LoadInfo(m_PossibleMoke[Moke]); //LO CARGO COMO MOKEPON ENEMIGO

        //CARGO LOS MOKEPONS
        Moke = Random.Range(1, m_PossibleMoke.Count);
        m_MokeponJ1.LoadInfo(m_PossibleMoke[Moke]);
        Moke = Random.Range(1, m_PossibleMoke.Count);
        m_MokeponJ2.LoadInfo(m_PossibleMoke[Moke]);

        ChangeState(States.PLAYER1); //EMPIEZA EL J1
    }

    public void OnSelectedAction(int opt)
    {
        m_ActionOption = opt;
    }
    public void OnSelectedAttack(int opt)
    {
        m_AttackOption = opt;
    }



}
