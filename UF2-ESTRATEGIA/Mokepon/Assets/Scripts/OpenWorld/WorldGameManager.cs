using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGameManager : MonoBehaviour
{
    private IEnumerator m_pokemonHierba;
    private IEnumerator m_segundosInvulnerable;
    [SerializeField]
    GameEventBoolean m_comienzoCombate;
    [SerializeField]
    JugadorController m_jugador1;
    [SerializeField]
    JugadorController m_jugador2;
    [SerializeField]
    int m_jugadoresTocandoHierba;
    [SerializeField]
    bool m_Atacable;

    private void Awake()
    {
        m_Atacable = false;
        m_pokemonHierba = posiblePokemonHierba();
        m_segundosInvulnerable = invulnerabilidad();
        m_jugador1.OnpisandoHierba += JugadorHierba;
        m_jugador2.OnpisandoHierba += JugadorHierba;
        m_jugadoresTocandoHierba = 0;
        StartCoroutine(m_segundosInvulnerable);
       
    }
    private void OnDisable()
    {
        m_jugador1.OnpisandoHierba -= JugadorHierba;
        m_jugador2.OnpisandoHierba -= JugadorHierba;
    }

    public void JugadorHierba(bool b)
    { 
        if (b)
        {
            if (m_jugadoresTocandoHierba == 0)
            {
                m_jugadoresTocandoHierba++;
                if(m_Atacable)
                StartCoroutine(m_pokemonHierba);
            }
           
        }
        else
        {
            m_jugadoresTocandoHierba--;
            if (m_jugadoresTocandoHierba == 0)
            {
                if (m_Atacable)
                StopCoroutine(m_pokemonHierba);
            }
        }

    }


    IEnumerator posiblePokemonHierba()
    {
        while (true)
        {
            int m_random = UnityEngine.Random.Range(0, 101);
            Debug.Log("Puede que te ataque un pokemon" + m_random);
            if (m_random > 65)
            {
                m_comienzoCombate.Raise(true);
                break;
            }
            else
            {
                Debug.Log("No te ataca un pokemon");
            }
            yield return new WaitForSeconds(1);
        }
       
    }

    IEnumerator invulnerabilidad()
    {
        Debug.Log("No me pueden atacar");
        yield return new WaitForSeconds(3);
        Debug.Log("Me pueden atacar");
        m_Atacable = true;
        if (m_jugadoresTocandoHierba != 0)
        {
            StartCoroutine(m_pokemonHierba);
        }
        yield return null;
    }

   

   
}
