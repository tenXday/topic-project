using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBossBullet : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {

            PlayboardEvent.CallBossHealthChange(-1);

        }
        Destroy(this.gameObject);
    }
}
