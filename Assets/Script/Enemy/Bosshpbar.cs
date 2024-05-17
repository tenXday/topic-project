using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bosshpbar : MonoBehaviour
{
    [SerializeField]
    private float Maxlife;
    [SerializeField]
    private GameObject Boss;
    private float BossLife;
    void OnEnable()
    {
        PlayboardEvent._BossHealthchange += HpChange;
    }
    void OnDisable()
    {
        PlayboardEvent._BossHealthchange -= HpChange;
    }
    void Start()
    {
        this.GetComponent<Image>().fillAmount = BossLife / Maxlife;
        Maxlife = Boss.GetComponent<Boss>().bossHp;
        BossLife = Maxlife;
    }
    void Update()
    {
        this.GetComponent<Image>().fillAmount = BossLife / Maxlife;
    }
    void HpChange(float Hp)
    {
        BossLife += Hp;
        this.GetComponent<Image>().fillAmount = BossLife / Maxlife;
    }
}
