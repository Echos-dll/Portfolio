using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class PatrolOfFriend : MonoBehaviour
{
   #region Variables
    
    private Vector3 areavector, normalizedvelocity, newvelocity, uppervec, lowervec;
    private GameObject thisObject, area;
    private Transform[] target;
    private float speed;
    private int nextTargetIndex,numberofarea;
    private bool islooping = true;

    #endregion

    //Does patrol job
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandlePatrol(other);
        
    }

    //Classical update function for necessary processes
    private void Update()
    {
        Zone();
    }

    //Patrol code
    private void HandlePatrol(Collider2D other)
    {
        if (other.gameObject == target[nextTargetIndex].gameObject)
        {
            nextTargetIndex++;
            if (nextTargetIndex >= target.Length)
            {
                if (islooping)
                    nextTargetIndex = 0;
                else nextTargetIndex = target.Length - 1;

            }
            goToNextTarget();
        }
    }

    //Changes target when object reaches current patrol target
    private void goToNextTarget()
    {
        var transform1 = transform;
        var position = transform1.position;
        newvelocity = (target[nextTargetIndex].position - position);
        transform.DOMove(target[nextTargetIndex].position, 1.2f).SetDelay(0.2f).SetEase(Ease.InQuad);
        transform.DORotate(new Vector3(0, 0, (180 * Mathf.Atan2(newvelocity.y, newvelocity.x) / Mathf.PI) - 90f), 0.5f);
    }

    //Creates zones for ball. If ball stays in zone game doesn't end until a friend object catch that.
    private void Zone()
    {
        if (target == null) return;
        
        if (numberofarea != target.Length)
        {
            for (int x = 0; x < target.Length; x++)
            {
                if (x == target.Length - 1)
                {
                    areavector = (target[x].position - target[0].position) / 2;
                    area.transform.position = target[x].position - areavector;
                    area.transform.localRotation = Quaternion.Euler(new Vector3(0, 0,
                        (180 * Mathf.Atan2(areavector.y, areavector.x) / Mathf.PI)));
                    area.transform.localScale = new Vector3(areavector.magnitude * 4, 0.55f, 0);

                    Instantiate(area);
                    numberofarea++;
                }
                else
                {
                    areavector = (target[x].position - target[x + 1].position) / 2;
                    area.transform.position = target[x].position - areavector;
                    area.transform.localRotation = Quaternion.Euler(new Vector3(0, 0,
                        (180 * Mathf.Atan2(areavector.y, areavector.x) / Mathf.PI)));
                    area.transform.localScale = new Vector3(areavector.magnitude * 4, 0.55f, 0);

                    Instantiate(area);
                    numberofarea++;
                }
            }
        }
    }
    
    #region Getter & Setter

    public GameObject thisObjectGetter
    {
        set => thisObject = gameObject;
        get => thisObject;
    }

    public Transform[] Target
    {
        get => target;
        set => target = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public GameObject Area
    {
        get => area;
        set => area = value;
    }

    #endregion
}
