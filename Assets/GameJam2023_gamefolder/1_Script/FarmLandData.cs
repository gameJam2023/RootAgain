using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "FarmLandData", menuName = "gameJam2023/FarmLandData", order = 0)]
public class FarmLandData : SerializedScriptableObject
{

    [PreviewField(150)] public GameObject model = null;
    public int farmLandIndex;
    public int nutrientTotalCount;
    public int stageGrowingCount = 3;
    public int stageMatureCount = 5;

    //public TMP_Text textNutrientTotalCount;
    //public TMP_Text textCountNum;
}

