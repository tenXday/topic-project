using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyintro : MonoBehaviour
{
    [SerializeField]
    List<UnityEngine.Video.VideoClip> Introclip_List;

    UnityEngine.Video.VideoPlayer VP;
    int c_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayboardEvent.CallGamePause();
        VP = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        VP.clip = Introclip_List[c_index];
        VP.isLooping = true;
        VP.Play();
    }
    public void StartPlay()
    {
        VP.Stop();
        PlayboardEvent.CallGameContinue();
    }
    public void Nextintro()
    {
        c_index++;
        VP.clip = Introclip_List[c_index];
        VP.Play();
    }
}
