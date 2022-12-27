using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking: MonoBehaviour
{
    public Transform target;
    public float distance = -15f;
    public float height = 7.0f;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y+height, target.position.z+distance);
    }
}
