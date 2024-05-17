using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField]
    float c_hp = 10;
    float p_hp;
    float maxhp = 10;
    void OnEnable()
    {
        c_hp = PlayerPrefs.GetFloat("PlayerHp", maxhp);
    }
    void Update()
    {
        if (p_hp != c_hp)
        {
            SetHp(c_hp);
            p_hp = c_hp;
        }
    }
    public float Playerlife
    {
        get { return c_hp; }
        set
        {
            c_hp = value;
            if (c_hp < 0)
            {
                c_hp = 0;
            }
        }
    }
    public float Maxlife
    {
        get { return maxhp; }
    }
    public void SetHp(float hp)
    {
        PlayerPrefs.SetFloat("PlayerHp", hp);
    }

}
