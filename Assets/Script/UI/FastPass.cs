using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPass : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Video.VideoPlayer C_MovieP;
    [SerializeField] GameObject player;
    [SerializeField] GameObject FastPassPoint;
    void OnEnable()
    {
        C_MovieP.loopPointReached += MovieEnd;
    }

    void MovieEnd(UnityEngine.Video.VideoPlayer vp)
    {
        PlayboardEvent.CallGameContinue();
        player.transform.position = FastPassPoint.transform.position;
        this.gameObject.SetActive(false);
    }
}
