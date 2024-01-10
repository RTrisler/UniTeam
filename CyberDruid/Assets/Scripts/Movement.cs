using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    #region Public Variables
    public float moveSpeed;

    #endregion

    #region Private Variables

    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector2 moveDirection;
    protected Vector2 targetPos;


    #endregion

    // Determine what direction the entity is facing
    protected enum Facing {UP, DOWN, LEFT, RIGHT};
    protected Facing facingDir = Facing.UP;

    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void Awake()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void UpdateFacingDirection()
    {
        // determine direction the entity is facing
        if (moveDirection.x == 1 & moveDirection.y == 0)
            facingDir = Facing.RIGHT;
        else if (moveDirection.x == -1f & moveDirection.y == 0f)
            facingDir = Facing.LEFT;
        else if (moveDirection.x == 0f & moveDirection.y == 1f)
            facingDir = Facing.UP;
        else if (moveDirection.x == 0f & moveDirection.y == -1f)
            facingDir = Facing.DOWN;
    }

    protected virtual void setAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}
