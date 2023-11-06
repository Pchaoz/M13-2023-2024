using System;
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
    [SerializeField]
    private int m_Speed=30;
    private Rigidbody2D m_Rigidbody;
    private int m_DangerLayer=0;
    private int m_EffectorLayer = 0;
    private IEnumerator m_pokemonHierba;
    private bool m_underEffector;
    private bool m_combateIniciado;
    public Action<Boolean> OnCombatStart;

    private enum SwitchMachinesStates { NONE, IDLE, WALK, BATTLE };
    [SerializeField]
    private SwitchMachinesStates m_CurrentState;

    private void ChangeState(SwitchMachinesStates newState)
    {
        if (newState == m_CurrentState)
            return;

        InitState(newState);
    }

    private void InitState(SwitchMachinesStates currentState)
    {
        m_CurrentState = currentState;
        switch (m_CurrentState)
        {
            case SwitchMachinesStates.IDLE:
                m_Rigidbody.velocity = Vector2.zero;

                break;

            case SwitchMachinesStates.WALK:

                break;
            case SwitchMachinesStates.BATTLE:
                m_Rigidbody.velocity = Vector2.zero;

                break;
        }

    }

    private void UpdateState()
    {
        switch (m_CurrentState)
        {

            case SwitchMachinesStates.IDLE:
                if(!m_underEffector)
                m_Rigidbody.velocity = Vector2.zero;
                if (m_MovementAction.ReadValue<Vector2>() != new Vector2(0, 0))
                {
                    ChangeState(SwitchMachinesStates.WALK);
                }

                break;
            case SwitchMachinesStates.WALK:
                if (m_Rigidbody.velocity.x < 1 && m_Rigidbody.velocity.x > -1 && m_Rigidbody.velocity.y < 1 && m_Rigidbody.velocity.y > -1)
                {
                    m_Rigidbody.AddForce(m_MovementAction.ReadValue<Vector2>() * m_Speed);
                    Debug.Log("Voy a" +m_Rigidbody.velocity);
                    Debug.Log("Recibo "+m_MovementAction.ReadValue<Vector2>());

                }

                if (m_MovementAction.ReadValue<Vector2>() == new Vector2(0, 0))
                    ChangeState(SwitchMachinesStates.IDLE);
                break;

           

        }
    }

    

    private void Awake()
    {

        m_combateIniciado = false;

        if (gameObject.CompareTag("Player1"))
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_pokemonHierba = posiblePokemonHierba();
            m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");

        }
        else
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld2").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_pokemonHierba = posiblePokemonHierba();
            m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions");
        }
       

    }

    // Start is called before the first frame update
    void Start()
    {
        InitState(SwitchMachinesStates.IDLE);
        m_DangerLayer = LayerMask.NameToLayer("Danger");
        m_EffectorLayer = LayerMask.NameToLayer("Effector");
        m_underEffector = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
            //Debug.Log("Inicio corutina");
            StartCoroutine(m_pokemonHierba);
        }

        if (collision.attachedRigidbody.gameObject.layer == m_EffectorLayer)
        {
            Debug.Log("estoy bajo un effector");
            m_underEffector = true;
           
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == m_DangerLayer)
        {
             //Debug.Log("Parada corutina");
             StopCoroutine(m_pokemonHierba);
        }

        if (collision.attachedRigidbody.gameObject.layer == m_EffectorLayer)
        {
            Debug.Log("salgo de un effector");
            m_underEffector = false;
        }
    }

    

    IEnumerator posiblePokemonHierba()
    {
        while (!m_combateIniciado) {
            //Debug.Log("Puede que te ataque un pokemon");
            int m_random= UnityEngine.Random.Range(0, 101);
            if (m_random > 65)
            {
                CombatStart();
            }
            else
            {
                Debug.Log("No te ataca un pokemon");
            }
            yield return new WaitForSeconds(5);
        }
    }

    public void CombatStart()
    {
        m_combateIniciado = true;
        OnCombatStart?.Invoke(true);
        Debug.Log("Te ataca un pokemon");
        ChangeState(SwitchMachinesStates.BATTLE);
    }


}
