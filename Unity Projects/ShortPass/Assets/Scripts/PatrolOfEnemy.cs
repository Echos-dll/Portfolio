using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PatrolOfEnemy : MonoBehaviour
{
    #region Variables
    
    private Transform[] target;
    private GameObject thisObject;
    private int nextTargetIndex = 0;
    private float speed;
    private Vector3 areavector, normalizedvelocity, newvelocity, uppervec, lowervec;
    
    #endregion
    
    //Does patrol job
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandlePatrol(other);
    }

    //Patrol code
    private void HandlePatrol(Collider2D other)
    {
        if (other.gameObject == target[nextTargetIndex].gameObject)
        {
            nextTargetIndex++;
            if (nextTargetIndex >= target.Length)
            {
                nextTargetIndex = 0;
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
    
    #region Getter & Setter
    
    public GameObject ThisObject
    {
        get => thisObject;
        set => thisObject = gameObject;
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
    
    #endregion
}
