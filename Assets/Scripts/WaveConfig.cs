using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] float spawnDelayVar = 0.3f;
    [SerializeField] int enemyNumber = 5;
    [SerializeField] float moveSpeed = 2f;

    //[SerializeField] List<Transform> waveWaypoints;

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public float GetSpawnDelay() { return spawnDelay; }

    public float GetSpawnDelayVar() { return spawnDelayVar; }

    public int GetEnemyNumber() { return enemyNumber; }

    public float GetMoveSpeed() { return moveSpeed; }
}
