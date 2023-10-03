using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 targetPos;
    public float moveSpeed;
    public Rigidbody2D rb;
    public float dashRange;

    private Vector2 moveDirection;
    private Animator animator;
    
    // Assign the actions asset to this field in the inspector:
    private GlobalInputActions Actions;

    // Determine what direction player is facing
    private enum Facing {UP, DOWN, LEFT, RIGHT};
    private Facing facingDir = Facing.DOWN;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
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

    private void OnDash(InputAction.CallbackContext context)
    {
        targetPos = Vector2.zero;

        if (facingDir == Facing.UP)
            targetPos.y = 1;
        if (facingDir == Facing.DOWN)
            targetPos.y = -1;
        if (facingDir == Facing.LEFT)
            targetPos.x = -1;
        if (facingDir == Facing.RIGHT)
            targetPos.x = 1;

        transform.Translate(targetPos * dashRange);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        setAnimatorMovement(moveDirection);
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
