using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    //STATE MACHINE STUFF

    //STATES
    private enum States { NONE, IDLE, PLAYER1, PLAYER2, IA };
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
                AttackInfo atk1 = m_MokeponJ1.m_AttacksList[Random.Range(1, m_MokeponJ1.m_AttacksList.Count)];
                if (atk1.pp > 0)
                {
                    atk1.pp--;
                    m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(atk1);
                    ChangeState(States.PLAYER2);
                }
                break;
            case States.PLAYER2:
                AttackInfo atk2 = m_MokeponJ2.m_AttacksList[Random.Range(1, m_MokeponJ2.m_AttacksList.Count)];
                if (atk2.pp > 0)
                {
                    atk2.pp--;
                    m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(atk2);
                    ChangeState(States.PLAYER2);
                }
                break;
            case States.IA:
                AttackInfo atkIA = m_MokeponIA.m_AttacksList[Random.Range(1, m_MokeponIA.m_AttacksList.Count)];
                if (atkIA.pp > 0)
                {
                    atkIA.pp--;
                    m_MokeponJ1.GetComponent<Mokepon>().ReciveAttack(atkIA);
                    m_MokeponJ2.GetComponent<Mokepon>().ReciveAttack(atkIA);
                    ChangeState(States.PLAYER2);
                }
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
    private Mokepon m_MokeponJ1;
    private Mokepon m_MokeponJ2;

    //IA
    private Mokepon m_MokeponIA;

    private void Start()
    {
        InitState(States.IDLE);
    }

    private void Update()
    {
        UpdateState();
    }

    public void SetUpBattle(Mokepon Moke1)
    {
        int Moke = Random.Range(1, m_PossibleMoke.Count); //ESCOJO UN MOKEPON ALEATORIO CONTRA EL QUE LUCHAR
        m_MokeponIA.LoadInfo(m_PossibleMoke[Moke]); //LO CARGO COMO MOKEPON ENEMIGO

        //CARGO LOS MOKEPONS
        m_MokeponJ1 = Moke1;

        ChangeState(States.PLAYER1); //EMPIEZA EL J1
    }

}
