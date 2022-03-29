using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStack : MonoBehaviour
{
    [SerializeField] private GameObject stackParent;
    [SerializeField] private GameObject stackItemPrefab;

    private List<GameObject> stackedItems = new List<GameObject>();
    private int _stackNumber = 0 ;
    private Material currentMaterial;

    private void Start()
    {
        currentMaterial = new Material(Shader.Find("Default-Material"));
    }

    public void RemoveItem()
    {
        Debug.Log("Removed Item");
    }

    public void AddItem()
    {
        var item = Instantiate(stackItemPrefab, stackParent.transform);
        item.GetComponent<Renderer>().material = currentMaterial;
        item.transform.position = stackParent.transform.position + Vector3.up / 2  * _stackNumber;
        StackedItems.Add(item);
        _stackNumber += 1;
    }
    
    public List<GameObject> StackedItems
    {
        get => stackedItems;
        set => stackedItems = value;
    }

    public Material CurrentMaterial
    {
        get => currentMaterial;
        set => currentMaterial = value;
    }
}
