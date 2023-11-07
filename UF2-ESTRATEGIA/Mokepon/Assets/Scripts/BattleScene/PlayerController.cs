using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int m_Player;

    private List<MokeponInfo> m_Mokepons;

    private void Start()
    {
        if (m_Player == 1)
        {
            //CODE OF INPUTSYSTEM FOR PLAYER 1 HERE
        }
        else if (m_Player == 2)
        {
            //CODE OF INPUTSYSTEM FOR PLAYER 2 HERE
        }else
        {
            Debug.Log("no tiene numero asignado");
        }
    }

 }
