using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    GameManager m_gameManager;
    private void Awake()
    {
        m_gameManager = GameManager.Instance;
    }

    public void activarCambio()
    {
        m_gameManager.SalirEscenaCombate();
    }
}
