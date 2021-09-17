using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Configs", menuName = "Game Configs/Arrow Config")]
public class ArrowConfigScriptable : ScriptableObject
{
    [SerializeField] private ArrowConfigListItems ArrowConfig;

    public ArrowConfigListItems ArrowConfigGetter
    {
        get => ArrowConfig;
        set => ArrowConfig = value;
    }
}
