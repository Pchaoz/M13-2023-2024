using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    //STATE MACHINE STUFF

    //STATES
    private enum States { NONE, IDLE, PLAYER1, PLAYER2, IA, WIN };
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

                if (!IsJ1Alive)
                    Debug.Log("EL MOKEPON 1 TA MURIDO :("); //GANAS

                if (m_ActionOption == -1)
                    return; //DOES NOTHING

                if (m_ActionOption == 0)
                {
                    //SENDS TO THE UI THE ATTACK LIST TO CHANGE THE DEFAULT BTN TEXT
                    m_AttacksInfo.Raise(m_MokeponJ1.m_AttacksList);
                    m_ActionOption = 2;
                    return;
                }
                if (m_ActionOption == 2)
                {
                    //THEN SELECTS ATTACK 
                    if (m_MokeponJ1.States != MokeStates.SLEEP && m_MokeponJ1.States != MokeStates.DEFEATED)
                    {
                        //THEN HE CAN ATTACK 
                        //Debug.Log("ATACA");
                        if (m_AttackOption == -1)
                            return;
                        if (m_AttackOption == 0 || m_AttackOption == 1)
                            m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(m_MokeponJ1.GetComponent<Mokepon>().m_AttacksList[m_AttackOption]);
                      
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

                if (!IsJ2Alive)
                    Debug.Log("EL MOKEPON 2 TA MURIDO :("); //GANAS

                if (m_ActionOption == -1)
                    return; //DOES NOTHING

                if (m_ActionOption == 0)
                {
                    //SENDS TO THE UI THE ATTACK LIST TO CHANGE THE DEFAULT BTN TEXT
                    m_AttacksInfo.Raise(m_MokeponJ2.m_AttacksList);
                    //THEN SELECTS ATTACK 
                    if (m_MokeponJ2.States != MokeStates.SLEEP && m_MokeponJ2.States != MokeStates.DEFEATED)
                    {
                        //THEN HE CAN ATTACK 
                        //Debug.Log("ATACA");
                        if (m_AttackOption == -1)
                            return;
                        if (m_AttackOption == 0 || m_AttackOption == 1)
                        {
                            m_MokeponIA.GetComponent<Mokepon>().ReciveAttack(m_MokeponJ2.GetComponent<Mokepon>().m_AttacksList[m_AttackOption]);

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

                if (!IsIaAlive)
                    Debug.Log("TE HAS CARGADO A LA IA :D"); //GANAS
                
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
                    return;
                }
                Debug.Log("NO ATACO A ESTE ATAQUE NO LE QUEDAN PP (IA)");
                break;

            case States.WIN:
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
                IsPlayerTurn.Raise(true); //PERMITS THE PLAYER DO THINGS
                break;
            case States.PLAYER2:
                break;
            case States.IA:
                IsPlayerTurn.Raise(false); //PREVENTS THE PLAYER FROM DOING ACTIONS
                break;
            case States.WIN:
                IsPlayerTurn.Raise(false);
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
            case States.WIN:
                break;
        }
    }

    // ------------------------------------------------------------------------------------------ \\

    //LOGIC

    [Header("ALL MOKEPON LIST FOR THE IA")]
    [SerializeField]
    List<MokeponInfo> m_PossibleMoke;

    [SerializeField]
    private Mokepon m_MokeponJ1;
    [SerializeField]
    private Mokepon m_MokeponJ2;

    [Header("IA OPTIONS")]
    [SerializeField]
    private Mokepon m_MokeponIA;

    [Header("MOKEPON ALIVE BOOLS")]
    private bool IsIaAlive;
    private bool IsJ1Alive;
    private bool IsJ2Alive;

    [Header("EVENTS")]
    [SerializeField]
    private GameEventAtkList m_AttacksInfo;
    [SerializeField]
    private GameEventBoolean IsPlayerTurn;
    [SerializeField]
    private GameEventMokeList BattleSetEvent;

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
        m_MokeponIA.LoadInfo(m_PossibleMoke[Moke], "IA"); //LO CARGO COMO MOKEPON ENEMIGO

        //CARGO LOS MOKEPONS
        Moke = Random.Range(1, m_PossibleMoke.Count);
        m_MokeponJ1.LoadInfo(m_PossibleMoke[Moke], "J1");
        Moke = Random.Range(1, m_PossibleMoke.Count);
        m_MokeponJ2.LoadInfo(m_PossibleMoke[Moke], "J2");

        //POR DEFECTO ESTAN VIVOS
        IsIaAlive = true;
        IsJ1Alive = true;
        IsJ2Alive = true;

    List<Mokepon> mokeSetUp = new List<Mokepon>();
        mokeSetUp.Add(m_MokeponJ1);
        mokeSetUp.Add(m_MokeponJ2);
        mokeSetUp.Add(m_MokeponIA);

        BattleSetEvent.Raise(mokeSetUp); //PONER LOS NOMBRES EN LA UI
        ChangeState(States.PLAYER1); //EMPIEZA EL J1
    }

    public void OnMokeDefeated(string trainer)
    {
        if (trainer.Equals("J1"))
            IsJ1Alive = false;
        else if (trainer.Equals("J2"))
            IsJ2Alive = false;
        else if (trainer.Equals("IA"))
            IsIaAlive = false;
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
