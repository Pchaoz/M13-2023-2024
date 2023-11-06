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
    private int m_Speed=100;
    private Rigidbody2D m_Rigidbody;
    private int m_DangerLayer=0;
    private IEnumerator m_pokemonHierba;


    private void Awake()
    {
        if (gameObject.CompareTag("Player1"))
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_pokemonHierba = posiblePokemonHierba();
            m_Input.FindActionMap("OpenWorld").FindAction("WorldActions").started += Moverse;
            m_Input.FindActionMap("OpenWorld").FindAction("WorldActions").canceled += Pararse;
        }
        else
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld2").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_pokemonHierba = posiblePokemonHierba();
            m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions").started += Moverse;
            m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions").canceled += Pararse;
        }
       

    }

    // Start is called before the first frame update
    void Start()
    {
        m_DangerLayer= LayerMask.NameToLayer("Danger");
    }

    // Update is called once per frame
    void Update()
    {
       
       
       
    }

    void Moverse(InputAction.CallbackContext actionContext)
    {
        if (m_Rigidbody.velocity.x < 4 && m_Rigidbody.velocity.x > -4 && m_Rigidbody.velocity.y < 4 && m_Rigidbody.velocity.y > -4)
            m_Rigidbody.AddForce(m_MovementAction.ReadValue<Vector2>() * m_Speed);

    }

    void Pararse(InputAction.CallbackContext actionContext)
    {
           m_Rigidbody.velocity= Vector2.zero;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
            //Debug.Log("Inicio corutina");
            StartCoroutine(m_pokemonHierba);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
             //Debug.Log("Parada corutina");
             StopCoroutine(m_pokemonHierba);
        }
    }

    

    IEnumerator posiblePokemonHierba()
    {
        while (true) {
            //Debug.Log("Puede que te ataque un pokemon");
            int m_random=Random.Range(0, 101);
            if (m_random > 85)
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
