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

    [Header("ATTACK MENU BUTTONS")]
    [SerializeField]
    TextMeshProUGUI m_AtkBtn1;
    [SerializeField]
    TextMeshProUGUI m_AtkBtn2;

    /*
    [SerializeField]
    GameObject m_AtkBtn3;
    [SerializeField]                   // DE MOMENTO SOLO TENDRAN DOS ATAQUES
    GameObject m_AtkBtn4;
    */ 

    public void SwitchToAtk ()
    {
        m_AtkMenu.SetActive(true);
        m_ActionMenu.SetActive(false);        
    }

    public void LoadAtkInfo(List<Attack> atk)
    {
        m_AtkBtn1.SetText(atk[0].moveName);
        m_AtkBtn2.SetText(atk[1].moveName);
    }

    public void SendOption(int opt)
    {
        ///
    }

}
