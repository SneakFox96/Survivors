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

    private float projectileSpeed = 100f;
    private List<GameObject> projectiles;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        projectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
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
            if(projectiles.Count < MAX_PROJECTILES)
            {
                GameObject bullet=  createProjectile();
                projectiles.Add(bullet);
            }
        }
        foreach(GameObject proj in projectiles)
        {
            if(proj)
            {
                proj.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            }
        }
    }

    GameObject createProjectile()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        bullet.transform.position = transform.position + transform.forward;
        bullet.transform.rotation = transform.rotation;
        bullet.tag = "projectile";
        bullet.AddComponent<Rigidbody>();
        return bullet;
    }

}
