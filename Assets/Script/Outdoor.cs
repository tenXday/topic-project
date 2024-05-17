using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Outdoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int nextSceneid;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            LoadPreScene();
        }


    }
    public void LoadPreScene()
    {
        StartCoroutine(Load(nextSceneid));

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
