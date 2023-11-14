using System;
using System.Collections;
using UnityEngine;

public class WorldGameManager : MonoBehaviour
{
    private IEnumerator m_pokemonHierba;
    [SerializeField]
    GameEventBoolean m_comienzoCombate;
    [SerializeField]
    JugadorController m_jugador1;
    [SerializeField]
    JugadorController m_jugador2;
    int m_jugadoresTocandoHierba;

    private void Awake()
    {
        m_pokemonHierba = posiblePokemonHierba();
        m_jugador1.OnpisandoHierba += JugadorHierba;
        m_jugador2.OnpisandoHierba += JugadorHierba;
        m_jugadoresTocandoHierba = 0;
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
            if (m_jugadoresTocandoHierba ==0)
            {
                m_jugadoresTocandoHierba++;
                StartCoroutine(m_pokemonHierba);
            }
        }
        else
        {
            m_jugadoresTocandoHierba--;
            if (m_jugadoresTocandoHierba == 0)
            {
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

   

   
}
