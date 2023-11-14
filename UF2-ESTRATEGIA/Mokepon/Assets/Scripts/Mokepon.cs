using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mokepon : MonoBehaviour
{


    //STATES

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
            case States.NONE:

                break;
            case States.SLEEP:

                break;
            case States.DEFEATED:

                break;
        }
    }
    private void InitState(States currentState)
    {
        m_CurrentState = currentState;

        switch (m_CurrentState)
        {
            case States.NONE:
                break;
            case States.SLEEP:
                break;
            case States.DEFEATED:
                break;
        }
    }
    private void ExitState()
    {
        switch (m_CurrentState)
        {
            case States.NONE:
                break;
            case States.SLEEP:
                break;
            case States.DEFEATED:
                break;
        }
    }

    // ------------------------------------------------------------------------------------------ \\


    [SerializeField]
    private string m_Mokename;

    //MOKEPON STATS
    [Header("POKEMON STATS")]
    [SerializeField]
    private int m_Hp;
    [SerializeField]
    public List<Attack> m_AttacksList;
    [SerializeField]
    private Types m_Type;
    [SerializeField]
    public States m_State;

    private void Awake()
    {
        UpdateState();
    }

    public void LoadInfo(MokeponInfo info)
    {
        m_AttacksList = new List<Attack>();
        m_Mokename = info.mokename;
        m_Hp = info.hp;
        m_Type = info.type;
        m_State = info.state;

        foreach (AttackInfo atk in info.attackList)
        {
            Attack at = new Attack(atk);
            m_AttacksList.Add(at);
        }
    }

    public void ReciveAttack(Attack atk)
    {
        int dmg;
        if (atk.type == m_Type)
        {
            //SOLO RECIBE EL DA�O BASE NO HAY MULTIPLICADOR PORQUE ES DEL MISMO TIPO
            dmg = atk.damage;
            GetHurt(dmg);
        }
        else if (atk.type == Types.Fuego)
        {
            if (m_Type == Types.Hierba)
            {
                //Da�o por 2?
                dmg = atk.damage / 2;
                GetHurt(dmg);
            }
            else if (m_Type == Types.Agua)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage * 2;
                GetHurt(dmg);
            }
        }
        else if (atk.type == Types.Hierba)
        {
            if (m_Type == Types.Fuego)
            {
                //Da�o por 2?
                dmg = atk.damage * 2;
                GetHurt(dmg);
            }
            else if (m_Type == Types.Agua)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage / 2;
                GetHurt(dmg);
            }
        }
        else if (atk.type == Types.Agua)
        {
            if (m_Type == Types.Hierba)
            {
                //Da�o por 2?
                dmg = atk.damage * 2;
                GetHurt(dmg);
            }
            else if (m_Type == Types.Fuego)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage / 2;
                GetHurt(dmg);
            }
        }
        else
        {
            dmg = atk.damage;
            GetHurt(dmg);
        }
        Debug.Log("HE RECIBIDO EL ATAQUE " + atk.moveName + " Y AHORA MI VIDA ES MENOR. MI VIDA ES: " + m_Hp);
        if (m_Hp < 1)
            m_State = States.DEFEATED;
    }

    private void GetHurt(int dmg)
    {
        Debug.Log("TENGO " + m_Hp + " DE VIDA Y RECIBO " + dmg + " DE DAÑO");
        m_Hp -= dmg;
    }
}
