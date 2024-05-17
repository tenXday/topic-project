using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioMixer audioMixer;
    static float max_volume = 10;
    public Volume Master_v, BGM_v, Effect_v;
    void OnEnable()
    {
        Init();
    }


    public void SetMasterVolume(float volume)
    {
        float setvolume = (volume / max_volume) * (Master_v.max_volume - Master_v.min_volume) + Master_v.min_volume;

        audioMixer.SetFloat("MasterVolume", setvolume);
        PlayerPrefs.SetFloat("Master_v", volume);

    }

    public void SetBGMVolume(float volume)
    {
        float setvolume = (volume / max_volume) * (BGM_v.max_volume - BGM_v.min_volume) + BGM_v.min_volume;
        audioMixer.SetFloat("BGMVolume", setvolume);
        PlayerPrefs.SetFloat("BGM_v", volume);
    }
    public void SetEffectVolume(float volume)
    {
        float setvolume = (volume / max_volume) * (Effect_v.max_volume - Effect_v.min_volume + Effect_v.min_volume);
        audioMixer.SetFloat("EffectVolume", setvolume);
        PlayerPrefs.SetFloat("Effect_v", volume);
    }
    public struct Volume
    {
        public float max_volume;
        public float min_volume;
    }
    void Init()
    {
        Master_v.max_volume = 10f;
        Master_v.min_volume = -20f;
        BGM_v.max_volume = 0;
        BGM_v.min_volume = -20f;
        Effect_v.max_volume = 20f;
        Effect_v.min_volume = 0f;

        if (PlayerPrefs.GetFloat("AudioSettingBool") != 1)
        {
            PlayerPrefs.SetFloat("Master_v", 5);
            PlayerPrefs.SetFloat("BGM_v", 5);
            PlayerPrefs.SetFloat("Effect_v", 5);
            PlayerPrefs.SetFloat("AudioSettingBool", 1);
        }
    }
    public void Reset()
    {
        PlayerPrefs.SetFloat("AudioSettingBool", 0);
        Init();
    }
}
