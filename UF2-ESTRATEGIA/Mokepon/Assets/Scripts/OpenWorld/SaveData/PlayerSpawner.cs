using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveData;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameEventVector32 m_posicionesJugadores;

    public void FromSaveData(SaveData data)
    {
        List<Vector3> m_posiciones= new List<Vector3>();
        foreach(SaveData.PlayerData playerData in data.m_players)
        {
            m_posiciones.Add(playerData.position);
        }

        m_posicionesJugadores.Raise(m_posiciones[0], m_posiciones[1]);
    }

}
