using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    public float moveSpeed;
    public float dashAmount = 15f;

    #endregion 

    #region Private Variables
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private Vector2 targetPos;
    private bool isDashing = false;
    [SerializeField] private LayerMask dashLayerMask;
    

    #endregion
    
    // Assign the actions asset to this field in the inspector:
    private GlobalInputActions Actions;

    // Determine what direction player is facing
    private enum Facing {UP, DOWN, LEFT, RIGHT};
    private Facing facingDir = Facing.UP;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody2D>();

        Actions = new GlobalInputActions();
        Actions.Player.Dash.performed += OnDash;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    // This function is called every fixed framerate frame. Physics should go here
    void FixedUpdate()
    {
        Move();
    }

    // Determine the inputs and put them in a vector to be used by Fixed Update processes
    void ProcessInputs()
    {
        Vector2 moveVector = Actions.Player.Move.ReadValue<Vector2>();
        moveDirection = moveVector.normalized;

        // determine direction player is facing
        if (moveDirection.x == 1 & moveDirection.y == 0)
            facingDir = Facing.RIGHT;
        else if (moveDirection.x == -1f & moveDirection.y == 0f)
            facingDir = Facing.LEFT;
        else if (moveDirection.x == 0f & moveDirection.y == 1f)
            facingDir = Facing.UP;
        else if (moveDirection.x == 0f & moveDirection.y == -1f)
            facingDir = Facing.DOWN;
    }
    void Move()
    {
        // Add velocity to the player rigidbody based on the moveDirection Vector
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        setAnimatorMovement(moveDirection);

        if (isDashing)
        { 
            Vector2 myPos = transform.position;
            Vector2 dashPosition = myPos + moveDirection * dashAmount;

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, moveDirection, dashAmount, dashLayerMask);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }

            rb.MovePosition(dashPosition);
            isDashing = false;
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        isDashing = true;

        // targetPos = Vector2.zero;

        // if (facingDir == Facing.UP)
        //     targetPos.y = 1;
        // if (facingDir == Facing.DOWN)
        //     targetPos.y = -1;
        // if (facingDir == Facing.LEFT)
        //     targetPos.x = -1;
        // if (facingDir == Facing.RIGHT)
        //     targetPos.x = 1;

        // transform.Translate(targetPos * dashRange);
    }

    private void setAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }

    private void OnEnable()
    {
        Actions.Player.Enable();
    }

    private void OnDisable()
    {
        Actions.Player.Disable();
    }
}
