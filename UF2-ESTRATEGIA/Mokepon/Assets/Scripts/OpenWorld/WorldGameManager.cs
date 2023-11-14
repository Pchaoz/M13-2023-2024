using System;
using System.Collections;
using UnityEngine;

public class WorldGameManager : MonoBehaviour
{
    [SerializeField]
    JugadorController m_jugador1;
    [SerializeField]
    JugadorController m_jugador2;
    private IEnumerator m_pokemonHierba;
    bool m_enHierba;
    bool m_enCombate;
    [SerializeField]
    GameEventBoolean m_comienzoCombate;

    private void Awake()
    {
        m_pokemonHierba = posiblePokemonHierba();
        m_enHierba = false;
        m_jugador1.OnpisandoHierba += JugadorHierba;
        m_jugador2.OnpisandoHierba += JugadorHierba;
        m_enCombate = false;
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
            if (!m_enHierba &&(m_jugador2.m_enHierba|| m_jugador1.m_enHierba))
            {
                StartCoroutine(m_pokemonHierba);
                m_enHierba = true;
            }
        }
        else
        {
            if (m_enHierba && (!m_jugador2.m_enHierba || !m_jugador1.m_enHierba))
            {
                StopCoroutine(m_pokemonHierba);
                m_enHierba = false;
            }
               
        }
       
    }


    IEnumerator posiblePokemonHierba()
    {
        while (!m_enCombate)
        {
           
            int m_random = UnityEngine.Random.Range(0, 101);
            Debug.Log("Puede que te ataque un pokemon" + m_random);
            if (m_random > 65)
            {
                m_comienzoCombate.Raise(true);
                m_enCombate = true;
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
