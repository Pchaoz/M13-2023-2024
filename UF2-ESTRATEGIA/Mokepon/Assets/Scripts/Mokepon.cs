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
    public void RecibeAttack(AttackInfo atk)
    {
        Debug.Log("AU JODER HE RECIBIDO:" + atk.damage);
    }
}
