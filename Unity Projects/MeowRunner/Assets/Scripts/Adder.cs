using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum addType
{
    plus,
    multi
};

public class Adder : MonoBehaviour
{
    public addType type;
    public TMP_Text valueText;
    private SwarmController _swarmController;
    private AdderGate _adderGate;
    private int addValue;

    public int AddValue
    {
        get => addValue;
        set => addValue = value;
    }

    private void Awake()
    {
        _swarmController = FindObjectOfType<SwarmController>();
        _adderGate = GetComponentInParent<AdderGate>();
    }

    private void Start()
    {
        var randomType = Random.Range(0, 2);
        if (randomType == 0)
        {
            type = addType.plus;
            addValue = Random.Range(5, 20);
            valueText.text = "+" + addValue;
        }

        if (randomType == 1)
        {
            type = addType.multi;
            addValue = Random.Range(1, 4);
            valueText.text = "x" + addValue;
        }

        
    }
}
