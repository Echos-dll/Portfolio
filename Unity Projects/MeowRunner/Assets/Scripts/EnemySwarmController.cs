using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

public class EnemySwarmController : MonoBehaviour
{
    public List<GameObject> activeUnits = new List<GameObject>();
    public GameObject playerSwarm;
    public TMP_Text unitCountText;
    public GameObject centerUnit;
    private int speed = 3;
    public int unitCount = 1;
    private bool isMoving = false;  
    
    private float _timer;
    private bool _playing;
    private bool canControl = true;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        var randomValue = Random.Range(5, 15);
        playerSwarm = GameObject.FindWithTag("PlayerSwarm");
        SpawnUnit(randomValue);
    }
    
    public void SpawnUnit(int count)
    {
        var randomPitch = Random.Range(.75f, 1f);
        AudioManager.Instance.PlaySoundwPitch("Pop", randomPitch);
        
        for (int i = 0; i < count; i++)
        {
            UnitCount += 1;
            var go = Instantiate(centerUnit, gameObject.transform);
            go.transform.position = transform.position + Random.insideUnitSphere ;
            go.SetActive(true);
        }

        unitCountText.text = unitCount.ToString();
    }

    public void MoveToPlayer()
    {
        if (!isMoving)
        {
            gameObject.transform.DOMove(playerSwarm.transform.position, 1);
            isMoving = true;
        }
    }
    
    public int UnitCount
    {
        get => unitCount;
        set => unitCount = value;
    }
}
