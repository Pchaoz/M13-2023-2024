using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [Header("ACTION MENU BUTTONS")]
    [SerializeField]
    GameObject m_AtkMenu;
    [SerializeField]
    GameObject m_ActionMenu;

    [Header("INTERFACE HP, STATE AND NAME TEXT")]
    [SerializeField]
    TextMeshProUGUI m_P1_Name;
    [SerializeField]
    TextMeshProUGUI m_P1_HP;
    [SerializeField]
    TextMeshProUGUI m_P1_State;
    [SerializeField]
    TextMeshProUGUI m_P2_Name;
    [SerializeField]
    TextMeshProUGUI m_P2_HP;
    [SerializeField]
    TextMeshProUGUI m_P2_State;
    [SerializeField]
    TextMeshProUGUI m_IA_Name;
    [SerializeField]
    TextMeshProUGUI m_IA_HP;
    [SerializeField]
    TextMeshProUGUI m_IA_State;

    [Header("ATTACK MENU BUTTONS")]
    [SerializeField]
    TextMeshProUGUI m_AtkBtn1_Name;
    [SerializeField]
    TextMeshProUGUI m_AtkBtn1_PP;
    [SerializeField]
    TextMeshProUGUI m_AtkBtn2_Name;
    [SerializeField]
    TextMeshProUGUI m_AtkBtn2_PP;

    [Header("GAME EVENTS")]
    [SerializeField]
    GameEventInt m_ActionChoise;
    [SerializeField]
    GameEventInt m_AttackChoise;
    

    [Header("OTHER OPTIONS")]
    bool isAvaible; //THIS MAKES AVAIBLE TO SELECT

    /*
    [SerializeField]
    GameObject m_AtkBtn3;
    [SerializeField]                   // DE MOMENTO SOLO TENDRAN DOS ATAQUES
    GameObject m_AtkBtn4;
    */

    public void SwitchToAtk ()
    {
        if (isAvaible)
        {
            m_AtkMenu.SetActive(true);
            m_ActionMenu.SetActive(false);
        }
    }

    public void SwitchToAction()
    {
        if (isAvaible)
        {
            m_AtkMenu.SetActive(false);
            m_ActionMenu.SetActive(true);
        }
       
    }
    public void LoadAtkInfo(List<Attack> atk)
    {
        m_AtkBtn1_Name.SetText(atk[0].moveName);
        m_AtkBtn1_PP.SetText(atk[0].pp.ToString());
        m_AtkBtn2_Name.SetText(atk[1].moveName);
        m_AtkBtn2_PP.SetText(atk[1].pp.ToString());
    }

    public void SendAction(int opt)
    {
        if (isAvaible)
            m_ActionChoise.Raise(opt);  

    }
    public void SendAttack(int opt)
    {
        if (isAvaible)
        {
            m_AttackChoise.Raise(opt);
            SwitchToAction();
        }
    }

    public void CanAttack(bool canAttack)
    {
        isAvaible = canAttack;
    }

    public void OnHpChange(string player, int hp)
    {
        if (player.Equals("IA"))
            m_IA_HP.SetText("HP: " + hp);
        if (player.Equals("J1"))
            m_P1_HP.SetText("HP: " + hp);
        if (player.Equals("J2"))
            m_P2_HP.SetText("HP: " + hp);
    }
    public void OnStateChange(string player, MokeStates state)
    {
        if (player.Equals("IA"))
            m_P1_State.SetText("STATE: " + state.ToString());
        if (player.Equals("J1"))
            m_P2_State.SetText("STATE: " + state.ToString());
        if (player.Equals("J2"))
            m_IA_State.SetText("STATE: " + state.ToString());
    }

    public void SetUpBattle(List<Mokepon> mokeList)
    {
        m_P1_Name.SetText(mokeList[0].GetMokename + " (J1)");
        m_P1_HP.SetText("HP: " + mokeList[0].GetMaxHp);
        m_P2_Name.SetText(mokeList[1].GetMokename + " (J2)");
        m_P2_HP.SetText("HP: " + mokeList[1].GetMaxHp);
        m_IA_Name.SetText(mokeList[2].GetMokename + " (IA)");
        m_IA_HP.SetText("HP: " + mokeList[2].GetMaxHp);
    }
}
