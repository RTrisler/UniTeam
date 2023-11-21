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

    // Determine what direction player is facing
    private enum Facing {UP, DOWN, LEFT, RIGHT};
    private Facing facingDir = Facing.UP;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        InputController.Instance.playerActionMap.Dash.performed += OnDash;

        InputController.Instance.playerActionMap.Move.performed += Move;
        InputController.Instance.playerActionMap.Move.canceled += Move;
    }

    private void Awake()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Move(InputAction.CallbackContext context)
    {
        moveDirection = InputController.Instance.playerActionMap.Move.ReadValue<Vector2>();

        UpdateFacingDirection();

        // Add velocity to the player rigidbody based on the moveDirection Vector
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        setAnimatorMovement(moveDirection);
    }

    private void UpdateFacingDirection()
    {
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
        isDashing = true;

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

    private void setAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}
