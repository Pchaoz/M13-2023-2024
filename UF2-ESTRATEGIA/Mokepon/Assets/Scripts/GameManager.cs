using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    private static GameManager m_Instance;
    [SerializeField]
    private GameEventBoolean m_vueltadeCombate;
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
        StartCoroutine(transicion("BattleScene"));
    }

    public void SalirEscenaCombate()
    {
        SceneManager.sceneLoaded += OnVueltaOverworld;
        StartCoroutine(transicion("WorldScene"));
    }

    void OnVueltaOverworld(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("GM de tornada");
        m_vueltadeCombate.Raise(true);
        SceneManager.sceneLoaded -= OnVueltaOverworld;
    }

    IEnumerator transicion(string siguienteEscena)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(siguienteEscena);
    }

    
}
