using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Laser : MonoBehaviour
{
    [Header("General Variables")]
    [SerializeField] GameObject laser_Prefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserLifeTime = 10f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [Header("AI or in other Words just if you want to use the script for an Enemy")]
    [SerializeField] bool useAI = false;

    [Header("Other Stuff")]
    public bool isShooting = false;
    private bool overdrive = false;
    private bool canShoot = true;

    Coroutine firingCoroutine;

    // Blue Overdrive Bar
    [SerializeField] Image overdriveBar;

    // Sound Effect
    [SerializeField] AudioSource shooter_AudioSource;

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        if (canShoot || useAI)
        {
            Fire();
        }

        if (!useAI)
        {
            if (overdriveBar.fillAmount < 0.9f)
            {
                RefillOverdriveBar();
            }

            if (overdriveBar.fillAmount <= 0.1f)
            {
                overdrive = true;
            }

            if (overdriveBar.fillAmount > 0.9f)
            {
                overdrive = false;
            }

            if (overdrive)
            {
                Overdrive();
            }
        }
    }

    private void Fire()
    {
        if (isShooting && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (useAI || !overdrive)
        {
            GameObject laser_instance = Instantiate(laser_Prefab, transform.position, Quaternion.identity);

            if (!useAI)
            {
                // Play Shooter Sound Effect
                shooter_AudioSource.Play();

                // Decrease Blue (Overdrive) Bar
                overdriveBar.fillAmount -= 0.05f;
            }

            Rigidbody2D laser_rb = laser_instance.GetComponent<Rigidbody2D>();

            if (laser_rb != null)
            {
                laser_rb.velocity = transform.up * laserSpeed;
            }

            float timeToNextLaser = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
            timeToNextLaser = Mathf.Clamp(timeToNextLaser, minimumFiringRate, float.MaxValue);

            Destroy(laser_instance, laserLifeTime);

            yield return new WaitForSeconds(timeToNextLaser);
        }
    }

    private void RefillOverdriveBar()
    {
        overdriveBar.fillAmount += Time.deltaTime / 17f;
        if (overdriveBar.fillAmount >= 1f)
        {
            overdriveBar.fillAmount = 1f;
            overdrive = false;
        }
    }

    private void Overdrive()
    {
        overdriveBar.fillAmount += 0.005f;
    }
}



/*
 * using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
 
public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserLifeTime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
 
 
    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
 
    AudioPlayer audioPlayer;
 
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
 
    void Start()
    {
        if (useAI == true)
        {
            isFiring = true;
        }
    }
 
    
    void Update()
    {
        Fire();
    }
 
    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
 
    IEnumerator FireContinously()
    {
        while (true)
        {
            //Laser zur Laufzeit am Ort des Raumschiffs erzeugen
            GameObject instance = Instantiate(laserPrefab, transform.position, Quaternion.identity);
 
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
 
            audioPlayer.PlayShootingClip();
 
            if(rb != null)
            {
                rb.velocity = transform.up * laserSpeed;
            }
 
            float timeToNextLaser = UnityEngine.Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
            timeToNextLaser = Mathf.Clamp(timeToNextLaser, minimumFiringRate, float.MaxValue);
 
 
            //Laser nach best. Zeit zerstören
            Destroy(instance, laserLifeTime);
 
            yield return new WaitForSeconds(timeToNextLaser);
        }
 
    }
}
 * 
 * 
 */