using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Image volumeBar;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        // Lade die Lautstärke aus der statischen Klasse
        volumeBar.fillAmount = StaticData.volumeToKeep;
        audioSource.volume = volumeBar.fillAmount;
    }

    public void IncreaseVolume()
    {
        volumeBar.fillAmount += 0.1f;
        audioSource.volume = volumeBar.fillAmount;
        SaveVolume();
    }

    public void DecreaseVolume()
    {
        volumeBar.fillAmount -= 0.1f;
        audioSource.volume = volumeBar.fillAmount;
        SaveVolume();
    }

    private void SaveVolume()
    {
        // Speichere die Lautstärke in der statischen Klasse
        StaticData.volumeToKeep = volumeBar.fillAmount;
    }
}