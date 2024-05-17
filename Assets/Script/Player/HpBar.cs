using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    [SerializeField]
    AudioSource AudioSource_effect;
    [SerializeField]
    AudioClip damage_audioclip;
    [SerializeField]
    GameObject Camera;


    [SerializeField]
    PlayerStatus c_status;
    [SerializeField]
    List<GameObject> life_c;
    float a;
    public bool Touch = false;
    void OnEnable()
    {
        PlayboardEvent._Healthchangeeffect += HpChange;
    }
    void OnDisable()
    {
        PlayboardEvent._Healthchangeeffect -= HpChange;
    }
    void Start()
    {
        UpdateHPView();
        a = c_status.Maxlife;
    }
    void Update()
    {


    }
    void HpChange(float Hp)
    {
        if (Hp < 0)
        {
            AudioSource_effect.PlayOneShot(damage_audioclip);
        }

        c_status.Playerlife += Hp;
        UpdateHPView();
    }
    public void UpdateHPView()
    {

        for (int i = 0; i < life_c.Count; i++)
        {

            if (i > c_status.Playerlife - 1)
            {

                life_c[i].SetActive(true);
            }
            if (i <= c_status.Playerlife - 1)
            {

                life_c[i].SetActive(false);
            }
        }
    }
}
