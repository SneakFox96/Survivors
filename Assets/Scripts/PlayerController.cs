using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private static int MAX_PROJECTILES = 30;
    private CharacterController controller;
    private Vector3 velocity;
    private float speed = 5.0f;
    private float nextFire;
    private float fireRate = 1.0f;

    private GameObject[] projectiles;
    private Animator anim;
    public bool IsWalking;


    public float projectileSpeed = 50f;
    public GameObject Projectile;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Debug.Log(anim);

        controller = gameObject.AddComponent<CharacterController>();
        projectiles = GameObject.FindGameObjectsWithTag("projectile");

        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Is Walking", IsWalking);

        if (!gameObject)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            IsWalking = true;
        }
        if (move == Vector3.zero)
        {
            IsWalking = false;
        }


        controller.Move(velocity  *Time.deltaTime);
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTo = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointTo, Color.blue);
            transform.LookAt(new Vector3(pointTo.x, transform.position.y, pointTo.z));
        }
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if(projectiles.Length< MAX_PROJECTILES)
            {
                GameObject bullet=  createProjectile();
            }
        }
        foreach(GameObject proj in projectiles)
        {
            if(proj)
            {
                proj.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            }
        }
        projectiles = GameObject.FindGameObjectsWithTag("projectile");

    }

    GameObject createProjectile()
    {
        return Instantiate(Projectile, transform.position+transform.forward, transform.rotation);
    }

}
