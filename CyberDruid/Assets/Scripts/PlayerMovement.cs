using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector2 targetPos;
    public float moveSpeed;
    public Rigidbody2D rb;
    public float dashRange;

    private Vector2 moveDirection;
    private Animator animator;

    // Determine what direction player is facing
    private enum Facing {UP, DOWN, LEFT, RIGHT};
    private Facing facingDir = Facing.DOWN;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // determine direction player is facing
        if (moveDirection.x == 1 & moveDirection.y == 0)
            facingDir = Facing.RIGHT;
        else if (moveDirection.x == -1f & moveDirection.y == 0f)
            facingDir = Facing.LEFT;
        else if (moveDirection.x == 0f & moveDirection.y == 1f)
            facingDir = Facing.UP;
        else if (moveDirection.x == 0f & moveDirection.y == -1f)
            facingDir = Facing.DOWN;

        // DASH
        if (Input.GetKeyDown(KeyCode.Space))
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
}
