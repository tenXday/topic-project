using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{


    public void LoadScene(int sceneid)
    {
        Time.timeScale = 1;
        Debug.Log("載入完成");
        StaticValue.previoussceneid = SceneManager.GetActiveScene().buildIndex;
        if (sceneid == 1)
        {
            PlayerPrefs.SetFloat("PlayerHp", 10);
            PlayerPrefs.SetFloat("PlayerInk", 5);
        }
        StartCoroutine(Load(sceneid));
    }
    public void LoadPreScene()
    {
        StartCoroutine(Load(StaticValue.previoussceneid));
    }
    private IEnumerator Load(int sceneid)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneid);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            Debug.Log("載入進度AA" + asyncOperation.progress);
            yield return null;
        }
        Debug.Log("載入進度" + asyncOperation.progress);

        asyncOperation.allowSceneActivation = true;
        yield return null;


        if (asyncOperation.isDone)
        {


        }
        else
        {
            Debug.Log("載入失敗");
        }
    }
}
