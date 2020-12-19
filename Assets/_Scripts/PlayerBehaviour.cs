using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    public Vector3 velocity;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
        _Move();
    }

    private void _Move()
    {
        velocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            // move right
            velocity += playerCam.transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            // move left
            velocity += -playerCam.transform.right * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") > 0.0f)
        {
            // move forward
            velocity += playerCam.transform.forward * speed * Time.deltaTime;
        }

        else if (Input.GetAxisRaw("Vertical") < 0.0f)
        {
            // move Back
            velocity += -playerCam.transform.forward * speed * Time.deltaTime;
        }



        if (isGrounded)
        {
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            velocity = new Vector3(velocity.x, 0.0f, velocity.z); // remove y

            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                velocity += transform.up * speed * 0.8f * Time.deltaTime;
            }
            body.velocity = velocity;
            transform.position += body.velocity;
        }
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
