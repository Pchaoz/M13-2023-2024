using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager m_Instance;
    [SerializeField]
    private GameEventVector32 m_vueltadeCombate;
    [SerializeField]
    private GameEventMokepon2 m_tusMokepon;
    Vector3 m_ultimaPoscionJugador1;
    Vector3 m_ultimaPoscionJugador2;
    [SerializeField]
    Mokepon m_mokeponJugador1;
    [SerializeField]
    Mokepon m_mokeponJugador2;


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
        SceneManager.sceneLoaded += OnCambioEscena;
        StartCoroutine(transicion("BattleScene"));
    }


    public void SalirEscenaCombate()
    {
        SceneManager.sceneLoaded += OnCambioEscena;
        StartCoroutine(transicion("WorldScene"));
    }

    void OnCambioEscena(Scene scene, LoadSceneMode mode)
    {
        if(scene.name== "WorldScene")
        {
            Debug.Log("Estoy en el world");
            m_vueltadeCombate.Raise(m_ultimaPoscionJugador1, m_ultimaPoscionJugador2);
            SceneManager.sceneLoaded -= OnCambioEscena;
        }
        if(scene.name== "BattleScene")
        {
            Debug.Log("Estoy en el combate");
            m_tusMokepon.Raise(m_mokeponJugador1, m_mokeponJugador2);
            SceneManager.sceneLoaded -= OnCambioEscena;

        }

    }

    IEnumerator transicion(string siguienteEscena)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(siguienteEscena);
    }

    //Guardado de información de los jugadores en los cambios de escenas
    public void setpos1(Vector3 v31)
    {
        m_ultimaPoscionJugador1 = v31;
    }
    public void setpos2(Vector3 v31)
    {
        m_ultimaPoscionJugador2 = v31;
    }

    public void mokepon1(Mokepon m)
    {
        m_mokeponJugador1 = m;
    }
    public void mokepon2(Mokepon m)
    {
        m_mokeponJugador2 = m;

    }


}

