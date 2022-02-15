using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //-- INSPECTOR VARIABLES -------------------------------------------------------------------------------------------------------

    [Header("Movement Variables")]
    public float speed;
    public float rollSpeed;
    public float jumpSpeed;
    public float heigh;
    public int facingDirection;

    [Header("Components")]
    public BossAnimatorController animatorController;
    public Rigidbody rigidBody;
    public CapsuleCollider boxCollider;

    [Header("Ground Check Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    public bool groundDetected;

    [Header("Jump Check Variables")]
    [SerializeField] private Transform _jumpCheck;
    [SerializeField] private float _jumpCheckDistance;
    public bool jumpDetected;
    public float fallingForce;

    [Header("Wall Check Variables")]
    [SerializeField] private Transform _wallCheck;
    public bool wallDetected;

    [SerializeField] private LayerMask _whatIsGround;

    [Header("States Variables")]
    public bool isEntranceStateRunning;
    public bool isAnyStateRunning;
    public bool canCheckJumpFinish;
    public bool isRolling;
    public bool isFallen;

    [Header("Jump Parabola Variable")]
    public ParabolaController parabolaController;
    public GameObject parabolaRoot;
    public GameObject startRoot;
    public GameObject heighRoot;
    public GameObject finishRoot;
    public float jumpHeigh;

    [Header("Player")]
    public GameObject player;

    [Header("Gravity")]
    public float gravityScale = 1.0f;
    public float globalGravity = -9.81f;
    private Vector3 _gravity;
    public bool canChangeGravity;

    [Header("End Dialogue")]
    public GameObject dialogue;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        rigidBody = GetComponent<Rigidbody>();
        animatorController = GetComponentInChildren<BossAnimatorController>();
        boxCollider = GetComponent<CapsuleCollider>();
        parabolaController = GetComponent<ParabolaController>();
    }

    private void Start()
    {
        isEntranceStateRunning = true;
        isAnyStateRunning = true;
        canCheckJumpFinish = false;
        rigidBody.useGravity = false;
        isRolling = false;
        isFallen = false;
    }

    private void Update()
    {
        groundDetected = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        jumpDetected = Physics.Raycast(_jumpCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        wallDetected = Physics.Raycast(_wallCheck.position, Vector3.right * facingDirection, _jumpCheckDistance, _whatIsGround);
    }

    private void FixedUpdate()
    {
        //SetGravity();
    }

    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    public void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingDirection *= -1;
    }

    public void Entrance()
    {
        rigidBody.useGravity = true;
    }

    private void SetGravity()
    {
        _gravity = globalGravity * gravityScale * Vector3.up;
        rigidBody.AddForce(_gravity, ForceMode.Acceleration);
    }

    public void Defeated()
    {
        dialogue.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance, 0.0f));

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_jumpCheck.position, new Vector3(_jumpCheck.position.x, _jumpCheck.position.y - _groundCheckDistance, 0.0f));

        Gizmos.color = Color.black;
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + (_jumpCheckDistance * facingDirection), _wallCheck.position.y, 0.0f));
    }

#endif


}
