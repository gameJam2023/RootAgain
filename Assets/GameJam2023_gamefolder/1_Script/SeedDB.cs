using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SeedDB", menuName = "gameJam2023/SeedDB", order = 0)]
public class SeedDB : ScriptableObject
{
    public List<SeedData> seedDatasList;
}

