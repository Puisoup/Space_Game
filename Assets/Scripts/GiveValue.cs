using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveValue : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        // Setze die Lautst�rke basierend auf dem Wert aus der statischen Klasse
        audioSource.volume = StaticData.volumeToKeep;
    }
}