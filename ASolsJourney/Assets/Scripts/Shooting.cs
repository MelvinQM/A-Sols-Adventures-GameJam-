using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletTransform;
    public bool canFire;
    private float timer;
    [SerializeField] private float timeBetweenFiring;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canFire) 
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring) 
            {
                canFire = true;
                timer = 0;
            }
        }
    }
    public void Shoot() 
    {
        if(canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
