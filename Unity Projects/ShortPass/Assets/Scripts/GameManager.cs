using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    public LevelGenScriptable Configuration;
    public ObjectPooler ObjectPool;
    public GameObject friendGoal, enemyGoal;
    private BallBehaviour BallB;
    private Aim ArrowC;
    private List<PatrolOfFriend> POF = new List<PatrolOfFriend>();
    private List<PatrolOfEnemy> POE = new List<PatrolOfEnemy>();
    private static int counter;

    public int _currentLevel = 0;
    private int friendCurrentPoint;
    private int enemyCurrentPoint;
    private readonly List<GameObject> FriendList = new List<GameObject>();
    private readonly List<GameObject> EnemyList = new List<GameObject>();
    private readonly List<GameObject> FriendPatrolPointList = new List<GameObject>();
    private readonly List<GameObject> EnemyPatrolPointList = new List<GameObject>();
    private readonly List<GameObject> FriendSpawnPointList = new List<GameObject>();
    private readonly List<GameObject> EnemySpawnPointList = new List<GameObject>();
    private List<GameObject> spawnlist = new List<GameObject>();

    #endregion
    
    private void FillWithObjects()
    {
        BallB = FindObjectOfType<BallBehaviour>();
        POF = FindObjectsOfType<PatrolOfFriend>().ToList();
        POE = FindObjectsOfType<PatrolOfEnemy>().ToList();
        ArrowC = FindObjectOfType<Aim>();
    }

    private void Randomize(GameObject[] spawnPoint)
    {
        var avarage = (enemyGoal.transform.position - friendGoal.transform.position).magnitude / spawnPoint.Length;

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            if (i == 0)
            {
                spawnPoint[i].transform.position = friendGoal.transform.position + new Vector3(0,0.5f,0) ;
            }
            
            if (i % 2 == 0 && i != 0)
            {
                Vector3 RandomPosition = new Vector3(0,0,0);
                RandomPosition.y = spawnPoint[i - 1].transform.position.y;
                float RNGHeight =avarage;
                float RNGWidth = Random.Range(-1f, 1f);
                RandomPosition.y += RNGHeight;
                RandomPosition.x += RNGWidth;
                spawnPoint[i].transform.position = RandomPosition;
            }
            
            if (i % 2 == 1 && i != spawnPoint.Length-1)
            {
                Vector3 RandomPosition = new Vector3(0,0,0);
                RandomPosition.y = spawnPoint[i - 1].transform.position.y;
                float RNGHeight =avarage;
                float RNGWidth = Random.Range(-1f, 1f);
                RandomPosition.y += RNGHeight;
                RandomPosition.x += RNGWidth;
                spawnPoint[i].transform.position = RandomPosition;
            }
            //FINISH
            if (i == spawnPoint.Length - 1)
            {
                spawnPoint[i].transform.position = enemyGoal.transform.position + new Vector3(0,-1,0);
            }
        }
    }
    
    private void ObjectGeter()
    {
        ObjectPool.Spawner();
        
        for (int i = 0; i < ObjectPool.PoolingObjectsList.Count; i++)
        {
            switch (ObjectPool.PoolingObjectsList[i].tag)
            {
                case "friend":
                    for (int j = 0; j < ObjectPool.pooledFriendObjects.Count; j++)
                    {
                        GameObject Friend = ObjectPooler.SharedInstance.GetPooledObject("friend");
                        if (Friend != null)
                        {
                            FriendList.Add(Friend);
                            Friend.SetActive(true);
                        }
                    }

                    break;

                case "friendspawnpoint":
                    for (int j = 0; j < ObjectPool.pooledFriendSpawnPoints.Count; j++)
                    {
                        GameObject FriendSpawnPoint = ObjectPooler.SharedInstance.GetPooledObject("friendspawnpoint");
                        if (FriendSpawnPoint != null)
                        {
                            FriendSpawnPointList.Add(FriendSpawnPoint);
                            FriendSpawnPoint.SetActive(true);
                        }
                    }

                    break;

                case "friendpatrolpoint":
                    for (int j = 0; j < ObjectPool.pooledFriendPatrolPoints.Count; j++)
                    {
                        GameObject FriendPatrolPoint = ObjectPooler.SharedInstance.GetPooledObject("friendpatrolpoint");
                        if (FriendPatrolPoint != null)
                        {
                            FriendPatrolPointList.Add(FriendPatrolPoint);
                            FriendPatrolPoint.SetActive(true);
                        }
                    }

                    break;

                case "enemy":
                    for (int j = 0; j < ObjectPool.pooledEnemyObjects.Count; j++)
                    {
                        GameObject Enemy = ObjectPooler.SharedInstance.GetPooledObject("enemy");
                        if (Enemy != null)
                        {
                            EnemyList.Add(Enemy);
                            Enemy.SetActive(true);
                        }
                    }

                    break;

                case "enemyspawnpoint":
                    for (int j = 0; j < ObjectPool.pooledEnemySpawnPoints.Count; j++)
                    {
                        GameObject EnemySpawnPoint = ObjectPooler.SharedInstance.GetPooledObject("enemyspawnpoint");
                        if (EnemySpawnPoint != null)
                        {
                            EnemySpawnPointList.Add(EnemySpawnPoint);
                            EnemySpawnPoint.SetActive(true);
                        }
                    }

                    break;

                case "enemypatrolpoint":
                    for (int j = 0; j < ObjectPool.pooledEnemyPatrolPoints.Count; j++)
                    {
                        GameObject EnemyPatrolPoint = ObjectPooler.SharedInstance.GetPooledObject("enemypatrolpoint");
                        if (EnemyPatrolPoint != null)
                        {
                            EnemyPatrolPointList.Add(EnemyPatrolPoint);
                            EnemyPatrolPoint.SetActive(true);
                        }
                    }

                    break;
            }
        }
        Debug.Log("Objects Created and Called");
    }

    private IEnumerator ObjectDestroyer(Action onComplete)
    {
        for (int i = 0; i < ObjectPool.PoolingObjectsList.Count; i++)
        {
            switch (ObjectPool.PoolingObjectsList[i].tag)
            {
                case "friend":
                    for (int j = 0; j < ObjectPool.pooledFriendObjects.Count; j++)
                    {
                        Destroy(FriendList[j]);
                    }

                    break;

                case "friendspawnpoint":
                    for (int j = 0; j < ObjectPool.pooledFriendSpawnPoints.Count; j++)
                    {
                        Destroy(FriendSpawnPointList[j]);
                    }

                    break;

                case "friendpatrolpoint":
                    for (int j = 0; j < ObjectPool.pooledFriendPatrolPoints.Count; j++)
                    {
                        Destroy(FriendPatrolPointList[j]);
                    }

                    break;

                case "enemy":
                    for (int j = 0; j < ObjectPool.pooledEnemyObjects.Count; j++)
                    {
                        Destroy(EnemyList[j]);
                    }

                    break;

                case "enemyspawnpoint":
                    for (int j = 0; j < ObjectPool.pooledEnemySpawnPoints.Count; j++)
                    {
                        Destroy(EnemySpawnPointList[j]);
                    }

                    break;

                case "enemypatrolpoint":
                    for (int j = 0; j < ObjectPool.pooledEnemyPatrolPoints.Count; j++)
                    {
                        Destroy(EnemyPatrolPointList[j]);
                    }

                    break;
            }
        }
        
        ObjectPool.pooledFriendObjects.Clear();
        ObjectPool.pooledFriendSpawnPoints.Clear();
        ObjectPool.pooledFriendPatrolPoints.Clear();
        ObjectPool.pooledEnemyObjects.Clear();
        ObjectPool.pooledEnemySpawnPoints.Clear();
        ObjectPool.pooledEnemyPatrolPoints.Clear();
        
        Debug.Log("Objects Destroyed");
        
        yield return new WaitForEndOfFrame();
        onComplete();
    }
    
    private void Start()
    {
        ObjectGeter();
        MergeAndRandomize();
        FillWithObjects();
        Assigner();
        BallB.Holder = FriendList[POF.Count - 1];
        BallB.Holder.GetComponent<SkeletonAnimation>().AnimationName = "idle";
        BallB.gameObject.transform.position = FriendList[POF.Count - 1].gameObject.transform.position + new Vector3(0f, 0.5f, 0);
        POF[0].Target[0] = POF[0].transform;
        POF[0].Target[1] = POF[0].transform;
    }

    private void MergeAndRandomize()
    {
        if (ObjectPool.pooledFriendSpawnPoints.Count < ObjectPool.pooledEnemySpawnPoints.Count)
            counter = ObjectPool.pooledEnemySpawnPoints.Count;
        else counter = ObjectPool.pooledFriendSpawnPoints.Count;
        
        
        int fcount = 0, ecount = 0;
        for (int i = 0; i < counter; i++)
        {
            if (ObjectPool.pooledFriendSpawnPoints.Count > fcount)
            {
                spawnlist.Add(ObjectPool.pooledFriendSpawnPoints[fcount]);
                fcount++;
            }

            if (ObjectPool.pooledEnemySpawnPoints.Count > ecount)
            {
                spawnlist.Add(ObjectPool.pooledEnemySpawnPoints[ecount]);
                ecount++;
            }
            
        }

        for (int i = 0; i < spawnlist.Count; i++)
        {
            Randomize(spawnlist.ToArray());
        }

    }

    private void Assigner()
    {
        //BALL
        var BallSettings = Configuration.LevelGenerator1[_currentLevel].BallOfLevel.BallConfigGetter;
        BallB.ThisObject = BallSettings.BallObject;
        BallB.Maxspeed = BallSettings.BallMaxSpeed;
        BallB.Ballr = BallSettings.BallRadius;

        //ARROW
        var ArrowSettings = Configuration.LevelGenerator1[_currentLevel].ArrowOfLevel.ArrowConfigGetter;
        ArrowC.ThisObject = ArrowSettings.ArrowObject;
        ArrowC.Arrowmaxscale = ArrowSettings.ArrowMaxScale;
        ArrowC.Arrowscalecoefficient = ArrowSettings.ArrowScaleCoefficient;
        ArrowC.Arrowr = ArrowSettings.ArrowRadius;
        ArrowC.ThisObject.GetComponent<SpriteRenderer>().sprite = ArrowSettings.ArrowSprite;
        

        //FRIEND
        var FriendSettings = Configuration.LevelGenerator1[_currentLevel].FriendListOfLevel.FriendConfigGetter;
        for (int i = 0; i < FriendSettings.Count; i++)
        {
            POF[i].thisObjectGetter = FriendSettings[i].FriendObject ;
            POF[i].Area = FriendSettings[i].AreaObject;
            POF[i].Speed = FriendSettings[i].FriendSpeed;
            POF[i].Target = FriendPatrolType(Random.Range(0,4), FriendSpawnPointList[i].transform.position,friendCurrentPoint);
            POF[i].transform.position = POF[i].Target[0].position;
        }
        
        //ENEMY
        var EnemySettings = Configuration.LevelGenerator1[_currentLevel].EnemyListOfLevel.EnemyConfigGetter;
       
        for (int i = 0; i < EnemySettings.Count; i++)
        {
            POE[i].ThisObject = EnemySettings[i].EnemyObject;
            POE[i].Speed = EnemySettings[i].EnemySpeed;
            if (i == EnemySettings.Count-1)
            {
                POE[i].Target = EnemyPatrolType(0, EnemySpawnPointList[i].transform.position, enemyCurrentPoint);
            }
            else
            {
                POE[i].Target = EnemyPatrolType(Random.Range(0,4), EnemySpawnPointList[i].transform.position, enemyCurrentPoint);
            }
            
            POE[i].gameObject.transform.position = POE[i].Target[0].position;
            //GOALKEEPER DATA ASSET CHANGES
//            var goalkeeperasset = Resources.Load(
//                "Assets/Animation Folder/Player_Animation/Goalkeeper SkeletonData.asset") as SkeletonDataAsset;
//            POE[Configuration.LevelGenerator1[currentLevel].EnemyListOfLevel.EnemyConfigGetter.Count-1].GetComponent<SkeletonAnimation>().ClearState();
        }
    }

    private Transform[] FriendPatrolType(int type, Vector3 core, int i)
    {
       
        switch (type)
        {
            case 0:
                Transform[] newFriendTargets1 = new Transform[2];
                FriendPatrolPointList[i].transform.position = core + new Vector3(1f, 0, 0);
                newFriendTargets1[0] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(-1f, 0, 0);
                newFriendTargets1[1] = FriendPatrolPointList[i].transform;
                i++;
                friendCurrentPoint = i;
                return newFriendTargets1;
            
            case 1:
                Transform[] newFrienTargets2 = new Transform[2];
                FriendPatrolPointList[i].transform.position = core + new Vector3(1f, 1f, 0);
                newFrienTargets2[0] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(-1f, -1f, 0);
                newFrienTargets2[1] = FriendPatrolPointList[i].transform;
                i++;
                friendCurrentPoint = i;
                return newFrienTargets2;
                
                
            case 2:
                Transform[] newFrienTargets3 = new Transform[2];
                FriendPatrolPointList[i].transform.position = core + new Vector3(-1f, 1f, 0);
                newFrienTargets3[0] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(1f, -1f, 0);
                newFrienTargets3[1] = FriendPatrolPointList[i].transform;
                i++;
                friendCurrentPoint = i;
                return newFrienTargets3;
                
                
            case 3:
                Transform[] newFrienTargets4 = new Transform[2];
                FriendPatrolPointList[i].transform.position = core + new Vector3(0, 1f, 0);
                newFrienTargets4[0] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(0, -1f, 0);
                newFrienTargets4[1] = FriendPatrolPointList[i].transform;
                i++;
                friendCurrentPoint = i;
                return newFrienTargets4;
            
            case 4:
                Transform[] newFriendTargets5 = new Transform[3];
                FriendPatrolPointList[i].transform.position = core + new Vector3(1f, 0, 0);
                newFriendTargets5[0] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(-1f, 0, 0);
                newFriendTargets5[1] = FriendPatrolPointList[i].transform;
                i++;
                FriendPatrolPointList[i].transform.position = core + new Vector3(0, -1f, 0);
                newFriendTargets5[2] = FriendPatrolPointList[i].transform;
                i++;
                friendCurrentPoint = i;
                return newFriendTargets5;
                
        }

        return null;
    }
    
    private Transform[] EnemyPatrolType(int type, Vector3 core, int j)
    {
        switch (type)
        {
            case 0:
                Transform[] newEnemyTargets1 = new Transform[2];
                EnemyPatrolPointList[j].transform.position = core + new Vector3(1f, 0, 0);
                newEnemyTargets1[0] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(-1f, 0, 0);
                newEnemyTargets1[1] = EnemyPatrolPointList[j].transform;
                j++;
                enemyCurrentPoint = j;
                return newEnemyTargets1;
            
            case 1:
                Transform[] newEnemyTargets2 = new Transform[2];
                EnemyPatrolPointList[j].transform.position = core + new Vector3(1f, 1f, 0);
                newEnemyTargets2[0] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(-1f, -1f, 0);
                newEnemyTargets2[1] = EnemyPatrolPointList[j].transform;
                j++;
                enemyCurrentPoint = j;
                return newEnemyTargets2;
                
            case 2:
                Transform[] newEnemyTargets3 = new Transform[2];
                EnemyPatrolPointList[j].transform.position = core + new Vector3(-1f, 1f, 0);
                newEnemyTargets3[0] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(1f, -1f, 0);
                newEnemyTargets3[1] = EnemyPatrolPointList[j].transform;
                j++;
                enemyCurrentPoint = j;
                return newEnemyTargets3;
                
            case 3:
                Transform[] newEnemyTargets4 = new Transform[2];
                EnemyPatrolPointList[j].transform.position = core + new Vector3(0, 1f, 0);
                newEnemyTargets4[0] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(0, -1f, 0);
                newEnemyTargets4[1] = EnemyPatrolPointList[j].transform;
                j++;
                enemyCurrentPoint = j;
                return newEnemyTargets4;
            
            case 4:
                Transform[] newEnemyTargets5 = new Transform[3];
                EnemyPatrolPointList[j].transform.position = core + new Vector3(1f, 0, 0);
                newEnemyTargets5[0] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(-1f, 0, 0);
                newEnemyTargets5[1] = EnemyPatrolPointList[j].transform;
                j++;
                EnemyPatrolPointList[j].transform.position = core + new Vector3(0, -1f, 0);
                newEnemyTargets5[2] = EnemyPatrolPointList[j].transform;
                j++;
                enemyCurrentPoint = j;
                return newEnemyTargets5;
                
        }

        return null;
    }

    public void NextLevel()
    {
        if (_currentLevel != Configuration.LevelGenerator1.Count - 1)
        {
            StartCoroutine(ObjectDestroyer(() =>
            {
                FriendList.Clear();
                FriendSpawnPointList.Clear();
                FriendPatrolPointList.Clear();
                EnemyList.Clear();
                EnemyPatrolPointList.Clear();
                EnemySpawnPointList.Clear();
                spawnlist.Clear();
                POE.Clear();
                POF.Clear();
                Debug.Log(POF.Count);
                friendCurrentPoint = 0;
                enemyCurrentPoint = 0;
                _currentLevel++;
                ObjectGeter();
                MergeAndRandomize();
                FillWithObjects();
                Assigner();
                BallB.Holder = FriendList[POF.Count - 1];
                BallB.Holder.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                BallB.gameObject.transform.position = FriendList[POF.Count - 1].gameObject.transform.position + new Vector3(0.5f, 0.5f, 0);
                POF[0].Target[0] = POF[0].transform;
                POF[0].Target[1] = POF[0].transform;
            }));
        }
        else
        {
            Debug.Log("GAME END");
            //End Game
        }
        
    }

    public void EndGame()
    {
        //GameEnd Function here
    }
    
    
    
}

