using System.Collections;
using Spine.Unity;
using UnityEngine;
using DG.Tweening;

public class BallBehaviour : MonoBehaviour
{
    #region Variables
    
    private GameObject thisObject,holder;
    private Vector2 mbdown, mbup, force;
    private Vector3 direction;
    private int counter, maxspeed;
    private float ballr;
    private bool friendlyContact = true, bonuscheck;
    private bool friendHaveBall = true, enemyHaveBall, ballGoing, inZone;
    
    #endregion

    private void Update()
    {
        GetComponent<Rigidbody2D>().rotation = 0;

        if (friendlyContact)
        {          
           MoveCam();
        }

        Prediction();

        //Creating force vector and adding it to ball.
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mbdown = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                counter++;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                holder.GetComponent<SkeletonAnimation>().AnimationName = "pass";
                StartCoroutine(Kick());
            }
        }
        
        if (inZone != true && GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f)
        {
            //GameEnds
            
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude < 1f && GetComponent<SkeletonAnimation>().timeScale != 0f)
        {
            GetComponent<SkeletonAnimation>().timeScale -= 1f * Time.deltaTime;
        }
        
    }

    IEnumerator Kick()
    {
        yield return new WaitForSeconds(0.2f);
        mbup = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        friendlyContact = false;
        counter++;
        if ((mbdown - mbup).magnitude > maxspeed)
        {
            force = (mbdown - mbup).normalized * maxspeed;
            GetComponent<Rigidbody2D>().velocity = force * Time.deltaTime;
            Debug.Log("If");
       
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = (mbdown - mbup) * 4.0f * Time.deltaTime;
            Debug.Log("Else");
         

        }
            

        GetComponent<SkeletonAnimation>().timeScale = 1f;
        GetComponent<SkeletonAnimation>().AnimationName = "animation";

        ballGoing = true;
        
        holder.GetComponent<SkeletonAnimation>().AnimationName = "idle";
    }

    //Repositions ball to look for direction side.
    private void Prediction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mbdown = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            mbup = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        direction = (mbdown-mbup).normalized;
        if (friendlyContact && direction.magnitude > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);        
            Vector2 pos = holder.transform.position + direction * ballr;
            Vector2 smoothpos = Vector2.Lerp(transform.position, pos, 0.7f);
            transform.position = smoothpos;
        }
    }

    //Changes parent of the ball to the object it collided if ball reaches to a friend object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "friend":
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                holder.transform.DORotate(new Vector3(0, 0, 0), 0f);
                holder = collision.gameObject;
                holder.GetComponent<Transform>().DOPause();
                ballGoing = false;
                friendlyContact = true;
                holder.GetComponent<SkeletonAnimation>().AnimationName = "idle";
                GetComponent<SkeletonAnimation>().AnimationName = "";
                if(GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                {
                    transform.DORotate(holder.transform.position - direction * ballr,0f);
                }
      
                friendHaveBall = true;
                enemyHaveBall = false;
//                if (bonuscheck)
//                {
//                    score x2
//                    bonuscheck == false;
//                }
                break;
            

            case "enemy":
                bonuscheck = true;
                enemyHaveBall = true;
                friendHaveBall = false;
                break;
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Goal")
        {
            Debug.Log("GOAL");
            FindObjectOfType<GameManager>().NextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "field")
        {
            Debug.Log("Ball Out Of Field");
            //GameEnds
        }

        if (other.gameObject.tag == "MainCamera")
        {
            //GameEnds
        }
    }


    private void MoveCam()
    {
        Vector3 initial = Camera.main.transform.position;
        float absx = Camera.main.transform.position.x;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, holder.transform.position, 1);
        float absy = Camera.main.transform.position.y;
        Vector3 target = new Vector3(absx, absy, -1);
        Vector3 smooth = Vector3.Lerp(initial, target, 0.1f);
        Camera.main.transform.position = smooth;
    }
    
    #region Getter & Setter
    public GameObject ThisObject
    {
        get => thisObject;
        set => thisObject = gameObject;
    }

    public bool Zone
    {
        get => inZone;
        set => inZone = value;
    }

    public GameObject Holder
    {
        get => holder;
        set => holder = value;
    }

    public int Maxspeed
    {
        get => maxspeed;
        set => maxspeed = value;
    }
    
    public float Ballr
    {
        get => ballr;
        set => ballr = value;
    }

    public Vector2 Mbdown
    {
        get => mbdown;
        set => mbdown = value;
    }

    public Vector2 Mbup
    {
        get => mbup;
        set => mbup = value;
    }

    public bool BallGoing
    {
        get => ballGoing;
        set => ballGoing = value;
    }

    #endregion
}
