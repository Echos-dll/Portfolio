using System;
using System.Security.Cryptography;
using System.Threading;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private int unitLevel = 1;
    private int goldLevel = 1;
    
    private int unitPrice = 100;
    private int goldPrice = 100;

    private int goldCount = 5000;
    
    private SwarmController _swarmController;
    private bool _reachedFinisLine;
    private bool _isLined;
    private float _goldFactor = 1;

    [Header("LEVEL GENERATOR")] 
    [SerializeField] private GameObject AdderGate;
    [SerializeField] private GameObject Obstacle1;
    [SerializeField] private GameObject Obstacle2;
    [SerializeField] private GameObject enemyGroup;
    [SerializeField] private GameObject finishLine;
    [SerializeField] private GameObject theBox;
    [SerializeField] private GameObject bigCat;
    [SerializeField] private GameObject endAngle;
    [SerializeField] private GameObject camera;
    private Vector3 spawnPosition;
    
    private void Awake()
    {
        _swarmController = FindObjectOfType<SwarmController>();
    }

    private void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _swarmController.StartGame();
        }
        
        if (!_reachedFinisLine) return;
        
        FinishGame();
    }

    public void GenerateLevel()
    {
        spawnPosition = new Vector3(0, 0, 20);
        var gate1 = Instantiate(AdderGate);
        gate1.transform.position = spawnPosition;

        for (int i = 0; i < 5; i++)
        {
            var randomObject = Random.Range(0, 4);
            spawnPosition = new Vector3(0, 0, i * 30 + 40);
            switch (randomObject)
            {
                case 0:
                    var gate = Instantiate(AdderGate);
                    gate.transform.position = spawnPosition;
                    break;
                case 1:
                    var obs1 = Instantiate(Obstacle1);
                    obs1.transform.position = spawnPosition + new Vector3(Random.Range(-5, 5), 0, 0);
                    break;
                case 2:
                    var obs2 = Instantiate(Obstacle2);
                    obs2.transform.position = spawnPosition + new Vector3(Random.Range(-5, 5), 2.5f, 0);
                    break;
                case 3:
                    var enemy = Instantiate(enemyGroup);
                    enemy.transform.position = spawnPosition + new Vector3(0, 0, 0);
                    break;
            }
        }
    }

    private void FinishGame()
    {
        goldCount += _swarmController.UnitCount * (int)_goldFactor;
        camera.GetComponent<PlayableDirector>().Play();
        TheBox.GetComponent<Animator>().Play("Take 001", -1, 0f);
        BigCat.transform.DOScale(new Vector3(40, 40, 40), 2);
        BigCat.transform.DORotate(new Vector3(-15, 90, 0), 5).OnComplete(()=>
        {
            camera.GetComponent<PlayableDirector>().time = 0;
            camera.GetComponent<PlayableDirector>().Stop();
            _swarmController.StopGame();
        });
        _reachedFinisLine = false;
    }

        private void LineFormation()
    {
        if (_isLined) return;
        for (int i = 0; i < _swarmController.activeUnits.Count; i++)
        {
            var go = _swarmController.activeUnits[i].gameObject;
            
            go.transform.DOMove(new Vector3(0,_swarmController.transform.position.y + 1 ,_swarmController.transform.position.z + i), 1).OnComplete(()=>go.GetComponent<NavMeshAgent>().velocity = Vector3.zero);
        }

        _isLined = true;
    }
    
    public bool ReachedFinisLine
    {
        get => _reachedFinisLine;
        set => _reachedFinisLine = value;
    }

    public float GoldFactor
    {
        get => _goldFactor;
        set => _goldFactor = value;
    }

    public int UnitLevel
    {
        get => unitLevel;
        set => unitLevel = value;
    }

    public int GoldLevel
    {
        get => goldLevel;
        set => goldLevel = value;
    }

    public int GoldPrice
    {
        get => goldPrice;
        set => goldPrice = value;
    }

    public int UnitPrice
    {
        get => unitPrice;
        set => unitPrice = value;
    }

    public int GoldCount
    {
        get => goldCount;
        set => goldCount = value;
    }

    public GameObject TheBox
    {
        get => theBox;
        set => theBox = value;
    }

    public GameObject BigCat
    {
        get => bigCat;
        set => bigCat = value;
    }
}
