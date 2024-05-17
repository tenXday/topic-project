using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<Button> VolumeButton_list;
    [SerializeField]
    AudioManager Mau;
    [SerializeField]
    Volumetype volumetype;

    void OnEnable()
    {
        if (VolumeButton_list.Count == 0)
        {
            VolumeButton_list.AddRange(GetComponentsInChildren<Button>());
        }
        for (int i = 0; i < VolumeButton_list.Count; i++)
        {
            int id = i;

            VolumeButton_list[i].onClick.AddListener(() => VolumeChange(id + 1));
        }

    }

    void VolumeChange(float volumenum)
    {
        //設定
        switch (volumetype)
        {
            case Volumetype.MasterVolume:
                Mau.SetMasterVolume(volumenum);
                break;
            case Volumetype.BGMVolume:
                Mau.SetBGMVolume(volumenum);
                break;
            case Volumetype.EffectVolume:
                Mau.SetEffectVolume(volumenum);
                break;
        }
        ShowUpdate(volumenum);


    }//顯示
    void ShowUpdate(float volumenum)
    {

        for (int i = 0; i < VolumeButton_list.Count; i++)
        {
            if (i < volumenum)
            {
                VolumeButton_list[i].gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                VolumeButton_list[i].gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

        }
    }
    void Start()
    {

        switch (volumetype)
        {
            case Volumetype.MasterVolume:
                ShowUpdate(PlayerPrefs.GetFloat("Master_v"));
                break;
            case Volumetype.BGMVolume:
                ShowUpdate(PlayerPrefs.GetFloat("BGM_v"));
                break;
            case Volumetype.EffectVolume:
                ShowUpdate(PlayerPrefs.GetFloat("Effect_v"));
                break;
        }
    }
    enum Volumetype
    {
        MasterVolume,
        BGMVolume,
        EffectVolume
    }
}
