using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 _goal;
    public GameObject love;
    public EnemySwarmController _enemySwarmController;
    private GameManager _gameManager;
    private bool _timerOn=false;
    private float _timer;
    private bool _stopAgent = false;
    private UIManager _uiManager;
    
    public Vector3 Goal
    {
        get => _goal;
        set => _goal = value;
    }
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        _enemySwarmController = GetComponentInParent<EnemySwarmController>();
        Goal = gameObject.transform.parent.transform.position;
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        agent.transform.rotation = Quaternion.identity;
        agent.destination = Goal;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            _enemySwarmController.MoveToPlayer();
            _stopAgent = false;
            _timerOn = false;
            _timer = 0;
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        var go = Instantiate(love);
        go.transform.position = gameObject.transform.position;
        go.transform.DOMoveY(5, 1).OnComplete(()=>Destroy(go));
        _enemySwarmController.UnitCount -= 1;
        _enemySwarmController.unitCountText.text = _enemySwarmController.UnitCount.ToString();
           
        if (_enemySwarmController.UnitCount <= 0 )
        {
            Destroy(_enemySwarmController.gameObject);
        }
    }

    private void OnEnable()
    {
        _timerOn = true;
    }
}
