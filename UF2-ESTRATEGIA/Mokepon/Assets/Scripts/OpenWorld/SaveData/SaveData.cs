using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    [Serializable]
    public struct PlayerData
    {
        public Vector3 position;
        public List<int> valoresMokepon;

        public PlayerData(Vector3 _position, List<int> vmokpe)
        {
            position = _position;
            valoresMokepon = vmokpe;

        }
    }

    public PlayerData[] m_players;

    public void PopulateData(ISaveableObject[] _playersData)
    {
        m_players = new PlayerData[_playersData.Length];
        for(int i =0; i<_playersData.Length;i++)
            m_players[i] = _playersData[i].Save();

    }

    public interface ISaveableObject
    {
        public PlayerData Save();
        public void Load(PlayerData _playerData);
    }

}
