using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Action<String> OnComunicateUI;

    void Start()
    {
        

    }

    void Update()
    {
        
    }

    public void ComunicateUI(String n)
    {
        OnComunicateUI?.Invoke(n);
    }

    public void OnCambioTurno ()
    {

    }
}
