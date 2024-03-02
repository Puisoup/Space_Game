/*
Setzt Enemy auf den ersten Waypooint in der Liste und iteriert dann durch
die Liste mit der EnemyPosition hindurch. Wenn der Letzte Waypoint erreicht ist,
wird der Enemy zerstört.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfig waveConfig; // Objekt deklarieren, welches eine Instanz der WaveConfig-Klasse aufnimmt
    List<Transform> waypoints; // Liste, welche die Waypoints enthält
    int waypointIndex = 0; // Integer um iterativ durch die Liste zu gehen



    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    // Enemy folgt den Waypoints
    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position; // Die position, an welche der jeweillige Enemy hingehen soll

            float delta = waveConfig.GetMoveSpeed() * waveConfig.GetMoveSpeed() * Time.deltaTime; // Geschwindigkeit des Enemys

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta); // Enemy bewegt sich smooth zum nächsten Target

            // Inkrement waypoint Index --> nächster Punkt
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
