using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private bool isColliding;
    public GameObject XpOrb;
    public float enemySpeed = 4.0f;
    GameObject[] enemyTargets;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindClosestEnemy();
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestEnemy();

        if (!gameObject)
        {
            return;
        }
        if (target && !isColliding)
        {
            float step = Time.deltaTime * enemySpeed;
            var newPos = new Vector3(
                target.transform.position.x,
                transform.position.y,
                target.transform.position.z
            );
            transform.LookAt(newPos);
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("projectile"))
        {
            Vector3 pos = gameObject.transform.position;
            Destroy(gameObject);
            int rng = Random.Range(0, 100);

            if (rng >= 49)
            {
                Instantiate(XpOrb, pos, Quaternion.identity);
            }
        }
        else if (collision.collider.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    void OnCollision(Collision colliion)
    {
        isColliding = false;
    }

    GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        enemyTargets = GameObject.FindGameObjectsWithTag("Player");
        Vector3 position = transform.position;
        float distance = Mathf.Infinity;

        foreach (GameObject target in enemyTargets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }
}
