using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mokepon : MonoBehaviour
{


    //STATES

    [SerializeField]
    private MokeStates m_CurrentState;
    public MokeStates States { get { return m_CurrentState; } }

    private void ChangeState(MokeStates newState)
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
            case MokeStates.NONE:

                break;
            case MokeStates.SLEEP:

                break;
            case MokeStates.DEFEATED:

                break;
        }
    }
    private void InitState(MokeStates currentState)
    {
        m_CurrentState = currentState;

        switch (m_CurrentState)
        {
            case MokeStates.NONE:
                break;
            case MokeStates.SLEEP:
                break;
            case MokeStates.DEFEATED:
                break;
        }
    }
    private void ExitState()
    {
        switch (m_CurrentState)
        {
            case MokeStates.NONE:
                break;
            case MokeStates.SLEEP:
                break;
            case MokeStates.DEFEATED:
                break;
        }
    }

    // ------------------------------------------------------------------------------------------ \\


    [SerializeField]
    public string m_Mokename;
    public string GetMokename => m_Mokename;

    //MOKEPON STATS
    [Header("POKEMON STATS")]
    private int m_MaxHp;
    public int GetMaxHp => m_MaxHp;
    [SerializeField]
    private int m_Hp;
    public int GetHp => m_Hp;
    [SerializeField]
    public List<Attack> m_AttacksList;
    [SerializeField]
    private Types m_Type;
    [SerializeField]
    private int m_experiencia;
    [SerializeField]
    private int m_level;

    [SerializeField]
    private StringIntEvent OnHpChange;

    private void Awake()
    {
        UpdateState();
        if(m_mokeponInfo != null)
        {
            LoadInfo(m_mokeponInfo);
        }
    }

    public void LoadInfo(MokeponInfo info)
    {
        m_AttacksList = new List<Attack>();
        m_Mokename = info.mokename;
        m_MaxHp = info.hp;
        m_Type = info.type;
        InitState(info.state);

        foreach (AttackInfo atk in info.attackList)
        {
            Attack at = new Attack(atk);
            m_AttacksList.Add(at);
        }

        m_Hp = m_MaxHp;
    }

    public void ReciveAttack(Attack atk, String player)
    {
        int dmg;
        if (atk.type == m_Type)
        {
            //SOLO RECIBE EL DA�O BASE NO HAY MULTIPLICADOR PORQUE ES DEL MISMO TIPO
            dmg = atk.damage;
            GetHurt(dmg, player);
        }
        else if (atk.type == Types.Fuego)
        {
            if (m_Type == Types.Hierba)
            {
                //Da�o por 2?
                dmg = atk.damage / 2;
                GetHurt(dmg, player);
            }
            else if (m_Type == Types.Agua)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage * 2;
                GetHurt(dmg, player);
            }
        }
        else if (atk.type == Types.Hierba)
        {
            if (m_Type == Types.Fuego)
            {
                //Da�o por 2?
                dmg = atk.damage * 2;
                GetHurt(dmg, player);
            }
            else if (m_Type == Types.Agua)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage / 2;
                GetHurt(dmg, player);
            }
        }
        else if (atk.type == Types.Agua)
        {
            if (m_Type == Types.Hierba)
            {
                //Da�o por 2?
                dmg = atk.damage * 2;
                GetHurt(dmg, player);
            }
            else if (m_Type == Types.Fuego)
            {
                //Da�o reducido? entre 2?
                dmg = atk.damage / 2;
                GetHurt(dmg, player);
            }
        }
        else
        {
            dmg = atk.damage;
            GetHurt(dmg, player);
        }

        if (m_Hp < 1)
            ChangeState(MokeStates.DEFEATED);
    }

    private void GetHurt(int dmg, string player)
    {
        m_Hp -= dmg;
        OnHpChange.Raise(player, m_Hp); //AVISO DE QUE HE RECIBIDO EL DAÑO
    }

    public void Rest(string player)
    {
        m_Hp += 5;
        if (m_Hp > m_MaxHp)
            m_Hp = m_MaxHp;

        Debug.Log("EL JUGADOR "  + player + " HA DESCANSADO Y TIENE " + m_Hp + " HP");
        OnHpChange.Raise(player, m_Hp);
    }

    public void recompensaGanarCombate(int n)
    {
        m_experiencia += n;
        while(m_experiencia >= 100)
        {
            m_experiencia -= 100;
            m_level++;
            Debug.Log("El Mokepon " + name + "sube de nivel a " + m_level);
            subirNivel();
        }


    }

    public void subirNivel()
    {
       int VidaExtra = Random.Range(5, 11);
       Debug.Log("El Mokepon " + name + "consigue más vida, recibe un total de: " + VidaExtra+". Ahora su vida total es de "+VidaExtra+ m_MaxHp);
       m_MaxHp += VidaExtra;
       m_Hp = m_MaxHp;

    }



}
