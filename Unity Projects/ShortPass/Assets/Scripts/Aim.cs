using UnityEngine;

public class Aim : MonoBehaviour
{
    private GameObject thisObject;
    private BallBehaviour ballb;
    private Vector3 vector1, vector2, temp;
    private int arrowmaxscale, arrowscalecoefficient;
    private float arrowr;

    private void Start()
    {
        ballb = FindObjectOfType<BallBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ballb.BallGoing !=true)
        {
            vector1 = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            GetComponent<SpriteRenderer>().enabled = true;
        }
            
        
        if (Input.GetMouseButton(0))
        {
            vector2 = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Scale();
        }
            

        if (Input.GetMouseButtonUp(0))
            GetComponent<SpriteRenderer>().enabled = false;

    }

    private void Scale()
    {
        Vector3 aimdirection = (vector1-vector2).normalized;
        transform.position = ballb.GetComponent<Transform>().position + aimdirection * arrowr;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan2(aimdirection.y, aimdirection.x) / Mathf.PI));
        ballb.Holder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan2(aimdirection.y, aimdirection.x) / Mathf.PI - 80));

        temp = transform.localScale;
        temp.x =Mathf.Min(arrowmaxscale, (vector1 - vector2).magnitude) / arrowscalecoefficient;
        transform.localScale = temp;
    }

    public GameObject ThisObject
    {
        get => thisObject;
        set => thisObject = gameObject;
    }


    public int Arrowmaxscale
    {
        get => arrowmaxscale;
        set => arrowmaxscale = value;
    }

    public int Arrowscalecoefficient
    {
        get => arrowscalecoefficient;
        set => arrowscalecoefficient = value;
    }

    public float Arrowr
    {
        get => arrowr;
        set => arrowr = value;
    }
    
    
}
