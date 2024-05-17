using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovieCon : MonoBehaviour
{
    // Start is called before the first frame update
    public bool RE;
    [SerializeField] GameObject MovieP;
    [SerializeField]
    UnityEngine.Video.VideoPlayer CMP;
    void Start()
    {

        if (RE)
        {
            PlayerPrefs.SetInt("Startmovie", 0);

        }
        if (PlayerPrefs.GetInt("Startmovie") == 0)
        {
            MovieP.SetActive(true);
        }
        else
        {
            PlayboardEvent.CallMovieEnd();
            MovieP.SetActive(false);
        }
    }
    void OnEnable()
    {
        CMP.loopPointReached += MovieEnd;
    }
    void OnDisable()
    {
        CMP.loopPointReached -= MovieEnd;
    }
    void MovieEnd(UnityEngine.Video.VideoPlayer vp)
    {
        PlayerPrefs.SetInt("Startmovie", 1);
    }
    public void REEE()
    {
        PlayerPrefs.SetInt("Startmovie", 1);
    }
}
