using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedHandler : MonoBehaviour
{
    int[] initalSeed = { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        print("[Seed Value]" + initalSeed[0] + initalSeed[1] + initalSeed[2] + initalSeed[3]);

        void generateSeedValue(int[] seedArray)
        {
            for (int loop = 0; loop < seedArray.Length; loop++)
            {
                seedArray[loop] = Random.Range(0, 10);
            }

        }
        generateSeedValue(initalSeed);

        print("[Seed Value]" + initalSeed[0] + initalSeed[1] + initalSeed[2] + initalSeed[3]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

