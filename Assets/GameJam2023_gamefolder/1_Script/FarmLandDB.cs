using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "FarmLandDB", menuName = "gameJam2023/FarmLandDB", order = 0)]
public class FarmLandDB : ScriptableObject
{
    public List<FarmLandData> farmLandDataList;
}
