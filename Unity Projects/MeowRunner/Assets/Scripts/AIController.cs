using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour 
{
    private NavMeshAgent agent;
    private Vector3 _goal;
    private SwarmController _swarmController;
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
        _swarmController = FindObjectOfType<SwarmController>();
        _gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        Goal = _swarmController.centerUnit.transform.position;
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        agent.transform.rotation = Quaternion.identity;
        
        if (_stopAgent)
        {
            agent.velocity = Vector3.zero;
        }
        else
        {
            agent.destination = Goal;
        }
        
        if (_timerOn)
        {
            _timer += Time.deltaTime;
            if (_timer >= .5f)
            {
                _stopAgent = true;
                _timerOn = false;
                _timer = 0;
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _gameManager.ReachedFinisLine = true;
            _swarmController.transform.DOMoveX(0, 1);
            _swarmController.CanControl = false;
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            
        }

        if (other.CompareTag("BigBox"))
        {
            
        }

        if (other.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Adder"))
        {
            var trigger = other.gameObject.GetComponent<Adder>();
            if (trigger.type == addType.plus)
            {
                var addValue = other.GetComponent<Adder>().AddValue;
                _swarmController.SpawnUnit(addValue);
            }

            if (trigger.type == addType.multi)
            {
                var amount = _swarmController.activeUnits.Count;
                var addValue = other.GetComponent<Adder>().AddValue;
                _swarmController.SpawnUnit(amount * addValue - _swarmController.UnitCount);
            }
            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
        }
        
    }
    
    private void OnDisable()
    {
        _swarmController.activeUnits.Remove(gameObject);
        _swarmController.unitCount -= 1;
        _swarmController.unitCountText.text = _swarmController.UnitCount.ToString();
        _stopAgent = false;
        _timerOn = false;
        _timer = 0;
    }
    
    GameObject GetNearestObject(GameObject[] units)
    {
        GameObject tGo = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = _swarmController.transform.position;
        foreach (GameObject go in units)
        {
            float dist = Vector3.Distance(go.transform.position, currentPos);
            if (dist < minDist)
            {
                tGo = go;
                minDist = dist;
            }
        }
        return tGo;
    }

    private void OnEnable()
    {
        _swarmController.activeUnits.Add(gameObject);
        _timerOn = true;
        _swarmController.UnitCount += 1;
        _swarmController.unitCountText.text = _swarmController.UnitCount.ToString();
    }
    
}
