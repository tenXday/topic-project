using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButton : MonoBehaviour
{
    [SerializeField] AttackBoss Gunner_Boss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gunner_Boss.Fire();
        }

    }
}
