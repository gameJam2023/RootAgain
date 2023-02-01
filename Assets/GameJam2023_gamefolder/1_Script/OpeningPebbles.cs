using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Formats.Alembic.Importer;



public class OpeningPebbles : MonoBehaviour
{
    AlembicStreamPlayer player;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        player.GetComponent<AlembicStreamPlayer>().CurrentTime = 3.6f;
    }
}
