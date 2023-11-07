using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mokepon : MonoBehaviour
{
    private string m_Name;

    //MOKEPON STATS
    [Header("POKEMON STATS")]
    [SerializeField]
    private int m_Hp;
    [SerializeField]
    public List<AttackInfo> m_AttacksList;
    [SerializeField]
    private string m_Type;
    [SerializeField]
    private string m_State;

    public void LoadInfo(MokeponInfo info)
    {
        m_Name = info.mokename;
        m_Hp = info.hp;
        m_AttacksList = info.attackList;
        m_Type = info.type;
        m_State = info.state;
    }

    public void ReciveAttack(AttackInfo atk)
    {
        if (atk.type == m_Type)
        {
            //SOLO RECIBE EL DA�O BASE NO HAY MULTIPLICADOR PORQUE ES DEL MISMO TIPO
            m_Hp -= atk.damage;
        }else if (atk.type == "Fuego" )
        {
            if (m_Type == "Hoja")
            {
                //Da�o por 2?
                m_Hp -= (atk.damage * 2);
            }
            else if (m_Type == "Agua")
            {
                //Da�o reducido? entre 2?
                m_Hp -= (atk.damage / 2);
            }
        }else if (atk.type == "Hoja")
        {
            if (m_Type == "Fuego")
            {
                //Da�o por 2?
                m_Hp -= (atk.damage * 2);
            }
            else if (m_Type == "Agua")
            {
                //Da�o reducido? entre 2?
                m_Hp -= (atk.damage / 2);
            }
        }
        else if (atk.type == "Agua")
        {
            if (m_Type == "Hoja")
            {
                //Da�o por 2?
                m_Hp -= (atk.damage * 2);
            }
            else if (m_Type == "Fuego")
            {
                //Da�o reducido? entre 2?
                m_Hp -= (atk.damage / 2);
            }
        } 
    }
    public void SendAttack(int choise)
    {
        m_AttacksList[choise].pp--;
        //Se envia al game manager y el game manager al 
    }
}
