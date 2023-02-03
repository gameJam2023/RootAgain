using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "NutrientFlaskDB", menuName = "gameJam2023/NutrientFlaskDB", order = 1)]
public class NutrientFlaskDB : ScriptableObject
{
    public List<NutrientFlaskData> NutrientFlaskDataList;
}

