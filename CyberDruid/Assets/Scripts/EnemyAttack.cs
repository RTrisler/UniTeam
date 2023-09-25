using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    protected GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
