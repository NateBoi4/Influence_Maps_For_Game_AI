using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> friendlyUnits;
    public List<GameObject> enemyUnits;

    public GameObject unit;

    public Transform friendlySpawnPoint;
    public Transform enemySpawnPoint;

    public int maxUnits = 50;
    public float spawnTime = 5.0f;

    private void Start()
    {
        InvokeRepeating("SpawnUnit", spawnTime, spawnTime);
    }

    public void SpawnUnit()
    {
        if (friendlyUnits.Count < maxUnits)
        {
            GameObject newFriendUnit = Instantiate(unit, friendlySpawnPoint.position, Quaternion.identity, friendlySpawnPoint);
            newFriendUnit.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
            friendlyUnits.Add(newFriendUnit);
        }
        if (enemyUnits.Count < maxUnits)
        {
            GameObject newEnemyUnit = Instantiate(unit, enemySpawnPoint.position, Quaternion.identity, enemySpawnPoint);
            newEnemyUnit.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            enemyUnits.Add(newEnemyUnit);
        }
    }
}
