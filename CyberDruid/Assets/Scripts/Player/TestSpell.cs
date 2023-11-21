using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    [SerializeField]
    int numberOfProjectiles;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        radius = 10f;
        numberOfProjectiles = 1;

        InputController.Instance.playerActionMap.PrimaryFire.performed += OnPrimaryFire;
        InputController.Instance.playerActionMap.SecondaryFire.performed += OnSecondaryFire;
    }

    private void OnPrimaryFire(InputAction.CallbackContext context)
    {
        Shoot(numberOfProjectiles);
    }

    private void OnSecondaryFire(InputAction.CallbackContext context)
    {
        if (PlayerStats.playerStats.specialCharge == PlayerStats.playerStats.maxSpecialCharge)
        {
            Debug.Log("Here");
            numberOfProjectiles = 5;
            PlayerStats.playerStats.ResetCharge();
            Shoot(numberOfProjectiles);
            numberOfProjectiles = 1;
        }
    }

    private void Shoot(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (numberOfProjectiles > 1)
        {
            Debug.Log("MADE IT");
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                Debug.Log("AHOYY");
                Vector2 myPos = transform.position;

                float projectileDirXposition = myPos.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = myPos.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - myPos).normalized * projectileForce;

                GameObject spell = Instantiate(projectile, myPos, Quaternion.identity);
                spell.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                angle += angleStep;
            }
        }
        else
        { 
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestProjectile>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        }
        
    }
}
