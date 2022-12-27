using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    private GameObject[] enemies;
    public const int MOB_CAP = 50;
    public const float DEFAULT_SPAWN_TIME = 1.0f;
    public float enemySpeed = 4.0f;
    public float spawnTime = DEFAULT_SPAWN_TIME;

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
                createEnemy();
            }
            spawnTime = DEFAULT_SPAWN_TIME;
        }
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    GameObject createEnemy()
    {
        int loopBreak = 999;
        Vector3 avgPos = FindAveragePlayerPositions();
        float rx = Random.Range(-25, 25);
        float rz = Random.Range(-25, 25);
        float y = 1f;
        Vector3 newPos = avgPos + new Vector3(rx, y, rz);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            while(Vector3.Distance(player.transform.position, newPos) < 5 && Vector3.Distance(player.transform.position, newPos) > 0)
            {
                loopBreak--;
                rx = Random.Range(-25, 25);
                rz = Random.Range(-25, 25);
                y = 1f;
                newPos = avgPos + new Vector3(rx, y, rz);
                if(loopBreak == 0)
                {
                    break;
                }
            }
        }
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
        return result / players.Length;
    }
}
