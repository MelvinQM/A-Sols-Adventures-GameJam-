using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] public Transform shootingPointTransform;
    
    public bool canFire;
    private float timer;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float rot;

    [SerializeField] private float timeBetweenFiring;
    [SerializeField] public Transform AttacksContainer;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        direction = mousePos - transform.position;

        // Update timer
        if (timer > 0) { timer -= Time.deltaTime; }

        if (Input.GetMouseButton(0))
        {
            // If the countdown is 0, then the player can fire
            if (timer <= 0)
            {
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        // Reset the timer
        timer = timeBetweenFiring;
        
        // Create new bullet
        GameObject projectile = Instantiate(bullet, AttacksContainer);
        projectile.transform.position = shootingPointTransform.position;
        projectile.GetComponent<BulletScript>().Boom(direction.x, direction.y, rot);
        
    }
}
