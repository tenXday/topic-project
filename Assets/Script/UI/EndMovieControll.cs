using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndMovieControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Video.VideoPlayer C_MovieP;


    void OnEnable()
    {

        C_MovieP.started += MovieStart;
        C_MovieP.loopPointReached += MovieEnd;

    }
    void OnDisable()
    {
        C_MovieP.started -= MovieStart;
        C_MovieP.loopPointReached -= MovieEnd;

    }
    void MovieStart(UnityEngine.Video.VideoPlayer vp)
    {
        PlayboardEvent.CallGamePause();
        Time.timeScale = 0;
        PlayboardEvent.CallMovieStart();

    }
    void MovieEnd(UnityEngine.Video.VideoPlayer vp)
    {
        PlayboardEvent.CallGameContinue();
        Time.timeScale = 1;
        PlayboardEvent.CallMovieEnd();
        StartCoroutine(Load(0));
        //this.gameObject.SetActive(false);
    }
    private IEnumerator Load(int sceneid)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneid);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            Debug.Log("載入進度" + asyncOperation.progress);
            yield return null;
        }
        Debug.Log("載入進度" + asyncOperation.progress);

        asyncOperation.allowSceneActivation = true;
        yield return null;


        if (asyncOperation.isDone)
        {
            Debug.Log("載入完成");
        }
        else
        {
            Debug.Log("載入失敗");
        }
    }

}
