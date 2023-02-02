using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "NutrientFlaskData", menuName = "gameJam2023/NutrientFlaskData", order = 0)]
public class NutrientFlaskData : ScriptableObject
{
    [PreviewField(100)] public GameObject flaskModel;
    public bool flaskFalling = false;
    public bool canDrag = true;
    public int flaskIndex;

}


