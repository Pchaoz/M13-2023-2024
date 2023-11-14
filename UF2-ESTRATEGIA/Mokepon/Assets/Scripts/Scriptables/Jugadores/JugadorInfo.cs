using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "JugadorInfo", menuName = "Scriptables/JugadorInfo")]
public class JugadorInfo : ScriptableObject
{
    public string m_name;
    public Vector3 m_posicionFinal;
    public Mokepon m_mokeponActivo;

}
