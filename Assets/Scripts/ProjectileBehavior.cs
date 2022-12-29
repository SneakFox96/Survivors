using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float duration = 10f;
    // Start is called before the first frame update
    private SphereCollider sphereCollider;

    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            Destroy(gameObject);
        } 
        else if(collision.collider.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, sphereCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject) return;
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
