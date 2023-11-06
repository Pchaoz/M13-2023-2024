using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjetosInfo", menuName = "Scriptables/ObjetosInfo")]
public class ObjetosInfo : ScriptableObject
{
    public string nombre;
    public string descripcion;
    public bool curacion;
    public int recuperacion;
    public bool paralizado;
    public bool dormido;
    public bool envenenado;
}
