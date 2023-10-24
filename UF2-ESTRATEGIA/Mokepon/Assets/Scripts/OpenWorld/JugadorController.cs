using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class JugadorController : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    private InputAction m_MovementAction;
    private int m_Speed=4;
    private Rigidbody2D m_Rigidbody;
    private int m_DangerLayer=0;
    private bool m_corutinaIniciada;


    private void Awake()
    {
        m_Input = Instantiate(m_InputAsset);
        m_MovementAction = m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
        m_Input.FindActionMap("OpenWorld").Enable();
        m_Rigidbody=GetComponent<Rigidbody2D>();
        m_corutinaIniciada = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_DangerLayer= LayerMask.NameToLayer("Danger");
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody.velocity = m_MovementAction.ReadValue<Vector2>() * m_Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
            if (!m_corutinaIniciada) {
            m_corutinaIniciada = true;
            Debug.Log("Inicio corutina");
            StartCoroutine(posiblePokemonHierba());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
            if (m_corutinaIniciada)
            {
                m_corutinaIniciada = false;
                Debug.Log("Parada corutina");
               StopCoroutine(posiblePokemonHierba());
            }
        }
    }

    

    IEnumerator posiblePokemonHierba()
    {
        while (true) {
            Debug.Log("Puede que te ataque un pokemon");
            int m_random=Random.Range(0, 101);
            if (m_random > 90)
            {
                Debug.Log("Te ataca un pokemon");
            }
            else
            {
                Debug.Log("No te ataca un pokemon");

            }
            yield return new WaitForSeconds(5);
        }
    }


}
