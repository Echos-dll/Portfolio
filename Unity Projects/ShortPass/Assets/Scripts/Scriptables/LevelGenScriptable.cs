using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Generator", menuName = "Game Configs/Level Generator")]
[System.Serializable]
public class LevelGenScriptable : ScriptableObject
{
    [SerializeField]
    private List<LevelListItems> LevelGenerator = new List<LevelListItems>();

    public List<LevelListItems> LevelGenerator1
    {
        get => LevelGenerator;
        set => LevelGenerator = value;
    }
}
