using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wafe Config")]
public class WaveConfig : ScriptableObject
{
    // Liste mit den zu erzeugenden Enemies
    public List<GameObject> enemyPrefabsList;
    // Variable in der Waypoints aufgenommen werden
    [SerializeField] Transform pathPrefab;

    // Geschwindigkeit der Enemies
    [SerializeField] float moveSpeed;

    // Abstände zwischen dem Spawnen der Enemies
    [SerializeField] float timeBetweenEnemySpawn = 1f;
    [SerializeField] float randomTimeVariable = 0f;
    [SerializeField] float mindTime = 0.2f;

    public float timeAfterWave = 0f; // Zeit nach der Welle


    // Get & Set
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        // Leere Liste für Transform-Elemente anlegen
        List<Transform> waypoints = new List<Transform>();

        // Alle Elemente von pathPrefab iterieren und in waypoints-Liste speichern
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;

    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //Gibt die Anzahl der Enemies in der Liste zurück
    public int GetEnemyCount()
    {
        int anzahl = 0;

        foreach (GameObject enemy in enemyPrefabsList)
        {
            anzahl++;
        }
        return anzahl;
    }

    // Gibt das Enemy-Objekt an einer bestimmten Listenposition (index) zurück
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabsList[index];
    }

    public float CalculateRandomSpamTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawn - randomTimeVariable, timeBetweenEnemySpawn + randomTimeVariable);

        return Mathf.Clamp(spawnTime, mindTime, float.MaxValue);
    }
}
