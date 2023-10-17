using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI m_battleText;

    void Start()
    {
        m_battleText.text = "Comença el combat Pokemon";
        GetComponent<GameManager>().OnComunicateUI += OnMostrarCambios;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMostrarCambios(string n)
    {
        new WaitForSeconds(5);
        m_battleText.text = n;
    }
}
