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
        m_battleText.text = "Comen�a el combat Pokemon";
       
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
