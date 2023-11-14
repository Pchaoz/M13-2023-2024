using System.Collections;
using System.Collections.Generic;
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
    GameObject m_AtkBtn1;
    [SerializeField]
    GameObject m_AtkBtn2;
    [SerializeField]
    GameObject m_AtkBtn3;
    [SerializeField]
    GameObject m_AtkBtn4;

    public void SwitchToAtk ()
    {
        m_AtkMenu.SetActive(true);
        m_ActionMenu.SetActive(false);        
    }

}
