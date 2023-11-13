using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private bool m_underEffector;
    public Action<Boolean> OnpisandoHierba;
    public bool m_enHierba;




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
                }

                if (m_MovementAction.ReadValue<Vector2>() == new Vector2(0, 0))
                    ChangeState(SwitchMachinesStates.IDLE);
                break;
            case SwitchMachinesStates.BATTLE:
                Debug.Log("Estoy en combate");
                break;
        }
    }

    

    private void Awake()
    {

       

        if (gameObject.CompareTag("Player1"))
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();      
            m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
           

        }
        else
        {
            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("OpenWorld2").FindAction("WorldActions");
            m_Input.FindActionMap("OpenWorld2").Enable();
            m_Rigidbody = GetComponent<Rigidbody2D>();
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
            Debug.Log("soy el jugador " + gameObject.tag+" y he pidado la hierba");
            m_enHierba = true;
            OnpisandoHierba?.Invoke(true);
            

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
            Debug.Log("soy el jugador " + gameObject.tag + " y he salido de la hierba");
            m_enHierba = false;
            OnpisandoHierba?.Invoke(false);
            
        }

        if (collision.attachedRigidbody.gameObject.layer == m_EffectorLayer)
        {
            Debug.Log("salgo de un effector");
            m_underEffector = false;
        }
    }
    public void CombatStart()
    {
        Debug.Log("Te ataca un pokemon");
        ChangeState(SwitchMachinesStates.BATTLE);
    }


}
