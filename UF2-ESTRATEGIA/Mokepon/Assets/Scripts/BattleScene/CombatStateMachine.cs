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
                Attack atkJ1;

                atkJ1 = m_MokeponJ1.m_AttacksList[Random.Range(0, m_MokeponJ1.m_AttacksList.Count)];
                if (atkJ1.pp > 0)
                {
                    atkJ1.pp--;
                    m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(atkJ1);
                    ChangeState(States.PLAYER2);
                }
                Debug.Log("NO ATACO A ESTE ATAQUE NO LE QUEDAN PP (JUGADOR 1)");
                break;
            case States.PLAYER2:

                if (m_MokeponJ2.m_State == global::States.SLEEP || m_MokeponJ2.m_State == global::States.DEFEATED)
                {
                    Debug.Log("The mokepon is: " + m_MokeponJ2.m_State + " he cant fight");
                    ChangeState(States.IA); //DOES NOTHING, THE POKEMON CANT FIGHT
                }
                   

                Attack atk2 = m_MokeponJ2.m_AttacksList[Random.Range(0, m_MokeponJ2.m_AttacksList.Count)];
                if (atk2.pp > 0)
                {
                    atk2.pp--;
                    m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(atk2);
                    ChangeState(States.IA);
                    return;
                }
                Debug.Log("NO ATACO A ESTE ATAQUE NO LE QUEDAN PP (JUGADOR 2)");
                break;
            case States.IA:
                if (m_MokeponIA.m_State == global::States.SLEEP || m_MokeponIA.m_State == global::States.DEFEATED)
                {
                    Debug.Log("The mokepon is: " + m_MokeponJ1.m_State + " he cant fight");
                    ChangeState(States.PLAYER1); //DOES NOTHING, THE POKEMON CANT FIGHT
                }

                Attack atkIA = m_MokeponIA.m_AttacksList[Random.Range(0, m_MokeponIA.m_AttacksList.Count)];
                if (atkIA.pp > 0)
                {
                    atkIA.pp--;
                    m_MokeponJ1.GetComponent<Mokepon>().ReciveAttack(atkIA);
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

    private void Awake()
    {
        InitState(States.IDLE);
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

}
