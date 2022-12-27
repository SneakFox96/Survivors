using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;
    const int MOB_CAP = 50;
    const float DEFAULT_SPAWN_TIME = 1.0f;
    public float enemySpeed = 4.0f;
    public float spawnTime = DEFAULT_SPAWN_TIME;
    private GameObject[] enemies;

    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }
    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * enemySpeed;
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0 || enemies.Length == 0)
        {
            if (enemies.Length < MOB_CAP)
            {
                GameObject enemy = createEnemy();
            }
            spawnTime = DEFAULT_SPAWN_TIME;
        }
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    GameObject createEnemy()
    {
        //int emerygencyLoopBreaker = 999;
        Vector3 newPos = FindAveragePlayerPositions();
        float rx = Random.Range(-15, 15);
        float rz = Random.Range(-15, 15);
        float y = 1f;
        newPos += new Vector3(rx, y, rz);
        //If the spawn location is within 5 units, remake the spawn location
        /**
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
        **/
        return Instantiate(Enemy, newPos, Quaternion.identity);
    }
    Vector3 FindAveragePlayerPositions()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Vector3 result = Vector3.zero;
        foreach (GameObject player in players) 
        {
            result += player.transform.position;
        }
        return result/players.Length;
    }
}
