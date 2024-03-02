using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : MonoBehaviour
{
    [SerializeField] GameObject laser_Prefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserLifeTime = 10f;
    [SerializeField] float timeBeetwenShots = 0.2f;

    public bool isShooting = false;

    Coroutine firingCoroutine;



    void Start()
    {

    }
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isShooting && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        else if (!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContiniously()
    {
        while (true)
        {
            GameObject laser_instance = Instantiate(laser_Prefab, transform.position, Quaternion.identity);

            Rigidbody2D laser_rb = laser_instance.GetComponent<Rigidbody2D>();

            if (laser_rb != null)
            {
                laser_rb.velocity = new Vector2(0, laserSpeed);
            }

            Destroy(laser_instance, laserLifeTime);

            yield return new WaitForSeconds(timeBeetwenShots);
        }
    }
}
