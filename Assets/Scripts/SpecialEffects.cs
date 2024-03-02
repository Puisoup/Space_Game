using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    // Particles
    [Header("Particles")]
    public ParticleSystem hitParticle;

    // Screen Shake
    [Header("Screen Shake")]
    public Camera mainCamera;
    public bool cameraShake;

    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;
    [SerializeField] float decreaseFactor = 1.0f;

    private Vector3 initialPosition;

    private void Start()
    {
        mainCamera = Camera.main;
        initialPosition = mainCamera.transform.position;
    }

    public void ScreenShake()
    {
        if (cameraShake)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            mainCamera.transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = initialPosition;
    }

    #region Particles
    public void PlayHitParticle()
    {
        if (hitParticle != null)
        {
            ParticleSystem instance = Instantiate(hitParticle, transform.position, Quaternion.identity);
            float duration = Mathf.Max(instance.main.duration, instance.main.startLifetime.constantMax);
            Destroy(instance.gameObject, duration);
        }
    }
    #endregion
}
