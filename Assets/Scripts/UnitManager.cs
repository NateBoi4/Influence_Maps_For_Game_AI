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
        int randProp = Random.Range(0, 2);
        if (friendlyUnits.Count < maxUnits)
        {
            GameObject newFriendUnit = Instantiate(unit, friendlySpawnPoint.position, Quaternion.identity, friendlySpawnPoint);
            newFriendUnit.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
            newFriendUnit.GetComponent<Health>().team = 1;
            Propogation prop = newFriendUnit.GetComponent<Propogation>();
            if (prop)
            {
                switch (randProp)
                {
                    case 0:
                        prop.SetPropogationInfo(-1, 7, "Soldier", 1);
                        break;
                    case 1:
                        prop.SetPropogationInfo(-1, 3, "Defence", 1);
                        break;
                    default:
                        Debug.Log("Failed to Propogate.");
                        break;
                }
            }
            friendlyUnits.Add(newFriendUnit);
        }
        if (enemyUnits.Count < maxUnits)
        {
            GameObject newEnemyUnit = Instantiate(unit, enemySpawnPoint.position, Quaternion.identity, enemySpawnPoint);
            newEnemyUnit.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            newEnemyUnit.GetComponent<Health>().team = 0;
            Propogation prop = newEnemyUnit.GetComponent<Propogation>();
            if (prop)
            {
                switch (randProp)
                {
                    case 0:
                        prop.SetPropogationInfo(1, 7, "Soldier", 0);
                        break;
                    case 1:
                        prop.SetPropogationInfo(1, 3, "Defence", 0);
                        break;
                    default:
                        Debug.Log("Failed to Propogate.");
                        break;
                }
            }
            enemyUnits.Add(newEnemyUnit);
        }
    }
}
