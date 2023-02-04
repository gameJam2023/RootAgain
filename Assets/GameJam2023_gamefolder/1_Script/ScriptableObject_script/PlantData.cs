using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "PlantData", menuName = "gameJam2023/PlantData", order = 0)]
public class PlantData : ScriptableObject
{

    [PreviewField(150)] public GameObject glowingModel;
    public AudioClip growingSound;
    [PreviewField(150)] public GameObject matureModel;
    public AudioClip matureSound;

    public string modelName;
}
