using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{

    private int globalCounter = 0;
    [SerializeField] bool isInfinitWave = false;
    [SerializeField] List<WaveConfig> waveConfigs; // Liste aller Waves/Wellenkonfigurationen
    [SerializeField] float timeBetweenWaves = 0f; // Zeit zwischen den Waves
    WaveConfig currentWave;

 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private void Update()
    {

    }


    IEnumerator SpawnEnemies()
    {
        while (isInfinitWave || globalCounter == 1)
        {
            

            // Diese Schleife durchläuft jede Wellenkonfiguration, die in der Liste "waveConfigs" enthalten ist.
            // Für jede Iteration der Schleife wird die Variable "wave" auf die aktuelle Wellenkonfiguration gesetzt.
            foreach (WaveConfig wave in waveConfigs)
            {
                // Hier wird die aktuelle Wellenkonfiguration auf die in der Schleife aktuelle "wave" gesetzt
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);


                    yield return new WaitForSeconds(currentWave.CalculateRandomSpamTime());
                }
                globalCounter++;
                // Hier wird eine Wartezeit eingefügt, bevor die nächste Welle beginnt.
                // Die Dauer der Wartezeit wird durch die Variable "timeBetweenWaves" gesteuert.


                /*
                foreach (GameObject Enemy in currentWave.enemyPrefabsList)
                {
                  
                }
                */
                


                    yield return new WaitForSeconds(currentWave.timeAfterWave);
                





            }
        }

    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

}
