using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI m_battleText;
    [SerializeField]
    GameManager m_gameManager;

    void Start()
    {
        //m_battleText.text = "Comença el combat Pokemon";
        m_gameManager.OnComunicateUI += OnMostrarCambios;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMostrarCambios(string n)
    {
        m_battleText.text = n;
    }
}
