using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy List",menuName = "Game Configs/Enemy List")]
[System.Serializable]
public class EnemyConfigScriptable : ScriptableObject
{
    [SerializeField]
    private List<EnemyConfigListItems> EnemyList = new List<EnemyConfigListItems>();

    public List<EnemyConfigListItems> EnemyConfigGetter
    {
        get => EnemyList;
        set => EnemyList = value;
    }
}
