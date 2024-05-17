using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Video.VideoPlayer C_MovieP;
    [SerializeField] GameObject BlackBoard;

    void OnEnable()
    {
        C_MovieP.prepareCompleted += (delegate (UnityEngine.Video.VideoPlayer vp)
        {

        });
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
        PlayboardEvent.CallMovieStart();
        BlackBoard.SetActive(false);
    }
    void MovieEnd(UnityEngine.Video.VideoPlayer vp)
    {
        PlayboardEvent.CallMovieEnd();
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            C_MovieP.Stop();
            this.gameObject.SetActive(false);
            PlayboardEvent.CallMovieEnd();
            PlayerPrefs.SetInt("Startmovie", 1);
        }
    }
}
