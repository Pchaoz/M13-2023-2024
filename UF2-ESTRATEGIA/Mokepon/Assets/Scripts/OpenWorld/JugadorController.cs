using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static SaveData;

public class JugadorController : MonoBehaviour, ISaveableObject
{

    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    private InputAction m_MovementAction;
    [SerializeField]
    private int m_Speed=20;
    float maxSpeed = 3.0f;
    private Rigidbody2D m_Rigidbody;
    private int m_DangerLayer=0;
    private int m_EffectorLayer = 0;
    private bool m_underEffector;
    public Action<Boolean> OnpisandoHierba;
    public bool m_enHierba;
    [SerializeField]
    GameEventVector3 m_ultimaposicion;
    [SerializeField]
    Mokepon m_mokepon;
    [SerializeField]
    GameEventMokepon m_misMokepon;


    private enum SwitchMachinesStates { NONE, IDLE, WALK};
    [SerializeField]
    private SwitchMachinesStates m_CurrentState;

    private void ChangeState(SwitchMachinesStates newState)
    {
        if (newState == m_CurrentState)
            return;
        ExitState();
        InitState(newState);
    }

    private void ExitState()
    {
       
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
               m_Rigidbody.AddForce(m_MovementAction.ReadValue<Vector2>() * m_Speed);
                Vector2 clampedVelocity = Vector2.ClampMagnitude(m_Rigidbody.velocity, maxSpeed);
                m_Rigidbody.velocity = clampedVelocity;

                if (m_MovementAction.ReadValue<Vector2>() == new Vector2(0, 0))
                    ChangeState(SwitchMachinesStates.IDLE);
                break;
        }
    }

    

    private void Awake()
    {
        Debug.Log("Jugador Awake");

        m_Rigidbody = GetComponent<Rigidbody2D>();

        m_Input = Instantiate(m_InputAsset);

        if (gameObject.CompareTag("Player1"))
            m_Input.bindingMask = InputBinding.MaskByGroup("Jugador1");
        else
            m_Input.bindingMask = InputBinding.MaskByGroup("Jugador2");

        m_MovementAction = m_Input.FindActionMap("OpenWorld").FindAction("WorldActions");
        m_Input.FindActionMap("OpenWorld").Enable();
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
        m_ultimaposicion.Raise(transform.position);
        m_misMokepon.Raise(m_mokepon);
    }

    public void cargarUltimaPosicion(Vector3 v1, Vector3 v2)
    {
        if (gameObject.CompareTag("Player1"))
        {
            transform.position = v1;
        }
        else
        {
            transform.position = v2;
        }
     
    }

    public PlayerData Save()
    {
        return new SaveData.PlayerData(transform.position);
    }

    public void Load(PlayerData _playerData)
    {
        transform.position = _playerData.position;
    }
}
