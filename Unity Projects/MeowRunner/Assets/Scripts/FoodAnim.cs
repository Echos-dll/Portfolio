using DG.Tweening;
using UnityEngine;

public class FoodAnim : MonoBehaviour
{
    [SerializeField] private GameObject can;

    // Update is called once per frame
    void Start()
    {
        can.transform.DORotate(new Vector3(0, 0, 360),2,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
