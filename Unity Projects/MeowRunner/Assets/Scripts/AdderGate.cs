using System;
using TMPro;
using UnityEngine;

public class AdderGate : MonoBehaviour
{
    [SerializeField] private GameObject adder1;
    [SerializeField] private GameObject adder2;

    private TMP_Text adder1Text;
    private TMP_Text adder2Text;

    private void Awake()
    {
        adder1Text = adder1.GetComponentInChildren<TMP_Text>();
        adder2Text = adder2.GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var go1 = Instantiate(adder1, gameObject.transform);
        go1.transform.localPosition = new Vector3(-4, .2f, 0);

        var go2 = Instantiate(adder2, gameObject.transform);
        go2.transform.localPosition = new Vector3(4, .2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableTrigger()
    {
        adder1.GetComponent<Collider>().enabled = false;
        adder2.GetComponent<Collider>().enabled = false;
    }
    
}
