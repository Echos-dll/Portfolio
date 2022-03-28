using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

public class SwarmController : MonoBehaviour
{
    public List<GameObject> activeUnits = new List<GameObject>();
    public TMP_Text unitCountText;
    public GameObject textPanel;
    public GameObject centerUnit;
    public int speed;
    public int unitCount = 0;
    
    private float _timer;
    private bool _playing;
    private bool canControl = true;
    private UIManager _uiManager;
    private GameManager _gameManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Playing)
        {
            textPanel.SetActive(true);
            _uiManager.GamePanel();
            centerUnit.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            transform.position += Vector3.forward * speed * Time.deltaTime;

            if (!CanControl) return;
        
            if (Input.GetKey(KeyCode.A) && transform.position.x > -5)
            {
                transform.position += (Vector3.forward + Vector3.left).normalized * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D) && transform.position.x < 5)
            {
                transform.position += (Vector3.forward + Vector3.right).normalized * speed * Time.deltaTime;
            }
        }
        else
        {
            textPanel.SetActive(false);
            gameObject.transform.position = new Vector3(0, 0, 10);
        }
        
    }

    public void StartGame()
    {
        if (Playing == false)
        {
            _uiManager.StartPanel();
            canControl = true;
            Playing = true;
            _timer = 0;
        }
    }

    public void StopGame()
    {
        Playing = false;
        _timer = 0;
        _uiManager.StartPanel();
        transform.position = new Vector3(0, 0, 10);
        foreach (var activeUnit in activeUnits)
        {
            activeUnit.SetActive(false);
        }
        _gameManager.GenerateLevel();
        SpawnUnit(_gameManager.UnitLevel);
    }

    public void SpawnUnit(int count)
    {
        AudioManager.Instance.PlaySoundwPitch("Pop", 1);
        
        for (int i = 0; i < count; i++)
        {
            var go = ObjectPooler.SharedInstance.GetPooledObject(0);
            //go.transform.position = transform.position + Vector3.forward * 2;
            go.transform.position = transform.position + Random.insideUnitSphere ;
            go.SetActive(true);
        }
        
        unitCountText.text = unitCount.ToString();
    }
    
    public bool CanControl
    {
        get => canControl;
        set => canControl = value;
    }

    public int UnitCount
    {
        get => unitCount;
        set => unitCount = value;
    }

    public bool Playing
    {
        get => _playing;
        set => _playing = value;
    }
}
