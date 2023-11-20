using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MokeponInfo", menuName = "Scriptables/MokeponInfo")]
public class MokeponInfo : ScriptableObject
{
    public string mokename;
    public int hp;
    public List<AttackInfo> attackList;
    public Types type;
    public MokeStates state;
}
