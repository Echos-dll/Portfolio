using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public LevelGenScriptable lgs;
    public GameManager GM;
    private int _currentLevel;
    
    [HideInInspector]
     public List<GameObject> PoolingObjectsList = new List<GameObject>();

     public List<GameObject> pooledFriendObjects = new List<GameObject>();
     public List<GameObject> pooledEnemyObjects = new List<GameObject>();

     public List<GameObject> pooledFriendPatrolPoints = new List<GameObject>();
     public List<GameObject> pooledEnemyPatrolPoints = new List<GameObject>();
    
     public List<GameObject> pooledFriendSpawnPoints = new List<GameObject>();
     public List<GameObject> pooledEnemySpawnPoints = new List<GameObject>();
    
     public static ObjectPooler SharedInstance;

     private void Creator()
     {
         GM = FindObjectOfType<GameManager>();
         //0
        PoolingObjectsList[0]=(lgs.LevelGenerator1[0].EnemyList.EnemyConfigGetter[0].EnemyObject);
        //1
        PoolingObjectsList[1]=(lgs.LevelGenerator1[0].EnemyList.EnemyConfigGetter[0].EnemyPatrolObject);
        //2
        PoolingObjectsList[2]=(lgs.LevelGenerator1[0].EnemyList.EnemyConfigGetter[0].EnemySpawnObject);
        //3
        PoolingObjectsList[3]=(lgs.LevelGenerator1[0].FriendList.FriendConfigGetter[0].FriendObject);
        //4
        PoolingObjectsList[4]=(lgs.LevelGenerator1[0].FriendList.FriendConfigGetter[0].FriendPatrolObject);
        //5
        PoolingObjectsList[5]=(lgs.LevelGenerator1[0].FriendList.FriendConfigGetter[0].FriendSpawnObject);

     }
     
    private void Awake()
    {
        SharedInstance = this;
        Creator();
    }

    public void Spawner()
    {
        if (SharedInstance == null)
        {
            
        }
        else
        {
            _currentLevel = GM._currentLevel;
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].EnemyList.EnemyConfigGetter.Count; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[0]);
                obj.SetActive(false);
                pooledEnemyObjects.Add(obj);
            }
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].EnemyList.EnemyConfigGetter.Count*4; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[1]);
                obj.SetActive(false);
                pooledEnemyPatrolPoints.Add(obj);
            }
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].EnemyList.EnemyConfigGetter.Count; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[2]);
                obj.SetActive(false);
                pooledEnemySpawnPoints.Add(obj);
            }
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].FriendList.FriendConfigGetter.Count; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[3]);
                obj.SetActive(false);
                pooledFriendObjects.Add(obj);
            }
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].FriendList.FriendConfigGetter.Count*4; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[4]);
                obj.SetActive(false);
                pooledFriendPatrolPoints.Add(obj);
            }
            for (int i = 0; i < lgs.LevelGenerator1[_currentLevel].FriendList.FriendConfigGetter.Count; i++)
            {
                GameObject obj = Instantiate(PoolingObjectsList[5]);
                obj.SetActive(false);
                pooledFriendSpawnPoints.Add(obj);
            }
        }
    }
    
    public GameObject GetPooledObject(string tag)
    {
        switch (tag)
        {
            case "friend":
                
                for (int i = 0; i <pooledFriendObjects.Count ; i++)
                {
                    if (!pooledFriendObjects[i].activeInHierarchy && pooledFriendObjects[i].CompareTag("friend"))
                        return pooledFriendObjects[i];
                }
                break;
            
            case "enemy":
                
                for (int i = 0; i <pooledEnemyObjects.Count ; i++)
                {
                    if (!pooledEnemyObjects[i].activeInHierarchy && pooledEnemyObjects[i].CompareTag("enemy"))
                        return pooledEnemyObjects[i];
                }
                break;
            
            case "friendpatrolpoint":
                
                for (int i = 0; i <pooledFriendPatrolPoints.Count ; i++)
                {
                    if (!pooledFriendPatrolPoints[i].activeInHierarchy && pooledFriendPatrolPoints[i].CompareTag("friendpatrolpoint"))
                        return pooledFriendPatrolPoints[i];
                }
                break;
            
            case "enemypatrolpoint":
                
                for (int i = 0; i <pooledEnemyPatrolPoints.Count ; i++)
                {
                    if (!pooledEnemyPatrolPoints[i].activeInHierarchy && pooledEnemyPatrolPoints[i].CompareTag("enemypatrolpoint"))
                        return pooledEnemyPatrolPoints[i];
                }
                break;
            
            case "friendspawnpoint":
                
                for (int i = 0; i <pooledFriendSpawnPoints.Count ; i++)
                {
                    if (!pooledFriendSpawnPoints[i].activeInHierarchy && pooledFriendSpawnPoints[i].CompareTag("friendspawnpoint"))
                        return pooledFriendSpawnPoints[i];
                }
                break;
            
            case "enemyspawnpoint":
                
                for (int i = 0; i <pooledEnemySpawnPoints.Count ; i++)
                {
                    if (!pooledEnemySpawnPoints[i].activeInHierarchy && pooledEnemySpawnPoints[i].CompareTag("enemyspawnpoint"))
                        return pooledEnemySpawnPoints[i];
                }
                break;
        }
        return null;
    }
}
