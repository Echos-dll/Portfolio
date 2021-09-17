using UnityEngine;
[System.Serializable]
public class BallConfigListItems
{
    [Header("Set Objects")]
        public GameObject BallObject;
        [Header("Set Sprites")]
        public Sprite BallSprite;
        [Header("Set Values")]
        public int BallMaxSpeed;
        public float BallRadius;

        private BallConfigListItems(GameObject newBallObject, Sprite newBallSprite, int newBallMaxSpeed, float newBallRadius)
    {
        BallObject = newBallObject;
        BallSprite = newBallSprite;
        BallMaxSpeed = newBallMaxSpeed;
        BallRadius = newBallRadius;
    }
    
    
}
