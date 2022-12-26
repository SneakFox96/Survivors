using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    const int MOB_CAP = 20;
    public Transform target;
    public float enemySpeed = 4.0f;
    public float spawnTime = 10f;
    private GameObject[] enemies;
    private int mobCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject[MOB_CAP];
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * enemySpeed;
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            if (mobCount < MOB_CAP)
            {
                GameObject enemy = createEnemy();
                enemies[mobCount] = enemy;
                mobCount++;
            }
            spawnTime = 10f;
        }
        foreach(GameObject enemy in enemies)
        {
            if (enemy)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.position, step);
            }
        }
    }

    GameObject createEnemy()
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemy.AddComponent<Rigidbody>();
        float rx = Random.Range(10, 15);
        float rz = Random.Range(10, 15);
        enemy.transform.position = new Vector3(target.position.x+rx, target.position.y, target.position.z+rz);
        Debug.Log("Enemy created");
        return enemy;
    }
}
