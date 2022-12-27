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

    public GameObject Enemy;
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
        if (spawnTime <= 0 || mobCount == 0)
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
        int emerygencyLoopBreaker = 999;
        GameObject enemy = Instantiate(Enemy, Vector3.zero, target.rotation);
        Vector3 newPos;
        float rx = Random.Range(-15, 15);
        float rz = Random.Range(-15, 15);
        float y = target.position.y;
        newPos = new Vector3(target.position.x+rx, y, target.position.z+rz);
        //If the spawn location is within 5 units, remake the spawn location
        while(Vector3.Distance(newPos, target.position) < 5 && Vector3.Distance(newPos, target.position) > 0)
        {
            emerygencyLoopBreaker--;
            rx = Random.Range(-15, 15);
            rz = Random.Range(-15, 15);
            newPos = new Vector3(target.position.x+rx, y, target.position.z+rz);
            if (emerygencyLoopBreaker == 0)
            {
                Debug.Log("Emergency loop break");
                break;
            }
        }
        enemy.transform.position = new Vector3(target.position.x+rx, target.position.y, target.position.z+rz);
        return enemy;
    }
}
