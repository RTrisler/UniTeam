using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> Enemies = new List<GameObject>();
    public float delay;
    private float x, y;
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        x = Random.Range(-1, 1);
        y = Random.Range(-1, 1);
        spawnPos.x += x;
        spawnPos.y += y;
        Instantiate(Enemies[0], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnEnemy());
    }
}
