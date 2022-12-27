using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public static int projectileCount = 0;
    public float duration = 10f;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject) return;
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
