using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Master_volume, BGM_volume, Effect_volume;
    [SerializeField] AudioManager Au_M;
    void Start()
    {
        Master_volume = PlayerPrefs.GetFloat("Master_v", Master_volume);
        BGM_volume = PlayerPrefs.GetFloat("BGM_v", BGM_volume);
        Effect_volume = PlayerPrefs.GetFloat("Effect_v", Effect_volume);
        Au_M.SetMasterVolume(Master_volume);
        Au_M.SetBGMVolume(BGM_volume);
        Au_M.SetEffectVolume(Effect_volume);
    }


}
