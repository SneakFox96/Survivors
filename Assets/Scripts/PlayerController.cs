using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5.0f;
    private float nextFire;
    private float fireRate = 1.25f;

    private Animator anim;
    private Vector3 hitpoint;
    public bool IsWalking;
    public Camera cam;


    private float projectileSpeed = 30f;
    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = gameObject.AddComponent<CharacterController>();
        controller.slopeLimit = 0; 
        controller.stepOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject)
        {
            return;
        }

        anim.SetBool("Is Walking", IsWalking);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        Vector3 pointTo; 
        if (ground.Raycast(cameraRay, out rayLength))
        {
            pointTo = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTo.x, transform.position.y, pointTo.z));
        } 
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShootProjectile();
        }
        if(transform.position.y > 1f)
        {
            var pos = transform.position;
            pos.y = 1;
            transform.position = pos;
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitpoint = hit.point;

        } else 
        {
            hitpoint = ray.GetPoint(100);
        }
        createProjectile();
    }
    GameObject createProjectile()
    {
        hitpoint.y = transform.position.y;
        var direction = (hitpoint-transform.position).normalized;
        var projectile = Instantiate(
            Projectile, 
            new Vector3(
                transform.position.x + direction.x,
                transform.position.y,
                transform.position.z + direction.z
            ),
            transform.rotation
        );
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        return projectile;
    }

}
