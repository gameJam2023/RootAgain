using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


namespace ForMakeGameNameSpace
{
    public class Shuffle
    {
        public static List<T> list<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++) // <--- 將個List大洗牌
            {
                T Temp = list[i];
                int randomIndex = UnityEngine.Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = Temp;
            }
            return list;
        }
    }


    public class LoadingSceneLocal
    {
        public static GameObject LoadingCanvas;
        public static void LoadScene(string Name, MonoBehaviour Mono, bool Single = true)
        {
            LoadingScreen.Start();
            LoadingCanvas = LoadingScreen.LoadingCanvas;
            Mono.StartCoroutine(LoadAsynchronously(Name, Single));
        }
        public static IEnumerator LoadAsynchronously(string Name, bool Single = true)
        {
            UnityEngine.AsyncOperation operation;
            if (Single)
            {
                operation = SceneManager.LoadSceneAsync(Name, LoadSceneMode.Single);
            }
            else
            {
                operation = SceneManager.LoadSceneAsync(Name, LoadSceneMode.Additive);

            }

            LoadingCanvas.SetActive(true);
            LoadingCanvas.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                LoadingCanvas.transform.GetChild(0).GetComponent<Slider>().value = progress;
                //progressText.text = progress * 100f + "%";
                yield return null;
            }
            yield return null;
        }
    }

    public class LoadingScreen
    {
        public static GameObject LoadingCanvas;
        public static void Start()
        {
            if (LoadingCanvas == null)
            {
                LoadingCanvas = GameObject.Find("LoadScenePanel");
                GameObject LoadingObject = Resources.Load<GameObject>("LoadScenePanel");
                LoadingCanvas = GameObject.Instantiate(LoadingObject, Vector3.zero, Quaternion.identity);
                LoadingCanvas.name = "LoadScenePanel";
            }
            LoadingCanvas.SetActive(true);
        }

        public static void End()
        {
            //LoadingCanvas = GameObject.Find("LoadScenePanel");
            if (LoadingCanvas == null)
            {
                Debug.Log($"<color=#7FFF00>[Error=Loading] </color><color=yellow>沒找到LoadScenePanel</color>");
                return;
            }
            else LoadingCanvas.SetActive(false);
        }
    }

    public class RaycastFunction
    {
        public static void Raycast3D(float rayLength, LayerMask layermask, Action actionAdd)
        {
            if (Input.GetMouseButtonDown(0))//&& !EventSystem.current.IsPointerOverGameObject()
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("hit");

                if (Physics.Raycast(ray, out hit, rayLength, layermask))
                {
                    // Debug.Log("Click");
                    //feedback.JumpFeedback();
                    //Debug.Log("null");
                    actionAdd();
                }
            }
        }
    }



}

