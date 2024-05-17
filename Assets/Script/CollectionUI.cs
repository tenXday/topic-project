using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectionUI : MonoBehaviour
{
    [SerializeField]
    GameObject C_UI;

    public float C_number;
    [SerializeField]
    float temp_inknumber;
    float pnumber;
    float c_max = 5;

    void OnEnable()
    {
        PlayboardEvent._CollectInk += Collected;
        C_number = PlayerPrefs.GetFloat("PlayerInk", 3);
        UpdateCollect();
    }
    void OnDisable()
    {
        PlayboardEvent._CollectInk -= Collected;
    }


    // Update is called once per frame
    void Update()
    {


    }
    void Collected()
    {
        C_number++;
        if (C_number > 5)
        {
            C_number = 5;
        }

        UpdateCollect();
    }
    public void UpdateCollect()
    {
        if (PlayerPrefs.GetFloat("PlayerInk") != C_number)
        {
            SetInk(C_number);
        }
        InvokeRepeating("CollectAni", 0, 0.03f);

    }
    void CollectAni()
    {
        if (temp_inknumber < C_number)
        {
            C_UI.GetComponent<Image>().fillAmount += 0.01f;
            if (C_UI.GetComponent<Image>().fillAmount >= C_number / c_max)
            {
                CancelInvoke();
                temp_inknumber = C_number;
            }
        }
        else if (temp_inknumber > C_number)
        {
            C_UI.GetComponent<Image>().fillAmount -= 0.01f;
            if (C_UI.GetComponent<Image>().fillAmount <= C_number / c_max)
            {
                CancelInvoke();
                temp_inknumber = C_number;
            }
        }

    }
    void SetInk(float ink)
    {
        PlayerPrefs.SetFloat("PlayerInk", ink);
    }
}
