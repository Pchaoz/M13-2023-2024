using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveData;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameEventVector32 m_posicionesJugadores;

    [SerializeField]
    GameEventListInt2 m_valoresMokepon;

    [SerializeField]
    GameEventMokeponInfo2 m_mokeponsInfo;

    [SerializeField]
    List<MokeponInfo> m_pokedex;

    public void FromSaveData(SaveData data)
    {
        List<Vector3> m_posiciones= new List<Vector3>();
        List<List<int>> valoresMokepon = new List<List<int>>();
        List<MokeponInfo> infoMokepon = new List<MokeponInfo>();

        foreach (SaveData.PlayerData playerData in data.m_players)
        {
            m_posiciones.Add(playerData.position);
            int idMoke = playerData.valoresMokepon[0];
            foreach(MokeponInfo info in m_pokedex)
            {
                if (info.id == idMoke)
                {
                    infoMokepon.Add(info);
                }
            }
            playerData.valoresMokepon.RemoveAt(0);
            valoresMokepon.Add(playerData.valoresMokepon);
            
        }

        m_posicionesJugadores.Raise(m_posiciones[0], m_posiciones[1]);
        m_mokeponsInfo.Raise(infoMokepon[0], infoMokepon[1]);
        m_valoresMokepon.Raise(valoresMokepon[0], valoresMokepon[1]);
    }

}
