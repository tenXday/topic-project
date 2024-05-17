using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAUManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Audio_S;
    // Start is called before the first frame update
    void OnEnable()
    {

        PlayboardEvent._MovieStart += MovieStart;
        PlayboardEvent._MovieEnd += MovieEnd;


    }
    void OnDisable()
    {
        PlayboardEvent._MovieStart -= MovieStart;
        PlayboardEvent._MovieEnd -= MovieEnd;

    }
    void MovieStart()
    {
        for (int i = 0; i < Audio_S.Count - 1; i++)
        {
            Audio_S[i].SetActive(false);
        }

        Audio_S[Audio_S.Count - 1].SetActive(true);
    }
    void MovieEnd()
    {
        for (int i = 0; i < Audio_S.Count - 1; i++)
        {
            Audio_S[i].SetActive(true);
        }

        Audio_S[Audio_S.Count - 1].SetActive(false);

    }
}
