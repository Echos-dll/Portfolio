using UnityEngine;

[System.Serializable]
public class FriendConfigListItems
{
    public string Name;
    [Header("Set Object")]
    public GameObject FriendObject;
    public GameObject AreaObject;
    public GameObject FriendPatrolObject;
    public GameObject FriendSpawnObject;
    [Header("Set Sprites")] 
    public Sprite FriendSprite;
    [Header("Set Variables")] 
    public float FriendSpeed;
    public int PatrolModel;

    private FriendConfigListItems(string newName,GameObject newFriendObject,GameObject newFriendPatrolObject, GameObject newFriendSpawnObject, GameObject newArea, Sprite newFriendSprite, float newFriendSpeed,
        int newPatrolModel)
    {
        Name = newName;
        FriendObject = newFriendObject;
        FriendPatrolObject = newFriendPatrolObject;
        FriendSpawnObject = newFriendSpawnObject;
        AreaObject = newArea;
        FriendSprite = newFriendSprite;
        FriendSpeed = newFriendSpeed;
        PatrolModel = newPatrolModel;
    }
    
}
