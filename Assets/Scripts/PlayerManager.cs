using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [Header("Scripts")]
    public AnimatorManager animatorManager;
    public PlayerLocomotion playerLocomotion;
    public InputManager inputManager;

    [Header("Components")]
    public Rigidbody rigidBody;
    public GameObject player;
    public Animator animator;
    [Header("MovementSpeed")]
    //Player Stats
    public float moveSpeed;
    public float rotSpeed;
    //New Scripts
    public float walkingSpeed;
    public float sprintSpeed;

    [Header("Actions")]
    public bool isSprinting;
    public bool isInteracting;
    [Header("Falling")]
    public bool isGrounded;
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingSpeed;
    public float fallingVelocity;
    public float rayCastheight;
    public LayerMask groundLayer;


    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        inputManager = player.GetComponent<InputManager>();
        playerLocomotion = player.GetComponent<PlayerLocomotion>();
        rigidBody = player.GetComponent<Rigidbody>();
        animatorManager = player.GetComponent<AnimatorManager>();
        animator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.HandleAllInput();

    }

    private void FixedUpdate()
    {
        playerLocomotion.HandlesAllMovement();
    }

    //New Scripts
    private void LateUpdate()
    {
        //everyframe were checking if it is interacting
        isInteracting = animator.GetBool("isInteracting");
    }
}
