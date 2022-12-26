using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(velocity  *Time.deltaTime);
    }
}
