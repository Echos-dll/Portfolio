using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Friend List",menuName = "Game Configs/Friend List")]
[System.Serializable]
public class FriendConfigScriptable : ScriptableObject
{
    [SerializeField]
    private List<FriendConfigListItems> FriendList = new List<FriendConfigListItems>();

    public List<FriendConfigListItems> FriendConfigGetter
    {
        get => FriendList;
        set => FriendList = value;
    }
}
