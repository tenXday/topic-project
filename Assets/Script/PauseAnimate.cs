using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimate : MonoBehaviour
{

    public void PauseAniStart()
    {

        PlayboardEvent.CallGamePause();
    }
    public void CountinueAniEnd()
    {
        PlayboardEvent.CallGameContinue();
        this.gameObject.SetActive(false);
    }
}
