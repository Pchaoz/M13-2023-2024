using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackInfo", menuName = "Scriptables/AttackInfo")]
public class AttackInfo : ScriptableObject
{
    public string name;
    public int damage;
    public int pp;
    public string type;
    public string state;
}
