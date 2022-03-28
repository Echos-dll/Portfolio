using DG.Tweening;
using UnityEngine;

public class FishAnim : MonoBehaviour
{
    [SerializeField] private GameObject fish;

    private void Start()
    {
        fish.transform.DOLocalMoveY(2, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
