using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_script : MonoBehaviour
{
    // public SeedData seedData = null;
    //public SeedDB seedDB;
    //public GameObject seedModel;
    public int index;
    public bool isSeed = true;
    //public Transform original_Transform;
    void Start()
    {
        this.gameObject.SetActive(true);

        // original_Transform.position = new Vector3(188f, -40f, -188f);
        // int num = Random.Range(0, seedDB.seedDatasList.Count);
        // seedData = seedDB.seedDatasList[0];
        //seedData.model = seedDB.seedDatasList[num].model;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
