using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float distance = -10f;
    public float height = 7.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x, target.position.y + height, target.position.z + distance
        );
    }
}
