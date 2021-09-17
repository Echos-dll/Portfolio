using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrowConfigListItems
{
    [Header("Set Objects")] 
    public GameObject ArrowObject;

    [Header("Set Sprite")] 
    public Sprite ArrowSprite;

    [Header("Set Values")] 
    public int ArrowMaxScale;
    public int ArrowScaleCoefficient;
    public float ArrowRadius;

    private ArrowConfigListItems(GameObject newArrowObject, Sprite newArrowSprite, int newArrowMaxScale,
        int newArrowScaleCoefficient, float newArrowRadius)
    {
        ArrowObject = newArrowObject;
        ArrowSprite = newArrowSprite;
        ArrowMaxScale = newArrowMaxScale;
        ArrowScaleCoefficient = newArrowScaleCoefficient;
        ArrowRadius = newArrowRadius;
    }


}
