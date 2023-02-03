using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SeedData", menuName = "gameJam2023/SeedData", order = 0)]
public class SeedData : ScriptableObject
{
    [PreviewField(150)] public GameObject model;
    public SeedType seedType;


}
public enum SeedType
{
    TypeA, TypeB, TypeC
}
