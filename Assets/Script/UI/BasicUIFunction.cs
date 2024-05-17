using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUIFunction : MonoBehaviour
{

    [SerializeField] GameObject SettingUI;
    [SerializeField] GameObject LeftCanvas;
    [SerializeField] GameObject VolumeCanvas;
    [SerializeField] GameObject FastPassCanvas;

    bool isplaying = true;
    void OnEnable()
    {
        PlayboardEvent.GamePause += GamePause;
        PlayboardEvent.GameContinue += GameContinue;
    }
    void OnDisable()
    {
        PlayboardEvent.GamePause -= GamePause;
        PlayboardEvent.GameContinue -= GameContinue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LeftCanvas.activeSelf && !VolumeCanvas.activeSelf)
        {
            if (isplaying)
            {
                SettingUI.SetActive(true);
                PauseUIAniOn();
            }
            else
            {
                PauseUIAniOff();
            }

        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayboardEvent.CallGamePause();
            FastPassCanvas.SetActive(true);
        }
        // if (Input.GetKeyDown(KeyCode.F10))
        // {
        //     PlayboardEvent.CallCollectInk();
        //     PlayboardEvent.CallCollectInk();
        //     PlayboardEvent.CallCollectInk();
        //     PlayboardEvent.CallCollectInk();
        //     PlayboardEvent.CallCollectInk();
        // }
    }
    public void PauseUIAniOn()
    {
        Animator PauseAni = SettingUI.GetComponent<Animator>();
        PauseAni.SetBool("countinue", false);
    }
    public void PauseUIAniOff()
    {

        Animator PauseAni = SettingUI.GetComponent<Animator>();
        PauseAni.SetBool("countinue", true);
    }

    public void GamePause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        isplaying = false;
    }
    public void GameContinue()
    {
        Debug.Log("Continue");
        Time.timeScale = 1;
        isplaying = true;
    }

    public void LeftGame()
    {
        // if (UnityEditor.EditorApplication.isPlaying)
        // {
        //     UnityEditor.EditorApplication.isPlaying = false;
        // }
        Application.Quit();
    }



}
