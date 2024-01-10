using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    #region Public Variables
    public float dashAmount = 15f;

    #endregion 

    #region Private Variables
    private bool isDashing = false;
    [SerializeField] private LayerMask dashLayerMask;
    

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        InputController.Instance.playerActionMap.Dash.performed += OnDash;
        InputController.Instance.playerActionMap.Move.performed += Move;
        InputController.Instance.playerActionMap.Move.canceled += Move;
    }

    void Move(InputAction.CallbackContext context)
    {
        moveDirection = InputController.Instance.playerActionMap.Move.ReadValue<Vector2>();

        UpdateFacingDirection();

        // Add velocity to the player rigidbody based on the moveDirection Vector
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        setAnimatorMovement(moveDirection);
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
}
