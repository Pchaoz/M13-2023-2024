using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public int damage;
    public int pp;
    public string type;
    public string state;

    public Attack(AttackInfo info)
    {
        name = info.name;
        damage = info.damage;
        pp = info.pp;
        type = info.type;
        state = info.state;
    }

}
