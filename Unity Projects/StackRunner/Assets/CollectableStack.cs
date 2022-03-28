using UnityEngine;

public class CollectableStack : MonoBehaviour
{
    [SerializeField] private GameObject stackParent;
    [SerializeField] private GameObject stackItemPrefab;
    
    private int _stackNumber = 0 ;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem()
    {
        var item = Instantiate(stackItemPrefab, stackParent.transform);
        item.transform.position = transform.position + Vector3.down * _stackNumber;
        _stackNumber += 1;
    }
}
