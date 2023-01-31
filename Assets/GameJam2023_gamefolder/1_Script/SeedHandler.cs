using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seed {
    public bool _seedStatus;
    public int _nutrient;
    public _seedType _seedType;
}
public enum _seedType
{
    TypeA,
    TypeB,
    TypeC
};

public class SeedHandler : MonoBehaviour
{
    public List<Seed> seedList = new List<Seed>();
    // Start is called before the first frame update
    void Start()
    {
        foreach ( Seed seed in seedList)
        {
            int randomSeedType = Random.Range(0, 10);
            if (randomSeedType < 3)
                seed._seedType = _seedType.TypeA;
            else if (randomSeedType > 3 && randomSeedType < 7)
                seed._seedType = _seedType.TypeB;
            else 
                seed._seedType = _seedType.TypeC;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
