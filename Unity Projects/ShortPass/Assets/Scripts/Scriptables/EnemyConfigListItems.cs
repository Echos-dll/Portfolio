using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyConfigListItems
{
    public string Name;
    [Header("Set Object")]
    public GameObject EnemyObject;
    public GameObject EnemyPatrolObject;
    public GameObject EnemySpawnObject;
    [Header("Set Variables")] 
    public float EnemySpeed;
    public int PatrolModel;

    private EnemyConfigListItems(string newName,GameObject newEnemyObject,GameObject newEnemyPatrolObject, GameObject newEnemySpawnObject, float newEnemySpeed, int newPatrolModel)
    {
        Name = newName;
        EnemyObject = newEnemyObject;
        EnemyPatrolObject = newEnemyPatrolObject;
        EnemySpawnObject = newEnemySpawnObject;
        EnemySpeed = newEnemySpeed;
        PatrolModel = newPatrolModel;
    }
    

}
