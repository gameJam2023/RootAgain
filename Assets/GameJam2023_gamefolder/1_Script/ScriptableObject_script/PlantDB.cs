using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlantDB", menuName = "gameJam2023/PlantDB", order = 0)]
public class PlantDB : ScriptableObject
{
    public List<PlantData> plantDataList;
}
