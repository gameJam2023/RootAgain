using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager
{

}
public class SystemManager : MonoBehaviour
{
    public List<GameObject> inventorySlot = new List<GameObject>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckingSequence()
    {
        for (int i = 0; i < inventorySlot.Count; i++)
        {
            if (inventorySlot[i].transform.childCount != 0)
            {

                inventorySlot[i].transform.GetChild(0).GetComponent<Draggableitem>();
            }

        }
    }


}
