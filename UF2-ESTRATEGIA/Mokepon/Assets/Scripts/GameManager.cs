using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Vector2 m_posicionJugador1;
    private Vector2 m_posicionJugador2;
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IniciarEscenaCombate()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void SalirEscenaCombate()
    {
        SceneManager.sceneLoaded += OnVueltaOverworld;
        SceneManager.LoadScene("WorldScene");
    }

    void OnVueltaOverworld(Scene scene, LoadSceneMode mode)
    {

        SceneManager.sceneLoaded -= OnVueltaOverworld;
    }

    // deberiamos hacer un game event que activa las posiciones de los jugadores
    // estaraia bien que este raise tambien cargue los datos de combate

}
