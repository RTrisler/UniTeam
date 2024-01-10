using Assets.Scripts.Helpers;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : Movement
{
    private Vector3 startingPosition;
    private GameObject player;
    private CircleCollider2D triggerPlayerFollowCollider;
    private BoxCollider2D hitBoxCollider;

    private void Start()
    {
        base.Start();

        startingPosition = transform.position;
        player = GameObject.Find("Player");

        triggerPlayerFollowCollider = gameObject.GetComponent<CircleCollider2D>();
        hitBoxCollider = gameObject.AddComponent<BoxCollider2D>();

        triggerPlayerFollowCollider.AddComponent<Collider2D_Proxy>().OnPlayerStayInCollider += OnPlayerStayInCollider;
        hitBoxCollider.GetComponent<Collider2D_Proxy>().OnPlayerExitCollider += OnPlayerExitCollider;
    }

    
    void OnPlayerStayInCollider(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Move();
        }
    }

    void OnPlayerExitCollider(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            StopMovement();
        }
    }

    void Move()
    {
        moveDirection = GetPlayerPosition() - transform.position;

        UpdateFacingDirection();

        // Add velocity to the entity rigidbody based on the moveDirection Vector
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        setAnimatorMovement(moveDirection);
    }

    void StopMovement()
    {
        rb.velocity = Vector2.zero;
        setAnimatorMovement(Vector2.zero);
    }

    private Vector3 GetPlayerPosition() => player.transform.position;

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    public static Vector3 GetRandomDir(){
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }


    protected override void setAnimatorMovement(Vector2 vector) { }
}
