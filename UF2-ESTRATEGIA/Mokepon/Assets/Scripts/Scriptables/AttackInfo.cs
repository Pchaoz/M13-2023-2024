using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackInfo", menuName = "Scriptables/AttackInfo")]
public class AttackInfo : ScriptableObject
{
    public string moveName;
    public int damage;
    public int pp;
    public Types type;
    public States state;
}
