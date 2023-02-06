using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIManager_script : MonoBehaviour
{
    [SerializeField] private GameObject gameManger;
    void Start()
    {
        gameManger = FindObjectOfType<Script_GameManager>().gameObject;

    }

    public void CloseBookPanel()
    {
        gameManger.GetComponent<Script_GameManager>().ObjectGroup[0].SetActive(false);

    }
    public void OpenSettingButton()
    {
        gameManger.GetComponent<Script_GameManager>().ObjectGroup[1].SetActive(true); //! open setting button
        print("openSettingButton");
    }
    public void CloseSettingButton()
    {
        gameManger.GetComponent<Script_GameManager>().ObjectGroup[1].SetActive(false);
        print("closeSettingButton");
    }

    public void QuitGame()
    {
        Application.Quit();
        print("quit");
        EditorApplication.isPlaying = false;

    }
}

